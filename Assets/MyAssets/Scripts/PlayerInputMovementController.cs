using System;
using UnityEngine;

public class PlayerInputMovementController : MonoBehaviour {
    [Header("玩家实体")] public Transform PlayerTr;
    [Header("跑步速度")] public float RunRatio;
    [Header("平滑速度")] public float LerpSpeed;
    [NonSerialized] public Transform FollowTargetTr;
    private float horizontal;
    private float vertical;
    private float targetHorizontal;
    private float targetVertical;
    private Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        PlayerTr.rotation = Quaternion.Euler(transform.eulerAngles.x, FollowTargetTr.eulerAngles.y, transform.eulerAngles.z);
        targetVertical = CustomInputSystem.GetKey_leftShift ? CustomInputSystem.GetAxis_Vertical * RunRatio : CustomInputSystem.GetAxis_Vertical;
        vertical = Mathf.Abs(vertical - targetVertical) > 0.01f ? Mathf.Lerp(vertical, targetVertical, Time.deltaTime * LerpSpeed) : targetVertical;
        horizontal = CustomInputSystem.GetAxis_Horizontal;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
}