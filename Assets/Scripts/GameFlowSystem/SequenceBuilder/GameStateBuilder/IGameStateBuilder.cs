using System.Collections;

namespace Project.GameFlowSystem
{
    public interface IGameStateBuilder
    {
        IGameState BuildState(SequenceData data, CommandProvider commandProvider);
    }

    public static class IEnumeratorUtils{
        public static IEnumerator Then(this IEnumerator a, IEnumerator b){
            yield return a;
            yield return b;
        }
    }
}