public class CubeEntity : Entity {
    private CubeData cubeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.cubeData = (CubeData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}