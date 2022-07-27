using System.Collections.Generic;
using UnityEngine;

public class BackpackEntity : Entity {
    private BackpackData backpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.backpackData = (BackpackData)data;
        MyGame.MyGameMessageCenter.Register<int, int>(GameMessageConstants.BACKPACKSYSTEM_ADD, MsgAdd);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        MyGame.MyGameMessageCenter.UnRegister<int, int>(GameMessageConstants.BACKPACKSYSTEM_ADD, MsgAdd);
        base.Clear();
    }

    #region 增

    private void MsgAdd(int id, int layer) {
        switch (layer) {
            case LayerData.SceneItemLayer:
                AddSceneItem(id);
                break;
            case LayerData.WeaponLayer:
                AddWeapon(id);
                break;
            case LayerData.EquipmentLayer:
                AddEquipment(id);
                break;
        }
    }

    private bool AddSceneItem(int id) {
        if (PickSceneItem(id)) {
            LogSystem.Print($"拾取物品成功 发送物品隐藏消息 Id : {id}");
            MyGame.MyGameSystem.MyItemSystem.GetSceneItemGameObj(id).Hide();
            return true;
        }

        LogSystem.Print($"拾取物品失败 Id : {id}");
        return false;
    }

    private bool AddWeapon(int id) {
        if (PickWeapon(id)) {
            LogSystem.Print($"拾取武器成功 发送武器隐藏消息 Id : {id}");
            MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(id).Hide();
            return true;
        }

        LogSystem.Print($"拾取武器失败 Id : {id}");
        return false;
    }

    private bool AddEquipment(int id) {
        if (PickEquipment(id)) {
            LogSystem.Print($"拾取装备成功 发送装备隐藏消息 Id : {id}");
            MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentGameObj(id).Hide();

            var level = MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentComponent(id).MyEquipmentLevel;
            if (level != 0) {
                SetBackpackLevel(level);
            }

            return true;
        }

        LogSystem.Print($"拾取装备失败 Id : {id}");
        return false;
    }

    public bool PickSceneItem(int id) {
        var type = MyGame.MyGameSystem.MyItemSystem.GetSceneItemType(id);
        if (backpackData.AddSceneItem(type, id)) {
            return true;
        }

        return false;
    }

    public bool PickWeapon(int id) {
        var type = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponType(id);
        var wepSign = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponSign(id);
        // 替换当前武器
        var curWepId = backpackData.GetCurWeapId();

        if (type == WeaponType.MainWeapon) {
            // 拿到空武器槽 放入
            if (backpackData.GetEmptyMainWeaponIndex(out int outIndex)) {
                backpackData.AddMainWeapon(outIndex, id);
            } else {
                // 当前武器是主武器
                if (backpackData.GetMainWeaponIndexById(curWepId, out int index)) {
                    DropMainWeapon(index);
                    backpackData.AddMainWeapon(index, id);
                } else {
                    // 当前武器不是主武器，这时候替换第一个武器
                    backpackData.AddMainWeapon(0, id);
                    DropMainWeapon(0);
                    backpackData.RemoveMainWeapon(0);
                }
            }
            MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterGameObj().SetHoldWeaponModel(wepSign);
            backpackData.SetCurWeapon(id);
            return true;
        } else if (type == WeaponType.SideWeapon) {
            if (curWepId != 0) {
                var curWepType = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponType(curWepId);
                if (curWepType == WeaponType.SideWeapon) {
                    DropSideWeapon();
                    MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterGameObj().SetHoldWeaponModel(wepSign);
                    backpackData.SetCurWeapon(id);
                }
            } else {
                MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterGameObj().SetHoldWeaponModel(wepSign);
                backpackData.SetCurWeapon(id);
            }
            // 拿到空武器槽 放入
            backpackData.AddSideWeapon(id);
            return true;
        }

        return false;
    }

    public bool PickEquipment(int id) {
        var type = MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentType(id);
        if (backpackData.AddEquipment(type, id)) {
            return true;
        }

        return false;
    }

    #endregion

    #region 删

    public bool DropSceneItem(int index) {
        var id = backpackData.GetSceneItemId(index);
        var type = MyGame.MyGameSystem.MyItemSystem.GetSceneItemType(id);
        if (backpackData.RemoveSceneItem(type, id)) {
            var dropPoint = GetDropPoint();
            MyGame.MyGameSystem.MyItemSystem.GetSceneItemGameObj(id).Drop(dropPoint);
            return true;
        }

        return false;
    }

    public bool DropMainWeapon(int index) {
        var weapId = backpackData.GetMainWeaponId(index);
        if (backpackData.RemoveMainWeapon(index)) {
            var dropPoint = GetDropPoint();
            MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(weapId).Drop(dropPoint);
            if (weapId == backpackData.GetCurWeapId()) {
                backpackData.SetCurWeapon(0);
                MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterGameObj().SetHoldWeaponModel("");
            }
            return true;
        }

        return false;
    }

    public bool DropSideWeapon() {
        var weapId = backpackData.GetSideWeaponId();
        if (backpackData.RemoveSideWeapon()) {
            var dropPoint = GetDropPoint();
            MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(weapId).Drop(dropPoint);
            if (weapId == backpackData.GetCurWeapId()) {
                backpackData.SetCurWeapon(0);
                MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterGameObj().SetHoldWeaponModel("");
            }
            return true;
        }

        return false;
    }

    public bool DropEquipment(int index) {
        var equipmentId = GetEquipmentId(index);
        if (backpackData.RemoveEquipment(index)) {
            var dropPoint = GetDropPoint();
            MyGame.MyGameSystem.MyEquipmentSystem.GetEquipmentGameObj(equipmentId).Drop(dropPoint);
            return true;
        }

        return false;
    }

    #endregion

    #region 改

    public bool SetBackpackLevel(int level) {
        return backpackData.SetSceneItemLevel(level);
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
        return backpackData.GetEquipmentIds();
    }

    public int GetEquipmentId(int index) {
        return backpackData.GetEquipmentIds()[index];
    }

    public int[] GetSceneItemIdsLevel() {
        return backpackData.GetSceneItemIdsLevel();
    }

    public List<int> GetSceneItemIds() {
        return backpackData.GetSceneItemIds();
    }

    public int GetSceneItemId(int index) {
        return backpackData.GetSceneItemIds()[index];
    }

    private Vector3 GetDropPoint() {
        var characterPoint = MyGame.MyGameSystem.MyCharacterSystem.GetMainCharacterComponent().transform.position;
        return GameData.GetGround(characterPoint);
    }

    public bool GetCurWeaponType(out WeaponType type) {
        var curId = backpackData.GetCurWeapId();
        if (curId == 0) {
            type = WeaponType.MainWeapon;
            return false;
        }

        type = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponType(curId);
        return true;
    }

    #endregion
}