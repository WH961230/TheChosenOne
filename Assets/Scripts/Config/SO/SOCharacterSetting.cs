using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCharacterSetting")]
public class SOCharacterSetting : ScriptableObject {
    public float MoveSpeed;
    public float JumpSpeed;
    public float JumpContinueTime;
    public GameObject CharacterPrefab;
    public CharacterInfo CharacterInfo;
}

[Serializable]
public struct CharacterInfo {
    public Vector3 MyCharacterPoint;
    public Quaternion MyCharacterQuaternion;
}