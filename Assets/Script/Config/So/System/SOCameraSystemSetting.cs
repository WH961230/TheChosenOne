using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOCameraSystemSetting")]
public class SOCameraSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new CameraSystem();
        instance.Init(gameSystem);
        return instance;
    }
}