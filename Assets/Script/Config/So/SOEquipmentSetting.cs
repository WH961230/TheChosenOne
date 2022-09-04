using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOEquipmentSetting")]
public class SOEquipmentSetting : ScriptableObject {
    public List<EquipmentMapInfo> MyEquipmentMapInfo; // 装备地图信息
    public List<EquipmentParameterInfo> MyEquipmentParameterInfo; // 装备信息
}

[Serializable]
public struct EquipmentMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}

[Serializable]
public struct EquipmentParameterInfo {
    public GameObject Prefab;
    public Sprite Picture;
}