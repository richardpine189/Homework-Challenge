using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AssetBundleLocalLoader : IAssetBundleLoader
{
    private Dictionary<string, AssetBundle> loadedBundles = new ();
    
    public string AssetBundleNameToUse { get; set; }
    public AssetBundleLocalLoader(string assetBundleNameToUse)
    {
        AssetBundleNameToUse = assetBundleNameToUse;
    }
    public Task LoadAssetBundleAsync()
    {
        if (!loadedBundles.ContainsKey(AssetBundleNameToUse))
        {
            string bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, AssetBundleNameToUse);
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            
            if (bundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle from {bundlePath}");
                return Task.CompletedTask;
            }
            
            loadedBundles[AssetBundleNameToUse] = bundle;
        }
        return Task.CompletedTask;
    }

    public void Dispose()
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

    public GameObject PrefabFromAssetBundle(ObjectToSpawnReference reference)
    {
        // Loaf the prefab from the bundle
        AssetBundle loadedBundle = loadedBundles[AssetBundleNameToUse];
        GameObject prefab = loadedBundle.LoadAsset<GameObject>(reference.AssetName);
        
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab {reference.AssetName} from bundle");
        }
        return prefab;
    }
}