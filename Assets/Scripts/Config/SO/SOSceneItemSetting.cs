using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<GameObject> MySceneItemPrefabList;
    public List<SceneItemInfo> MySceneItemInfoList;

    public List<GameObject> MySceneItemPrefabOfficialList;

    public List<GameObject> GetSceneItemPrefabList() {
        if (GameData.IsOfficial) {
            return MySceneItemPrefabOfficialList;
        } else {
            return MySceneItemPrefabList;
        }
    }

    public List<SceneItemInfo> GetSceneItemInfoList() {
        // 物资生成点相同
        return MySceneItemInfoList;
    }
}

[Serializable]
public struct SceneItemInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}