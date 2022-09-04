using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class EquipmentSystem : GameSys {
    #region 创建

    public void InstanceMapEquipment() {
        var mapInfo = SoData.MySOEquipmentSetting.MyEquipmentMapInfo;
        if (mapInfo.Count < 0) {
            return;
        }

        var parameterInfo = SoData.MySOEquipmentSetting.MyEquipmentParameterInfo;
        if (parameterInfo.Count < 0) {
            return;
        }

        foreach (var info in mapInfo) {
            var rand = UnityEngine.Random.Range(0, parameterInfo.Count);
            var go = SoData.MySOEquipmentSetting.MyEquipmentParameterInfo[rand].Prefab;
            InstanceEquipment(new EquipmentData() {
                MyName = "Equipment",
                MyObj = Object.Instantiate(go),
                MyRootTran = GameData.ItemRoot,
                MySprite = SoData.MySOEquipmentSetting.MyEquipmentParameterInfo[rand].Picture,
                MyTranInfo = new TranInfo() {
                    MyPos = info.Point,
                    MyRot = info.Quaternion,
                }
            });
        }
    }

    public EquipmentData InstanceEquipment(Vector3 point, Quaternion rot) {
        var index = UnityEngine.Random.Range(0, SoData.MySOEquipmentSetting.MyEquipmentParameterInfo.Count);
        var equipmentData = new EquipmentData() {
            MyName = "Equipment",
            MyObj = UnityEngine.Object.Instantiate(SoData.MySOEquipmentSetting.MyEquipmentParameterInfo[index].Prefab),
            MyTranInfo = new TranInfo() {
                MyPos = point,
                MyRot = rot,
            },
            MyRootTran = GameData.EquipmentRoot,
        };
        InstanceEquipment(equipmentData);
        return equipmentData;
    }

    private void InstanceEquipment(EquipmentData data) {
        MyGS.InstanceGameObj<EquipmentGameObj, EquipmentEntity>(data);
    }

    #endregion

    public EquipmentGameObj GetGO(int id) {
        return base.GetGameObj<EquipmentGameObj>(id);
    }

    public EquipmentEntity GetEtity(int id) {
        return base.GetEntity<EquipmentEntity>(id);
    }
}