using UnityEngine;

public class WeaponSystem : GameSys {
    #region 查

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
            GameData.WeaponCameraId = MyGS.Get<CameraSystem>().InstanceCamera(CameraType.WeaponCamera);
        }

        int index = Random.Range(0, WeaponConfig.GetAll().Count);
        WeaponConfig.Weapon weapon = WeaponConfig.GetIndex(index);
        GameObject go = AssetsLoad.Load<GameObject>(weapon.prefabPath) as GameObject;

        var weaponData = new WeaponData() {
            MyName = "Weapon_",
            Sign = weapon.name,
            MyObj = Object.Instantiate(go),
            MyTranInfo = new TranInfo() {
                MyPos = point,
                MyRot = rot,
            },
            Weapon = weapon,
            MyRootTran = GameData.WeaponRoot,
        };

        InstanceWeapon(weaponData);
        return weaponData;
    }

    private void InstanceWeapon(WeaponData weaponData) {
        MyGS.InstanceGameObj<WeaponGameObj, WeaponEntity>(weaponData);
    }

    #endregion
}