using UnityEngine;

public class WeaponSystem : GameSys {
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    public WeaponGameObj GetWeaponGameObj(int weaponId) {
        return MyGameSystem.MyGameObjFeature.Get<WeaponGameObj>(weaponId);
    }

    public WeaponComponent GetWeaponComponent(int id) {
        return GetWeaponGameObj(id).GetComponent<WeaponComponent>();
    }

    public void InstanceWeapon() {
        var weaponMapInfo = SOData.MySOWeaponSetting.MyWeaponMapInfo;
        var weaponParameterInfo = SOData.MySOWeaponSetting.MyWeaponParameterInfo;
        foreach (var weapon in weaponMapInfo) {
            var rand = Random.Range(0, weaponParameterInfo.Count);
            InstanceWeapon(new WeaponData() {
                MyName = "Weapon",
                MyRootTran = GameData.ItemRoot,
                MyObj = Object.Instantiate(weaponParameterInfo[rand].Prefab),
                MyTranInfo = new TranInfo() {
                    MyPos = weapon.Point,
                    MyRot = weapon.Quaternion,
                },
                IsWindowPrefab = false,
                IfInitMyObj = true,
            });
        }
    }

    private int InstanceWeapon(WeaponData weaponData) {
        return MyGameSystem.InstanceGameObj<WeaponGameObj, WeaponEntity>(weaponData);
    }

    public WeaponType GetWeaponType(int id) {
        return GetWeaponComponent(id).MyWeaponType;
    }
}