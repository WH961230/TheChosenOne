using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<SceneItemPrefabInfo> SceneItemPrefabInfoList;
    public List<SceneItemInfo> SceneItemInfoList;
    
    public bool TryGetSceneItemPrefabBySign(string sign, out GameObject MyPrefab) {
        foreach (var item in SceneItemPrefabInfoList) {
            if (string.Equals(item.MyItemSign, sign)) {
                MyPrefab = item.MySceneItemPrefab;
                return true;
            }
        }

        MyPrefab = null;
        return false;
    }
}

[Serializable]
public struct SceneItemInfo {
    public string MyItemSign;
    public Vector3 MySceneItemVector3;
    public Quaternion MySceneItemQuaternion;
}

[Serializable]
public struct SceneItemPrefabInfo {
    public string MyItemSign;
    public GameObject MySceneItemPrefab;
}

