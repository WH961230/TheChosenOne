using UnityEngine;

public class ItemSystem : GameSys {

    #region 创建

    /// <summary>
    /// 创建地图物品
    /// </summary>
    public void InstanceMapItem() {
        foreach (var item in SoData.MySOItemSetting.MyMapInfo) {
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
                MyGS.ConsumeS.InstanceConsume(point, rot);
                return;
            case ItemType.EQUIPMENT:
                MyGS.EquipmentS.InstanceEquipment(point, rot);
                return;
            case ItemType.WEAPON:
                MyGS.WeapS.InstanceWeapon(point, rot);
                return;
        }
    }

    #endregion

    public string GetItemSign(int layer, int id) {
        switch (layer) {
            case LayerData.EquipmentLayer:
                return MyGS.EquipmentS.GetGO(id).GetComp().MySign;
            case LayerData.ConsumeLayer:
                return MyGS.ConsumeS.GetGO(id).GetComp().MySign;
            case LayerData.WeaponLayer:
                return MyGS.WeapS.GetGO(id).GetComp().MySign;
        }

        return "";
    }
}