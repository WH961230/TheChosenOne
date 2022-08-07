using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<SceneItemMapInfo> MySceneItemMapInfo;
    public List<SceneItemParameterInfo> MySceneItemParameterInfo;
    [Header("场景可拾取物品配置")] public List<ScriptableObject> MySceneItemSettings;
}

[Serializable]
public struct SceneItemMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
    public SceneItemType MySceneItemType;
}

[Serializable]
public struct SceneItemParameterInfo {
    public GameObject SceneItemPrefab;
    public Sprite SceneItemPicture;
}