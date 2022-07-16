using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<GameObject> MySceneItemPrefabList;
    public List<SceneItemInfo> MySceneItemInfoList;

    public List<GameObject> MySceneItemPrefabOfficialList;
    public List<SceneItemInfo1> MySceneItemPrefabOfficialList1;

    public List<GameObject> GetSceneItemPrefabList() {
        return MySceneItemPrefabOfficialList;
    }

    public List<SceneItemInfo1> GetSceneItemPrefabList1() {
        return MySceneItemPrefabOfficialList1;
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

[Serializable]
public struct SceneItemInfo1 {
    public GameObject SceneItemPrefab;
    public Sprite SceneItemPicture;
}