using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerInputMovementController : MonoBehaviour {
    [Header("玩家实体")] public Transform PlayerTr;
    [Header("虚拟相机")] public CinemachineVirtualCamera VirtualCamera;
    [Header("控制器蹲下走路速度")] public float CrouchVerticalSpeed;
    [Header("控制器前后走路速度")] public float MoveVerticalSpeed;
    [Header("控制器前后跑步速度")] public float RunVerticalSpeed;
    [Header("瞄准状态移动速度系数")] public float AimMoveSpeedRatio;
    [Header("重力加速度")] public float GravityAccelerate;
    [Header("底部重力球偏移量")] public float BottomGravityCapsuleOff;
    [Header("重力检测地面层级")] public LayerMask GravityDetectMaskLayer;
    [Header("角色动画控制器")] public Animator animator;
    [Header("角色控制器")] public CharacterController controller;
    [Header("角色瞄准 IK")] public MultiRotationConstraint aimConstraint;
    [Header("角色瞄准 IK修改速度")] [SerializeField] private float rigLerpSpeed;
    [Header("角色旋转平滑速度")] public float TurnLerpSpeed;
    [Header("瞄准角色旋转平滑速度")] public float AimTurnLerpSpeed;
    [Header("走路 FOV")] public float MoveFOV;
    [Header("蹲走 FOV")] public float CrouchFOV;
    [Header("跑步 FOV")] public float RunFOV;
    [Header("默认 FOV")] public float DefaultFOV;
    [Header("瞄准 FOV")] public float AimFOV;
    [Header("FOV 修改速度")] public float FOVLerpSpeed;
    [Header("瞄准 FOV 修改速度")] public float AimFOVLerpSpeed;

    [SerializeField] private bool isMove;
    [SerializeField] private bool isGround;
    [SerializeField] private bool canJump;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isAim;

    [SerializeField] public bool isAimDebug;
    [Header("跳跃高度")] public float JumpHeight;

    [Header("默认虚拟相机参数")] [SerializeField] private VirtualCameraData defaultVirtualCameraData;
    [Header("瞄准虚拟相机参数")] [SerializeField] private VirtualCameraData aimVirtualCamData;
    [Header("虚拟相机参数修改速度")] [SerializeField] private float virLerpSpeed;

    [NonSerialized] public Transform FollowTargetTr;
    private Collider[] colliders;
    private float startSpeed;
    private float currentSpeed;
    private float horizontal;
    private float vertical;
    private float targetHorizontal;
    private float targetVertical;
    private Vector3 playerVelocity;

    void Start() {
    }

    void Update() {
        float delta = Time.deltaTime;

        isAimDebug = CustomInputSystem.GetKey_H ? !isAimDebug : isAimDebug;
        isAim = isAimDebug || CustomInputSystem.GetMouse_Right;

        // virtual camera
        SetVirtualCamData(isAim ? aimVirtualCamData : defaultVirtualCameraData);

        // aimConstraint weight
        aimConstraint.weight = Mathf.Lerp(aimConstraint.weight, isAim ? 1 : 0, Time.deltaTime * rigLerpSpeed);

        // rotation
        PlayerTr.rotation = isMove || isAim ? Quaternion.Slerp(PlayerTr.rotation, Quaternion.Euler(PlayerTr.eulerAngles.x, FollowTargetTr.eulerAngles.y, PlayerTr.eulerAngles.z), delta * (isAim ? AimTurnLerpSpeed : TurnLerpSpeed)) : PlayerTr.rotation;

        // move
        bool motionDirKey = CustomInputSystem.GetKey_W || CustomInputSystem.GetKey_S || CustomInputSystem.GetKey_A || CustomInputSystem.GetKey_D;
        bool motionCrouchKey = CustomInputSystem.GetKey_LeftCtrl;
        float motionSpeed =  motionDirKey ? (motionCrouchKey ? CrouchVerticalSpeed : (CustomInputSystem.GetKey_LeftShift && !isJump ? RunVerticalSpeed : MoveVerticalSpeed)) : 0;
        playerVelocity.x = motionDirKey ? Vector3.ProjectOnPlane(FollowTargetTr.forward, Vector3.up).x * motionSpeed : 0;
        playerVelocity.z = motionDirKey ? Vector3.ProjectOnPlane(FollowTargetTr.forward, Vector3.up).z * motionSpeed : 0;
        isMove = playerVelocity.x != 0 || playerVelocity.z != 0;
        float fov = isMove ? (CustomInputSystem.GetKey_LeftShift ? RunFOV : motionCrouchKey ? CrouchFOV : MoveFOV) : DefaultFOV;
        
        // aim move
        Vector3 aimMoveDir = (CustomInputSystem.GetKey_W ? PlayerTr.forward : Vector3.zero) + (CustomInputSystem.GetKey_S ? -PlayerTr.forward : Vector3.zero) + (CustomInputSystem.GetKey_A ? -PlayerTr.right : Vector3.zero) + (CustomInputSystem.GetKey_D ? PlayerTr.right : Vector3.zero);

        // motionSpeed = isAim && motionDirKey ? motionSpeed / 2 : motionSpeed;
        playerVelocity.x = isAim && motionDirKey ? Vector3.ProjectOnPlane(aimMoveDir, Vector3.up).x * motionSpeed : playerVelocity.x;
        playerVelocity.z = isAim && motionDirKey ? Vector3.ProjectOnPlane(aimMoveDir, Vector3.up).z * motionSpeed : playerVelocity.z;

        // aim
        fov = isAim ? AimFOV : fov;
        float lerpSpeed = isAim ? AimFOVLerpSpeed : FOVLerpSpeed;
        Cursor.visible = !isAim; 
        VirtualCamera.m_Lens.FieldOfView = Mathf.Abs(fov - VirtualCamera.m_Lens.FieldOfView) < 0.01f ? fov : Mathf.Lerp(VirtualCamera.m_Lens.FieldOfView, fov, Time.deltaTime * lerpSpeed);

        // gravity
        Vector3 bottom = PlayerTr.position + PlayerTr.up * controller.radius + PlayerTr.up * BottomGravityCapsuleOff;
        Vector3 top = PlayerTr.position + PlayerTr.up * controller.height - PlayerTr.up * controller.radius;
        colliders = Physics.OverlapCapsule(bottom, top, controller.radius, GravityDetectMaskLayer);
        isGround = colliders.Length > 0;

        // jump
        if (canJump) {
            // 到达地面 速度置空
            if (isGround && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
                isJump = false;
                animator.SetBool("IsJump", isJump);
            }

            // 按下跳跃键 且在地面 垂直速度赋值
            if (isGround && CustomInputSystem.GetKeyDown_Space) {
                playerVelocity.y += Mathf.Sqrt(JumpHeight * -2.0f * GravityAccelerate);
                animator.SetTrigger("Jump");
                isJump = true;
                animator.SetBool("IsJump", isJump);
            }
        }

        // 不在地面且有速度
        playerVelocity.y += playerVelocity.y == 0 && isGround ? 0 : GravityAccelerate * delta;
        controller.Move(playerVelocity * delta);

        // animator calculate param
        vertical = CustomInputSystem.GetAxis_Vertical;
        horizontal = CustomInputSystem.GetAxis_Horizontal;

        // animator set param
        animator.SetFloat("Horizontal", isAim ? horizontal * AimMoveSpeedRatio : horizontal);
        animator.SetFloat("Vertical", isAim ? vertical * AimMoveSpeedRatio : vertical);
        animator.SetBool("IsAim", isAim);
        animator.SetBool("IsCrouch", motionCrouchKey);
        animator.SetFloat("MotionSpeed", motionSpeed);
    }

    void SetVirtualCamData(VirtualCameraData data) {
        Cinemachine3rdPersonFollow cine = VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        cine.CameraDistance = Mathf.Lerp(cine.CameraDistance, data.CameraDistance, Time.deltaTime * virLerpSpeed);
        cine.ShoulderOffset = Vector3.Lerp(cine.ShoulderOffset, data.ShoulderOffSet, Time.deltaTime * virLerpSpeed);
        cine.VerticalArmLength = Mathf.Lerp(cine.VerticalArmLength, data.VerticalArmLength, Time.deltaTime * virLerpSpeed);
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

[Serializable]
public struct VirtualCameraData {
    public float CameraDistance;
    public float VerticalArmLength;
    public Vector3 ShoulderOffSet;
}