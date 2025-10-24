using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject m_PrefabToSpawn; 
    void Start() { 
        Instantiate(m_PrefabToSpawn, transform.position, transform.rotation); 
    } 
}
