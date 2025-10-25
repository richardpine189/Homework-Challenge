using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundleFromUrl : MonoBehaviour
{
    [SerializeField] private string url = "https://drive.usercontent.google.com/u/0/uc?id=1PFU7ZF8krkFLhVsV5S8Wzj4bIQPliQwq&export=download";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }

    IEnumerator DownloadAssetBundleFromServer()
    {
        GameObject go = null;

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
            }
            else
            {
                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);
                go = assetBundle.LoadAsset(assetBundle.GetAllAssetNames()[0]) as GameObject;
                assetBundle.Unload(false);
                yield return new WaitForEndOfFrame();
                
            }
            www.Dispose();
            InstantiateGameObjectFromAssetBundle(go);
        }
    }

    private void InstantiateGameObjectFromAssetBundle(GameObject go)
    {
        if (go != null)
        {
            GameObject instance = Instantiate(go);
            instance.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Instantiate GameObject from AssetBundle is null");
        }
    }
}
