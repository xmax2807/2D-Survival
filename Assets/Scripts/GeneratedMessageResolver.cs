// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Resolvers
{
    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(7)
            {
                { typeof(global::System.Collections.Generic.List<global::Project.SaveSystem.ISaveable>), 0 },
                { typeof(global::System.Collections.Generic.List<global::System.Type>), 1 },
                { typeof(global::Project.SaveSystem.ISaveable), 2 },
                { typeof(global::Project.PlayerBehaviour.PlayerData), 3 },
                { typeof(global::Project.SaveSystem.SerializableGameData), 4 },
                { typeof(global::Project.SaveSystem.TestSaveData), 5 },
                { typeof(global::Project.Utils.SerializableGuid), 6 },
            };
        }

        internal static object GetFormatter(global::System.Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<global::Project.SaveSystem.ISaveable>();
                case 1: return new global::MessagePack.Formatters.ListFormatter<global::System.Type>();
                case 2: return new MessagePack.Formatters.Project.SaveSystem.ISaveableFormatter();
                case 3: return new MessagePack.Formatters.Project.PlayerBehaviour.PlayerDataFormatter();
                case 4: return new MessagePack.Formatters.Project.SaveSystem.SerializableGameDataFormatter();
                case 5: return new MessagePack.Formatters.Project.SaveSystem.TestSaveDataFormatter();
                case 6: return new MessagePack.Formatters.Project.Utils.SerializableGuidFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1649 // File name should match first type name



// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.Project.SaveSystem
{
    public sealed class ISaveableFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.SaveSystem.ISaveable>
    {
        private readonly global::System.Collections.Generic.Dictionary<global::System.RuntimeTypeHandle, global::System.Collections.Generic.KeyValuePair<int, int>> typeToKeyAndJumpMap;
        private readonly global::System.Collections.Generic.Dictionary<int, int> keyToJumpMap;

        public ISaveableFormatter()
        {
            this.typeToKeyAndJumpMap = new global::System.Collections.Generic.Dictionary<global::System.RuntimeTypeHandle, global::System.Collections.Generic.KeyValuePair<int, int>>(2, global::MessagePack.Internal.RuntimeTypeHandleEqualityComparer.Default)
            {
                { typeof(global::Project.PlayerBehaviour.PlayerData).TypeHandle, new global::System.Collections.Generic.KeyValuePair<int, int>(0, 0) },
                { typeof(global::Project.SaveSystem.TestSaveData).TypeHandle, new global::System.Collections.Generic.KeyValuePair<int, int>(1, 1) },
            };
            this.keyToJumpMap = new global::System.Collections.Generic.Dictionary<int, int>(2)
            {
                { 0, 0 },
                { 1, 1 },
            };
        }

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.SaveSystem.ISaveable value, global::MessagePack.MessagePackSerializerOptions options)
        {
            global::System.Collections.Generic.KeyValuePair<int, int> keyValuePair;
            if (value != null && this.typeToKeyAndJumpMap.TryGetValue(value.GetType().TypeHandle, out keyValuePair))
            {
                writer.WriteArrayHeader(2);
                writer.WriteInt32(keyValuePair.Key);
                switch (keyValuePair.Value)
                {
                    case 0:
                        global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.PlayerBehaviour.PlayerData>(options.Resolver).Serialize(ref writer, (global::Project.PlayerBehaviour.PlayerData)value, options);
                        break;
                    case 1:
                        global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.SaveSystem.TestSaveData>(options.Resolver).Serialize(ref writer, (global::Project.SaveSystem.TestSaveData)value, options);
                        break;
                    default:
                        break;
                }

                return;
            }

            writer.WriteNil();
        }

        public global::Project.SaveSystem.ISaveable Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            if (reader.ReadArrayHeader() != 2)
            {
                throw new global::System.InvalidOperationException("Invalid Union data was detected. Type:global::Project.SaveSystem.ISaveable");
            }

            options.Security.DepthStep(ref reader);
            var key = reader.ReadInt32();

            if (!this.keyToJumpMap.TryGetValue(key, out key))
            {
                key = -1;
            }

            global::Project.SaveSystem.ISaveable result = null;
            switch (key)
            {
                case 0:
                    result = (global::Project.SaveSystem.ISaveable)global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.PlayerBehaviour.PlayerData>(options.Resolver).Deserialize(ref reader, options);
                    break;
                case 1:
                    result = (global::Project.SaveSystem.ISaveable)global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.SaveSystem.TestSaveData>(options.Resolver).Deserialize(ref reader, options);
                    break;
                default:
                    reader.Skip();
                    break;
            }

            reader.Depth--;
            return result;
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name


// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.Project.PlayerBehaviour
{
    public sealed class PlayerDataFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.PlayerBehaviour.PlayerData>
    {
        // Health
        private static global::System.ReadOnlySpan<byte> GetSpan_Health() => new byte[1 + 6] { 166, 72, 101, 97, 108, 116, 104 };
        // MoveSpeed
        private static global::System.ReadOnlySpan<byte> GetSpan_MoveSpeed() => new byte[1 + 9] { 169, 77, 111, 118, 101, 83, 112, 101, 101, 100 };
        // Attack
        private static global::System.ReadOnlySpan<byte> GetSpan_Attack() => new byte[1 + 6] { 166, 65, 116, 116, 97, 99, 107 };
        // Defense
        private static global::System.ReadOnlySpan<byte> GetSpan_Defense() => new byte[1 + 7] { 167, 68, 101, 102, 101, 110, 115, 101 };
        // CriticalRate
        private static global::System.ReadOnlySpan<byte> GetSpan_CriticalRate() => new byte[1 + 12] { 172, 67, 114, 105, 116, 105, 99, 97, 108, 82, 97, 116, 101 };
        // Id
        private static global::System.ReadOnlySpan<byte> GetSpan_Id() => new byte[1 + 2] { 162, 73, 100 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.PlayerBehaviour.PlayerData value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            var formatterResolver = options.Resolver;
            writer.WriteMapHeader(6);
            writer.WriteRaw(GetSpan_Health());
            writer.Write(value.Health);
            writer.WriteRaw(GetSpan_MoveSpeed());
            writer.Write(value.MoveSpeed);
            writer.WriteRaw(GetSpan_Attack());
            writer.Write(value.Attack);
            writer.WriteRaw(GetSpan_Defense());
            writer.Write(value.Defense);
            writer.WriteRaw(GetSpan_CriticalRate());
            writer.Write(value.CriticalRate);
            writer.WriteRaw(GetSpan_Id());
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.Utils.SerializableGuid>(formatterResolver).Serialize(ref writer, value.Id, options);
        }

        public global::Project.PlayerBehaviour.PlayerData Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            var formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __Health__ = default(uint);
            var __MoveSpeed__ = default(float);
            var __Attack__ = default(float);
            var __Defense__ = default(float);
            var __CriticalRate__ = default(float);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 6:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 114849243817288UL:
                                __Health__ = reader.ReadUInt32();
                                continue;
                            case 118074580956225UL:
                                __Attack__ = reader.ReadSingle();
                                continue;
                        }
                    case 9:
                        if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_MoveSpeed().Slice(1))) { goto FAIL; }

                        __MoveSpeed__ = reader.ReadSingle();
                        continue;
                    case 7:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 28555890632582468UL) { goto FAIL; }

                        __Defense__ = reader.ReadSingle();
                        continue;
                    case 12:
                        if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_CriticalRate().Slice(1))) { goto FAIL; }

                        __CriticalRate__ = reader.ReadSingle();
                        continue;
                    case 2:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 25673UL) { goto FAIL; }

                        reader.Skip();
                        continue;

                }
            }

            var ____result = new global::Project.PlayerBehaviour.PlayerData(__Health__, __MoveSpeed__, __Attack__, __Defense__, __CriticalRate__);
            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name

// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.Project.SaveSystem
{
    public sealed class SerializableDictionaryFormatter<TKey, TValue> : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.SaveSystem.SerializableDictionary<TKey, TValue>>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.SaveSystem.SerializableDictionary<TKey, TValue> value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(2);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<TKey>>(formatterResolver).Serialize(ref writer, value.keys, options);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<TValue>>(formatterResolver).Serialize(ref writer, value.values, options);
        }

        public global::Project.SaveSystem.SerializableDictionary<TKey, TValue> Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var ____result = new global::Project.SaveSystem.SerializableDictionary<TKey, TValue>();

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        ____result.keys = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<TKey>>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    case 1:
                        ____result.values = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<TValue>>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class SerializableGameDataFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.SaveSystem.SerializableGameData>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.SaveSystem.SerializableGameData value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(2);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::System.Type>>(formatterResolver).Serialize(ref writer, value.keys, options);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Project.SaveSystem.ISaveable>>(formatterResolver).Serialize(ref writer, value.values, options);
        }

        public global::Project.SaveSystem.SerializableGameData Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var ____result = new global::Project.SaveSystem.SerializableGameData();

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        ____result.keys = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::System.Type>>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    case 1:
                        ____result.values = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Project.SaveSystem.ISaveable>>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name

// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.Project.SaveSystem
{
    public sealed class TestSaveDataFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.SaveSystem.TestSaveData>
    {
        // Id
        private static global::System.ReadOnlySpan<byte> GetSpan_Id() => new byte[1 + 2] { 162, 73, 100 };
        // TestInt
        private static global::System.ReadOnlySpan<byte> GetSpan_TestInt() => new byte[1 + 7] { 167, 84, 101, 115, 116, 73, 110, 116 };
        // TestFloat
        private static global::System.ReadOnlySpan<byte> GetSpan_TestFloat() => new byte[1 + 9] { 169, 84, 101, 115, 116, 70, 108, 111, 97, 116 };
        // TestString
        private static global::System.ReadOnlySpan<byte> GetSpan_TestString() => new byte[1 + 10] { 170, 84, 101, 115, 116, 83, 116, 114, 105, 110, 103 };
        // TestBool
        private static global::System.ReadOnlySpan<byte> GetSpan_TestBool() => new byte[1 + 8] { 168, 84, 101, 115, 116, 66, 111, 111, 108 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.SaveSystem.TestSaveData value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            var formatterResolver = options.Resolver;
            writer.WriteMapHeader(5);
            writer.WriteRaw(GetSpan_Id());
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::Project.Utils.SerializableGuid>(formatterResolver).Serialize(ref writer, value.Id, options);
            writer.WriteRaw(GetSpan_TestInt());
            writer.Write(value.TestInt);
            writer.WriteRaw(GetSpan_TestFloat());
            writer.Write(value.TestFloat);
            writer.WriteRaw(GetSpan_TestString());
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Serialize(ref writer, value.TestString, options);
            writer.WriteRaw(GetSpan_TestBool());
            writer.Write(value.TestBool);
        }

        public global::Project.SaveSystem.TestSaveData Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            var formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var ____result = new global::Project.SaveSystem.TestSaveData();

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 2:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 25673UL) { goto FAIL; }

                        reader.Skip();
                        continue;
                    case 7:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 32772359063823700UL) { goto FAIL; }

                        ____result.TestInt = reader.ReadInt32();
                        continue;
                    case 9:
                        if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_TestFloat().Slice(1))) { goto FAIL; }

                        ____result.TestFloat = reader.ReadSingle();
                        continue;
                    case 10:
                        if (!global::System.MemoryExtensions.SequenceEqual(stringKey, GetSpan_TestString().Slice(1))) { goto FAIL; }

                        ____result.TestString = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<string>(formatterResolver).Deserialize(ref reader, options);
                        continue;
                    case 8:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 7813586209723344212UL) { goto FAIL; }

                        ____result.TestBool = reader.ReadBoolean();
                        continue;

                }
            }

            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name

// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.Project.Utils
{
    public sealed class SerializableGuidFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Project.Utils.SerializableGuid>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::Project.Utils.SerializableGuid value, global::MessagePack.MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(4);
            writer.Write(value.Part1);
            writer.Write(value.Part2);
            writer.Write(value.Part3);
            writer.Write(value.Part4);
        }

        public global::Project.Utils.SerializableGuid Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                throw new global::System.InvalidOperationException("typecode is null, struct not supported");
            }

            options.Security.DepthStep(ref reader);
            var length = reader.ReadArrayHeader();
            var __Part1__ = default(uint);
            var __Part2__ = default(uint);
            var __Part3__ = default(uint);
            var __Part4__ = default(uint);

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        __Part1__ = reader.ReadUInt32();
                        break;
                    case 1:
                        __Part2__ = reader.ReadUInt32();
                        break;
                    case 2:
                        __Part3__ = reader.ReadUInt32();
                        break;
                    case 3:
                        __Part4__ = reader.ReadUInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::Project.Utils.SerializableGuid(__Part1__, __Part2__, __Part3__, __Part4__);
            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name

