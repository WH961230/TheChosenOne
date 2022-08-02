using UnityEngine;

public class ItemSystem : GameSys {
    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 获取

    public SceneItemGameObj GetSceneItemGameObj(int id) { // 物体
        return gameSystem.MyGameObjFeature.Get<SceneItemGameObj>(id);
    }

    public SceneItemEntity GetSceneItemEntity(int id) { // 实体
        return gameSystem.MyEntityFeature.Get<SceneItemEntity>(id);
    }

    public SceneItemComponent GetSceneItemComponent(int id) { // 实体 - 组件
        return GetSceneItemGameObj(id).GetComponent<SceneItemComponent>();
    }

    public SceneItemData GetSceneItemData(int id) { // 组件 - 数据
        return GetSceneItemEntity(id).GetData<SceneItemData>();
    }

    public SceneItemType GetSceneItemType(int id) {
        return GetSceneItemComponent(id).MySceneItemType;
    }

    #endregion

    #region 创建

    public void InstanceMapSceneItem() {
        var mapInfo = SOData.MySOSceneItemSetting.MySceneItemMapInfo;
        var parameterInfo = SOData.MySOSceneItemSetting.MySceneItemParameterInfo;
        foreach (var item in mapInfo) {
            var index = Random.Range(0, parameterInfo.Count);
            var obj = parameterInfo[index];
            InstanceSceneItem(new SceneItemData() {
                MyName = obj.SceneItemPrefab.name,
                MyObj = Object.Instantiate(obj.SceneItemPrefab),
                MyBackpackSprite = obj.SceneItemPicture,
                MyRootTran = GameData.ItemRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point,
                    MyRot = item.Quaternion,
                }
            });
        }
    }

    public int InstanceSceneItem() {
        // InstanceSceneItem(new SceneItemData() {
        //     MyName = obj.SceneItemPrefab.name,
        //     MyObj = Object.Instantiate(obj.SceneItemPrefab),
        //     MyBackpackSprite = obj.SceneItemPicture,
        //     MyRootTran = GameData.ItemRoot,
        //     MyTranInfo = new TranInfo() {
        //         MyPos = item.Point,
        //         MyRot = item.Quaternion,
        //     }
        // });
        return 0;
    }

    private int InstanceSceneItem(SceneItemData sceneItemData) {
        return gameSystem.InstanceGameObj<SceneItemGameObj, SceneItemEntity>(sceneItemData);
    }

    #endregion
}