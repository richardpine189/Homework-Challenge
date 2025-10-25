using UnityEngine;
using System.Threading.Tasks;

public class AssetBundleManager : MonoBehaviour
{
    [SerializeField] private string assetBundleNameToUse;
    [SerializeField] private AssetBundleSourceLocation assetBundleSourceLocation;
    [SerializeField] private string url = "https://drive.usercontent.google.com/u/0/uc?id=1PFU7ZF8krkFLhVsV5S8Wzj4bIQPliQwq&export=download";
    
    
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
        InitializeAssetBundle(assetBundleLoader);
    }

    private void SetAssetBundleLoaderLocation()
    {
        switch (assetBundleSourceLocation)
        {
            case AssetBundleSourceLocation.Local:
                assetBundleLoader = new AssetBundleLocalLoader(assetBundleNameToUse);
                break;
            case AssetBundleSourceLocation.Server:
                assetBundleLoader = new AssetBundleServerLoader(assetBundleNameToUse, url);
                break;
        }
    }
    
    private void InitializeAssetBundle(IAssetBundleLoader loader)
    {
        loader.LoadAssetBundleAsync();
    }

    public async Task<GameObject> TryLoadPrefabAsync(ObjectToSpawnReference reference)
    {
        if (!CheckForHealthyReference(reference))
            return null;
        
        await assetBundleLoader.LoadAssetBundleAsync();
        return assetBundleLoader.PrefabFromAssetBundle(reference);
    }
    
    private bool CheckForHealthyReference(ObjectToSpawnReference reference)
    {
        if (reference == null || string.IsNullOrEmpty(reference.AssetGuid))
        {
            Debug.LogError("Invalid AssetBundleReference");
            return false;
        }
        return true;
    }
    
    private void OnDestroy()
    {
        assetBundleLoader.Dispose();
    }
}