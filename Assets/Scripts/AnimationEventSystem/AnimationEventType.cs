namespace Project.AnimationEventSystem
{
    public enum MaterialDectectionType: int{
        Floor = 0, //Ground
        Body = 1, // enemies armor material type: leather, steel, iron,...
        Wall = 2
    }

    public enum ActionType : byte{
        None,
        PlaySound,
        PlayParticle
    }
}