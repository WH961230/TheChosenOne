using UnityEngine;

public class PlayerInputMovementController : MonoBehaviour {
    public float runRatio;
    public float lerpSpeed;
    private Animator animator;

    public float Horizontal;
    public float Vertical;

    private float targetHorizontal;
    private float targetVertical;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        bool leftShift = Input.GetKey(KeyCode.LeftShift);

        Horizontal = Input.GetAxis("Horizontal");

        float newVertical = Input.GetAxis("Vertical");
        if (leftShift) {
            targetVertical = newVertical * runRatio;
        } else {
            targetVertical = newVertical;
        }

        Vertical = LerpAnimParam(Vertical, targetVertical, 0.01f, Time.deltaTime * lerpSpeed);

        animator.SetFloat("Horizontal", Horizontal);
        animator.SetFloat("Vertical", Vertical);
    }

    //缓冲参数
    float LerpAnimParam(float origin, float target, float closeOff, float timeDelta) {
        if (Mathf.Abs(origin - target) > closeOff) {
            origin = Mathf.Lerp(origin, target, timeDelta);
        } else {
            origin = target;
        }

        return origin;
    }
}