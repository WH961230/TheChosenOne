using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOSceneItemSetting : ScriptableObject {
    public List<GameObject> MySceneItemPrefabList;
    public List<SceneItemInfo> MySceneItemInfoList;
}

[Serializable]
public struct SceneItemInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}