using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOConsumeSystemSetting")]
public class SOConsumeSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new ConsumeSystem();
        instance.Init(gameSystem);
        return instance;
    }
}