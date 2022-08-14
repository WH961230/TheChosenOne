public class ConsumeGameObj : GameObj {
    private ConsumeData consumeData;
    private ConsumeComponent consumeComponent;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        consumeData = (ConsumeData)data;
        consumeComponent = (ConsumeComponent) MyComponent;
        consumeData.ConsumeNum = consumeComponent.ConsumeNum;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}