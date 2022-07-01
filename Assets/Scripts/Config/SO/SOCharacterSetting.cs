﻿using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCharacterSetting")]
public class SOCharacterSetting : ScriptableObject {
    public float MoveSpeed;
    public float JumpSpeed;
    public float JumpContinueTime;
    public GameObject CharacterCamera;
    public GameObject CharacterPrefab;
}