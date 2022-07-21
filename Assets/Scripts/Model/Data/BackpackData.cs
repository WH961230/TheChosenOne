using System.Collections.Generic;

public class BackpackData : Data {

    #region 数据

    // 当前武器
    public int MyCurrentSceneItemWeapon = 0;

    // 主武器
    private const int MyWeaponMaxNum = 2;
    private int[] MyMainWeaponIds = new int[MyWeaponMaxNum];

    // 副武器
    private int MySideWeaponId;

    // 消耗物品 包含 1投掷物 2能量饮料 3子弹 4其他等 有数量下标
    private const int MySceneItemConsumeNum = 2;
    private List<int> MySceneItemConsumeIds = new List<int>();

    // 装备
    private const int MySceneItemEquipmentNum = 4;
    private int[] MySceneItemEquipmentIds = new int[MySceneItemEquipmentNum]; // 1、头盔 2、防弹衣 3、背包

    #endregion

    #region 新增

    public bool AddSceneItem(SceneItemType type, int sceneItemId) {
        switch (type) {
            case SceneItemType.Consume:
                // 判断物体 进行叠加
                MySceneItemConsumeIds.Add(sceneItemId);
                return true;
        }

        return false;
    }

    public bool AddMainWeapon(int id) {
        MyMainWeaponIds[0] = id;
        return true;
    }

    public bool AddSideWeapon(int id) {
        MySideWeaponId = id;
        return true;
    }

    public bool AddEquipment(EquipmentType type, int id) {
        switch (type) {
            case EquipmentType.Helmet:
                MySceneItemEquipmentIds[0] = id;
                return true;
            case EquipmentType.Armour:
                MySceneItemEquipmentIds[1] = id;
                return true;
            case EquipmentType.Backpack:
                MySceneItemEquipmentIds[2] = id;
                return true;
        }

        return false;
    }

    #endregion

    #region 修改



    #endregion

    #region 查找

    public int[] GetMainWeaponIds() {
        return MyMainWeaponIds;
    }

    public int GetMainWeaponId(int index) {
        return MyMainWeaponIds[index];
    }

    public int GetSideWeaponId() {
        return MySideWeaponId;
    }

    public List<int> GetSceneItemIds() {
        return MySceneItemConsumeIds;
    }

    public int[] GetEquipmentIds() {
        return MySceneItemEquipmentIds;
    }

    public int GetEquipmentId(int index) {
        return MySceneItemEquipmentIds[index];
    }

    #endregion

    #region 删除

    public bool RemoveSceneItem(SceneItemType type, int id) {
        switch (type) {
            case SceneItemType.Consume:
                // 判断物体 进行叠加
                MySceneItemConsumeIds.Remove(id);
                return true;
        }

        return false;
    }

    public bool RemoveMainWeapon(int index) {
        MyMainWeaponIds[index] = 0;
        return true;
    }

    public bool RemoveSideWeapon() {
        MySideWeaponId = 0;
        return true;
    }

    public bool RemoveEquipment(int index) {
        MySceneItemEquipmentIds[index] = 0;
        return true;
    }

    #endregion
}