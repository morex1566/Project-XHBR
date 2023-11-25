using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// <br/> FEATURE : Provides utls function at title scene.
/// <br/>           instance lifespan is as same as title scene.
/// </summary>
public partial class TitleInstance : MonoBehaviour
{
    public static TitleInstance                             Instance;

    [SerializeField] private Canvas                         canvas;
    [SerializeField] private AudioSource                    audioSource;
    [SerializeField] private AssetReferenceT<AudioClip>     titleAudioClipAssetRef;
    [SerializeField] private GameObject                     splashTimelinePrpb;
    [SerializeField] private GameObject                     titleBckgPrpb;
    [SerializeField] private GameObject                     titleAudioSpectrumPrpb;
    private GameObject                                      titleBckgObj;
    private GameObject                                      splashTimelineObj;
    private GameObject                                      titleAudioSpectrumObj;
    private AudioClip                                       titleAudioClip;
    private AsyncOperationHandle                            addressableHandle;

    private void Awake()
    {
        // Make instance to singleton.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }

        // Check dependency is existed.
        {
            canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            if (canvas == null)
            {
                Utls.throwMissingError(canvas, nameof(canvas));
            }

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Utls.throwMissingError(audioSource, nameof(audioSource));
            }
        }

        // Check composition is assigned.
        {
            if (titleAudioClipAssetRef == null)
            {
                Utls.throwMissingError(titleAudioClipAssetRef, nameof(titleAudioClipAssetRef));
            }

            if (titleBckgPrpb == null)
            {
                Utls.throwMissingError(titleBckgPrpb, nameof(titleBckgPrpb));
            }

            if (splashTimelinePrpb == null)
            {
                Utls.throwMissingError(splashTimelinePrpb, nameof(splashTimelinePrpb));
            }

            if (titleAudioSpectrumPrpb == null)
            {
                Utls.throwMissingError(titleAudioSpectrumPrpb, nameof(titleAudioSpectrumPrpb));
            }
        }
    }

    private async void Start()
    {
        // Load title audio.
        var loadTitleAudioClip = new TaskCompletionSource<bool>();
        {
            titleAudioClipAssetRef.LoadAssetAsync().Completed += (AsyncOperationHandle<AudioClip> handle) =>
            {
                addressableHandle = handle;
                titleAudioClip = handle.Result;

                loadTitleAudioClip.SetResult(true);
            };
        }

        // If load title audio completed...
        await loadTitleAudioClip.Task;
        if (loadTitleAudioClip.Task.IsCompleted)
        {
            // Attach title bckg.
            titleBckgObj = Instantiate(titleBckgPrpb, canvas.transform, false);
            {
                titleBckgObj.SetActive(false);
            }

            // Attach title audio spectrum.
            titleAudioSpectrumObj = Instantiate(titleAudioSpectrumPrpb, canvas.transform, false);
            {
                titleAudioSpectrumObj.SetActive(false);
            }

            // Start splash event.
            splashTimelineObj = Instantiate(splashTimelinePrpb);

            // Attach title audio clip.
            audioSource.clip = titleAudioClip;
        }
    }

    public void PlayTitleAudioClip(IEnumerator additionalProcessing = null)
    {
        audioSource.Play();

        if (additionalProcessing != null)
        {
            StartCoroutine(additionalProcessing);
        }
    }

    public void StopTitleAudioClip(IEnumerator additionalProcessing = null)
    {
        if (additionalProcessing != null)
        {
            StartCoroutine(additionalProcessing);
        }

        audioSource.Stop();
    }

    public IEnumerator FadeInTitleAudioClip(float duration = 1.0f)
    {
        float time = 0f;

        while (time <= duration)
        {
            time += Time.deltaTime;

            // Set volume.
            float v = Mathf.Lerp(0f, 1f, Mathf.Clamp01(time / duration));
            {
                audioSource.volume = v;
            }

            yield return null;
        }
    }
}

/// <summary>
/// DESC : Properties and utls are here.
/// </summary>
public partial class TitleInstance
{
    public GameObject TitleBckgObj
    {
        get { return titleBckgObj; }
        set { titleBckgObj = value; }
    }

    public GameObject TitleAudioSpectrumObj
    {
        get { return titleAudioSpectrumObj; }
        set { titleAudioSpectrumObj = value; }
    }
}

#if UNITY_EDITOR
/// <summary>
/// Using variable naming at inspector gui.
/// </summary>
public partial class TitleInstance
{
    public static readonly string SplashTimelinePrpbName = nameof(splashTimelinePrpb);
    public static readonly string AudioClipAssetRefName = nameof(titleAudioClipAssetRef);
    public static readonly string TitleBckgPrpbName = nameof(titleBckgPrpb);
    public static readonly string TitleAudioSpectrumPrpbName = nameof(titleAudioSpectrumPrpb);
}
#endif