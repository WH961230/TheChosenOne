using System.Collections.Generic;
using UnityEngine;

public class CharacterData : Data {
    public bool IsMainCharacter;
    public bool IsJumping;
    public bool IsLanding = true;
    public int BackpackInstanceId = -1;

    // 当前武器
    private int myCurrentSceneItemWeapon = 0;
    public int MyCurrentSceneItemWeapon {
        get { return myCurrentSceneItemWeapon; }
        set { myCurrentSceneItemWeapon = value; }
    }

    // 主武器
    private const int MyWeaponMaxNum = 2;
    private int[] mySceneItemMainWeaponIds = new int[MyWeaponMaxNum];
    public int[] MySceneItemMainWeaponIds {
        get { return mySceneItemMainWeaponIds; }
    }

    // 副武器
    private int mySceneItemSideWeaponId; 
    public int MySceneItemSideWeaponId {
        get { return mySceneItemSideWeaponId; }
    }

    // 消耗物品 包含 1投掷物 2能量饮料 3子弹 4其他等 有数量下标
    private const int MySceneItemConsumeNum = 2;
    private int[] mySceneItemConsumeIds = new int[MySceneItemConsumeNum];
    public int[] MySceneItemConsumeIds {
        get { return mySceneItemConsumeIds; }
    }

    private const int MySceneItemEquipmentNum = 4;
    private int[] mySceneItemEquipmentIds = new int[MySceneItemEquipmentNum]; // 1、头盔 2、防弹衣 3、背包
    public int[] MySceneItemEquipmentIds {
        get { return mySceneItemEquipmentIds; }
    }

    // 所有物体
    private List<int> myAllSceneItemIds = new List<int>(); // 所有物品 id 上限为当前
    public List<int> MyAllSceneItemIds {
        get { return myAllSceneItemIds; }
    }

    private bool HasSceneItemId(int id) {
        foreach (var ids in MyAllSceneItemIds) {
            if (ids == id) {
                return true;
            }
        }

        return false;
    }

    private bool HasSceneItemMainWeaponId(int id) {
        foreach (var wid in MySceneItemMainWeaponIds) {
            if (wid == id) {
                return true;
            }
        }

        return false;
    }


    private bool HasSceneItemConsumeId(int id) {
        foreach (var wid in MySceneItemConsumeIds) {
            if (wid == id) {
                return true;
            }
        }

        return false;
    }

    private bool HasSceneItemEquipmentId(int id) {
        foreach (var eId in MySceneItemEquipmentIds) {
            if (eId == id) {
                return true;
            }
        }

        return false;
    }

    #region 添加

    public bool AddSceneItemMainWeapon(int id) {
        if (HasSceneItemId(id)) {
            return false;
        }

        if (HasSceneItemMainWeaponId(id)) {
            return false;
        }

        // 按顺序放置
        if (mySceneItemMainWeaponIds[0] == 0) {
            Debug.Log("一号位为空 赋值");
            mySceneItemMainWeaponIds[0] = id;
        } else {
            if (mySceneItemMainWeaponIds[1] == 0) {
                mySceneItemMainWeaponIds[1] = id;
                Debug.Log("二号位为空 赋值");
            } else {
                mySceneItemMainWeaponIds[0] = id;
                Debug.Log("都不为空 替换一号位");
            }
        }

        myAllSceneItemIds.Add(id);
        return true;
    }

    public bool AddSceneItemSideWeapon(int id) {
        if (HasSceneItemId(id)) {
            return false;
        }

        // 副武器不为空
        if (mySceneItemSideWeaponId != 0) {
            // 移除副武器
            RemoveSceneItemSideWeapon();
        }

        MyAllSceneItemIds.Add(id);
        mySceneItemSideWeaponId = id;
        return true;
    }

    public bool AddSceneItemConsume(int id) {
        if (HasSceneItemId(id)) {
            return false;
        }

        if (HasSceneItemConsumeId(id)) {
            return false;
        }

        myAllSceneItemIds.Add(id);
        for (int i = 0; i < MySceneItemConsumeIds.Length; i++) {
            if (MySceneItemConsumeIds[i] == 0) {
                MySceneItemConsumeIds[i] = id;
                break;
            }
        }

        return true;
    }

    public bool AddSceneItemEquipment(int id, string sceneName) {
        if (id == 0) {
            return false;
        }

        if (HasSceneItemId(id)) {
            return false;
        }

        if (HasSceneItemEquipmentId(id)) {
            return false;
        }

        myAllSceneItemIds.Add(id);

        if (sceneName.Contains("头盔")) {
            MySceneItemEquipmentIds[0] = id;
        } else if (sceneName.Contains("防弹衣")) {
            MySceneItemEquipmentIds[1] = id;
        } else if (sceneName.Contains("背包")) {
            mySceneItemEquipmentIds[2] = id;
        }

        return true;
    }

    #endregion

    #region 移除

    public bool RemoveSceneItemMainWeapon(int id) {
        if (id == 0) {
            return false;
        }

        if (!HasSceneItemId(id)) {
            return false;
        }

        if (!HasSceneItemMainWeaponId(id)) {
            return false;
        }

        var index = -1;
        for (int i = 0; i < myAllSceneItemIds.Count; i++) {
            if (id == myAllSceneItemIds[i]) {
                index = i;
            }
        }

        myAllSceneItemIds.RemoveAt(index);

        for (int i = 0; i < mySceneItemMainWeaponIds.Length; i++) {
            if (id == mySceneItemMainWeaponIds[i]) {
                index = i;
            }
        }

        mySceneItemMainWeaponIds[index] = 0;
        return true;
    }

    public bool RemoveSceneItemSideWeapon() {
        if (mySceneItemSideWeaponId == 0) {
            return false;
        }

        var index = -1;
        for (int i = 0; i < myAllSceneItemIds.Count; i++) {
            if (mySceneItemSideWeaponId == myAllSceneItemIds[i]) {
                index = i;
            }
        }

        myAllSceneItemIds.RemoveAt(index);
        mySceneItemSideWeaponId = 0;
        return true;
    }

    public bool RemoveSceneItemConsume(int id) {
        if (id == 0) {
            return false;
        }

        if (!HasSceneItemId(id)) {
            return false;
        }

        if (!HasSceneItemConsumeId(id)) {
            return false;
        }

        var index = -1;
        for (int i = 0; i < myAllSceneItemIds.Count; i++) {
            if (id == myAllSceneItemIds[i]) {
                index = i;
            }
        }

        myAllSceneItemIds.RemoveAt(index);

        for (int i = 0; i < mySceneItemConsumeIds.Length; i++) {
            if (id == mySceneItemConsumeIds[i]) {
                index = i;
            }
        }

        mySceneItemConsumeIds[index] = 0;
        return true;
    }

    public bool RemoveSceneItemEquipment(int id, int index) {
        if (id == 0) {
            return false;
        }

        if (!HasSceneItemId(id)) {
            return false;
        }

        if (!HasSceneItemEquipmentId(id)) {
            return false;
        }

        var ii = -1;
        for (int i = 0; i < myAllSceneItemIds.Count; i++) {
            if (id == myAllSceneItemIds[i]) {
                ii = i;
            }
        }

        myAllSceneItemIds.RemoveAt(ii);
        mySceneItemEquipmentIds[index] = 0;

        return true;
    }

    #endregion
}