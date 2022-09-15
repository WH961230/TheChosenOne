using UnityEngine;

[CreateAssetMenu(menuName = "SO/System/SOAudioSystemSetting")]
public class SOAudioSystemSetting : SystemSettingBase {
    public override GameSys OnInit(GameSystem gameSystem) {
        base.OnInit(gameSystem);
        var instance = new AudioSystem();
        instance.Init(gameSystem);
        return instance;
    }
}