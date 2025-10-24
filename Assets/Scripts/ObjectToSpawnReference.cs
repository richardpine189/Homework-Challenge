using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectToSpawnReference", menuName = "SpawnerReference/ObjectToSpawnReference")]
public class ObjectToSpawnReference : ScriptableObject
{
    

    [SerializeField] private string assetGuid;
    [SerializeField] private string assetPath;
    [SerializeField] private string assetName;
    
    public string AssetGuid => assetGuid;
    public string AssetName => assetName;
    
#if UNITY_EDITOR
    [SerializeField] private GameObject prefabReference;

    public GameObject PrefabReference => prefabReference;

    private void OnValidate()
    {
        UpdateReferenceData(); // Automatic way when you drag a new object in the inspector.
    }
    
    [ContextMenu("UpdateReferenceData")] // Add a context menu button if you would like to do manually.
    public void UpdateReferenceData()
    {
        if (prefabReference != null)
        {
            assetPath = UnityEditor.AssetDatabase.GetAssetPath(prefabReference);
            assetGuid = UnityEditor.AssetDatabase.AssetPathToGUID(assetPath);
            assetName = prefabReference.name;
        }
    }
#endif
}
