using System.Collections;
using System.Collections.Generic;
using Project.Pooling;
using TMPro;
using UnityEngine;

namespace Project.Utils
{
    public class FadeObjectHandler2D : MonoBehaviour
    {
        [SerializeField] Color fadeColor;

        readonly List<FadeObject2D> current_fadeObjects = new();

        public void OnRayCastHitFadeObjects(RaycastHit2D[] hits, int count)
        {
            if (hits == null) return;

            count = Mathf.Min(count, hits.Length);

            //Get new need fade objects
            List<FadeObject2D> needAdd_fadeObjects = QuickListPool<FadeObject2D>.GetList();
            for (int i = 0; i < count; ++i)
            {
                if (hits[i].transform == null)
                {
                    continue;
                }
                if (hits[i].transform.TryGetComponent<FadeObject2D>(out FadeObject2D fadeObject))
                {
                    needAdd_fadeObjects.Add(fadeObject);
                }
            }

            HandleFadeObjects(needAdd_fadeObjects);

            QuickListPool<FadeObject2D>.ReturnList(needAdd_fadeObjects);
        }

        public void OnEnterWithFadeObjectCollider(Collider2D collider)
        {
            if (collider == null) return;

            if (!collider.TryGetComponent<FadeObject2D>(out FadeObject2D fadeObject))
            {
                return;
            }

            if (false == current_fadeObjects.Contains(fadeObject)){
                StartCoroutine(fadeObject.FadeOut(fadeColor));
                current_fadeObjects.Add(fadeObject);
            }
        }

        public void OnExitWithFadeObjectCollider(Collider2D collider)
        {
            if (collider == null) return;
            
            if (!collider.TryGetComponent<FadeObject2D>(out FadeObject2D fadeObject))
            {
                return;
            }

            if (true == current_fadeObjects.Contains(fadeObject)){
                StartCoroutine(fadeObject.ForceFadeIn());
                current_fadeObjects.Remove(fadeObject);
            }
        }

        private void HandleFadeObjects(in List<FadeObject2D> needAdd_fadeObjects)
        {
            // find need remove fade objects in current list
            // any object that not in new list but in current list => remove
            List<FadeObject2D> needRemove_fadeObjects = QuickListPool<FadeObject2D>.GetList();

            for (int i = current_fadeObjects.Count - 1; i >= 0; --i)
            {
                if (!needAdd_fadeObjects.Contains(current_fadeObjects[i]))
                {
                    needRemove_fadeObjects.Add(current_fadeObjects[i]);
                }
            }

            // Start fade in removed objects
            for (int i = needRemove_fadeObjects.Count - 1; i >= 0; --i)
            {
                StartCoroutine(needRemove_fadeObjects[i].ForceFadeIn());
                current_fadeObjects.Remove(needRemove_fadeObjects[i]);
            }

            //start fade out need fade objects
            for (int i = needAdd_fadeObjects.Count - 1; i >= 0; --i)
            {
                if (current_fadeObjects.Contains(needAdd_fadeObjects[i])) continue;

                StartCoroutine(needAdd_fadeObjects[i].FadeOut(fadeColor, force: true));
                current_fadeObjects.Add(needAdd_fadeObjects[i]);
            }

            QuickListPool<FadeObject2D>.ReturnList(needRemove_fadeObjects);
        }
    }
}