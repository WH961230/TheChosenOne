public class UITipEntity : Entity {
    private UITipData uitipData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.uitipData = (UITipData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}