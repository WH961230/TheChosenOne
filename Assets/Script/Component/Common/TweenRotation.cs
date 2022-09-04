using System;
using UnityEngine;

public class TweenRotation : MonoBehaviour {
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public Vector3 defaultRotation;
    public float speed;
    public bool isLerp;

    private void Update() {
        if (isLerp) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * speed);
        }
    }

    public void SetTargetRotation() {
        transform.eulerAngles = defaultRotation;
        isLerp = true;
    }
}