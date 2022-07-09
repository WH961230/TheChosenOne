public class LightGameObj : GameObj {
    private LightData lightData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        lightData = (LightData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}