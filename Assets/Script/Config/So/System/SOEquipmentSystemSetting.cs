using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOEquipmentSystemSetting")]
public class SOEquipmentSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new EquipmentSystem();
        instance.Init(gameSystem);
        return instance;
    }
}