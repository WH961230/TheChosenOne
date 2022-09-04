public class EffectEntity : Entity {
    private EffectData effectData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        effectData = (EffectData)data;
    }

    public EffectData GetData() {
        return base.GetData() as EffectData;
    }
}