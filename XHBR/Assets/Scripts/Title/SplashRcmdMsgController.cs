using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class SplashRcmdMsgController : MonoBehaviour
{
    [SerializeField] private Image bckgImg;
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI msgTMP;
    [SerializeField] private AssetReferenceSprite iconAssetRef;
    private string msgString;
    private AsyncOperationHandle addressableHandle;
    private GameObject bckgObj;
    private GameObject iconObj;
    private GameObject msgObj;

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
            msgTMP.font = GameInstance.Instance.Font;
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

    public IEnumerator FadeInContext(float duration = 1.0f)
    {
        float time = 0f;

        Color iconColor = iconImg.color;
        Color textColor = msgTMP.color;

        while (time <= duration)
        {
            time += Time.deltaTime;

            // Get alpha from lerp.
            float a = Mathf.Lerp(0f, 1f, Mathf.Clamp01(time / duration));
            {
                iconColor.a = a;
                textColor.a = a;
            }

            iconImg.color = iconColor;
            msgTMP.color = textColor;

            yield return null;
        }
    }

    public IEnumerator FadeOutContext(float duration = 1.0f)
    {
        float time = 0f;

        Color iconColor = iconImg.color;
        Color textColor = msgTMP.color;

        while (time <= duration)
        {
            time += Time.deltaTime;

            // Get alpha from lerp.
            float a = Mathf.Lerp(1f, 0f, Mathf.Clamp01(time / duration));
            {
                iconColor.a = a;
                textColor.a = a;
            }

            iconImg.color = iconColor;
            msgTMP.color = textColor;

            yield return null;
        }
    }

    public IEnumerator FadeOutBckg(float duration = 1.0f)
    {
        float time = 0f;

        Color bckgColor = bckgImg.color;

        while (time <= duration)
        {
            time += Time.deltaTime;

            // Get alpha from lerp.
            float a = Mathf.Lerp(1f, 0f, Mathf.Clamp01(time / duration));
            {
                bckgColor.a = a;
            }

            bckgImg.color = bckgColor;

            yield return null;
        }
    }
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
}

#if UNITY_EDITOR
public partial class SplashRcmdMsgController
{
    public static readonly string BackgroundImgName = nameof(bckgImg);
    public static readonly string IconImgName = nameof(iconImg);
    public static readonly string MsgTMPName = nameof(msgTMP);
    public static readonly string IconAssetRefName = nameof(iconAssetRef);
}
#endif