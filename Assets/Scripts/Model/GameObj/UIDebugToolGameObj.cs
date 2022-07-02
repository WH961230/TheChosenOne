public class UIDebugToolGameObj : GameObj {
    private UIDebugToolData uidebugtoolData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uidebugtoolData = (UIDebugToolData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}