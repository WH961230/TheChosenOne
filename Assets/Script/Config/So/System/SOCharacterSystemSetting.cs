using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOCharacterSystemSetting")]
public class SOCharacterSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new CharacterSystem();
        instance.Init(gameSystem);
        return instance;
    }
}