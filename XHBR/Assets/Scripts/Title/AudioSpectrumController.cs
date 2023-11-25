using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class AudioSpectrumController : MonoBehaviour
{
    [SerializeField] private AssetReferenceSprite               ringImgAssetRef;
    [SerializeField] private AudioSource                        audioSource;
    private Image                                               ringImg;
    private GameObject                                          ringImgObj;
    private AsyncOperationHandle                                addressableHandle;

    private void Awake()
    {
        // Check dependency is existed.
        {
            audioSource = TitleInstance.Instance.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Utls.throwMissingError(audioSource, nameof(audioSource));
            }

            ringImgObj = transform.GetChild(0).gameObject;
            ringImg = ringImgObj.GetComponent<Image>();
            if (ringImg == null)
            {
                Utls.throwMissingError(ringImg, nameof(ringImg));
            }
        }

        // Check composition is assigned.
        {
            if (ringImgAssetRef == null)
            {
                Utls.throwMissingError(ringImgAssetRef, nameof(ringImgAssetRef));
            }
        }
    }

    private void Start()
    {
        ringImgAssetRef.LoadAssetAsync().Completed += (AsyncOperationHandle<Sprite> handle) =>
        {
            addressableHandle = handle;
            ringImg.sprite = handle.Result;
        };
    }

    private void OnDestroy()
    {
        Addressables.Release(addressableHandle);
        ringImg.sprite = null;
    }
}

#if UNITY_EDITOR
public partial class AudioSpectrumController
{
    public static readonly string RingImgAssetRefName = nameof(ringImgAssetRef);
    public static readonly string AudioSourceName = nameof(audioSource);
}
#endif
