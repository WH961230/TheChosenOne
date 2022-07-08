using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCharacterSetting")]
public class SOCharacterSetting : ScriptableObject {
    public float GroundMoveSpeed; // 地面移动速度
    public float AirMoveSpeed; // 空中移动速度
    public float RunMoveSpeed; // 跑步移动速度
    public float JumpSpeed;
    public float JumpContinueTime;
    public GameObject CharacterPrefab;
    public CharacterInfo CharacterInfo;
    public GameObject UICharacterPrefab;
}

[Serializable]
public struct CharacterInfo {
    public Vector3 MyCharacterPoint;
    public Quaternion MyCharacterQuaternion;
}