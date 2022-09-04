public class UIDebugToolEntity : Entity {
    private UIDebugToolData uidebugtoolData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uidebugtoolData = (UIDebugToolData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}