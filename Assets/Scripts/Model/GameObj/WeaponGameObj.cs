using UnityEngine;

public class WeaponGameObj : GameObj {
    private WeaponComponent weaponComponent;
    private WeaponData weaponData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        weaponData = (WeaponData)data;
        weaponComponent = (WeaponComponent) Comp;
        weaponData.MyFirePos = weaponComponent.MyFirePos;
        weaponData.MyWeaponSign = weaponComponent.MySign;
        weaponData.MyWeaponType = weaponComponent.MyWeaponType;
    }

    public void SetWeaponPlace(Transform weaponPlace, Vector3 point, Quaternion rot) {
        weaponData.MyObj.gameObject.transform.SetParent(weaponPlace);
        weaponData.MyObj.transform.localPosition = point;
        weaponData.MyObj.transform.localRotation = rot;
    }

    public WeaponComponent GetComp() {
        return base.GetComp() as WeaponComponent;
    }
}