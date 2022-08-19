using System;
using UnityEngine;

public class ConsumeSystem : GameSys {
    #region 增

    public ConsumeData InstanceConsume(Vector3 point, Quaternion rot) {
        var index = UnityEngine.Random.Range(0, SOData.MyConsumeSetting.ConsumeParameterInfos.Count);
        var consumeData = new ConsumeData() {
            MyName = "Consume",
            MyObj = UnityEngine.Object.Instantiate(SOData.MyConsumeSetting.ConsumeParameterInfos[index].Prefab),
            MyTranInfo = new TranInfo() {
                MyPos = point,
                MyRot = rot,
            },
            MyRootTran = GameData.ConsumeRoot,
        };
        InstanceConsume(consumeData);
        return consumeData;
    }

    private void InstanceConsume(ConsumeData consumeData) {
        MyGS.InstanceGameObj<ConsumeGameObj, ConsumeEntity>(consumeData);
    }

    #endregion

    public ConsumeGameObj GetGO(int id) {
        return base.GetGameObj<ConsumeGameObj>(id);
    }

    public ConsumeEntity GetEntity(int id) {
        return base.GetEntity<ConsumeEntity>(id);
    }
}