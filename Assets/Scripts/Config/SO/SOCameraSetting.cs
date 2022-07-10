using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCameraSetting")]
public class SOCameraSetting : ScriptableObject {
    public Vector3 CameraOffsetPosition;
    public List<CameraInfo> CameraInfos;
}

public enum CameraType {
    MainCamera, // 主相机
    MainCharacterCamera, // 主角色主相机
}

[Serializable]
public struct CameraInfo {
    public Camera MyCamera;
    public CameraType MyCameraType;
}