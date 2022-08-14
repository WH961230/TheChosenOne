using UnityEngine;

public class WeaponComponent : GameComp {
    public string MyWeaponSign;
    public WeaponType MyWeaponType;
    public TweenRotation MyWeaponRotation;
    public TweenPosition MyWeaponPosition;
    public Transform MyFirePos;
    public Vector3 MyCharacterHandlePos;
    public Quaternion MyCharacterHandleRot;
}