using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
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
            MyObj = Object.Instantiate(SOData.MySOLightSetting.MainLightPrefab),
            MyRootTran = GameData.LightRoot,
            MyTranInfo = new TranInfo() {
                MyPos = SOData.MySOLightSetting.MainLightInfo.position,
                MyRot = SOData.MySOLightSetting.MainLightInfo.rotation,
            },
        });
    }

    public void InstanceMapBuilding() {
        List<BuildingInfo> buildingMapInfoList = SOData.MySoBuildingSetting.MyBuildingMapInfoList;
        foreach (var item in buildingMapInfoList) {
            InstanceBuilding(new BuildingData() {
                MyName = item.Sign,
                MyObj = Object.Instantiate(SOData.MySoBuildingSetting.GetBuildingBySign(item.Sign)),
                MyRootTran = GameData.EnvironmentRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point,
                    MyRot = item.Quaternion,
                },
            });
        }
    }

    private void InstanceBuilding(BuildingData buildingData) {
        MyGameSystem.InstanceGameObj<BuildingGameObj, BuildingEntity>(buildingData);
    }

    private void InstanceLight(LightData lightData) {
        MyGameSystem.InstanceGameObj<LightGameObj, LightEntity>(lightData);
    }
}