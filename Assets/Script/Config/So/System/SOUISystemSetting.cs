using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOUISystemSetting")]
public class SOUISystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new UISystem();
        instance.Init(gameSystem);
        return instance;
    }
}