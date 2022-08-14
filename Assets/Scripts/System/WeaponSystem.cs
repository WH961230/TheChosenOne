﻿using UnityEngine;

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