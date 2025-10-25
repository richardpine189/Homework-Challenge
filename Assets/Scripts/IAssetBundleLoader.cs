using System.Threading.Tasks;
using UnityEngine;

public interface IAssetBundleLoader
{
    public Task LoadAssetBundleAsync();
    public void Dispose();
    GameObject PrefabFromAssetBundle(ObjectToSpawnReference reference);
}