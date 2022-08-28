using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOTps")]
public class SOTps : ScriptableObject {
    public Texture2D AimCrosshair, ShootCrosshair;
    public Material BulletHole;
    public AudioClip[] stepClips;
}