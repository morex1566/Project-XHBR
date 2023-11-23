using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// FEATURE : Singleton, manage all of instance class at game.
    /// </summary>
    public partial class GameInstance : MonoBehaviour
    {
        public static GameInstance                      GameManager;

        [SerializeField] private AssetReference         fontAssetRef;
        [SerializeField] private TMP_FontAsset          font;

        private AsyncOperationHandle                    addressableHandle;

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

            // Load font asset.
            //if (!LoadAsset())
            //{
            //    Debug.LogError($"{nameof(fontAssetRef)} : Load {nameof(TMP_FontAsset)} is failed.");
            //}
        }

        private void OnDestroy()
        {
            //Addressables.Release(addressableHandle);
            //font = null;
        }


        public bool LoadAsset()
        {
            var LoadAssetAsyncProc = Addressables.LoadAssetAsync<TMP_FontAsset>(fontAssetRef);

            LoadAssetAsyncProc.Task.Wait();

            if (LoadAssetAsyncProc.Status == AsyncOperationStatus.Succeeded)
            {
                addressableHandle = LoadAssetAsyncProc;
                font = LoadAssetAsyncProc.Result;

                return true;
            }

            return false;
        }
    }

    // Properties and utls are here.
    public partial class GameInstance
    {
        public TMP_FontAsset FontAsset { get; set; }
        public static readonly string FontAssetName = nameof(font);
    }
}
