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

    #region 查

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

    public string GetWeaponSign(int id) {
        return GetWeaponComponent(id).MyWeaponSign;
    }

    #endregion

    #region 增

    // public void InstanceMapWeapon() {
    //     var weaponMapInfo = SOData.MySOWeaponSetting.MyWeaponMapInfo;
    //     if (weaponMapInfo.Count <= 0) {
    //         return;
    //     }
    //
    //     if (GameData.WeaponCameraId == 0) {
    //         GameData.WeaponCameraId = MyGameSystem.MyCameraSystem.InstanceCamera(CameraType.WeaponCamera);
    //     }
    //
    //     var weaponParameterInfo = SOData.MySOWeaponSetting.MyWeaponParameterInfo;
    //     foreach (var weapon in weaponMapInfo) {
    //         var rand = Random.Range(0, weaponParameterInfo.Count);
    //         var tempInfo = weaponParameterInfo[rand];
    //         InstanceWeapon(new WeaponData() {
    //             MyName = "Weapon",
    //             MyRootTran = GameData.ItemRoot,
    //             MyObj = Object.Instantiate(tempInfo.Prefab),
    //             MySprite = weaponParameterInfo[rand].Picture,
    //             MyTranInfo = new TranInfo() {
    //                 MyPos = weapon.Point, MyRot = weapon.Quaternion,
    //             },
    //             WeaponCameraAimPoint = tempInfo.WeaponCameraAimPoint,
    //             WeaponCameraAimFOV = tempInfo.WeaponCameraAimFOV,
    //             IfInitMyObj = true,
    //         });
    //     }
    // }

    public int InstanceWeapon() {
        int index = Random.Range(0, SOData.MySOWeaponSetting.MyWeaponParameterInfo.Count);
        var param = SOData.MySOWeaponSetting.MyWeaponParameterInfo[index];
        return InstanceWeapon(new WeaponData() {
            MyName = "Weapon",
            MyObj = UnityEngine.Object.Instantiate(param.Prefab),
            IsDefaultClose = true,
            MyRootTran = GameData.WeaponRoot,
            WeaponCameraAimPoint = param.WeaponCameraAimPoint,
            WeaponCameraAimFOV = param.WeaponCameraAimFOV,
        });
    }

    private int InstanceWeapon(WeaponData weaponData) {
        return MyGameSystem.InstanceGameObj<WeaponGameObj, WeaponEntity>(weaponData);
    }

    #endregion
}