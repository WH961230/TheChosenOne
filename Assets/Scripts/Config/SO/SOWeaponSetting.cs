using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/SOWeaponSetting")]
public class SOWeaponSetting : ScriptableObject {
    [Header("角色开镜模型位置")] public Vector3 WeaponAimModelPoint; // 角色开镜模型位置
    [Header("角色武器信息")] public List<WeaponParameterInfo> MyWeaponParameterInfo;
}

[Serializable]
public struct WeaponMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
}

[Serializable]
public struct WeaponParameterInfo {
    [Header("武器物体")] public GameObject Prefab;
    [Header("武器图片")] public Sprite Picture;
    [Header("武器开镜位置")] public Vector3 WeaponCameraAimPoint; // 相机开镜位置
    [Header("相机开镜 FOV")] public float WeaponCameraAimFOV; // 武器相机开镜 FOV
}