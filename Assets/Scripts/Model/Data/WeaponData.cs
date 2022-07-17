using UnityEngine;

public class WeaponData : Data {
    public string MyWeaponSign;
    public Sprite MyWeaponSprite;
    public WeaponType MyWeaponType;
}

public enum WeaponType {
    MainWeapon,
    SideWeapon,
}