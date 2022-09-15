using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOItemSystemSetting")]
public class SOItemSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new ItemSystem();
        instance.Init(gameSystem);
        return instance;
    }
}