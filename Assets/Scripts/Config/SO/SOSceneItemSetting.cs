using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public GameObject SceneItemFloor; // 地板
    public GameObject SceneItemHouse; // 房屋

    public List<SceneItemFloorInfo> SceneItemFloorInfoList; // 地板信息
    public List<SceneItemHouseInfo> SceneItemHouseInfoList;
}

[Serializable]
public struct SceneItemFloorInfo {
    public Vector3 MySceneItemFloorVector3;
    public Quaternion MySceneItemFloorQuaternion;
}

[Serializable]
public struct SceneItemHouseInfo {
    public Vector3 MySceneItemHouseVector3;
    public Quaternion MySceneItemHouseQuaternion;
}