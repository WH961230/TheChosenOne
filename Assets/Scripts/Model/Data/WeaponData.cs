using UnityEngine;

public class WeaponData : Data {
    public string MyWeaponSign;
    public Sprite MySprite;
    public WeaponType MyWeaponType;
    public Transform MyFirePos;
    public Vector3 WeaponCameraAimPoint; // 相机开镜位置
    public float WeaponCameraAimFOV; // 相机开镜 FOV
}

public enum WeaponType {
    MainWeapon,
    SideWeapon,
}