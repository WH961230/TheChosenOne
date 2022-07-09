public class LightEntity : Entity {
    private LightData lightData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.lightData = (LightData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}