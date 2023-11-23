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

#pragma warning disable CS0649 // IMPORTANT : This prefab must be assigned in inspector.
        [SerializeField] private GameObject                 splashTimelinePrefab;
#pragma warning restore CS0649 

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
            if (splashTimelinePrefab == null)
            {
                Debug.LogError($"{nameof(splashTimelinePrefab)} : Splash timeline prefab is not assigned. Please assign a prefab in inspector.");
            }
        }

        private void Start()
        {
            GameObject splashTimelineObject = Instantiate(splashTimelinePrefab);
            {
                splashTimelineObject.SetActive(true);
            }
        }
    }
}
