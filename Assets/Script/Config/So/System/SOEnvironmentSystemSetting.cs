using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOEnvironmentSystemSetting")]
public class SOEnvironmentSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new EnvironmentSystem();
        instance.Init(gameSystem);
        return instance;
    }
}