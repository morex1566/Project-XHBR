using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// FEATURE : Singleton, manage all of instance class at game.
    /// </summary>
    public partial class GameInstance : MonoBehaviour
    {
        public static GameInstance                                 GameManager;

        [SerializeField] private AssetReferenceT<TMP_FontAsset>    fontAssetRef;

        private TMP_FontAsset                                      font;
        private Task                                               LoadAssetAsyncOperationHandle;
        private AsyncOperationHandle                               addressableHandle;

        private void Awake()
        {
            // Make instance to singleton.
            if (GameManager == null)
            {
                GameManager = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }

            // Load font.
            LoadAssetAsyncOperationHandle = LoadAssetAsync();
        }

        private async void Start()
        {
            await LoadAssetAsyncOperationHandle;
            if (LoadAssetAsyncOperationHandle.IsCompleted)
            {
                SceneManager.LoadScene("Title");
            }
        }

        private void OnDestroy()
        {
            // Release font asset.
            {
                Addressables.Release(addressableHandle);
                font = null;
            }
        }


        public async Task LoadAssetAsync()
        {
            var loadAssetTask = new TaskCompletionSource<bool>();

            fontAssetRef.LoadAssetAsync().Completed += (AsyncOperationHandle<TMP_FontAsset> handle) =>
            {
                addressableHandle = handle;
                font = handle.Result;

                loadAssetTask.SetResult(true);
            };

            await loadAssetTask.Task;
        }
    }

    // Properties and utls are here.
    public partial class GameInstance
    {
        public TMP_FontAsset Font
        {
            get { return font; }
            set { font = value; }
        }
        public static readonly string FontName = nameof(font);

        public static readonly string FontAssetRef = nameof(fontAssetRef);
    }
}
