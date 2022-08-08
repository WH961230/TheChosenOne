using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneItemSetting")]
public class SOItemSetting : ScriptableObject {
    [Header("物品地图信息")] public List<ItemMapInfo> MyMapInfo;
    [Header("物品信息 - 武器")] public List<ItemInfo> MyWeaponItemInfo;
    [Header("物品信息 - 物品")] public List<ItemInfo> MyConsumeItemInfo;
    [Header("物品信息 - 装备")] public List<ItemInfo> MyEquipmentItemInfo;

    
    /// <summary>
    /// 根据类型获取物品信息
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<ItemInfo> GetItemInfoByType(ItemType type) {
        switch (type) {
            case ItemType.WEAPON:
                return MyWeaponItemInfo;
            case ItemType.CONSUME:
                return MyConsumeItemInfo;
            case ItemType.EQUIPMENT:
                return MyEquipmentItemInfo;
        }

        return null;
    }
}

[Serializable]
public struct ItemMapInfo {
    public Vector3 Point;
    public Quaternion Quaternion;
    public ItemType MyItemType;
}

[Serializable]
public struct ItemInfo {
    public GameObject MyItemPrefab;
    public Sprite MyItemPicture;
}

public enum ItemType {
    CONSUME,
    WEAPON,
    EQUIPMENT,
}