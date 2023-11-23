using UnityEngine;

namespace Assets.Scripts.Title
{
    /// <summary>
    /// <br/> FEATURE : Provides utls function at title scene.
    /// <br/>           instance lifespan is as same as title scene.
    /// </summary>
    public class TitleInstance : MonoBehaviour
    {
        private static TitleInstance                        instance;
        
        [SerializeField] private GameObject                 splashTimelineObj;

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

            // Check splash event instance is assigned.
            if (splashTimelineObj == null)
            {
                Debug.LogError($"{nameof(splashTimelineObj)} : Splash timeline prefab is not assigned. Please assign a prefab in inspector.");
            }
        }

        private void Start()
        {
            GameObject splashTimelineObject = Instantiate(splashTimelineObj);
            {
                splashTimelineObject.SetActive(true);
            }
        }
    }
}
