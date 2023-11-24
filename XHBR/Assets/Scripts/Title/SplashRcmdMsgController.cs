using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Title
{
    public partial class SplashRcmdMsgController : MonoBehaviour
    {
        [SerializeField] private Image                      bckgImg;
        [SerializeField] private Image                      iconImg;
        [SerializeField] private TextMeshProUGUI            msgTMP;
        [SerializeField] private AssetReferenceSprite       iconAssetRef;
        private string                                      msgString;
        private AsyncOperationHandle                        addressableHandle;


        private void Awake()
        {
            // Check asset ref.
            if (iconAssetRef == null)
            {
                Debug.LogError($"{nameof(iconAssetRef)} : {nameof(AssetReference)} is missing.");
            }

            // Check background img component.
            bckgImg = transform.GetChild(0).GetComponent<Image>();
            if (bckgImg == null)
            {
                Debug.LogError($"{nameof(bckgImg)} : {nameof(Image)} is missing.");
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

    public partial class SplashRcmdMsgController
    {
        public static readonly string BackgroundImgName = nameof(bckgImg);
        public static readonly string IconImgName = nameof(iconImg);
        public static readonly string MsgTMPName = nameof(msgTMP);
        public static readonly string IconAssetRefName = nameof(iconAssetRef);
    }
}
