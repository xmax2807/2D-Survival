namespace Project.SaveSystem
{
    public interface IJsonSerializer
    {
        TObject Deserialize<TObject>(string input);
        string Serialize<TObject>(TObject obj);
    }

    public interface IBinarySerializer{
        TObject Deserialize<TObject>(byte[] data);
        byte[] Serialize<TObject>(TObject obj);
    }
}