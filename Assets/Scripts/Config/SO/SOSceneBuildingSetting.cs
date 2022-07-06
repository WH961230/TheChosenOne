using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneBuildingSetting")]
public class SOSceneBuildingSetting : ScriptableObject {
    public List<GameObject> MySceneBuildingPrefabInfoList;
    public List<SceneBuildingInfo> MySceneBuildingInfoList;
}

[Serializable]
public struct SceneBuildingInfo {
    public string Sign;
    public Vector3 Point;
    public Quaternion Quaternion;
}