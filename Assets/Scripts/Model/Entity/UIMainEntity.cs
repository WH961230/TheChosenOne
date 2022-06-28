public class UIMainEntity : Entity {
    private UIMainData uimainData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uimainData = (UIMainData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}