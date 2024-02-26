using System.IO;
using System.Threading;
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

        public static void Initialize()
        {
            if (!serializerRegistered)
            {
                StaticCompositeResolver.Instance.Register(
                    Project.MessagePack.Resolvers.GeneratedResolver.Instance,
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