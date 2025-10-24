using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle : MonoBehaviour
{
    [MenuItem ("AmberTools/AssetBundles/CreateAssetBundles")]
    private static void BuildAllAssetBundles()
    {
        string assetBundlePath = Application.dataPath + "/StreamingAssets";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundlePath, BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();
            Debug.Log($"AssetBundles built successfully at: {assetBundlePath}");
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            throw;
        }
        
    }
}
