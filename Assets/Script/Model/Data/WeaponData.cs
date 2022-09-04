using UnityEngine;

public class WeaponData : Data {
    public string MyWeaponSign;
    public Sprite MySprite;
    public WeaponType MyWeaponType;
    public InteractiveWeapon.WeaponType WeaponType;
    public Transform MyFirePos;
    public WeaponParameterInfo WeapParamInfo; // 武器参数信息
    public WeaponConfig.Weapon Weapon;
}

public enum WeaponType {
    MainWeapon,
    SideWeapon,
}