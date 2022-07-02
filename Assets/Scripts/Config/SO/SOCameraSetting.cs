using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCameraSetting")]
public class SOCameraSetting : ScriptableObject {
    public List<CameraInfo> CameraInfos;
}

public enum CameraType {
    MainCamera,
    CharacterCamera,
}

[Serializable]
public struct CameraInfo {
    public Camera MyCamera;
    public CameraType MyCameraType;
}