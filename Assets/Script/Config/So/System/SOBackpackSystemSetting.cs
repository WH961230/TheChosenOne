using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOBackpackSystemSetting")]
public class SOBackpackSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new BackpackSystem();
        instance.Init(gameSystem);
        return instance;
    }
}