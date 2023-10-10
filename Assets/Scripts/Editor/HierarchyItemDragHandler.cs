using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class HierarchyItemDragHandler
    {
        static HierarchyItemDragHandler()
        {
            // Attach delegate
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyOnGUI;
        }

        private static void HierarchyOnGUI(int instanceID, Rect selectionRect)
        {
            // Detect when the mouse is released
            if ((Event.current.type != EventType.DragExited && Event.current.type != EventType.MouseUp) ||
                !selectionRect.Contains(Event.current.mousePosition)) return;
            
            foreach (var obj in DragAndDrop.objectReferences)
            {
                // Check if the dragged object is an AudioClip
                if (obj is not AudioClip clip ||
                    System.IO.Path.GetExtension(AssetDatabase.GetAssetPath(clip)) != ".wav") continue;
                
                // Get the GameObject that the audio clip was dragged onto
                var draggedOntoObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                
                // If the GameObject has an AudioSource, remove it
                if (draggedOntoObject && draggedOntoObject.GetComponent<AudioSource>())
                {
                    Object.DestroyImmediate(draggedOntoObject.GetComponent<AudioSource>(), true);
                }

                // Load the prefab
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ImmersiveAudioSource.prefab");
                if (!prefab) continue;
                
                var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance!.name = clip.name;

                var audioSource = instance.GetComponent<AudioSource>();
                if (!audioSource) continue;
                
                audioSource.clip = clip;

                // Set the parent of the instantiated prefab to the GameObject tagged with "Animation"
                var parentObj = GameObject.FindGameObjectWithTag("Animation");
                if (!parentObj) continue;
                
                instance.transform.SetParent(parentObj.transform, false);
            }
        }
    }
}
