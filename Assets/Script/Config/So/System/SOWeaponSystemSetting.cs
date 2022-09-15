using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOWeaponSystemSetting")]
public class SOWeaponSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new WeaponSystem();
        instance.Init(gameSystem);
        return instance;
    }
}