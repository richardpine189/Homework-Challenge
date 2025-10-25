using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsListToSpawnReference", menuName = "SpawnerReference/ObjectsListToSpawnReference")]
public class ObjectsListToSpawnReference : ScriptableObject
{
    
#if UNITY_EDITOR
    //TODO: the name of asset bundle and the variant name should be written always in lowercase and without spaces,
    // its something to take in mind in case to improve and make a tool from this concept
    [SerializeField] private string assetBundleNameToCreate;
    [SerializeField] private string assetBundleVariantNameToCreate;
    [SerializeField] private List<GameObject> objectsToSpawnReference = new();
    [SerializeField] private List<ObjectReferenceData> objectReferencesData = new ();

    private void OnValidate()
    {
        UpdateReferenceData(); // Automatic way when you drag a new object in the inspector.
    }
    
    [ContextMenu("UpdateReferenceData")] // Add a context menu button if you would like to do manually.
    public void UpdateReferenceData()
    {
        objectReferencesData.Clear();
        if (objectsToSpawnReference.Count == 0)
        {
            Debug.LogWarning("No ObjectsToSpawnReference");
            return;
        }

        foreach (var obj in objectsToSpawnReference)
        {
            if (obj != null)
            {
                var assetPath = UnityEditor.AssetDatabase.GetAssetPath(obj);
                var assetGuid = UnityEditor.AssetDatabase.AssetPathToGUID(assetPath);
                var assetName = obj.name;
                objectReferencesData.Add(new ObjectReferenceData()
                {
                    AssetPath = assetPath,
                    AssetGuid = assetGuid,
                    AssetName = assetName,
                }
                );
            }
        }
    }

    [ContextMenu("AddPrefabsToAssetBundle")]
    public void AddPrefabToAssetBundle()
    {
        foreach (var obj in objectsToSpawnReference)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            AssetImporter importer = AssetImporter.GetAtPath(path);

            if (importer != null)
            {
                importer.assetBundleName = assetBundleNameToCreate;
                importer.assetBundleVariant = assetBundleVariantNameToCreate;
                importer.SaveAndReimport();
                Debug.Log($"{obj.name} Was assigned → bundle: {importer.assetBundleName}, variant: {importer.assetBundleVariant}");
            }
        }

        AssetDatabase.RemoveUnusedAssetBundleNames();
    }
#endif
}

[Serializable]
public struct ObjectReferenceData
{
    [SerializeField] private string assetGuid;
    [SerializeField] private string assetPath;
    [SerializeField] private string assetName;
    
    public string AssetGuid
    {
        get => assetGuid;
        set => assetGuid = value;
    }

    public string AssetName
    {
        get => assetName;
        set => assetName = value;
    }

    public string AssetPath
    {
        get => assetPath;
        set => assetPath = value;
    }
}
