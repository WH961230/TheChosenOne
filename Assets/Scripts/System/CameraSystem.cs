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

    public void InstanceCamera(CameraType cameraType) {
        if (GameData.MainCamera != -1 && cameraType == CameraType.MainCamera) {
            LogSystem.Print("【ERROR:主相机重复创建】");
        }

        if (GameData.MainCharacterCamera != -1 && cameraType == CameraType.MainCharacterCamera) {
            LogSystem.PrintE("玩家相机重复创建");
        }

        if (TryGetCamera(cameraType, out GameObject cameraObj)) {
            var instanceId = InstanceCamera(new CameraData() {
                MyName = "Camera",
                MyObj = Object.Instantiate(cameraObj),
                MyRootTran = GameData.CameraRoot,
                MyCameraType = cameraType,
            });

            // 赋值全局相机参数
            if (cameraType == CameraType.MainCamera) {
                GameData.MainCamera = instanceId;
            } else if (cameraType == CameraType.MainCharacterCamera) {
                GameData.MainCharacterCamera = instanceId;
            }
        }
    }

    private bool TryGetCamera(CameraType cameraType, out GameObject camera) {
        foreach (var info in SOData.MySOCameraSetting.CameraInfos) {
            if (info.MyCameraType == cameraType) {
                camera = info.MyCameraObj;
                return true;
            }
        }

        camera = null;
        return false;
    }

    private int InstanceCamera(CameraData cameraData) {
        return gameSystem.InstanceGameObj<CameraGameObj, CameraEntity>(cameraData);
    }
}