using UnityEngine;

public class BackpackData : Data {
    // 当前武器
    public int MyCurrentSceneItemWeapon = 0;

    // 主武器
    private const int MyWeaponMaxNum = 2;
    public int[] MySceneItemMainWeaponIds = new int[MyWeaponMaxNum];

    // 副武器
    public int MySceneItemSideWeaponId;

    // 消耗物品 包含 1投掷物 2能量饮料 3子弹 4其他等 有数量下标
    private const int MySceneItemConsumeNum = 2;
    public int[] MySceneItemConsumeIds = new int[MySceneItemConsumeNum];

    // 装备
    private const int MySceneItemEquipmentNum = 4;
    public int[] MySceneItemEquipmentIds = new int[MySceneItemEquipmentNum]; // 1、头盔 2、防弹衣 3、背包
}