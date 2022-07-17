using UnityEngine;

public static class SOData {
    public static SOGameSetting MySOGameSetting;
    public static SOCharacterSetting MySOCharacter;
    public static SOSceneBuildingSetting MySOSceneBuildingSetting;
    public static SOSceneItemSetting MySOSceneItemSetting;
    public static SOEnvironmentSetting MySOEnvironmentSetting;
    public static SOCameraSetting MySOCameraSetting;
    public static SOLightSetting MySOLightSetting;
    public static SOAudioMainSetting MySOAudioMainSetting;
    public static SOWeaponSetting MySOWeaponSetting;
    public static void Init() {
        //初始化配置
        MySOGameSetting = Resources.Load<SOGameSetting>(PathData.SOGameSettingPath);
        MySOCharacter = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        MySOSceneBuildingSetting = Resources.Load<SOSceneBuildingSetting>(PathData.SOSceneBuildingSettingPath);
        MySOSceneItemSetting = Resources.Load<SOSceneItemSetting>(PathData.SOSceneItemSettingPath);
        MySOEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
        MySOCameraSetting = Resources.Load<SOCameraSetting>(PathData.SOCameraSettingPath);
        MySOLightSetting = Resources.Load<SOLightSetting>(PathData.SOLightSettingPath);
        MySOAudioMainSetting = Resources.Load<SOAudioMainSetting>(PathData.SOAudioMainSettingPath);
        MySOWeaponSetting = Resources.Load<SOWeaponSetting>(PathData.SOWeaponSettingPath);
    }
}