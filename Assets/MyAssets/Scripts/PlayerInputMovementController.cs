using System;
using FIMSpace.FProceduralAnimation;
using UnityEngine;

public class PlayerInputMovementController : MonoBehaviour {
    [Header("玩家实体")] public Transform PlayerTr;
    [Header("控制器蹲下走路速度")] public float CrouchVerticalSpeed;
    [Header("控制器前后走路速度")] public float MoveVerticalSpeed;
    [Header("控制器前后跑步速度")] public float RunVerticalSpeed;
    [Header("重力加速度")] public float GravityAccelerate;
    [Header("底部重力球偏移量")] public float BottomGravityCapsuleOff;
    [Header("重力检测地面层级")] public LayerMask GravityDetectMaskLayer;
    [Header("角色动画控制器")] public Animator animator;
    [Header("角色控制器")] public CharacterController controller;
    [Header("角色旋转平滑速度")] public float TurnLerpSpeed;
    [SerializeField] private bool isMove;
    [SerializeField] private bool isGround;

    [NonSerialized] public Transform FollowTargetTr;
    private Collider[] colliders;
    private readonly Vector3 gravityDir = Vector3.down;
    private float gravityPassTime = 0f;
    private float horizontal;
    private float vertical;
    private float targetHorizontal;
    private float targetVertical;

    void Start() {
    }

    void Update() {
        // rotation
        PlayerTr.rotation = isMove ? Quaternion.Lerp(PlayerTr.rotation, Quaternion.Euler(PlayerTr.eulerAngles.x, FollowTargetTr.eulerAngles.y, PlayerTr.eulerAngles.z), Time.deltaTime * TurnLerpSpeed) : PlayerTr.rotation;

        // move
        bool motionDirKey = CustomInputSystem.GetKey_W || CustomInputSystem.GetKey_S || CustomInputSystem.GetKey_A || CustomInputSystem.GetKey_D;
        bool motionCrouchKey = CustomInputSystem.GetKey_LeftCtrl;
        float motionSpeed = motionDirKey ? (motionCrouchKey ? /*下蹲*/ CrouchVerticalSpeed : /*直立*/ (CustomInputSystem.GetKey_LeftShift ? RunVerticalSpeed : MoveVerticalSpeed)) : 0;
        Vector3 motionVector = motionDirKey ? (PlayerTr.forward * motionSpeed) : Vector3.zero;
        isMove = motionVector != Vector3.zero;

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
        vertical = CustomInputSystem.GetAxis_Vertical;
        horizontal = CustomInputSystem.GetAxis_Horizontal;

        // animator set param
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Crouch", motionCrouchKey);
        animator.SetFloat("MotionSpeed", motionSpeed);
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