using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MessagePack;
using MessagePack.Resolvers;

namespace Project.SaveSystem
{
    /// <summary>
    /// use lib: Message Pack
    /// </summary>
    public class MPSerializer : IBinarySerializer
    {
        private static bool serializerRegistered;

        public MPSerializer(){
            if(serializerRegistered == false){
                Initialize();
            }
        }

        public TObject Deserialize<TObject>(byte[] input)
        {
            return MessagePackSerializer.Deserialize<TObject>(input);
        }

        public byte[] Serialize<TObject>(TObject obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public async Task SerializeAsync<TObject>(TObject obj, Stream stream){
            await MessagePackSerializer.SerializeAsync(stream,obj);
        }

        public async Task<TObject> Deserialize<TObject>(Stream stream)
        {
            return await MessagePackSerializer.DeserializeAsync<TObject>(stream);
        }

        public static void Initialize()
        {
            if (!serializerRegistered)
            {
                StaticCompositeResolver.Instance.Register(
                    GeneratedResolver.Instance,
                    AttributeFormatterResolver.Instance,
                    PrimitiveObjectResolver.Instance,
                    StandardResolver.Instance
                );

                var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);

                MessagePackSerializer.DefaultOptions = option;
                serializerRegistered = true;
            }
        }
    }
}