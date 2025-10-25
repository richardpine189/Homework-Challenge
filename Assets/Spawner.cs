using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectToSpawnReference objectToSpawnReference; 
    async void Start()
    {
        if (objectToSpawnReference != null)
        {
            GameObject prefab = await AssetBundleManager.Instance.TryLoadPrefabAsync(objectToSpawnReference) ;
            
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
