using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "AnimatorGraphContainer", menuName = "AnimatorOverride/AnimatorGraphContainer", order = 0)]
    public class AnimatorGraphContainer : ScriptableObject
    {
        public PlayableGraph PlayableGraph {get;private set;}
        private readonly Dictionary<int, int> m_animators = new();

        public AnimationPlayableOutput RegisterAnimator(Animator animator)
        {
            if(!PlayableGraph.IsValid()){
                PlayableGraph = PlayableGraph.Create("AnimatorUpdateGraph");
            }

            int id = animator.GetInstanceID();

            if(m_animators.ContainsKey(id)){
                return (AnimationPlayableOutput)PlayableGraph.GetOutputByType<AnimationPlayableOutput>(m_animators[id]);
            }
            animator.runtimeAnimatorController = null;
            var result = AnimationPlayableOutput.Create(PlayableGraph, animator.name, animator);
            m_animators[id] = PlayableGraph.GetOutputCount() - 1;
            return result;
        }

        public void ChangeControllerPlayable(RuntimeAnimatorController runtimeAnimatorController, ref AnimatorControllerPlayable controllerPlayable){
            if(controllerPlayable.IsValid()){
                controllerPlayable.Destroy();
            }
            controllerPlayable = AnimatorControllerPlayable.Create(PlayableGraph, runtimeAnimatorController);
        }
    }
}