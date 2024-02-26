using System;
using UnityEngine;

namespace Project.LootSystem
{
    public class Gold : AutoLootableItem
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Animator animator;

        void OnTriggerEnter2D(Collider2D other){
            OnPickerLoot(other.transform.root.gameObject);
        }

        #region Visual
        public override void ChangeAlpha(float alpha)
        {
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }

        public override void ChangeColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public override void ChangeSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public override void ChangeAnimParamValue<T>(string paramName, T value)
        {
            animator.SetFloat(paramName, Convert.ToSingle(value));
        }
        #endregion
    }
}