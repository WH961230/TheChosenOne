using UnityEngine;

public class EnvironmentSystem : GameSys {
    private SOSceneBuildingSetting soSceneBuildingSetting;
    public SOSceneBuildingSetting MySoSceneBuildingSetting {
        get {
            return soSceneBuildingSetting;
        }
    }

    private SOEnvironmentSetting soEnvironmentSetting;
    public SOEnvironmentSetting mySoEnvironmentSetting {
        get {
            return soEnvironmentSetting;
        }
    }

    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        soSceneBuildingSetting = Resources.Load<SOSceneBuildingSetting>(PathData.SOSceneBuildingSettingPath);
        soEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    private GameObject GetSceneBuildingBySign(string sign) {
        foreach (var building in soSceneBuildingSetting.MySceneBuildingPrefabInfoList) {
            if (sign.Contains(building.name)) {
                return building;
            }
        }

        return null;
    }

    public void InstanceEnvironment() {
        foreach (var item in soSceneBuildingSetting.MySceneBuildingInfoList) {
            InstanceSceneBuilding(new SceneBuildingData() {
                MyName = item.Sign,
                MyObj = Object.Instantiate(GetSceneBuildingBySign(item.Sign)),
                MyRootTran = GameData.EnvironmentRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point,
                    MyRot = item.Quaternion,
                },
            });
        }
    }

    private void InstanceSceneBuilding(SceneBuildingData sceneBuildingData) {
        // gameSystem.InstanceGameObj<SceneBuildingGameObj, SceneBuildingEntity>(sceneBuildingData);
    }
}