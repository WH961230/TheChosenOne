public class UIMapEntity : Entity {
    private UIMapData uimapData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uimapData = (UIMapData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}