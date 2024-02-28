using System;
using Project.Utils;

namespace Project.SaveSystem
{
    public interface IBindRegistry{
        void Register<TSaveable>(ISaveBind binder) where TSaveable : ISaveable;
        void Unregister<TSaveable>(ISaveBind binder) where TSaveable : ISaveable;
    }

    public interface IBindDataContainer{
        TSaveable GetSaveable<TSaveable>(SerializableGuid id) where TSaveable : ISaveable;
    }
}