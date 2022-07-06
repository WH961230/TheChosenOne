using UnityEngine;

public class ItemSystem : GameSys {
    private SOSceneItemSetting soSceneItemSetting;
    public SOSceneItemSetting MySoSceneItemSetting {
        get {
            return soSceneItemSetting;
        }
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

    public void InstanceSceneItem() {
        var config = soSceneItemSetting;
        // 遍历生成点
        foreach (var item in config.MySceneItemInfoList) {
            var index = Random.Range(0, config.MySceneItemPrefabList.Count);
            var obj = config.MySceneItemPrefabList[index];
            InstanceSceneItem(new SceneItemData() {
                MyName = obj.name,
                MyObj = Object.Instantiate(obj),
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
}