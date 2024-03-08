using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Project.UIToolKit
{
    public class UITTransformClip : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField] private UITTransformBehaviour _template;

        public ClipCaps clipCaps => ClipCaps.All;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<UITTransformBehaviour>.Create(graph, _template);
        }
    }
}