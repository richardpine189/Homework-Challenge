using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

//TODO: Currently WIP for problems with Google Drive protocols
public class AssetBundleServerLoader : IAssetBundleLoader
{
    private AssetBundle loadedBundle;
    public string AssetBundleNameToUse { get; set; }
    public string Url { get; set; }

    private UnityWebRequest _request;
    private bool isLoaded = false;
    public AssetBundleServerLoader(string assetBundleNameToUse, string url)
    {
        AssetBundleNameToUse = assetBundleNameToUse;
        Url = url;
    }



    public async Task LoadAssetBundleAsync()
    {
        if (isLoaded)
            return;
            
        try
        {
            _request = UnityWebRequestAssetBundle.GetAssetBundle(Url);
            var operation = _request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (_request.result == UnityWebRequest.Result.ConnectionError ||
                _request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error downloading AssetBundle: {_request.error}");
                return;
            }

            loadedBundle = DownloadHandlerAssetBundle.GetContent(_request);
            isLoaded = true;
            
            Debug.Log("AssetBundle download correctly");
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception: {e.Message}");
        }
    }


    public void Dispose()
    {
        if (loadedBundle != null)
        {
            loadedBundle.Unload(false);
            loadedBundle = null;
        }
        
        _request?.Dispose();
        isLoaded = false;
    }

    public GameObject PrefabFromAssetBundle(ObjectToSpawnReference reference)
    {
        if (loadedBundle == null)
        {
            Debug.LogError("AssetBundle is not loaded");
            return null;
        }
        var go = loadedBundle.LoadAsset<GameObject>(reference.AssetName);
        return go;
    }
}