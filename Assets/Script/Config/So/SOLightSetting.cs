using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOLightSetting")]
public class SOLightSetting : ScriptableObject {
    public GameObject MainLightPrefab;
    public LightInfo MainLightInfo;
}

[Serializable]
public struct LightInfo {
    public Vector3 position;
    public Quaternion rotation;
}