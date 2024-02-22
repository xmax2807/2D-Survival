using Project.BuffSystem;
namespace Project.PlayerBehaviour{
    public class PlayerModifier : IStatModifier
    {
        private PlayerBuffs m_buffData;
        public PlayerModifier(PlayerStats playerStats, PlayerBuffs buffData){
            m_buffData = buffData;
        }

        public IBuffPlus IBuffPlus => m_buffData;

        public IBuffMul IBuffMul => m_buffData;
    }
}