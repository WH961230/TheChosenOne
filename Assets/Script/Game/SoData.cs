using UnityEngine;

public static class SoData {
    public static SOGameSetting MySOGameSetting;
    public static SOCharacterSetting MySOCharacter;
    public static SOBuildingSetting MySoBuildingSetting;
    public static SOItemSetting MySOItemSetting;
    public static SOEnvironmentSetting MySOEnvironmentSetting;
    public static SOCameraSetting MySOCameraSetting;
    public static SOLightSetting MySOLightSetting;
    public static SOAudioMainSetting MySOAudioMainSetting;
    public static SOWeaponSetting MySOWeaponSetting;
    public static SOEquipmentSetting MySOEquipmentSetting;
    public static SOConsumeSetting MyConsumeSetting;
    public static SOBulletSetting MyBulletSetting;
    public static SOEffectSetting MyEffectSetting;

    public static void Init() {
        //初始化配置
        MySOGameSetting = Resources.Load<SOGameSetting>(PathData.SOGameSettingPath);
        MySOCharacter = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        MySoBuildingSetting = Resources.Load<SOBuildingSetting>(PathData.SOBuildingSettingPath);
        MySOItemSetting = Resources.Load<SOItemSetting>(PathData.SOItemSettingPath);
        MySOEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
        MySOCameraSetting = Resources.Load<SOCameraSetting>(PathData.SOCameraSettingPath);
        MySOLightSetting = Resources.Load<SOLightSetting>(PathData.SOLightSettingPath);
        MySOAudioMainSetting = Resources.Load<SOAudioMainSetting>(PathData.SOAudioMainSettingPath);
        MySOWeaponSetting = Resources.Load<SOWeaponSetting>(PathData.SOWeaponSettingPath);
        MySOEquipmentSetting = Resources.Load<SOEquipmentSetting>(PathData.SOEquipmentSettingPath);
        MyConsumeSetting = Resources.Load<SOConsumeSetting>(PathData.SOConsumeSettingPath);
        MyBulletSetting = Resources.Load<SOBulletSetting>(PathData.SOBulletSettingPath);
        MyEffectSetting = Resources.Load<SOEffectSetting>(PathData.SOEffectSettingPath);
    }
}