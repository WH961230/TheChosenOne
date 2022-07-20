using System;
using System.Linq;
using Object = UnityEngine.Object;

public class EquipmentSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    #region 获取

    public EquipmentGameObj GetEquipmentGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<EquipmentGameObj>(id);
    }

    public EquipmentType GetEquipmentType(int id) {
        return GetEquipmentGameObj(id).GetComponent<EquipmentComponent>().MyEquipmentType;
    }

    public EquipmentEntity GetEquipmentEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<EquipmentEntity>(id);
    }

    public EquipmentData GetEquipmentData(int id) {
        return GetEquipmentEntity(id).GetData<EquipmentData>();
    }

    #endregion

    #region 创建

    public void InstanceEquipment() {
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
            InstanceEquipment(new EquipmentData() {
                MyName = "Character",
                MyObj = Object.Instantiate(SOData.MySOEquipmentSetting.MyEquipmentParameterInfo[rand].Prefab),
                MyRootTran = GameData.ItemRoot,
                MyTranInfo = new TranInfo() {
                    MyPos = info.Point,
                    MyRot = info.Quaternion,
                }
            });
        }
    }

    private int InstanceEquipment(EquipmentData data) {
        return MyGameSystem.InstanceGameObj<EquipmentGameObj, EquipmentEntity>(data);
    }

    #endregion
}