using System;
using UnityEngine;

namespace Project.LootSystem
{
    public abstract class AutoLootableItem : MonoBehaviour, IAutoLootableObject, IVisibleLootObject2D
    {
        public int Id => GetInstanceID();

        public event Action<IAutoLootableObject, GameObject> OnPickerApproachedEvent;

        public void OnPickerLoot(GameObject picker)
        {
            OnPickerApproachedEvent?.Invoke(this, picker);
        }

        #region Visual
        public virtual void ChangeAlpha(float alpha){}

        public virtual void ChangeAnimParamValue<T>(string paramName, T value){}

        public virtual void ChangeColor(Color color){}

        public virtual void ChangeSprite(Sprite sprite){}
        #endregion Visual
    }
}