using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOInputSystemSetting")]
public class SOInputSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new InputSystem();
        instance.Init(gameSystem);
        return instance;
    }
}