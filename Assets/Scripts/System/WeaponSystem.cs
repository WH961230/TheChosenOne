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

    #region 获取

    public WeaponGameObj GetWeaponGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<WeaponGameObj>(id);
    }

    public WeaponComponent GetWeaponComponent(int id) {
        return GetWeaponGameObj(id).GetComponent<WeaponComponent>();
    }

    public WeaponEntity GetWeaponEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<WeaponEntity>(id);
    }

    public WeaponData GetWeaponData(int id) {
        return GetWeaponEntity(id).GetData<WeaponData>();
    }

    public WeaponType GetWeaponType(int id) {
        return GetWeaponComponent(id).MyWeaponType;
    }

    #endregion

    #region 创建

    public void InstanceWeapon() {
        var weaponMapInfo = SOData.MySOWeaponSetting.MyWeaponMapInfo;
        if (weaponMapInfo.Count <= 0) {
            return;
        }

        var weaponParameterInfo = SOData.MySOWeaponSetting.MyWeaponParameterInfo;
        foreach (var weapon in weaponMapInfo) {
            var rand = Random.Range(0, weaponParameterInfo.Count);
            InstanceWeapon(new WeaponData() {
                MyName = "Weapon",
                MyRootTran = GameData.ItemRoot,
                MyObj = Object.Instantiate(weaponParameterInfo[rand].Prefab),
                MyWeaponSprite = weaponParameterInfo[rand].Picture,
                MyTranInfo = new TranInfo() {
                    MyPos = weapon.Point, MyRot = weapon.Quaternion,
                },
                IsWindowPrefab = false,
                IfInitMyObj = true,
            });
        }
    }

    private int InstanceWeapon(WeaponData weaponData) {
        return MyGameSystem.InstanceGameObj<WeaponGameObj, WeaponEntity>(weaponData);
    }

    #endregion
}