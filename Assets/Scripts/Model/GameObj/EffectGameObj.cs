public class EffectGameObj : GameObj {
    private EffectData effectData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        effectData = (EffectData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}