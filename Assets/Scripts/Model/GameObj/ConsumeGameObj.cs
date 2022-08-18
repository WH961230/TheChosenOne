public class ConsumeGameObj : GameObj {
    private ConsumeData consumeData;
    private ConsumeComponent consumeComponent;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        consumeData = (ConsumeData)data;
        consumeComponent = (ConsumeComponent) MyComp;
        consumeData.ConsumeNum = consumeComponent.ConsumeNum;
    }
}