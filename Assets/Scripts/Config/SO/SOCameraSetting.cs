using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCameraSetting")]
public class SOCameraSetting : ScriptableObject {
    public Vector3 CameraOffsetPosition;
    public List<CameraInfo> CameraInfos;
    public float CameraTraceSpeed;
    public Vector3 LookTargetOffsetPosition;
    public float CharacterCameraDefaultFOV; // 默认 FOV
    public float CharacterCameraAimFOV; // 开镜 FOV
}

public enum CameraType {
    MainCamera, // 主相机
    MainCharacterCamera, // 主角色主相机
    WeaponCamera, // 武器相机
}

[Serializable]
public struct CameraInfo {
    public GameObject MyCameraObj;
    public CameraType MyCameraType;
}