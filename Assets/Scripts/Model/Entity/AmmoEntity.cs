public class AmmoEntity : Entity {
    private AmmoData ammoData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.ammoData = (AmmoData)data;
    }
}