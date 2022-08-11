using UnityEngine;

public class EffectSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
    }

    public void InstanceEffect() {
        EffectData data = new EffectData() {
            MyName = "Effect",
            MyObj = Object.Instantiate(SOData.MyEffectSetting.MyBulletFX),
            MyRootTran = GameData.EffectRoot,
        };

        InstanceEffect(data);
    }

    private void InstanceEffect(EffectData data) {
        MyGameSystem.InstanceGameObj<EffectGameObj, EffectEntity>(data);
    }
}