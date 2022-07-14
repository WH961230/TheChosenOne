public class PlaneEntity : Entity {
    private PlaneData planeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.planeData = (PlaneData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}