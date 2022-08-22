using UnityEngine;

public class WeaponData : Data {
    public string MyWeaponSign;
    public Sprite MySprite;
    public WeaponType MyWeaponType;
    public Transform MyFirePos;
    public WeaponParameterInfo WeapParamInfo; // 武器参数信息
}

public enum WeaponType {
    MainWeapon,
    SideWeapon,
}