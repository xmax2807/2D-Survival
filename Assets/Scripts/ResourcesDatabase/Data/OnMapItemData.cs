using UnityEngine;

namespace Project.ResourcesDatabase
{
    [System.Serializable]
    public class OnMapItemData
    {
        [SerializeField] ushort id;
        [SerializeField] Sprite visual;

        public ushort Id => id;
        public Sprite Visual => visual;
    }
}