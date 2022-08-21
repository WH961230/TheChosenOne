using UnityEngine;

public class EffectGameObj : GameObj {
    private EffectData effectData;
    private EffectComponent effectComp;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        effectData = (EffectData)data;
        effectComp = GetComp();
    }

    public void SetEffect(Vector3 targetPos) {
        effectComp.IsOpen = true;
        effectComp.FlySpeed = 2;
    }

    public EffectComponent GetComp() {
        return base.GetComp() as EffectComponent;
    }
}