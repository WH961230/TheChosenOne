using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : GameSys {
    private SOSceneBuildingSetting soSceneBuildingSetting;
    public SOSceneBuildingSetting MySoSceneBuildingSetting {
        get {
            return soSceneBuildingSetting;
        }
    }

    private SOLightSetting soLightSetting;
    public SOLightSetting MySoLightSetting {
        get {
            return soLightSetting;
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
        soLightSetting = Resources.Load<SOLightSetting>(PathData.SOLightSettingPath);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public void InstanceLight() {
        InstanceLight(new LightData() {
            MyName = "MainLight",
            MyObj = Object.Instantiate(soLightSetting.MainLightPrefab),
            MyRootTran = GameData.LightRoot,
            MyTranInfo = new TranInfo() {
                MyPos = soLightSetting.MainLightInfo.position,
                MyRot = soLightSetting.MainLightInfo.rotation,
            },
        });
    }

    public void InstanceEnvironment() {
        List<SceneBuildingInfo> tempList = null;
        tempList = soSceneBuildingSetting.GetSceneBuildingInfoList();
        foreach (var item in tempList) {
            InstanceSceneBuilding(new SceneBuildingData() {
                MyName = item.Sign,
                MyObj = Object.Instantiate(soSceneBuildingSetting.GetSceneBuildingBySign(item.Sign)),
                MyRootTran = GameData.EnvironmentRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point,
                    MyRot = item.Quaternion,
                },
            });
        }
    }

    private void InstanceSceneBuilding(SceneBuildingData sceneBuildingData) {
        gameSystem.InstanceGameObj<SceneBuildingGameObj, SceneBuildingEntity>(sceneBuildingData);
    }

    private void InstanceLight(LightData lightData) {
        gameSystem.InstanceGameObj<LightGameObj, LightEntity>(lightData);
    }
}