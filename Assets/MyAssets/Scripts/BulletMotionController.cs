using UnityEngine;

public class BulletMotionController : MonoBehaviour {
    private Vector3 startOffVec;
    // 初始化
    public void Init(Vector3 startOffVec) {
        this.startOffVec = startOffVec;
    }

    private void FixedUpdate() {
        transform.localPosition += startOffVec * Time.fixedDeltaTime;
    }
}