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
        InstanceSceneItem(new SceneItemData() {
            MyName = "SceneFloor",
            MyObj = Object.Instantiate(soSceneItemSetting.SceneItemFloor),
            MyTranInfo = new TranInfo() {
                MyRootTran = GameData.EnvironmentRoot,
            },
        });

        InstanceSceneItem(new SceneItemData() {
           MyName = "SceneHouse",
           MyObj = Object.Instantiate(soSceneItemSetting.SceneItemHouse),
           MyTranInfo = new TranInfo() {
               MyRootTran = GameData.EnvironmentRoot,
           }
        });
    }

    private void InstanceSceneItem(SceneItemData sceneItemData) {
        gameSystem.InstanceGameObj<SceneItemGameObj, SceneItemEntity>(sceneItemData);
    }
}