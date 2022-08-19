public class ConsumeEntity : Entity {
    private ConsumeData consumeData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.consumeData = (ConsumeData)data;
    }

    public ConsumeData GetData() {
        return base.GetData() as ConsumeData;
    }
}