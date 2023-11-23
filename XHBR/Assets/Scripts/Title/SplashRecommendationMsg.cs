using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Title
{
    public partial class SplashRecommendationMsg : MonoBehaviour
    {
        [SerializeField] private Image                      backgroundImg;
        [SerializeField] private Image                      iconImg;
        [SerializeField] private TextMeshProUGUI            msgTMP;
        [SerializeField] private string                     msgString;
        [SerializeField] private AssetReferenceSprite       iconAssetRef;

        private AsyncOperationHandle                        addressableHandle;

        private void Awake()
        {
            // Check asset ref.
            if (iconAssetRef == null)
            {
                Debug.LogError($"{nameof(iconAssetRef)} : {nameof(AssetReference)} is missing.");
            }

            // Check background img component.
            backgroundImg = transform.GetChild(0).GetComponent<Image>();
            if (backgroundImg == null)
            {
                Debug.LogError($"{nameof(backgroundImg)} : {nameof(Image)} is missing.");
            }

            // Check icon img component.
            iconImg = transform.GetChild(1).GetComponent<Image>();
            if (iconImg == null)
            {
                Debug.LogError($"{nameof(iconImg)} : {nameof(Image)} is missing.");
            }

            // Check msg tmp component.
            msgTMP = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            if (msgTMP == null)
            {
                Debug.LogError($"{nameof(msgTMP)} : {nameof(TextMeshProUGUI)} is missing.");
            }

            // Set msg to tmp text.
            {
                msgString = "Use headphone for the best experience.";
                msgTMP.text = msgString;
            }
        }

        private void OnDestroy()
        {
            // Release icon asset.
            {
                Addressables.Release(addressableHandle);
                iconImg.sprite = null;
            }
        }


        public async Task LoadAssetAsync()
        {
            var loadAssetTask = new TaskCompletionSource<bool>();

            
            iconAssetRef.LoadAssetAsync().Completed += (AsyncOperationHandle<Sprite> handle) =>
            {
                addressableHandle = handle;
                iconImg.sprite = handle.Result;

                loadAssetTask.SetResult(true);
            };

            await loadAssetTask.Task;
        }
    }
}
