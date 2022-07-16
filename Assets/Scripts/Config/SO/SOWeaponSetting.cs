using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOWeaponSetting")]
public class SOWeaponSetting : ScriptableObject {
    public List<WeaponCreatePointInfo> weaponCreatePointInfo = new List<WeaponCreatePointInfo>();
}

[Serializable]
public struct WeaponCreatePointInfo {
    public Vector3 MyCreatePosition;
    public Quaternion MyCreateRotation;
}