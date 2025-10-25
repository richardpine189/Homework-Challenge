using UnityEngine;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour
{
    [SerializeField] private string assetBundleNameToUse;
    [SerializeField] private AssetBundleSourceLocation assetBundleSourceLocation;
    
    private Dictionary<string, AssetBundle> loadedBundles = new ();
    private IAssetBundleLoader assetBundleLoader;
    #region Singleton declaration
    private static AssetBundleManager instance;
    
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
    private void InitializeSingleton()
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
    #endregion  
    
    private void Awake()
    {
        InitializeSingleton();
        SetAssetBundleLoaderLocation();
    }

    private void SetAssetBundleLoaderLocation()
    {
        switch (assetBundleSourceLocation)
        {
            case AssetBundleSourceLocation.Local:
                assetBundleLoader = new AssetBundleLocalLoader();
                break;
            case AssetBundleSourceLocation.Server:
                assetBundleLoader = new AssetBundleServerLoader();
                break;
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
        if (!loadedBundles.ContainsKey(assetBundleNameToUse))
        {
            string bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, assetBundleNameToUse);
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            
            if (bundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle from {bundlePath}");
                return null;
            }
            
            loadedBundles[assetBundleNameToUse] = bundle;
        }
        
        // Loaf the prefab from the bundle
        AssetBundle loadedBundle = loadedBundles[assetBundleNameToUse];
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

public enum AssetBundleSourceLocation
{
    Local,
    Server
}

public class AssetBundleLocalLoader : IAssetBundleLoader
{
    public void LoadAssetBundle()
    {
        
    }
}

public class AssetBundleServerLoader : IAssetBundleLoader
{
    public void LoadAssetBundle()
    {
        
    }
}
public interface IAssetBundleLoader
{
    public void LoadAssetBundle();
}