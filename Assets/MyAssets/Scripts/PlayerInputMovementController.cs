using System;
using UnityEngine;

public class PlayerInputMovementController : MonoBehaviour {
    [Header("玩家实体")] public Transform PlayerTr;
    [Header("动画跑步速度")] [Range(1, 3)] public float RunRatio;
    [Header("控制器前后走路速度")] public float MoveVerticalSpeed;
    [Header("控制器前后跑步速度")] public float RunVerticalSpeed;
    [Header("控制器左右跑步速度")] public float MoveHorizontalSpeed;
    [Header("重力加速度")] public float GravityAccelerate;
    [Header("底部重力球偏移量")] public float BottomGravityCapsuleOff;
    [Header("重力检测地面层级")] public LayerMask GravityDetectMaskLayer;
    [Header("角色控制器")] public CharacterController controller;
    [Header("平滑速度")] public float LerpSpeed;
    [NonSerialized] public Transform FollowTargetTr;

    private bool isGround;
    private Collider[] colliders;
    private readonly Vector3 gravityDir = Vector3.down;
    private float gravityPassTime = 0f;
    private Vector3 motionVector;
    private float horizontal;
    private float vertical;
    private float targetHorizontal;
    private float targetVertical;
    private Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        // rotation
        PlayerTr.rotation = Quaternion.Euler(PlayerTr.eulerAngles.x, FollowTargetTr.eulerAngles.y, PlayerTr.eulerAngles.z);

        // init motion vector
        motionVector = Vector3.zero;
        
        // move
        Vector3 moveVerticalDir = Vector3.ProjectOnPlane(FollowTargetTr.forward, Vector3.up).normalized * CustomInputSystem.GetAxis_Vertical;
        motionVector += moveVerticalDir * (CustomInputSystem.GetKey_LeftShift ? RunVerticalSpeed : MoveVerticalSpeed);
        
        Vector3 moveHorizontalDir = Vector3.ProjectOnPlane(FollowTargetTr.right, Vector3.up).normalized * CustomInputSystem.GetAxis_Horizontal;
        motionVector += moveHorizontalDir * MoveHorizontalSpeed;

        // gravity
        Vector3 bottom = PlayerTr.position + PlayerTr.up * controller.radius + PlayerTr.up * BottomGravityCapsuleOff;
        Vector3 top = PlayerTr.position + PlayerTr.up * controller.height - PlayerTr.up * controller.radius;
        colliders = Physics.OverlapCapsule(bottom, top, controller.radius, GravityDetectMaskLayer);
        isGround = colliders.Length > 0;
        gravityPassTime = !isGround ? gravityPassTime + Time.deltaTime : 0;
        motionVector += isGround ? Vector3.zero : gravityDir * GravityAccelerate * gravityPassTime;
        
        // controller move
        controller.Move(motionVector * Time.deltaTime);

        // animator calculate param
        targetVertical = CustomInputSystem.GetKey_LeftShift ? CustomInputSystem.GetAxis_Vertical * RunRatio : CustomInputSystem.GetAxis_Vertical;
        vertical = Mathf.Abs(vertical - targetVertical) > 0.01f ? Mathf.Lerp(vertical, targetVertical, Time.deltaTime * LerpSpeed) : targetVertical;
        horizontal = CustomInputSystem.GetAxis_Horizontal;
        
        // animator set param
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Color buttom = isGround ? Color.green : Color.red;
        buttom.a = 0.2f;
        Gizmos.color = buttom;
        Gizmos.DrawSphere(PlayerTr.position + PlayerTr.up * controller.radius + PlayerTr.up * BottomGravityCapsuleOff, controller.radius);

        Color top = Color.blue;
        top.a = 0.2f;
        Gizmos.color = top;
        Gizmos.DrawSphere(PlayerTr.position + PlayerTr.up * controller.height - PlayerTr.up * controller.radius, controller.radius);
    }
#endif

}