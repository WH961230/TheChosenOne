using System;
using UnityEngine;

public class ConsumeSystem : GameSys {
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
        MyGameSystem.InstanceGameObj<ConsumeGameObj, ConsumeEntity>(consumeData);
    }

    #endregion

    #region 查

    public ConsumeGameObj GetConsumeGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<ConsumeGameObj>(id);
    }

    public ConsumeComponent GetConsumeComponent(int id) {
        return GetConsumeGameObj(id).GetComp<ConsumeComponent>();
    }

    public ConsumeData GetConsumeData(int id) {
        return GetEntity<ConsumeEntity>(id).GetData<ConsumeData>();
    }

    #endregion
}