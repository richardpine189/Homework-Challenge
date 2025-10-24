using UnityEngine;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour
{
    private static AssetBundleManager instance;
    private Dictionary<string, AssetBundle> loadedBundles = new ();
    
    public static AssetBundleManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AssetBundleManager");
                instance = go.AddComponent<AssetBundleManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public GameObject LoadPrefab(ObjectToSpawnReference reference)
    {
        if (reference == null || string.IsNullOrEmpty(reference.AssetGuid))
        {
            Debug.LogError("Invalid AssetBundleReference");
            return null;
        }
        
        // Load the AssetBundle if is not
        if (!loadedBundles.ContainsKey("prefabs"))
        {
            string bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, "prefabs");
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            
            if (bundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle from {bundlePath}");
                return null;
            }
            
            loadedBundles["prefabs"] = bundle;
        }
        
        // Loaf the prefab from the bundle
        AssetBundle loadedBundle = loadedBundles["prefabs"];
        GameObject prefab = loadedBundle.LoadAsset<GameObject>(reference.AssetName);
        
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab {reference.AssetName} from bundle");
        }
        
        return prefab;
    }
    
    private void OnDestroy()
    {
        foreach (var bundle in loadedBundles.Values)
        {
            if (bundle != null)
            {
                bundle.Unload(false);
            }
        }
        loadedBundles.Clear();
    }
}