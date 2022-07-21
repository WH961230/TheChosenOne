using System.Collections.Generic;

public class BackpackEntity : Entity {
    private BackpackData backpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.backpackData = (BackpackData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 拾取物体到背包

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

    #region 丢弃物体

    public bool DropSceneItem(int id) {
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

    #region 消耗物体

    

    #endregion

    #region 获取物体

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

    public List<int> GetSceneItemIds() {
        return backpackData.GetSceneItemIds();
    }
    #endregion
}