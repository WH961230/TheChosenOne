using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/SOWeaponSetting")]
public class SOWeaponSetting : ScriptableObject {
    public Vector3 WeaponAimModelPoint; // 角色开镜模型位置
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
    public Vector3 WeaponCameraAimPoint; // 相机开镜位置
    public float WeaponCameraAimFOV; // 武器相机开镜 FOV
}