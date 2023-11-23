using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// FEATURE : Singleton, manage all of instance class at game.
    /// </summary>
    public class GameInstance : MonoBehaviour
    {
        private static GameInstance gameInstance;

        private void Awake()
        {
            // Make instance to singleton.
            if (gameInstance == null)
            {
                gameInstance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }

            // AddressableAssetSettings.BuildPlayerContent();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("Title");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("Lobby");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                SceneManager.LoadScene("InGame");
            }
        }
    }
}
