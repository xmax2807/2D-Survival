using UnityEngine;
using UITTimeline;
using UnityEngine.Playables;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    public class UITCustomMixerBehaviour : PlayableBehaviour
    {
        public List<VisualElement> Elements { get; internal set; }
        private readonly List<IAnimatedVisualElementBehaviour> animatedBehaviours;
        public bool AutomaticUsageHints { get; internal set; }

        public UITCustomMixerBehaviour(){
            animatedBehaviours = new List<IAnimatedVisualElementBehaviour>();
        }

        public override void OnGraphStart(Playable playable)
        {
            var suggestedHints = UsageHints.None;

            // Initialize clips
            for (int i = playable.GetInputCount() - 1; i >= 0; --i)
            {
                var playableInput = (ScriptPlayable<UITBehaviour>)playable.GetInput(i);
                var behaviour = playableInput.GetBehaviour();

                suggestedHints |= behaviour.Hints;

                if(behaviour is IAnimatedVisualElementBehaviour animatedBehaviour){
                    animatedBehaviours.Add(animatedBehaviour);
                }
                switch (behaviour)
                {
                    case UITClassBehaviour cls:
                        cls.Elements = Elements;
                        break;
                    case UITDisplayBehaviour dsp:
                        dsp.Elements = Elements;
                        break;
                    case UITVisibilityBehaviour vis:
                        vis.Elements = Elements;
                        break;
                }
            }

            if (AutomaticUsageHints)
            {
                foreach (var e in Elements)
                {
                    e.usageHints = suggestedHints;
                }
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            foreach (VisualElement e in Elements)
            {
                for(int i = animatedBehaviours.Count - 1; i >= 0; --i){
                    animatedBehaviours[i].Animate(e, info.weight);
                }
            }
        }
    }
}
