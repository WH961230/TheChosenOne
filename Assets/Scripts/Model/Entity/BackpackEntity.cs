using System.Collections.Generic;
using UnityEngine;

public class BackpackEntity : Entity {
    private BackpackData backpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.backpackData = (BackpackData)data;
        MyGame.MyGameMessageCenter.Register<int, int>(GameMessageConstants.BACKPACKSYSTEM_ADD, MsgAdd);
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<int, int>(GameMessageConstants.BACKPACKSYSTEM_ADD, MsgAdd);
        base.Clear();
    }

    #region 增

    private void MsgAdd(int layer, int id) {
        switch (layer) {
            case LayerData.ConsumeLayer:
                // 创建消耗品
                var num = MyGS.ConsumeS.GetEntity(id).GetData().ConsumeNum;
                AddConsume(id, num);
                MyGS.ConsumeS.GetGO(id).Hide();
                break;
            case LayerData.WeaponLayer:
                // 创建武器
                if (AddWeapon(id)) {
                    MyGS.WeapS.GetGO(id).Hide();
                }
                break;
            case LayerData.EquipmentLayer:
                // 创建装备
                if (AddEquip(id)) {
                    MyGS.EquipmentS.GetGO(id).Hide();
                }
                break;
        }
    }

    private void AddConsume(int id, int num) {
        backpackData.AddConsume(id, num);
    }

    private bool AddWeapon(int id) {
        if (PickWeapon(id)) {
            LogSystem.Print($"拾取武器成功 Id : {id}");
            return true;
        }

        LogSystem.Print($"拾取武器失败 Id : {id}");
        return false;
    }

    private bool AddEquip(int id) {
        if (PickEquipment(id)) {
            LogSystem.Print($"拾取装备成功 Id : {id}");
            MyGS.EquipmentS.GetGO(id).Hide();

            var level = MyGS.EquipmentS.GetGO(id).GetComp().MyEquipmentLevel;
            if (level != 0) {
                SetBackpackLevel(level);
            }

            return true;
        }

        LogSystem.Print($"拾取装备失败 Id : {id}");
        return false;
    }

    /// <summary>
    /// 拾取武器
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool PickWeapon(int id) {
        var type = MyGS.WeapS.GetGO(id).GetComp().MyWeaponType;
        var curWepId = backpackData.GetCurWeapId();
        bool curWeapNull = curWepId == 0;
        if (type == WeaponType.MainWeapon) {
            // 拿到空武器槽 放入
            if (backpackData.GetEmptyMainWeaponIndex(out int outIndex)) {
                if (!backpackData.AddMainWeapon(outIndex, id)) {
                    return false;
                }
            } else {
                if (!curWeapNull) {
                    var curWeapType = MyGS.WeapS.GetGO(curWepId).GetComp().MyWeaponType;
                    // 主武器满 如果当前 是主武器 移除当前主武器
                    if (curWeapType == WeaponType.MainWeapon) {
                        if (backpackData.GetMainWeaponIndexById(curWepId, out int curIndex)) {
                            if (!backpackData.AddMainWeapon(curIndex, id)) {
                                return false;
                            }
                            if (backpackData.RemoveMainWeapon(curIndex)) {
                                DropWeapon(curWepId);
                            }
                        }
                    } else {
                        // 主武器满 如果当前 不是主武器 移除第一个主武器
                        if (!backpackData.AddMainWeapon(0, id)) {
                            return false;
                        }
                        if (backpackData.RemoveMainWeapon(0)) {
                            DropWeapon(curWepId);
                        }
                    }
                }
            }
        } else if (type == WeaponType.SideWeapon) {
            if (curWeapNull) {
                var curWeapType = MyGS.WeapS.GetGO(curWepId).GetComp().MyWeaponType;
                if (curWeapType == WeaponType.SideWeapon) {
                    if (backpackData.RemoveSideWeapon()) {
                        MyGS.WeapS.GetGO(curWepId).UnInstall(GameData.WeaponRoot, GetDropPoint(), Quaternion.identity, false);
                    }
                }
            }

            // 拿到空武器槽 放入
            if (!backpackData.AddSideWeapon(id)) {
                return false;
            }
        }

        if (!curWeapNull) {
            MyGS.CharacterS.GetGO(GameData.MainCharacterId).InstallCurWeapon(id);
            backpackData.SetCurWeapId(id);
            // 刷新玩家界面
            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UISYSTEM_UICHARACTER_REFRESH);
        }

        return true;
    }

    private void DropWeapon(int curWepId) {
        var dropPoint = GetDropPoint();
        MyGS.WeapS.GetGO(curWepId).UnInstall(GameData.WeaponRoot, dropPoint, Quaternion.identity, false);
    }

    public bool PickEquipment(int id) {
        var type = MyGS.EquipmentS.GetGO(id).GetComp().MyEquipmentType;
        if (backpackData.AddEquipment(type, id)) {
            return true;
        }

        return false;
    }

    #endregion

    #region 删

    public bool DropMainWeapon(int index) {
        var weapId = backpackData.GetMainWeaponId(index);
        if (backpackData.RemoveMainWeapon(index)) {
            MyGS.WeapS.GetGO(weapId).UnInstall(GameData.WeaponRoot, GetDropPoint(), Quaternion.identity, false);
            if (weapId == backpackData.GetCurWeapId()) {
                MyGS.CharacterS.GetGO(GameData.MainCharacterId).UnInstallCurWeapon(weapId);
                backpackData.SetCurWeapId(0);
            }
            return true;
        }

        return false;
    }

    public bool DropSideWeapon() {
        var weapId = backpackData.GetSideWeaponId();
        if (backpackData.RemoveSideWeapon()) {
            MyGS.WeapS.GetGO(weapId).UnInstall(GameData.WeaponRoot, GetDropPoint(), Quaternion.identity, false);
            if (weapId == backpackData.GetCurWeapId()) {
                MyGS.CharacterS.GetGO(GameData.MainCharacterId);
                backpackData.SetCurWeapId(0);
            }
            return true;
        }

        return false;
    }

    public bool DropEquipment(int index) {
        var equipmentId = GetEquipmentId(index);
        if (backpackData.RemoveEquip(index)) {
            MyGS.EquipmentS.GetGO(equipmentId).UnInstall(GameData.EquipmentRoot, GetDropPoint(), Quaternion.identity, false);
            return true;
        }

        return false;
    }

    #endregion

    #region 改

    public bool SetBackpackLevel(int level) {
        return backpackData.SetCurBackpackLevel(level);
    }

    public void SetCurMainWeapon(int index) {
        var wId = backpackData.GetMainWeaponIds()[index];
        if (wId != 0) {
            backpackData.SetCurWeapId(wId);
        }
    }

    #endregion

    #region 查

    public int[] GetMainWeaponIds() {
        return backpackData.GetMainWeaponIds();
    }

    public int GetMainWeaponId(int index) {
        return backpackData.GetMainWeaponId(index);
    }

    public int GetSideWeaponId() {
        return backpackData.GetSideWeaponId();
    }

    public int[] GetEquipmentIds() {
        return backpackData.GetEquipIds();
    }

    public int GetEquipmentId(int index) {
        return backpackData.GetEquipIds()[index];
    }

    public int GetEquipmentLevel() {
        return backpackData.GetCurBackpackLevel();
    }

    public List<int> GetSceneItemIds() {
        return backpackData.GetConsumeIds();
    }

    private Vector3 GetDropPoint() {
        var characterPoint = MyGS.CharacterS.GetGO(GameData.MainCharacterId).GetComp().transform.position;
        return GameData.GetGround(characterPoint);
    }

    public bool GetCurWeaponType(out WeaponType type) {
        var curId = backpackData.GetCurWeapId();
        if (curId == 0) {
            type = WeaponType.MainWeapon;
            return false;
        }

        type = MyGS.WeapS.GetGO(curId).GetComp().MyWeaponType;
        return true;
    }

    public int GetCurWeaponId() {
        return backpackData.GetCurWeapId();
    }

    #endregion
}