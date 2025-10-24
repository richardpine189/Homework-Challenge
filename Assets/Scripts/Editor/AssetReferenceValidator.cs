#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class AssetReferenceValidator
{
    [MenuItem("AmberTools/AssetBundles/ValidateReferenceData")]
    public static void ValidateReferenceData()
    {
        string[] guids = AssetDatabase.FindAssets("t:ObjectToSpawnReference");
        
        int updatedCount = 0;
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ObjectToSpawnReference reference = AssetDatabase.LoadAssetAtPath<ObjectToSpawnReference>(path);
            
            if (reference != null)
            {
                reference.UpdateReferenceData();
                EditorUtility.SetDirty(reference);
                updatedCount++;
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log($"Validation Complete. {updatedCount} ObjectToSpawnReference updated.");
    }
}
#endif
