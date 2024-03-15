using UnityEngine;
using UnityEngine.UIElements;
namespace Project.UIToolKit.Asset
{
    [CreateAssetMenu(fileName = "ProgressBarAssetDefinition", menuName = "UIToolKit/Assets/ProgressBarAssetDefinition")]
    public class ProgressBarAssetDefinition : ScriptableObject
    {
        public enum Direction{
            LeftToRight = 0,
            RightToLeft = 1,
            BottomToTop = 2,
            TopToBottom = 3
        }

        public StyleSheet styleSheet;
        public Sprite headBackground;
        public Sprite middleBackground;
        public Sprite tailBackground;
        public Sprite headFill;
        public Sprite middleFill;
        public Sprite tailFill;
        public Direction direction;

        public float partitialThreshold = 0.2f; // for each 0.2f gain or lost => do a partitial fill
        public Vector2 size;
    }
}