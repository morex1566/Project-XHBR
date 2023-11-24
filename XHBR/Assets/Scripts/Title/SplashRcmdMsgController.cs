using System.Threading.Tasks;
using Assets.Scripts.Game;
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
        private GameObject                                  bckgObj;
        private GameObject                                  iconObj;
        private GameObject                                  msgObj;

        private void Awake()
        {
            // Check asset ref.
            if (iconAssetRef == null)
            {
                Debug.LogError($"{nameof(iconAssetRef)} : {nameof(AssetReference)} is missing.");
            }

            // Check background img component.
            bckgObj = transform.GetChild(0).gameObject;
            bckgImg = bckgObj.GetComponent<Image>();
            if (bckgImg == null)
            {
                Debug.LogError($"{nameof(bckgImg)} : {nameof(Image)} is missing.");
            }

            // Check icon img component.
            iconObj = transform.GetChild(1).gameObject;
            iconImg = iconObj.GetComponent<Image>();
            if (iconImg == null)
            {
                Debug.LogError($"{nameof(iconImg)} : {nameof(Image)} is missing.");
            }

            // Check msg tmp component.
            msgObj = transform.GetChild(2).gameObject;
            msgTMP = msgObj.GetComponent<TextMeshProUGUI>();
            if (msgTMP == null)
            {
                Debug.LogError($"{nameof(msgTMP)} : {nameof(TextMeshProUGUI)} is missing.");
            }

            // Set tmp.
            {
                msgString = "Use headphone for the best experience.";
                msgTMP.text = msgString;
                msgTMP.font = GameInstance.GameManager.Font;
            }
        }

        private void Start()
        {
 
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

            // Load icon img.
            iconAssetRef.LoadAssetAsync().Completed += (AsyncOperationHandle<Sprite> handle) =>
            {
                addressableHandle = handle;
                iconImg.sprite = handle.Result;

                loadAssetTask.SetResult(true);
            };

            await loadAssetTask.Task;
        }

        //public IEnumerator FadeIn(float duration = 1.5f)
        //{

        //}

        //public IEnumerator FadeOut(float duration = 1.5f)
        //{

        //}
    }

    public partial class SplashRcmdMsgController
    {
        public GameObject BckgObj
        {
            get { return bckgObj; }
            set { bckgObj = value; }
        }

        public GameObject IconObj
        {
            get { return iconObj; }
            set { iconObj = value; }
        }

        public GameObject MsgObj
        {
            get { return msgObj; }
            set { msgObj = value; }
        }

        public static readonly string BackgroundImgName = nameof(bckgImg);
        public static readonly string IconImgName = nameof(iconImg);
        public static readonly string MsgTMPName = nameof(msgTMP);
        public static readonly string IconAssetRefName = nameof(iconAssetRef);
    }
}
