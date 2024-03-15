using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace Project.UIToolKit
{
    /// <summary>
    /// Represents a <see cref="VisualElement"/> that may be partially filled. This is useful for HP bars, for example.
    /// </summary>
    public class FillableBar : VisualElement
    {
        //Must create a UxmlFactory in order to be exposed to UXML and UI Builder!
        public new class UxmlFactory : UxmlFactory<FillableBar, UxmlTraits> { }

        //Use this to expose additional custom UXML attributes!
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlFloatAttributeDescription fillAmount = new UxmlFloatAttributeDescription()
            {
                name = "fill-amount", //The name used for an actual UXML attribute (written in a .uxml file)
                defaultValue = 1,
            };
            private UxmlEnumAttributeDescription<FillDirection> fillDirection = new UxmlEnumAttributeDescription<FillDirection>()
            {
                name = "fill-direction",
                defaultValue = FillDirection.LeftToRight
            };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);

                FillableBar element = visualElement as FillableBar;

                element.FillAmount = fillAmount.GetValueFromBag(bag, context);
                element.FillDirection = fillDirection.GetValueFromBag(bag, context);
            }
        }

        private float fillAmount;
        private FillDirection fillDirection;

        private Texture2D originalTexture;
        private Texture2D copyTexture;

        /// <summary>
        /// <para>The amount that this bar should be filled, in range [0, 1].</para>
        /// <para>TODO: Figure out how to impose validation in the Unity editor for this value to only be within range [0, 1] (equivalent of OnValidate()?).
        /// The UI Builder inspector seems to somehow set the field directly, bypassing this C# Property setter.</para>
        /// </summary>
        public float FillAmount
        {
            get { return fillAmount; }
            set { fillAmount = math.saturate(value); }
        }

        /// <summary>
        /// The direction that the fill should be based on.
        /// </summary>
        public FillDirection FillDirection
        {
            get { return fillDirection; }
            set { fillDirection = value; }
        }

        public FillableBar()
        {
            generateVisualContent = GenerateVisualContent;
        }

        ~FillableBar()
        {
            if (copyTexture != null)
                Texture2D.Destroy(copyTexture);
        }

        private void GenerateVisualContent(MeshGenerationContext context)
        {
            IResolvedStyle resolvedStyle = this.resolvedStyle;
            Texture2D backgroundTexture = resolvedStyle.backgroundImage.texture;

            Sprite backgroundSprite = resolvedStyle.backgroundImage.sprite;
            if (backgroundTexture == null && backgroundSprite != null){
                backgroundTexture = backgroundSprite.texture;
            }

            if (backgroundTexture != null)
            {
                if (backgroundTexture != copyTexture)
                {
                    if (copyTexture != null)
                    {
                        Texture2D.Destroy(copyTexture);
                        copyTexture = null;
                    }
                    originalTexture = backgroundTexture;
                    copyTexture = CreateCopyTexture(originalTexture, FillAmount, FillDirection);

                    //Must execute this later (perhaps the next frame or so) because we shouldn't dirty this VisualElement during the generateVisualContent callback!
                    //    (If we did, it would cause an infinite loop, basically-speaking)
                    schedule.Execute(() =>
                    {
                        style.backgroundImage = new StyleBackground(copyTexture);
                    });
                }
                else
                {
                    UpdateTexture(copyTexture, originalTexture, FillAmount, FillDirection);
                }
            }
            else
            {
                if (copyTexture != null)
                {
                    Texture2D.Destroy(copyTexture);
                    copyTexture = null;
                }
            }
        }

        /// <summary>
        /// Creates a copy of the <paramref name="source"/> texture, which may be modified to achieve the visual effect of a bar filling up or emptying.
        /// </summary>
        /// <returns>A deep copy of the texture given.</returns>
        private Texture2D CreateCopyTexture(Texture2D source, float fillAmount, FillDirection fillDirection)
        {
            //Texture2D copy = new Texture2D(source.width, source.height, source.format, false);
            //source.GetRawTextureData();
            Texture2D copy = Texture2D.Instantiate(source);
            copy.name = source.name + " (Copy)";
            UpdateTexture(copy, source, fillAmount, fillDirection);
            return copy;
        }

        /// <summary>
        /// Updates the pixels of <paramref name="target"/> to appear as a filled image based on <paramref name="fillAmount"/> and <paramref name="fillDirection"/>.
        /// Call this whenever you need to visually update the texture to be filled to a new value or direction.
        /// </summary>
        private void UpdateTexture(Texture2D target, Texture2D original, float fillAmount, FillDirection fillDirection)
        {
            int width = target.width;
            int height = target.height;

            Rect uvRect = CalculateFilledRect(fillAmount, fillDirection);
            int2 minPixel = new int2((int)math.round(uvRect.xMin * (width - 1)), (int)math.round(uvRect.yMin * (height - 1)));
            int2 maxPixel = new int2((int)math.round(uvRect.xMax * (width - 1)), (int)math.round(uvRect.yMax * (height - 1)));

            switch (target.format)
            {
                case TextureFormat.RGBA32:
                case TextureFormat.DXT5:
                case TextureFormat.BGRA32:
                    {
                        NativeArray<Color32> pixels = target.GetRawTextureData<Color32>();
                        NativeArray<Color32> originalPixels = original.GetRawTextureData<Color32>();

                        //TODO: Optimize this?
                        int i = 0;
                        for (int py = 0; py < height; py++)
                        {
                            for (int px = 0; px < width; px++)
                            {
                                byte alpha = originalPixels[i].a;

                                bool inFilledArea = (px >= minPixel.x && px <= maxPixel.x && py >= minPixel.y && py <= maxPixel.y);
                                if (!inFilledArea)
                                    alpha = 0;

                                Color32 c = pixels[i];
                                c.a = alpha;
                                pixels[i++] = c;
                            }
                        }

                        target.LoadRawTextureData(pixels);
                        target.Apply();
                    }
                    break;
                default:
                    Debug.LogError("Unsupported texture format: " + target.format + "!\n" +
                        "If you'd like to add support for other texture formats, edit this!");
                    break;
            }
        }

        /// <summary>
        /// Calculates the normalized area in UV-space that a filled image should be showing within, based on <paramref name="fillAmount"/> and <paramref name="fillDirection"/>.
        /// </summary>
        private Rect CalculateFilledRect(float fillAmount, FillDirection fillDirection)
        {
            fillAmount = math.saturate(fillAmount); //Saturate => keep it in range [0, 1]

            //These are coordinates using bottom-left (0, 0) like UVs
            float leftUV = 0;
            float rightUV = 1;
            float bottomUV = 0;
            float topUV = 1;

            switch (fillDirection)
            {
                case FillDirection.LeftToRight:
                    rightUV = fillAmount;
                    break;
                case FillDirection.RightToLeft:
                    leftUV = 1 - fillAmount;
                    break;
                case FillDirection.BottomToTop:
                    topUV = fillAmount;
                    break;
                case FillDirection.TopToBottom:
                    bottomUV = 1 - fillAmount;
                    break;
            }

            return new Rect(leftUV, bottomUV, (rightUV - leftUV), (topUV - bottomUV));
        }
    }
}