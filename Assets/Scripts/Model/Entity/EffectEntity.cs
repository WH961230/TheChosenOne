public class EffectEntity : Entity {
    private EffectData effectData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.effectData = (EffectData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}