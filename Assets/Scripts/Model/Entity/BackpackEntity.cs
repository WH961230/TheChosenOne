using System.Collections.Generic;

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
        if (type == WeaponType.MainWeapon) {
            if (backpackData.AddMainWeapon(id)) {
                return true;
            }
        } else if (type == WeaponType.SideWeapon) {
            if (backpackData.AddSideWeapon(id)) {
                return true;
            }
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
            return true;
        }

        return false;
    }

    public bool DropMainWeapon(int index) {
        if (backpackData.RemoveMainWeapon(index)) {
            return true;
        }

        return false;
    }

    public bool DropSideWeapon() {
        if (backpackData.RemoveSideWeapon()) {
            return true;
        }

        return false;
    }

    public bool DropEquipment(int index) {
        if (backpackData.RemoveEquipment(index)) {
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

    #endregion
}