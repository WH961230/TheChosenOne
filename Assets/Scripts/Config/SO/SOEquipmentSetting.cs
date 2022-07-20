using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOEquipmentSetting")]
public class SOEquipmentSetting : ScriptableObject {
    public List<EquipmentMapInfo> MyEquipmentMapInfo;
    public List<EquipmentParameterInfo> MyEquipmentParameterInfo;
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