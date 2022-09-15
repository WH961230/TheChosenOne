using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOEffectSystemSetting")]
public class SOEffectSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new EffectSystem();
        instance.Init(gameSystem);
        return instance;
    }
}