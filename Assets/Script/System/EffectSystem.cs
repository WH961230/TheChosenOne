using UnityEngine;

public class EffectSystem : GameSys {
    public int InstanceEffect(Vector3 point, Quaternion rot) {
        EffectData data = new EffectData() {
            MyName = "Effect",
            MyObj = Object.Instantiate(SoData.MyEffectSetting.MyBulletFX),
            MyTranInfo = new TranInfo() {
                MyPos = point,
                MyRot = rot,
            },
            MyRootTran = GameData.EffectRoot,
        };

        return InstanceEffect(data);
    }

    private int InstanceEffect(EffectData data) {
        return MyGS.InstanceGameObj<EffectGameObj, EffectEntity>(data);
    }

    public EffectGameObj GetEffectGO(int id) {
        return GetGameObj<EffectGameObj>(id);
    }

    public EffectEntity GetEffectEntity(int id) {
        return GetEntity<EffectEntity>(id);
    }
}