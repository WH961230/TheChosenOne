using UnityEngine;

public class TweenPosition : MonoBehaviour {
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public Vector3 defaultPosition;
    public float speed;
    public bool isLerp;

    private void Update() {
        if (isLerp) {
            transform.position = Vector3.Lerp(transform.position, SoData.MySOWeaponSetting.WeaponAimModelPoint, Time.deltaTime * speed);
        }
    }

    public void SetTargetPosition() {
        transform.position = defaultPosition;
        isLerp = true;
    }
}