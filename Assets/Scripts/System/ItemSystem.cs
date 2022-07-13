using UnityEngine;

public class ItemSystem : GameSys {
    private SOSceneItemSetting soSceneItemSetting;

    public SOSceneItemSetting MySoSceneItemSetting {
        get { return soSceneItemSetting; }
    }

    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        soSceneItemSetting = Resources.Load<SOSceneItemSetting>(PathData.SOSceneItemSettingPath);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 创建

    public void InstanceSceneItem() {
        var config = soSceneItemSetting;
        var tempInfoList = config.GetSceneItemInfoList();
        var tempPrefabList1 = config.GetSceneItemPrefabList1();
        // 遍历生成点
        foreach (var item in tempInfoList) {
            var index = Random.Range(0, tempPrefabList1.Count);
            var obj = tempPrefabList1[index];
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

    private void InstanceSceneItem(SceneItemData sceneItemData) {
        gameSystem.InstanceGameObj<SceneItemGameObj, SceneItemEntity>(sceneItemData);
    }

    #endregion
}