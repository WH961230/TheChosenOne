using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOConsumeSetting")]
public class SOConsumeSetting : ScriptableObject {
    public List<ConsumeParameterInfo> ConsumeParameterInfos;
}

[Serializable]
public struct ConsumeParameterInfo {
    public GameObject Prefab;
    public Sprite Picture;
}