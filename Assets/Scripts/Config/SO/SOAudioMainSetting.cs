using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOAudioMainSetting")]
public class SOAudioMainSetting : ScriptableObject {
    public GameObject AudioMain;
    public AudioClip BackMusic; // 背景音效
    public AudioClip PlaneMusic; // 飞机音效
}