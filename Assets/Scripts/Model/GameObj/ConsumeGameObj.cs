public class ConsumeGameObj : GameObj {
    private ConsumeData consumeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        consumeData = (ConsumeData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}