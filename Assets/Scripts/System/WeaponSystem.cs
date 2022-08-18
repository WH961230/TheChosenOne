using UnityEngine;

public class WeaponSystem : GameSys {
    #region 查

    public WeaponGameObj GetWeaponGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<WeaponGameObj>(id);
    }

    public WeaponComponent GetWeaponComponent(int id) {
        return GetWeaponGameObj(id).GetComp() as WeaponComponent;
    }

    public WeaponEntity GetWeaponEntity(int id) {
        return MyGameSystem.MyEntityFeature.Get<WeaponEntity>(id);
    }

    public WeaponData GetWeaponData(int id) {
        return GetWeaponEntity(id).GetData() as WeaponData;
    }

    public WeaponGameObj GetGO(int id) {
        return GetGameObj<WeaponGameObj>(id);
    }

    public WeaponEntity GetEntity(int id) {
        return GetEntity<WeaponEntity>(id);
    }

    #endregion

    #region 增

    public WeaponData InstanceWeapon(Vector3 point, Quaternion rot) {
        if (GameData.WeaponCameraId == 0) {
            GameData.WeaponCameraId = MyGameSystem.MyCameraSystem.InstanceCamera(CameraType.WeaponCamera);
        }

        int index = Random.Range(0, SOData.MySOWeaponSetting.MyWeaponParameterInfo.Count);
        var param = SOData.MySOWeaponSetting.MyWeaponParameterInfo[index];
        var weaponData = new WeaponData() {
            MyName = "Weapon",
            MyObj = Object.Instantiate(param.Prefab),
            MyTranInfo = new TranInfo() {
                MyPos = point,
                MyRot = rot,
            },
            WeaponCameraAimPoint = param.WeaponCameraAimPoint,
            WeaponCameraAimFOV = param.WeaponCameraAimFOV,
            MyRootTran = GameData.WeaponRoot,
        };
        InstanceWeapon(weaponData);
        return weaponData;
    }

    private void InstanceWeapon(WeaponData weaponData) {
        MyGameSystem.InstanceGameObj<WeaponGameObj, WeaponEntity>(weaponData);
    }

    #endregion
}