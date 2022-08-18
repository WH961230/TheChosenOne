using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class EquipmentSystem : GameSys {
    #region 获取

    
    // 基础
    public EquipmentGameObj GetEquipmentGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<EquipmentGameObj>(id);
    }

    public EquipmentComponent GetEquipmentComponent(int id) {
        return GetEquipmentGameObj(id).GetComp<EquipmentComponent>();
    }

    public EquipmentType GetEquipmentType(int id) {
        return GetEquipmentGameObj(id).GetComp<EquipmentComponent>().MyEquipmentType;
    }

    public EquipmentEntity GetEquipmentEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<EquipmentEntity>(id);
    }

    public EquipmentData GetEquipmentData(int id) {
        return GetEquipmentEntity(id).GetData<EquipmentData>();
    }

    #endregion

    #region 创建

    public void InstanceMapEquipment() {
        var mapInfo = SOData.MySOEquipmentSetting.MyEquipmentMapInfo;
        if (mapInfo.Count < 0) {
            return;
        }

        var parameterInfo = SOData.MySOEquipmentSetting.MyEquipmentParameterInfo;
        if (parameterInfo.Count < 0) {
            return;
        }

        foreach (var info in mapInfo) {
            var rand = UnityEngine.Random.Range(0, parameterInfo.Count);
            var go = SOData.MySOEquipmentSetting.MyEquipmentParameterInfo[rand].Prefab;
            InstanceEquipment(new EquipmentData() {
                MyName = "Equipment",
                MyObj = Object.Instantiate(go),
                MyRootTran = GameData.ItemRoot,
                MySprite = SOData.MySOEquipmentSetting.MyEquipmentParameterInfo[rand].Picture,
                MyTranInfo = new TranInfo() {
                    MyPos = info.Point,
                    MyRot = info.Quaternion,
                }
            });
        }
    }

    public EquipmentData InstanceEquipment(Vector3 point, Quaternion rot) {
        var index = UnityEngine.Random.Range(0, SOData.MySOEquipmentSetting.MyEquipmentParameterInfo.Count);
        var equipmentData = new EquipmentData() {
            MyName = "Equipment",
            MyObj = UnityEngine.Object.Instantiate(SOData.MySOEquipmentSetting.MyEquipmentParameterInfo[index].Prefab),
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
        MyGameSystem.InstanceGameObj<EquipmentGameObj, EquipmentEntity>(data);
    }

    #endregion
}