using UnityEngine;

public class CameraSystem : GameSys {
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        InstanceCamera(CameraType.MainCamera);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    #region 增

    public int InstanceCamera(CameraType cameraType) {
        if (TryGetCameraObj(cameraType, out GameObject cameraObj)) {
            string name = "";
            bool isDefaultClose = false;
            // 赋值全局相机参数
            if (cameraType == CameraType.MainCamera) {
                name = "MainCamera";
            } else if (cameraType == CameraType.MainCharacterCamera) {
                name = "MainCharacterCamera";
            } else if (cameraType == CameraType.WeaponCamera) {
                name = "WeaponCamera";
                isDefaultClose = true;
            }

            return InstanceCamera(new CameraData() {
                MyName = name,
                MyObj = Object.Instantiate(cameraObj),
                MyRootTran = GameData.CameraRoot,
                MyCameraType = cameraType,
                IsDefaultClose = isDefaultClose,
            });
        }

        return 0;
    }

    private int InstanceCamera(CameraData cameraData) {
        return gameSystem.InstanceGameObj<CameraGameObj, CameraEntity>(cameraData);
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
        return GetCameraGameObj(id).GetComponent<CameraComponent>();
    }

    public CameraGameObj GetWeaponCameraGameObj() {
        return GetCameraGameObj(GameData.WeaponCameraId);
    }

    public CameraComponent GetWeaponCameraComponent() {
        return GetWeaponCameraGameObj().GetComponent<CameraComponent>();
    }

    #endregion
}