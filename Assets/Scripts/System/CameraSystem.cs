using UnityEngine;

public class CameraSystem : GameSys {
    #region 增

    public int InstanceCamera(CameraType cameraType) {
        if (TryGetCameraObj(cameraType, out GameObject cameraObj)) {
            string name = "";
            bool isActiveTemp= true;
            // 赋值全局相机参数
            if (cameraType == CameraType.MainCamera) {
                name = "MainCamera";
            } else if (cameraType == CameraType.MainCharacterCamera) {
                name = "MainCharacterCamera";
            } else if (cameraType == CameraType.WeaponCamera) {
                name = "WeaponCamera";
                isActiveTemp = false;
            }

            return InstanceCamera(new CameraData() {
                MyName = name,
                MyObj = Object.Instantiate(cameraObj),
                MyRootTran = GameData.CameraRoot,
                MyCameraType = cameraType,
                IsActive = isActiveTemp,
            });
        }

        return 0;
    }

    private int InstanceCamera(CameraData cameraData) {
        return MyGameSystem.InstanceGameObj<CameraGameObj, CameraEntity>(cameraData);
    }

    #endregion

    #region 查

    private bool TryGetCameraObj(CameraType cameraType, out GameObject camera) {
        foreach (var info in SOData.MySOCameraSetting.CameraInfos) {
            if (info.MyCameraType == cameraType) {
                camera = info.MyCameraObj;
                return true;
            }
        }

        camera = null;
        return false;
    }

    public CameraGameObj GetCameraGameObj(int id) {
        return MyGameSystem.MyGameObjFeature.Get<CameraGameObj>(id);
    }

    public CameraComponent GetCameraComponent(int id) {
        return GetCameraGameObj(id).GetComp<CameraComponent>();
    }

    public CameraGameObj GetWeaponCameraGameObj() {
        return GetCameraGameObj(GameData.WeaponCameraId);
    }

    public CameraComponent GetWeaponCameraComponent() {
        return GetWeaponCameraGameObj().GetComp<CameraComponent>();
    }

    #endregion
}