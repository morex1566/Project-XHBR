using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Scripts.Title
{
    /// <summary>
    /// FEATURE : Controls the title splash event.
    /// </summary>
    public partial class SplashTimelineController : MonoBehaviour
    {
        [SerializeField] private Canvas                                  canvas;
        [SerializeField] private PlayableDirector                        director;
        [SerializeField] private GameObject                              rcmdMsgPrfb;
        [SerializeField] private SplashRcmdMsgController                 rcmdMsgControllerCpnt;
                                                                         
        private Task                                                     LoadAssetAsyncOperationHandle;
        private GameObject                                               rcmdMsgObj;

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

            // Check splash img prefab.
            if (rcmdMsgPrfb == null)
            {
                Debug.LogError($"{nameof(rcmdMsgPrfb)} : {nameof(GameObject)} is missing.");
            }

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
        }

        private async void Start()
        {
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

        public void FadeInRcmdMsg()
        {

        }

        public void FadeOutRcmdMsg()
        {

        }

        public void EndRcmdMsg()
        {
            rcmdMsgObj.SetActive(false);
        }
    }

    // Properties and utls are here.
    public partial class SplashTimelineController
    {
        public Canvas Canvas { get; set; }
        public static readonly string CanvasName = nameof(canvas);


        public PlayableDirector Director { get; set; }
        public static readonly string DirectorName = nameof(director);


        public GameObject RecommendationMsgPrfb { get; set; }
        public static readonly string RecommendationMsgPrfbName = nameof(rcmdMsgPrfb);
    }
}
