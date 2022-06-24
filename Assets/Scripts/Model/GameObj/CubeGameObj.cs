public class CubeGameObj : GameObj {
    private CubeData cubeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        cubeData = (CubeData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}