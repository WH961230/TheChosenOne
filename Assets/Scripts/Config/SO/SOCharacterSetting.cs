using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOCharacterSetting")]
public class SOCharacterSetting : ScriptableObject {
    public float MoveSpeed;
    public float JumpSpeed;
    public GameObject CharacterPrefab;
}