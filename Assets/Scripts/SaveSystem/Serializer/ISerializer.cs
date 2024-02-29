using System.IO;
using System.Threading.Tasks;

namespace Project.SaveSystem
{
    public interface IJsonSerializer
    {
        TObject Deserialize<TObject>(string input);
        string Serialize<TObject>(TObject obj);
    }

    public interface IBinarySerializer{
        TObject Deserialize<TObject>(byte[] data);
        Task<TObject> Deserialize<TObject>(Stream stream);
        Task SerializeAsync<TObject>(TObject obj, Stream stream);
        byte[] Serialize<TObject>(TObject obj);
    }
}