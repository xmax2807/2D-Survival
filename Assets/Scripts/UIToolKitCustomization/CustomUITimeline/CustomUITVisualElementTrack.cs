using System;
using System.Collections.Generic;
using UITTimeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
namespace Project.UIToolKit
{
    [Serializable]
    [TrackClipType(typeof(UITClassClip)),
     TrackClipType(typeof(UITVisibilityClip)),
     TrackClipType(typeof(UITDisplayClip)),
     TrackClipType(typeof(UITTransformClip))]
    [TrackColor(0.259f, 0.529f, 0.961f)]
    public class CustomUITVisualElementTrack : TrackAsset, ILayerable
    {
        private const char UINameHashtag = '#';
        private const char UIClassName = '.';
        [SerializeField,
         Tooltip("If enabled, the track will add appropriate UsageHints to it's element(s), " +
                 "based on the added clips, to optimize performance. Disable this only if you're adding them " +
                 "yourself or want more control over how they are used.")]
        private bool automaticUsageHints = true;
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var playableMixer = ScriptPlayable<UITCustomMixerBehaviour>.Create(graph, inputCount);

            var uid = go.GetComponentInParent<UIDocument>();
            if (uid == null)
            {
                Debug.LogError("Could not find UIDocument in parent hierarchy.");
                return Playable.Null;
            }

            var root = uid.rootVisualElement;
            if (root == null)
            {
                Debug.LogError("rootVisualElement is null. Check that you don't have PlayOnAwake enabled " +
                               "in your PlayableDirector (Use the DelayedPlayOnAwake MonoBehaviour instead).");
                return Playable.Null;
            }

            var behaviour = playableMixer.GetBehaviour();
            behaviour.Elements = QueryElements(name, go.GetComponentInParent<UIDocument>().rootVisualElement);
            behaviour.AutomaticUsageHints = automaticUsageHints;

            return playableMixer;
        }

        private List<VisualElement> QueryElements(string queryPath, VisualElement root)
        {
            var query = root.Query();
            var results = new List<VisualElement>(1);

            var path = queryPath.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < path.Length; i++)
            {
                var part = path[i];

                if (part.StartsWith(UINameHashtag))
                {
                    if (part.IndexOf(UINameHashtag) != part.LastIndexOf(UINameHashtag))
                    {
                        Debug.LogError($"Invalid pattern (only one Name(#) selector is allowed per part): {part}");
                        return results;
                    }

                    query.Name(part.Replace(UINameHashtag.ToString(), ""));
                }
                else if (part.StartsWith(UIClassName))
                {
                    if (part.Contains(UINameHashtag))
                    {
                        Debug.LogError(
                            $"Invalid pattern (no mixing of Names(#) and Classes(#) in a part). Did you forget a space?: {part}");
                        return results;
                    }

                    string[] classNames = part.Split(UIClassName, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string className in classNames)
                    {
                        query.Class(className);
                    }
                }
                else
                {
                    Debug.LogError($"Invalid pattern (only Name(#) and Class(.) is allowed): {part}");
                }

                if (i < path.Length - 1)
                {
                    query = query.Children<VisualElement>();
                }
            }

            return query.Build().ToList();
        }

        public Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return Playable.Null;
        }
    }
}