using System.Collections.Generic;
using System.Linq;

public class BackpackData : Data {

    #region 消耗品

    private Dictionary<int, int> MyConsumeInfoDic = new Dictionary<int, int>();

    public void AddConsume(int id, int num) {
        if (MyConsumeInfoDic.TryGetValue(id, out int tempNum)) {
            tempNum += num;
            MyConsumeInfoDic[id] = tempNum;
        } else {
            MyConsumeInfoDic.Add(id, num);
        }
    }

    public bool RemoveConsume(int id) {
        if (MyConsumeInfoDic.ContainsKey(id)) {
            MyConsumeInfoDic.Remove(id);
            return true;
        }

        return false;
    }

    public bool Consume(int id) {
        if(MyConsumeInfoDic.TryGetValue(id, out int num)){
            if (num <= 0) {
                return false;
            }
            num--;
            MyConsumeInfoDic[id] = num;
            return true;
        }

        return false;
    }

    public List<int> GetConsumeIds() {
        return MyConsumeInfoDic.Keys.ToList();
    }

    public int GetConsumeNum(int id) {
        return MyConsumeInfoDic[id];
    }

    #endregion

    #region 武器

    private int[] MyMainWeaponIds = new int[2];

    // 添加主武器
    public bool AddMainWeapon(int index, int id) {
        MyMainWeaponIds[index] = id;
        return true;
    }

    // 移除主武器
    public bool RemoveMainWeapon(int index) {
        MyMainWeaponIds[index] = 0;
        return true;
    }

    public int[] GetMainWeaponIds() {
        return MyMainWeaponIds;
    }
    
    public int GetMainWeaponId(int index) {
        return MyMainWeaponIds[index];
    }

    //副武器

    private int MySideWeaponId;

    public bool AddSideWeapon(int id) {
        MySideWeaponId = id;
        return true;
    }

    public bool RemoveSideWeapon() {
        MySideWeaponId = 0;
        return true;
    }

    public int GetSideWeaponId() {
        return MySideWeaponId;
    }

    private int MyCurWeapId = 0; // 当前武器
    public bool SetCurWeapId(int id) {
        MyCurWeapId = id;
        return true;
    }

    public int GetCurWeapId() {
        return MyCurWeapId;
    }

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

    #region 装备

    private int MyCurEquipLevel = 0;
    private int[] MyEquipIds = new int[4];

    public bool AddEquipment(EquipmentType type, int id) {
        switch (type) {
            case EquipmentType.Helmet:
                MyEquipIds[0] = id;
                return true;
            case EquipmentType.Armour:
                MyEquipIds[1] = id;
                return true;
            case EquipmentType.Backpack:
                MyEquipIds[2] = id;
                // 设置背包 level
                return true;
        }

        return false;
    }

    public bool RemoveEquip(int index) {
        MyEquipIds[index] = 0;
        return true;
    }

    public bool SetCurBackpackLevel(int level) {
        MyCurEquipLevel = level;
        return true;
    }

    public int GetCurBackpackLevel() {
        return MyCurEquipLevel;
    }

    public int[] GetEquipIds() {
        return MyEquipIds;
    }

    public int GetEquipId(int index) {
        return MyEquipIds[index];
    }

    #endregion
}