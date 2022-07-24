using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCharacterSetting")]
public class SOCharacterSetting : ScriptableObject {
    public MoveInfo MyMoveInfo;
    public MoveSwitch MyMoveSwitch;
    public GameObject MyPrefab;
    public CharacterInfo MyCharacterInfo;

    public GameObject GetCharacterPrefab (){
        return MyPrefab;
    }
}

[Serializable]
public struct MoveSwitch {
    public bool IsOpenWalk;
    public bool IsOpenJump;
    public bool IsOpenRun;
    public bool IsOpenAirWalk;
}

[Serializable]
public struct MoveInfo {
    public float WalkSpeed; // 地面移动速度
    public float AirWalkSpeed; // 空中移动速度
    public float RunSpeed; // 跑步移动速度
    public float JumpSpeed;
    public float JumpContinueTime;
}

[Serializable]
public struct CharacterInfo {
    public Vector3 MyCharacterPoint;
    public Quaternion MyCharacterQuaternion;
}