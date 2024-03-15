using System;

namespace Project.GameDb
{
    public interface IPlayerHUDRepository
    {
        event Action<int> PlayerHealthChangedEvent;
        event Action<int> PlayerMaxHealthChangedEvent;
        event Action<int> PlayerReceiveGoldEvent;
    }

    public interface IPlayerRepository{
        int MaxHealth { get; set; }
        int Health { get; set; }
        int Gold { get; set; }
    }
}