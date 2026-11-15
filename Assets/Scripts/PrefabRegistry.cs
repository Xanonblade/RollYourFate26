using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRegistry : MonoBehaviour
{
    public static PrefabRegistry Instance;

    public List<GameObject> prefabList;
    public List<String> prefabKey;

    private void Start()
    {
        Instance = this;
    }

    public static GameObject GetPrefab(string key)
    {
        return Instance.prefabList[Instance.prefabKey.IndexOf(key)];
    }
}
