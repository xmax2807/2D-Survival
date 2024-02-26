using System;
using UnityEngine;
namespace Project.LootSystem
{
    public interface IVisibleLootObject2D
    {
        Transform transform { get; }
        void ChangeSprite(Sprite sprite);
        void ChangeColor(Color color);
        void ChangeAlpha(float alpha);
        void ChangeAnimParamValue<T>(string paramName, T value);
    }
}