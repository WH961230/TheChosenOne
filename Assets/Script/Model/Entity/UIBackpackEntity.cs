public class UIBackpackEntity : Entity {
    private UIBackpackData uibackpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uibackpackData = (UIBackpackData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}