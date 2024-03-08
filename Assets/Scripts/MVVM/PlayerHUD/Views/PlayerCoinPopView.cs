using System.Collections;
using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkit;
using Project.UIToolKit.Asset;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.MVVM.PlayerHUD
{
    public class PlayerCoinPopView : EmbeddedView, IRecipient<CoinReceiveData>
    {
        [Header("Asset")]
        [SerializeField] PopupAssetDefinition popupAsset;

        [Header("Config")]
        [SerializeField] float popDuration = 8f;
        [SerializeField] string mainCoinContainerName = "mainCoinContainer";
        [SerializeField] string additionalCoinContainerName = "additionalCoinContainer";
        [SerializeField] string iconName = "icon";

        [Header("Animation")]
        [SerializeField] string hiddenPopName = "visible";
        Label mainCoinContainer;
        Label additionalCoinContainer;

        Coroutine currentCoroutine;
        Coroutine delayVisible;
        int delta;

        protected override VisualElement Instantiate(){
            var root = base.Instantiate();
            mainCoinContainer = root.Q<Label>(mainCoinContainerName);
            additionalCoinContainer = root.Q<Label>(additionalCoinContainerName);
            var icon = root.Q<VisualElement>(iconName);

            mainCoinContainer.parent.style.backgroundImage = new StyleBackground(popupAsset.Background);
            icon.style.backgroundImage = new StyleBackground(popupAsset.Icon);
            return root;
        }
        public void Receive(CoinReceiveData message)
        {
            if(currentCoroutine == null){
                currentCoroutine = StartCoroutine(SimulateCoinReceive(message));
            }

            delta += message.newCoinValue - message.oldCoinValue;
            additionalCoinContainer.text = string.Concat(delta < 0 ? "" : "+",delta.ToString());
            additionalCoinContainer.visible = true;
            
            if(delayVisible != null){
                StopCoroutine(delayVisible);
            }
            else{
                mainCoinContainer.parent.ToggleInClassList(hiddenPopName);
            }   
        }

        private IEnumerator SimulateCoinReceive(CoinReceiveData data){
            mainCoinContainer.text = data.oldCoinValue.ToString();
            int currentCoinValue = data.oldCoinValue;

            yield return new WaitForSeconds(2f);
            int remaining = delta;
            int step = Mathf.Max(Mathf.Abs(delta) / 10, 1);
            step = delta > 0 ? step : -step;
            while(Mathf.Abs(remaining) > 0){ 
                currentCoinValue += step;
                mainCoinContainer.text = currentCoinValue.ToString();
                yield return null;
                remaining -= step;
            }
            delta = 0;
            mainCoinContainer.text = data.newCoinValue.ToString();
            additionalCoinContainer.visible = false;
            delayVisible = StartCoroutine(DelayVisibleForMainCoin(popDuration));
            currentCoroutine = null;
        }


        private IEnumerator DelayVisibleForMainCoin(float duration){
            yield return new WaitForSeconds(duration);
            mainCoinContainer.parent.ToggleInClassList(hiddenPopName);
            delayVisible = null;
        }
    }
}