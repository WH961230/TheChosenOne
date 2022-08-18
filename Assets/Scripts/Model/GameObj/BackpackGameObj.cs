public class BackpackGameObj : GameObj {
    private BackpackData backpackData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        backpackData = (BackpackData)data;
    }
}