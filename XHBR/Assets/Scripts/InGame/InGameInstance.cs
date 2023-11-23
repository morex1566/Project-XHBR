using UnityEngine;

namespace Assets.Scripts.InGame
{
    /// <summary>
    /// <br/> FEATURE : Provides utls function at ingame scene.
    /// <br/>           instance lifespan is as same as ingame scene.
    /// </summary>
    public class InGameInstance : MonoBehaviour
    {
        private static InGameInstance instance;

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
