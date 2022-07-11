public class UIBackpackGameObj : GameObj {
    private UIBackpackData uibackpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uibackpackData = (UIBackpackData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}