using System;
using Cinemachine;
using FIMSpace.FProceduralAnimation;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerInputMovementController : MonoBehaviour {
    [Header("组件")]
    [Tooltip("玩家实体")] public Transform PlayerTr;
    [Tooltip("虚拟相机")] public CinemachineVirtualCamera VirtualCamera;
    [Tooltip("重力检测地面层级")] public LayerMask GravityDetectMaskLayer;
    [Tooltip("角色动画控制器")] public Animator animator;
    [Tooltip("腿动画状态机")] public LegsAnimator legs;
    [Tooltip("角色控制器")] public CharacterController controller;
    [Tooltip("弹药")] public GameObject BulletPrefab;
    [Tooltip("枪口")] public Transform WeaponMuzzleTr;
    [Tooltip("角色朝向物体")] public PlayerInputLookAtController PlayerInputLookAtController;
    private Recoil recoil;

    [Header("速度")]
    [Tooltip("控制器蹲下走路速度")] public float CrouchVerticalSpeed;
    [Tooltip("控制器前后走路速度")] public float MoveVerticalSpeed;
    [Tooltip("控制器前后跑步速度")] public float RunVerticalSpeed;
    [Tooltip("瞄准状态移动速度系数")] public float AimMoveSpeedRatio;
    [Tooltip("重力加速度")] public float GravityAccelerate;
    [Tooltip("底部重力球偏移量")] public float BottomGravityCapsuleOff;
    [Tooltip("角色旋转平滑速度")] public float TurnLerpSpeed;
    [Tooltip("瞄准角色旋转平滑速度")] public float AimTurnLerpSpeed;
    [Tooltip("子弹射击间隔")] public float FireInterval;
    [Tooltip("子弹开始射击延迟")] [SerializeField] public float FireDelay;
    [Tooltip("子弹检测模式")] public CollisionDetectionMode CollisionDetectionMode;

    [Header("FOV")]
    [Tooltip("走路 FOV")] public float MoveFOV;
    [Tooltip("蹲走 FOV")] public float CrouchFOV;
    [Tooltip("跑步 FOV")] public float RunFOV;
    [Tooltip("默认 FOV")] public float DefaultFOV;
    [Tooltip("瞄准 FOV")] public float AimFOV;
    [Tooltip("FOV 修改速度")] public float FOVLerpSpeed;
    [Tooltip("瞄准 FOV 修改速度")] public float AimFOVLerpSpeed;

    public AimController AimController;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float magnitude;

    [Header("Debug 查看 不可修改")]
    [SerializeField] private bool isMove;
    [SerializeField] private bool isGround;
    [SerializeField] private bool canJump;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isAim;
    [SerializeField] private bool isFire;
    [SerializeField] public bool isAimDebug;

    [Header("跳跃高度")] public float JumpHeight;

    [Header("虚拟相机参数")]
    [Tooltip("默认虚拟相机参数")] [SerializeField] private VirtualCameraData defaultVirtualCameraData;
    [Tooltip("瞄准虚拟相机参数")] [SerializeField] private VirtualCameraData aimVirtualCamData;
    [Tooltip("虚拟相机参数修改速度")] [SerializeField] private float virLerpSpeed;

    [NonSerialized] public Transform FollowTargetTr;
    private Collider[] colliders;
    private bool motionDirKey => CustomInputSystem.GetKey_W || CustomInputSystem.GetKey_S || CustomInputSystem.GetKey_A || CustomInputSystem.GetKey_D;
    private bool motionCrouchKey => CustomInputSystem.GetKey_LeftCtrl;
    
    private float startSpeed;
    private float currentSpeed;

    private float targetHorizontal;
    private float targetVertical;
    private Vector3 playerVelocity;
    private float motionSpeed;

    private float horizontal => CustomInputSystem.GetAxis_Horizontal;
    private float vertical => CustomInputSystem.GetAxis_Vertical;
    
    private float FireIntervalDeploy;
    private float FireDelayDeploy;

    void Start() {
        recoil = GetComponentInChildren<Recoil>();
        legs = GetComponentInChildren<LegsAnimator>();
        AimController = GetComponentInChildren<AimController>();
    }

    void FixedUpdate() {
        FirePlay();
    }

    void Update() {
        float deltaTime = Time.deltaTime;
        GetInput();
        SetVirtualCamData();
        MoveAndRotate(deltaTime);
        legs.LegsAnimatorBlend = isMove ? 0 : 1;
        Aim();
        Fire();
        Gravity(deltaTime);
        Jump();
        controller.Move(playerVelocity * deltaTime);
        AnimSet();
    }

    void FirePlay() {
        if (isFire) {
            if (FireDelayDeploy > 0) {
                FireDelayDeploy -= Time.fixedDeltaTime;
            } else {
                FireDelayDeploy = 0;
                if (FireIntervalDeploy > FireInterval) {
                    Instantiate(BulletPrefab, WeaponMuzzleTr.position, WeaponMuzzleTr.rotation);
                    FireIntervalDeploy = 0;
                    recoil.Fire(magnitude);
                    PlayerInputLookAtController.CameraShake();
                } else {
                    FireIntervalDeploy += Time.fixedDeltaTime;
                }
            }
        }
    }

    void GetInput() {
        isAimDebug = CustomInputSystem.GetKey_H ? !isAimDebug : isAimDebug;
        isAim = isAimDebug || CustomInputSystem.GetMouse_Right;
    }

    void MoveAndRotate(float delta) {
        // rotate
        if (isAim || isMove) {
            Quaternion qua = Quaternion.Euler(PlayerTr.eulerAngles.x, FollowTargetTr.eulerAngles.y, PlayerTr.eulerAngles.z);
            float quaLerpSpeed = isAim ? AimTurnLerpSpeed : TurnLerpSpeed;
            PlayerTr.rotation = Quaternion.Slerp(PlayerTr.rotation, qua, delta * quaLerpSpeed);
        }

        // move
        if (motionDirKey) {
            if (motionCrouchKey) {
                motionSpeed = CrouchVerticalSpeed;
            } else if (CustomInputSystem.GetKey_LeftShift && !isJump) {
                motionSpeed = RunVerticalSpeed;
            } else {
                motionSpeed = MoveVerticalSpeed;
            }
        } else {
            motionSpeed = 0;
        }

        playerVelocity.x = motionDirKey ? Vector3.ProjectOnPlane(FollowTargetTr.forward, Vector3.up).x * motionSpeed : 0;
        playerVelocity.z = motionDirKey ? Vector3.ProjectOnPlane(FollowTargetTr.forward, Vector3.up).z * motionSpeed : 0;
        isMove = playerVelocity.x != 0 || playerVelocity.z != 0;

        Vector3 aimMoveDir = (CustomInputSystem.GetKey_W ? PlayerTr.forward : Vector3.zero) + (CustomInputSystem.GetKey_S ? -PlayerTr.forward : Vector3.zero) + (CustomInputSystem.GetKey_A ? -PlayerTr.right : Vector3.zero) + (CustomInputSystem.GetKey_D ? PlayerTr.right : Vector3.zero);

        playerVelocity.x = isAim && motionDirKey ? Vector3.ProjectOnPlane(aimMoveDir, Vector3.up).x * motionSpeed * AimMoveSpeedRatio : playerVelocity.x;
        playerVelocity.z = isAim && motionDirKey ? Vector3.ProjectOnPlane(aimMoveDir, Vector3.up).z * motionSpeed * AimMoveSpeedRatio : playerVelocity.z;
    }

    void Aim() {
        float fov = isMove ? (CustomInputSystem.GetKey_LeftShift ? RunFOV : motionCrouchKey ? CrouchFOV : MoveFOV) : DefaultFOV;
        fov = isAim ? AimFOV : fov;
        float lerpSpeed = isAim ? AimFOVLerpSpeed : FOVLerpSpeed;
        Cursor.visible = !isAim;
        Cursor.lockState = CursorLockMode.Locked;
        AimController.target = isAim ? aimTarget : null;
        VirtualCamera.m_Lens.FieldOfView = Mathf.Abs(fov - VirtualCamera.m_Lens.FieldOfView) < 0.01f ? fov : Mathf.Lerp(VirtualCamera.m_Lens.FieldOfView, fov, Time.deltaTime * lerpSpeed);
    }

    void Fire() {
        if (isAim) {
            if (CustomInputSystem.GetMouse_Left && !isFire) {
                isFire = true;
                FireDelayDeploy = FireDelay;
            }

            if (CustomInputSystem.GetMouseUp_Left && isFire) {
                isFire = false;
            }
        } else {
            isFire = false;
        }
    }

    void Gravity(float delta) {
        Vector3 bottom = PlayerTr.position + PlayerTr.up * controller.radius + PlayerTr.up * BottomGravityCapsuleOff;
        Vector3 top = PlayerTr.position + PlayerTr.up * controller.height - PlayerTr.up * controller.radius;
        colliders = Physics.OverlapCapsule(bottom, top, controller.radius, GravityDetectMaskLayer);
        isGround = colliders.Length > 0;
        playerVelocity.y += playerVelocity.y == 0 && isGround ? 0 : GravityAccelerate * delta;
    }

    void Jump() {
        if (canJump) {
            if (isGround && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
                isJump = false;
                animator.SetBool("IsJump", isJump);
            }

            if (isGround && CustomInputSystem.GetKeyDown_Space) {
                playerVelocity.y += Mathf.Sqrt(JumpHeight * -2.0f * GravityAccelerate);
                animator.SetTrigger("Jump");
                isJump = true;
                animator.SetBool("IsJump", isJump);
            }
        }
    }

    void AnimSet() {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsAim", isAim);
        animator.SetBool("IsCrouch", motionCrouchKey);
        animator.SetFloat("MotionSpeed", motionSpeed);
    }

    void SetVirtualCamData() {
        VirtualCameraData data = isAim ? aimVirtualCamData : defaultVirtualCameraData;
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
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(WeaponMuzzleTr.position, WeaponMuzzleTr.position + WeaponMuzzleTr.forward * 5);
    }
#endif

}

[Serializable]
public struct VirtualCameraData {
    public float CameraDistance;
    public float VerticalArmLength;
    public Vector3 ShoulderOffSet;
}