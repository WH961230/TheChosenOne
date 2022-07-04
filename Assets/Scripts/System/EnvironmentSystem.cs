using UnityEngine;

public class EnvironmentSystem : GameSys {
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

    public void InstanceEnvironment() {
        foreach (var item in soSceneItemSetting.SceneItemInfoList) {
            if (soSceneItemSetting.TryGetSceneItemPrefabBySign(item.MyItemSign, out var prefab)) {
                InstanceSceneItem(new SceneItemData() {
                    MyName = item.MyItemSign,
                    MyObj = Object.Instantiate(prefab),
                    MyTranInfo = new TranInfo() {
                        MyPos = item.MySceneItemVector3,
                        MyRot = item.MySceneItemQuaternion,
                        MyRootTran = GameData.EnvironmentRoot,
                    },
                });
            }
        }
    }

    private void InstanceSceneItem(SceneItemData sceneItemData) {
        gameSystem.InstanceGameObj<SceneItemGameObj, SceneItemEntity>(sceneItemData);
    }
}