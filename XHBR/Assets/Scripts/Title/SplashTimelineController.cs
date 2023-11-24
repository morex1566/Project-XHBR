using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace Assets.Scripts.Title
{
    /// <summary>
    /// FEATURE : Controls the title splash event.
    /// </summary>
    public partial class SplashTimelineController : MonoBehaviour
    {
        [SerializeField] private Canvas                                   canvas;
        [SerializeField] private PlayableDirector                         director;
        [SerializeField] private GameObject                               recommendationMsgObj;
        [SerializeField] private SplashRcmdMsgController        _rcmdMsgControllerCpnt;

        private Task                                            LoadAssetAsyncOperationHandle;

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
            if (recommendationMsgObj == null)
            {
                Debug.LogError($"{nameof(recommendationMsgObj)} : {nameof(GameObject)} is missing.");
            }

            // Instantiate game object.
            GameObject obj = Instantiate(recommendationMsgObj);
            {
                obj.transform.SetParent(canvas.transform, false);

                // Check splash img component.
                _rcmdMsgControllerCpnt = obj.GetComponent<SplashRcmdMsgController>();
                if (_rcmdMsgControllerCpnt == null)
                {
                    Debug.LogError($"{nameof(_rcmdMsgControllerCpnt)} : {nameof(SplashRcmdMsgController)} is missing.");
                }

                // Load icon sprite.
                LoadAssetAsyncOperationHandle = _rcmdMsgControllerCpnt.LoadAssetAsync();
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
    }

    // Properties and utls are here.
    public partial class SplashTimelineController
    {
        public Canvas Canvas { get; set; }
        public static readonly string CanvasName = nameof(canvas);


        public PlayableDirector Director { get; set; }
        public static readonly string DirectorName = nameof(director);


        public GameObject RecommendationMsgObj { get; set; }
        public static readonly string RecommendationMsgObjName = nameof(recommendationMsgObj);
    }
}
