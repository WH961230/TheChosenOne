using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOAnimatorSystemSetting")]
public class SOAnimatorSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new AnimatorSystem();
        instance.Init(gameSystem);
        return instance;
    }
}