public class WeaponEntity : Entity {
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        weaponData = (WeaponData)data;
    }

    public WeaponData GetData() {
        return base.GetData() as WeaponData;
    }
}