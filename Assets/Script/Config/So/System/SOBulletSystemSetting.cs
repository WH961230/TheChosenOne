using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOBulletSystemSetting")]
public class SOBulletSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new BulletSystem();
        instance.Init(gameSystem);
        return instance;
    }
}