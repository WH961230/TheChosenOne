public class AmmoGameObj : GameObj {
    private AmmoData ammoData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        ammoData = (AmmoData)data;
    }
}