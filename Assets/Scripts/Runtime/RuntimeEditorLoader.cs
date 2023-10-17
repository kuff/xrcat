using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class RuntimeEditorLoader : MonoBehaviour
    {
        private const string PlaymodeSceneName = "PlaymodeObjects";
        private const string RuntimeSceneName = "RuntimeObjects";

        private void Awake()
        {
#if UNITY_EDITOR
            LoadSceneAdditively(PlaymodeSceneName);
#else
            LoadSceneAdditively(RuntimeSceneName);
#endif
        }

        private static void LoadSceneAdditively(string sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}