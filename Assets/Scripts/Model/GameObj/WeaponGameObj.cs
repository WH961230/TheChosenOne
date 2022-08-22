using UnityEngine;

public class WeaponGameObj : GameObj {
    private WeaponComponent wepComp;
    private WeaponData wepData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        wepData = (WeaponData)data;
        wepComp = (WeaponComponent) Comp;
        wepData.MyFirePos = wepComp.MyFirePos;
        wepData.MyWeaponSign = wepComp.MySign;
        wepData.MyWeaponType = wepComp.MyWeaponType;
    }

    public void SetWeaponPlace(Transform root, Vector3 point, Quaternion rot) {
        wepData.MyObj.gameObject.transform.SetParent(root);
        wepData.MyObj.transform.localPosition = point;
        wepData.MyObj.transform.localRotation = rot;
    }

    public WeaponComponent GetComp() {
        return base.GetComp() as WeaponComponent;
    }
}