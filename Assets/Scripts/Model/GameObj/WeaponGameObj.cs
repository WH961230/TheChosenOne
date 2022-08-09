using UnityEngine;

public class WeaponGameObj : GameObj {
    private WeaponComponent weaponComponent;
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        weaponData = (WeaponData)data;
        weaponComponent = (WeaponComponent) MyComponent;
        weaponData.MyWeaponSign = weaponComponent.MyWeaponSign;
        weaponData.MyWeaponType = weaponComponent.MyWeaponType;
    }
}