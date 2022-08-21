using UnityEditor;
using UnityEngine;

public class EffectComponent : GameComp {
    public float MyEffectScale;
    public bool IsOpen;
    public float FlyTime;
    public float FlySpeed;
    protected void Update() {
        if (IsOpen) {
            transform.position += transform.forward * Time.deltaTime * FlySpeed;
        }
    }
}