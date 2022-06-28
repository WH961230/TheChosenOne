public class UIMainGameObj : GameObj {
    private UIMainData uimainData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        uimainData = (UIMainData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}