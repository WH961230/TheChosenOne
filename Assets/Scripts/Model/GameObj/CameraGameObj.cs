public class CameraGameObj : GameObj {
    private CameraData cameraData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        cameraData = (CameraData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}