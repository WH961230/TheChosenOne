public class BuildingEntity : Entity {
    private BuildingData buildingData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.buildingData = (BuildingData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}