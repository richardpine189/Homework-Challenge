using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectToSpawnReference objectToSpawnReference; 
    void Start()
    {
        if (objectToSpawnReference != null)
        {
            GameObject prefab = AssetBundleManager.Instance.LoadPrefab(objectToSpawnReference);
            
            if (prefab != null)
            {
                Instantiate(prefab, transform.position, transform.rotation);
                Debug.Log($"Spawned prefab: {prefab.name} at {transform.position}");
            }
        }
        else
        {
            Debug.LogWarning("No prefab reference assigned to Spawner");
        }
    }
}
