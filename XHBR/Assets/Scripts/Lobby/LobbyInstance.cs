using UnityEngine;

namespace Assets.Scripts.Lobby
{
    /// <summary>
    /// <br/> FEATURE : Provides utls function at lobby scene.
    /// <br/>           instance lifespan is as same as lobby scene.
    /// </summary>
    public class LobbyInstance : MonoBehaviour
    {
        private static LobbyInstance instance;

        private void Awake()
        {
            // Make instance to singleton.
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }
        }
    }
}
