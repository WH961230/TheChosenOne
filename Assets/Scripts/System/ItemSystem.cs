using UnityEngine;

public class ItemSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 获取

    public ItemGameObj GetItemGameObj(int id) { // 物体
        return MyGameSystem.MyGameObjFeature.Get<ItemGameObj>(id);
    }

    public ItemEntity GetItemEntity(int id) { // 实体
        return MyGameSystem.MyEntityFeature.Get<ItemEntity>(id);
    }

    public ItemComponent GetItemComponent(int id) { // 实体 - 组件
        return GetItemGameObj(id).GetComponent<ItemComponent>();
    }

    public ItemData GetItemData(int id) { // 组件 - 数据
        return GetItemEntity(id).GetData<ItemData>();
    }

    #endregion

    #region 创建

    /// <summary>
    /// 创建地图物品
    /// </summary>
    public void InstanceMapItem() {
        foreach (var item in SOData.MySOItemSetting.MyMapInfo) {
            InstanceItemByItemType(item.MyItemType, item.Point, item.Quaternion);
        }
    }

    /// <summary>
    /// 根据物品类型创建物品
    /// </summary>
    /// <param name="type"></param>
    /// <returns>物品 id</returns>
    private void InstanceItemByItemType(ItemType type, Vector3 point, Quaternion rot) {
        switch (type) {
            case ItemType.CONSUME:
                MyGameSystem.MyConsumeSystem.InstanceConsume(point, rot);
                return;
            case ItemType.EQUIPMENT:
                MyGameSystem.MyEquipmentSystem.InstanceEquipment(point, rot);
                return;
            case ItemType.WEAPON:
                MyGameSystem.MyWeaponSystem.InstanceWeapon(point, rot);
                return;
        }
    }

    #endregion

    public string GetItemSign(int layer, int id) {
        switch (layer) {
            case LayerData.EquipmentLayer:
                return MyGameSystem.MyEquipmentSystem.GetEquipmentComponent(id).MySign;
            case LayerData.ConsumeLayer:
                return MyGameSystem.MyConsumeSystem.GetConsumeComponent(id).MySign;
            case LayerData.WeaponLayer:
                return MyGameSystem.MyWeaponSystem.GetWeaponComponent(id).MySign;
        }

        return "";
    }
}