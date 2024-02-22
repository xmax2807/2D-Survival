using System;
using Project.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace Project.LOD{
    /// <summary>
    /// track number of visible renderers to set LOD level
    /// </summary>
    public class DensityLOD : MonoBehaviour{
        [SerializeField] int[] densityThresholds;
        [SerializeField] UnityEvent<int> OnLODChanged;
        private ICentralizedRenderer m_centralizedRenderer;
        private int currentLOD = 0;

        void Awake(){
            m_centralizedRenderer = GameManager.Instance.StorageRendererPublisher;
        }

        void OnEnable(){
            m_centralizedRenderer.OnRendererCountChanged += OnRendererCountChanged;
        }
        void OnDisable(){
            m_centralizedRenderer.OnRendererCountChanged -= OnRendererCountChanged;
        }

        private void OnRendererCountChanged(int count)
        {
            //pre-check 
            //if the count is not greater than the current threshold and greater than the previous one => return
            // if(count < densityThresholds[currentLOD]){
            //     if(currentLOD == 0) return;
            //     if(count > densityThresholds[currentLOD - 1]) return;
            // }
            if(count <= densityThresholds[currentLOD]){
                
                // if count is still in range => return
                if(currentLOD == 0) return;
                if(count > densityThresholds[currentLOD - 1]) return;
                // else start re-calculating LOD 

                int i = currentLOD - 1;
                while(i > 0 && count < densityThresholds[i]){
                    --i;
                }

                currentLOD = i;
                OnLODChanged?.Invoke(currentLOD);
            }
            else{
                if(currentLOD == densityThresholds.Length - 1) return;

                int i = currentLOD + 1;
                while(i < densityThresholds.Length - 1 && count > densityThresholds[i]){
                    ++i;
                }
                currentLOD = i;
                OnLODChanged?.Invoke(currentLOD);
            }

            // for(int i = densityThresholds.Length - 1; i >= 0; --i){
            //     if(count < densityThresholds[i]){ // if number of renderers < threshold => continue
            //         continue;
            //     }

            //     if(currentLOD != i){ // if this is not the current LOD
            //         OnLODChanged?.Invoke(i);
            //         currentLOD = i;
            //         Debug.Log("LOD: " + currentLOD);
            //         return;
            //     }
            // }
        }
    }
}