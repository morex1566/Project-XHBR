using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// FEATURE : Controls the title splash event.
/// </summary>
public partial class SplashTimelineController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject rcmdMsgPrfb;
    [SerializeField] private SplashRcmdMsgController rcmdMsgControllerCpnt;

    private Task LoadAssetAsyncOperationHandle;
    private GameObject rcmdMsgObj;

    private void Awake()
    {
        // Check splash image output target is existed.
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError($"{nameof(canvas)} : {nameof(Canvas)} is missing.");
        }

        // Check timeline prefab is normal.
        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            Debug.LogError($"{nameof(director)} : {nameof(PlayableDirector)} is missing.");
        }

        // Check main camera is enable.
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError($"{nameof(mainCamera)} : {nameof(Camera)} is missing.");
        }

        // Check splash img prefab.
        if (rcmdMsgPrfb == null)
        {
            Debug.LogError($"{nameof(rcmdMsgPrfb)} : {nameof(GameObject)} is missing.");
        }
    }

    private async void Start()
    {
        // Instantiate game object.
        rcmdMsgObj = Instantiate(rcmdMsgPrfb, canvas.transform, false);
        {
            // Check splash img component.
            rcmdMsgControllerCpnt = rcmdMsgObj.GetComponent<SplashRcmdMsgController>();
            if (rcmdMsgControllerCpnt == null)
            {
                Debug.LogError($"{nameof(rcmdMsgControllerCpnt)} : {nameof(SplashRcmdMsgController)} is missing.");
            }

            // Load icon sprite.
            LoadAssetAsyncOperationHandle = rcmdMsgControllerCpnt.LoadAssetAsync();

            rcmdMsgObj.SetActive(false);
        }

        await LoadAssetAsyncOperationHandle;
        if (LoadAssetAsyncOperationHandle.IsCompleted)
        {
            director.Play();
        }
    }

    public void StartRcmdMsg()
    {
        rcmdMsgObj.SetActive(true);
    }

    public void FadeInRcmdMsgContext()
    {
        StartCoroutine(rcmdMsgControllerCpnt.FadeInContext());
    }

    public void FadeOutRcmdMsgContext()
    {
        StartCoroutine(rcmdMsgControllerCpnt.FadeOutContext());
    }

    public void EndRcmdMsg()
    {
        // Activate title background.
        mainCamera.clearFlags = CameraClearFlags.Skybox;
        TitleInstance.Instance.TitleBckgObj.SetActive(true);
        TitleInstance.Instance.TitleAudioSpectrumObj.SetActive(true);

        // Start rcmd msg disappear.
        StartCoroutine(rcmdMsgControllerCpnt.FadeOutBckg(3f));

        // Start title audio clip.
        TitleInstance titleInstance = TitleInstance.Instance;
        {
            titleInstance.PlayTitleAudioClip(titleInstance.FadeInTitleAudioClip(3f));
        }
    }

    public void DestroyRcmdMsgObj()
    {
        Destroy(rcmdMsgObj);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

/// <summary>
/// Properties and utls are here.
/// </summary>
public partial class SplashTimelineController
{
    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    public PlayableDirector Director
    {
        get { return director; }
        set { director = value; }
    }

    public GameObject RcmdMsgObj
    {
        get { return rcmdMsgObj; }
        set { rcmdMsgObj = value; }
    }
}

#if UNITY_EDITOR
/// <summary>
/// Using variable naming at inspector gui.
/// </summary>
public partial class SplashTimelineController
{
    public static readonly string CanvasName = nameof(canvas);
    public static readonly string DirectorName = nameof(director);
    public static readonly string RcmdMsgPrfbName = nameof(rcmdMsgPrfb);
}
#endif