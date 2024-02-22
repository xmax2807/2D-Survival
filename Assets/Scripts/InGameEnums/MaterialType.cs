namespace Project.Enums
{
    public enum MaterialType
    {
        None = 0,
        Metal = 1,
        Wood = 2
    }
    public enum MaterialDectectionType : byte{
        Floor,
        Body,
        Wall
    }

    public enum FeedbackType : byte
    {
        None,
        PlaySound,
        PlayParticle
    }
}