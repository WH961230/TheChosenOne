public class BulletEntity : Entity {
    private BulletData bulletData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.bulletData = (BulletData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}