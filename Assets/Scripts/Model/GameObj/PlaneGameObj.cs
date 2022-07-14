public class PlaneGameObj : GameObj {
    private PlaneData planeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        planeData = (PlaneData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}