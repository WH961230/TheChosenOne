using System;

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

    public ConsumeData InstanceConsume() {
        var index = UnityEngine.Random.Range(0, SOData.MyConsumeSetting.ConsumeParameterInfos.Count);
        var consumeData = new ConsumeData() {
            MyName = "Consume",
            MyObj = UnityEngine.Object.Instantiate(SOData.MyConsumeSetting.ConsumeParameterInfos[index].Prefab),
            IsDefaultClose = true,
            MyRootTran = GameData.ConsumeRoot,
        };
        InstanceConsume(consumeData);
        return consumeData;
    }

    private void InstanceConsume(ConsumeData consumeData) {
        MyGameSystem.InstanceGameObj<ConsumeGameObj, ConsumeEntity>(consumeData);
    }

    #endregion
}