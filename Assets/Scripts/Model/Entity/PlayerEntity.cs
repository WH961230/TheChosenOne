public class PlayerEntity : Entity {
    private PlayerData playerData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.playerData = (PlayerData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}