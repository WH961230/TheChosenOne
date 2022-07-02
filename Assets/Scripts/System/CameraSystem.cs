using UnityEngine;

public class CameraSystem : GameSys {
    private SOCameraSetting soCameraSetting;
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        soCameraSetting = Resources.Load<SOCameraSetting>(PathData.SOCameraSettingPath);
        InstanceCamera(CameraType.MainCamera);
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public int InstanceCamera(CameraType cameraType) {
        if (GameData.MainCamera != -1 && cameraType == CameraType.MainCamera) {
            Debug.LogError("【ERROR:主相机重复创建】");
        }

        if (TryGetCamera(cameraType, out Camera camera)) {
            var instanceId = InstanceCamera(new CameraData() {
                MyName = "Camera",
                MyObj = Object.Instantiate(camera.gameObject),
                MyTranInfo = new TranInfo() {
                    MyRootTran = GameData.CameraRoot,
                },
            });

            // 赋值全局相机参数
            if (cameraType == CameraType.MainCamera) {
                GameData.MainCamera = instanceId;
            } else if (cameraType == CameraType.CharacterCamera) {
                GameData.CharacterCamera.Add(instanceId);
            }

            return instanceId;
        }

        return -1;
    }

    private bool TryGetCamera(CameraType cameraType, out Camera camera) {
        foreach (var info in soCameraSetting.CameraInfos) {
            if (info.MyCameraType == cameraType) {
                camera = info.MyCamera;
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