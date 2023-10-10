using UnityEngine;

namespace Runtime
{
    public class RuntimeEditorToggle : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR
            DisableObjectsWithTag("Runtime");
#else
        DisableObjectsWithTag("Playmode");
#endif
        }

        private static void DisableObjectsWithTag(string tag)
        {
            var objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}