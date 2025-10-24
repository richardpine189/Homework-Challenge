#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectToSpawnReference))]
public class AssetBundleReferenceEditor : Editor
{
    private SerializedProperty prefabReferenceProperty;
    private SerializedProperty assetGuidProperty;
    private SerializedProperty assetNameProperty;
    private SerializedProperty assetPathProperty;
    
    private void OnEnable()
    {
        prefabReferenceProperty = serializedObject.FindProperty("prefabReference");
        assetGuidProperty = serializedObject.FindProperty("assetGuid");
        assetNameProperty = serializedObject.FindProperty("assetName");
        assetPathProperty = serializedObject.FindProperty("assetPath");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(prefabReferenceProperty, new GUIContent("Prefab"));
        
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            
            ObjectToSpawnReference reference = (ObjectToSpawnReference)target;
            GameObject prefab = prefabReferenceProperty.objectReferenceValue as GameObject;
            
            if (prefab != null)
            {
                assetPathProperty.stringValue = AssetDatabase.GetAssetPath(prefab);
                assetGuidProperty.stringValue = AssetDatabase.AssetPathToGUID(assetPathProperty.stringValue);
                assetNameProperty.stringValue = prefab.name;
                
                //TODO: Remove this from here to the AssetBundleManager
                AssetImporter importer = AssetImporter.GetAtPath(assetPathProperty.stringValue);
                if (importer != null)
                {
                    importer.assetBundleName = "prefabs"; //TODO: Remove this and create a setting field for group assetbundles
                    importer.SaveAndReimport();
                }
                
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            }
        }
        
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("GUID", assetGuidProperty.stringValue);
        EditorGUILayout.TextField("Asset Name", assetNameProperty.stringValue);
        EditorGUILayout.TextField("Asset Path", assetPathProperty.stringValue);
        EditorGUI.EndDisabledGroup();
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif