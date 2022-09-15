using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOGameSetting")]
public class SOGameSetting : ScriptableObject {
    public GameObject UIRoot;
    public GameObject UIMainPrefab;
    public GameObject UIDebugToolPrefab;
    public GameObject UIMapPrefab;
    public GameObject UICharacterPrefab;
    public GameObject UIBackpackPrefab;
    public GameObject UITipPrefab;

    public List<SystemSettingBase> systemSetting = new List<SystemSettingBase>();
}