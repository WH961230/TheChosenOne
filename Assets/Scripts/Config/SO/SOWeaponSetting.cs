using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOWeaponSetting")]
public class SOWeaponSetting : ScriptableObject {
    public List<WeaponMapInfo> MyWeaponMapInfo;
    public List<WeaponParameterInfo> MyWeaponParameterInfo;
}

[Serializable]
public struct WeaponMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}

[Serializable]
public struct WeaponParameterInfo {
    public GameObject Prefab;
    public Sprite Picture;
}