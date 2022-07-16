public class WeaponEntity : Entity {
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.weaponData = (WeaponData)data;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }
}