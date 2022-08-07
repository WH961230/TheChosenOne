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

    public int InstanceConsume() {
        var index = UnityEngine.Random.Range(0, SOData.MyConsumeSetting.ConsumeParameterInfos.Count);
        return InstanceConsume(new ConsumeData() {
            MyName = "Consume",
            MyObj = UnityEngine.Object.Instantiate(SOData.MyConsumeSetting.ConsumeParameterInfos[index].Prefab),
            IsDefaultClose = true,
        });
    }

    private int InstanceConsume(ConsumeData consumeData) {
        return MyGameSystem.InstanceGameObj<ConsumeGameObj, ConsumeEntity>(consumeData);
    }

    #endregion
}