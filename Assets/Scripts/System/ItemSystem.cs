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
            var templateInfo = SOData.MySOItemSetting.GetItemInfoByType(item.MyItemType);
            var obj = templateInfo[Random.Range(0, templateInfo.Count)];
            if (InstanceItemByItemType(item.MyItemType, out int id, out string itemSign)) {
                InstanceItem(new ItemData() {
                    MyName = obj.MyItemPrefab.name,
                    MyObj = Object.Instantiate(obj.MyItemPrefab),
                    MyItemId = id,
                    MyRootTran = GameData.ItemRoot,
                    MyTranInfo = new TranInfo() {
                        MyPos = item.Point, MyRot = item.Quaternion,
                    },
                    MyItemType = item.MyItemType,
                    MyItemSign = itemSign,
                });
            }
        }
    }

    /// <summary>
    /// 根据物品类型创建物品
    /// </summary>
    /// <param name="type"></param>
    /// <returns>物品 id</returns>
    private bool InstanceItemByItemType(ItemType type, out int id, out string itemSign) {
        Data data;
        switch (type) {
            case ItemType.CONSUME:
                data = MyGameSystem.MyConsumeSystem.InstanceConsume();
                id = data.InstanceID;
                itemSign = data.MyName;
                return true;
            case ItemType.EQUIPMENT:
                data = MyGameSystem.MyEquipmentSystem.InstanceEquipment();
                id = data.InstanceID;
                itemSign = data.MyName;
                return true;
            case ItemType.WEAPON:
                data = MyGameSystem.MyWeaponSystem.InstanceWeapon();
                id = data.InstanceID;
                itemSign = data.MyName;
                return true;
        }

        id = default;
        itemSign = default;
        return false;
    }

    /// <summary>
    /// 实例化物品
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns>物品 id</returns>
    private int InstanceItem(ItemData itemData) {
        return MyGameSystem.InstanceGameObj<ItemGameObj, ItemEntity>(itemData);
    }

    #endregion
}