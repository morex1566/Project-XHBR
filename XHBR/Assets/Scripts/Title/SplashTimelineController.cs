using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Scripts.Title
{
    public class SplashTimelineController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        public Canvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        [SerializeField] private PlayableDirector director;
        public PlayableDirector Director
        {
            get { return director; }
            set { director = value; }
        }

        private void Awake()
        {
            // Check splash image output target is existed.
            canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError($"{nameof(canvas)} : Canvas is not assigned. Please assign a canvas tag to 'MainCanvas'.");
            }

            // Check timeline prefab is normal.
            director = GetComponent<PlayableDirector>();
            if (director == null)
            {
                Debug.LogError($"{nameof(director)} : PlayableDirector is missing. Please use prefab again, or create a playableDirector component to use splash timeline assets.");
            }
        }
    }
}
