using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOItemSetting : ScriptableObject {
    [Header("物品地图信息")] public List<ItemMapInfo> MyMapInfo;
}

[Serializable]
public struct ItemMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
    public ItemType MyItemType;
}

public enum ItemType {
    CONSUME,
    WEAPON,
    EQUIPMENT,
}