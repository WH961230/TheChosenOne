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

    public SceneItemGameObj GetSceneItemGameObj(int id) { // 物体
        return MyGameSystem.MyGameObjFeature.Get<SceneItemGameObj>(id);
    }

    public SceneItemEntity GetSceneItemEntity(int id) { // 实体
        return MyGameSystem.MyEntityFeature.Get<SceneItemEntity>(id);
    }

    public SceneItemComponent GetSceneItemComponent(int id) { // 实体 - 组件
        return GetSceneItemGameObj(id).GetComponent<SceneItemComponent>();
    }

    public SceneItemData GetSceneItemData(int id) { // 组件 - 数据
        return GetSceneItemEntity(id).GetData<SceneItemData>();
    }

    #endregion

    #region 创建

    public void InstanceMapSceneItem() {
        var mapInfo = SOData.MySOSceneItemSetting.MySceneItemMapInfo;
        var parameterInfo = SOData.MySOSceneItemSetting.MySceneItemParameterInfo;
        foreach (var item in mapInfo) {
            int tempSceneItemId = 0;
            if (item.MySceneItemType == SceneItemType.CONSUME) {
                tempSceneItemId = MyGameSystem.MyConsumeSystem.InstanceConsume();
            } else if (item.MySceneItemType == SceneItemType.EQUIPMENT) {
                tempSceneItemId = MyGameSystem.MyEquipmentSystem.InstanceEquipment();
            } else if (item.MySceneItemType == SceneItemType.WEAPON) {
                tempSceneItemId = MyGameSystem.MyWeaponSystem.InstanceWeapon();
            }

            var obj = parameterInfo[Random.Range(0, parameterInfo.Count)];
            InstanceSceneItem(new SceneItemData() {
                MyName = obj.SceneItemPrefab.name,
                MyObj = Object.Instantiate(obj.SceneItemPrefab),
                MySceneItemId = tempSceneItemId,
                MyRootTran = GameData.ItemRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point, MyRot = item.Quaternion,
                },
                MySceneItemType = item.MySceneItemType,
            });
        }
    }

    private int InstanceSceneItem(SceneItemData sceneItemData) {
        return MyGameSystem.InstanceGameObj<SceneItemGameObj, SceneItemEntity>(sceneItemData);
    }

    #endregion
}