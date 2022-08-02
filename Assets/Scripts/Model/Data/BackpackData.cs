using System.Collections.Generic;

public class BackpackData : Data {

    #region 数据

    // 当前武器
    public int MyCurrentWeapon = 0;

    // 主武器
    private const int MyWeaponMaxNum = 2;
    private int[] MyMainWeaponIds = new int[MyWeaponMaxNum];

    // 副武器
    private int MySideWeaponId;

    // 消耗物品 包含 1投掷物 2能量饮料 3子弹 4其他等 有数量下标
    private int MySceneItemConsumeCurrentLevel = 0;
    private const int MySceneItemConsumeNum_level_1 = 1;
    private int[] MySceneItemConsumeIds_level_1 = new int[MySceneItemConsumeNum_level_1];
    private const int MySceneItemConsumeNum_level_2 = 2;
    private int[] MySceneItemConsumeIds_level_2 = new int[MySceneItemConsumeNum_level_2];
    private const int MySceneItemConsumeNum_level_3 = 3;
    private int[] MySceneItemConsumeIds_level_3 = new int[MySceneItemConsumeNum_level_3];
    private const int MySceneItemConsumeNum_level_4 = 4;
    private int[] MySceneItemConsumeIds_level_4 = new int[MySceneItemConsumeNum_level_4];

    private List<int> MySceneItemConsumeIds = new List<int>(); // 当前的消耗品
    private Dictionary<int, int> MySceneItemConsumeNumDic = new Dictionary<int, int>();

    // 装备
    private const int MySceneItemEquipmentNum = 4;
    private int[] MySceneItemEquipmentIds = new int[MySceneItemEquipmentNum]; // 1、头盔 2、防弹衣 3、背包

    #endregion

    #region 增

    public bool AddSceneItem(SceneItemType type, int sceneItemId, int num) {
        switch (type) {
            case SceneItemType.CONSUME:
                // 判断物体 进行叠加
                MySceneItemConsumeIds.Add(sceneItemId);
                return true;
        }

        return false;
    }

    public bool AddMainWeapon(int index, int id) {
        MyMainWeaponIds[index] = id;
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
                // 设置背包 level
                
                return true;
        }

        return false;
    }

    #endregion

    #region 改

    public bool SetSceneItemLevel(int level) {
        MySceneItemConsumeCurrentLevel = level;
        return true;
    }

    public bool SetCurWeapon(int id) {
        MyCurrentWeapon = id;
        return true;
    }

    #endregion

    #region 查

    public bool HaveWeapon() {
        if (GetCurWeapId() != 0) {
            return true;
        }

        var mainIds = GetMainWeaponIds();
        for (int i = 0; i < mainIds.Length; i++) {
            if (mainIds[i] != 0) {
                return true;
            }
        }

        return false;
    }
    
    public int GetCurWeapId() {
        return MyCurrentWeapon;
    }

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

    public int GetSceneItemId(int index) {
        return MySceneItemConsumeIds[index];
    }

    public int[] GetSceneItemIdsLevel() {
        switch (MySceneItemConsumeCurrentLevel) {
            case 1:
                return MySceneItemConsumeIds_level_1;
            case 2:
                return MySceneItemConsumeIds_level_2;
            case 3:
                return MySceneItemConsumeIds_level_3;
            case 4:
                return MySceneItemConsumeIds_level_4;
            default:
                return MySceneItemConsumeIds_level_1;
        }
    }

    public int[] GetEquipmentIds() {
        return MySceneItemEquipmentIds;
    }

    public int GetEquipmentId(int index) {
        return MySceneItemEquipmentIds[index];
    }

    public bool GetEmptyMainWeaponIndex(out int outIndex) {
        for (int i = 0; i < MyMainWeaponIds.Length; i++) {
            if (MyMainWeaponIds[i] == 0) {
                outIndex = i;
                return true;
            }
        }

        outIndex = 0;
        return false;
    }

    public bool GetMainWeaponIndexById(int id, out int outIndex) {
        for (int i = 0; i < MyMainWeaponIds.Length; i++) {
            if (MyMainWeaponIds[i] == id) {
                outIndex = i;
                return true;
            }
        }

        outIndex = 0;
        return false;
    }

    #endregion

    #region 删

    public bool RemoveSceneItem(SceneItemType type, int id) {
        switch (type) {
            case SceneItemType.CONSUME:
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