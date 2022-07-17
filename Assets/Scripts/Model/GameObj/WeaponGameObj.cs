public class WeaponGameObj : GameObj {
    private WeaponComponent weaponComponent;
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        weaponData = (WeaponData)data;
        MyComponent = MyObj.transform.GetComponent<WeaponComponent>();
        weaponComponent = (WeaponComponent) MyComponent;
        weaponData.MyWeaponSign = weaponComponent.MyWeaponSign;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }
}