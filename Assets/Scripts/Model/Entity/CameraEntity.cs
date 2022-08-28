public class CameraEntity : Entity {
    private CameraData cameraData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.cameraData = (CameraData)data;
    }

    public CameraData GetData() {
        return base.GetData() as CameraData;
    }
}