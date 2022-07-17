using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<SceneItemMapInfo> MySceneItemMapInfo;
    public List<SceneItemParameterInfo> MySceneItemParameterInfo;
}

[Serializable]
public struct SceneItemMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}

[Serializable]
public struct SceneItemParameterInfo {
    public GameObject SceneItemPrefab;
    public Sprite SceneItemPicture;
}