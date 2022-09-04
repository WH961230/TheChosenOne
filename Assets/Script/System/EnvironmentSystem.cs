using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : GameSys {
    public void InstanceLight() {
        InstanceLight(new LightData() {
            MyName = "MainLight",
            MyObj = Object.Instantiate(SoData.MySOLightSetting.MainLightPrefab),
            MyRootTran = GameData.LightRoot,
            MyTranInfo = new TranInfo() {
                MyPos = SoData.MySOLightSetting.MainLightInfo.position,
                MyRot = SoData.MySOLightSetting.MainLightInfo.rotation,
            }
        });
    }

    public void InstanceMapBuilding() {
        List<BuildingInfo> buildingMapInfoList = SoData.MySoBuildingSetting.MyBuildingMapInfoList;
        foreach (var item in buildingMapInfoList) {
            InstanceBuilding(new BuildingData() {
                MyName = item.Sign,
                MyObj = Object.Instantiate(SoData.MySoBuildingSetting.GetBuildingBySign(item.Sign)),
                MyRootTran = GameData.EnvironmentRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = item.Point,
                    MyRot = item.Quaternion,
                },
            });
        }
    }

    private void InstanceBuilding(BuildingData buildingData) {
        MyGS.InstanceGameObj<BuildingGameObj, BuildingEntity>(buildingData);
    }

    private void InstanceLight(LightData lightData) {
        MyGS.InstanceGameObj<LightGameObj, LightEntity>(lightData);
    }
}