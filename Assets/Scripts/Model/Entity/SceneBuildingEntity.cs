public class SceneBuildingEntity : Entity {
    private SceneBuildingData sceneBuildingData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.sceneBuildingData = (SceneBuildingData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}