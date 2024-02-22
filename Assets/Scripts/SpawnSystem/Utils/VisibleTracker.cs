using UnityEngine;
using UnityEngine.Events;

namespace Project.SpawnSystem
{
    public class VisibleTracker : MonoBehaviour
    {
        [SerializeField] UnityEvent<bool> OnVisibilityChanged;

        void OnBecameVisible() => OnVisibilityChanged?.Invoke(true);
        void OnBecameInvisible() => OnVisibilityChanged?.Invoke(false);
    }
}