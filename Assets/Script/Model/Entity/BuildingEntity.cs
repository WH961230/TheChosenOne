public class BuildingEntity : Entity {
    private BuildingData buildingData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        buildingData = (BuildingData)data;
    }
}