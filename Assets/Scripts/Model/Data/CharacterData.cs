using System.Collections.Generic;
using UnityEngine;

public class CharacterData : Data {
    public bool IsMainCharacter;
    public bool IsJumping;
    public bool IsLanding = true;

    private int MyCurrentSceneItemWeapon;

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

    #endregion
}