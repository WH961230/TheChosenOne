public class SceneBuildingGameObj : GameObj {
    private SceneBuildingData scenebuildingData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        scenebuildingData = (SceneBuildingData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}