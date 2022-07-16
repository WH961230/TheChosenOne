using System;
using UnityEngine;

public class CharacterComponent : MonoBehaviour {
    public GameObject Head;
    public GameObject Body;
    public CharacterController CC;
    public Animator AnimatorController;

    [Header("武器模型")] 
    public WeaponInfo AKC;
    public WeaponInfo AK47;
    public WeaponInfo Pistol;
}

[Serializable]
public struct WeaponInfo {
    public GameObject WeaponGameObj;
    public int WeaponId;
}