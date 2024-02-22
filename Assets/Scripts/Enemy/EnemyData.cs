namespace Project.Enemy
{
    public readonly struct EnemyDeathData{
        public readonly int Id;
        public readonly int Killer_Id;
        public readonly uint DropRateMul;
        public readonly uint ExpMul;

        public EnemyDeathData(int id, int killerId, uint dropRate, uint expMul){
            Id = id;
            Killer_Id = killerId;
            DropRateMul = dropRate;
            ExpMul = expMul;
        }
    }
}