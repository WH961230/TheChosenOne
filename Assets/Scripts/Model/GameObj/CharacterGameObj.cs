using System.Net;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

public class CharacterGameObj : GameObj {
    private float moveSpeed = 0.0f; // 移动速度
    private float yRotate = 0.0f; // 视角参数
    private float jumpTimer = -1; // 跳跃参数
    private bool isStartGravity = false; // 延迟一帧执行重力，否则初始化位置有问题 会被拉回到原点坐标
    private bool isAim = false;
    private bool isLerpCharacterCameraFOV = false;
    private bool isLerpWeaponCameraFOV = false;
    private Vector3 moveVector = Vector3.zero; // 移动向量
    private CharacterComponent characterComponent;

    private InputSystem inputSystem {
        get { return game.MyGameSystem.MyInputSystem; }
    }

    private CharacterData characterData;
    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        characterData = (CharacterData) data;
        characterComponent = (CharacterComponent) MyComponent;
        characterComponent.Body.transform.localPosition = MyData.MyTranInfo.MyPos;
        characterComponent.Body.transform.localRotation = MyData.MyTranInfo.MyRot;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        CharacterMotion();
        CharacterAnimation();
        CharacterWeaponAction();
    }

    private float weaponCameraFOVTarget;
    private float characterCameraFOVTarget;
    private void CharacterWeaponAction() {
        if (MyGame.MyGameSystem.MyInputSystem.GetMouseButtonDown(1)) {
            Aim();
        }

        if (MyGame.MyGameSystem.MyInputSystem.GetMouseButtonDown(0)) {
            Fire();
        }

        if (isLerpCharacterCameraFOV) {
            var characterCameraComponent = MyGame.MyGameSystem.MyCameraSystem.GetCameraComponent(characterData.CameraInstanceId);
            characterCameraComponent.MyCamera.fieldOfView = Mathf.Lerp(characterCameraComponent.MyCamera.fieldOfView, characterCameraFOVTarget, Time.deltaTime * 5); 
        }

        if (isLerpWeaponCameraFOV) {
            var weaponCameraComponent = MyGame.MyGameSystem.MyCameraSystem.GetWeaponCameraComponent();
            weaponCameraComponent.MyCamera.fieldOfView = Mathf.Lerp(weaponCameraComponent.MyCamera.fieldOfView, weaponCameraFOVTarget, Time.deltaTime * 5);
        }
    }

    private void Fire() {
        var curWeapId = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterData.BackpackInstanceId).GetCurWeaponId();
        var curWeapNull = curWeapId == 0;
        if (curWeapNull) {
            return;
        }

        var weaponData = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(curWeapId);
        //加载子弹
        MyGame.MyGameSystem.MyEffectSystem.InstanceEffect(weaponData.MyFirePos.position, weaponData.MyFirePos.rotation);
        EditorApplication.isPaused = true;
    }

    private void Aim() {
        // 获取当前武器
        // var curWeapId = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterData.BackpackInstanceId).GetCurWeaponId();
        var curWeapId = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterData.BackpackInstanceId)
            .GetCurWeaponId();
        if (curWeapId == 0) {
            return;
        }

        var curWeapGameObj = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(curWeapId);
        var curWeapComponent = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponComponent(curWeapId);
        var weaponCameraGameObj = MyGame.MyGameSystem.MyCameraSystem.GetWeaponCameraGameObj();

        if (isAim) {
            // 开启角色模型
            SetCharacterMeshActive(true);

            // 调整 CharacterCamera FOV 为 配置【相机默认】 FOV
            characterCameraFOVTarget = SOData.MySOCameraSetting.CharacterCameraDefaultFOV;
            isLerpCharacterCameraFOV = true;

            // 隐藏开镜武器模型
            curWeapGameObj.MyData.MyObj.SetActive(false);

            // 隐藏 WeaponCamera
            weaponCameraGameObj.MyData.MyObj.SetActive(false);
            isAim = false;
        } else {
            // 隐藏角色模型
            SetCharacterMeshActive(false);

            // 调整 CharacterCamera 【开镜 FOV】
            characterCameraFOVTarget = SOData.MySOCameraSetting.CharacterCameraAimFOV;
            isLerpCharacterCameraFOV = true;

            // 显示开镜武器模型 setActive 并至【开镜配置位置】
            curWeapGameObj.MyData.MyObj.SetActive(true);
            curWeapGameObj.MyData.MyObj.transform.position = SOData.MySOWeaponSetting.WeaponAimModelPoint;

            // 显示 WeaponCamera
            weaponCameraGameObj.MyData.MyObj.SetActive(true);
            weaponCameraGameObj.MyData.MyObj.transform.position = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(curWeapId).WeaponCameraAimPoint;

            // 调整 WeaponCamera 【开镜 FOV】 并至【开镜位置】
            weaponCameraFOVTarget = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponData(curWeapId).WeaponCameraAimFOV;
            isLerpWeaponCameraFOV = true;

            // 设置武器开镜旋转
            curWeapComponent.MyWeaponRotation.SetTargetRotation();
            curWeapComponent.MyWeaponPosition.SetTargetPosition();

            isAim = true;
        }
    }

    private void SetCharacterMeshActive(bool isShow) {
        characterComponent.CharacterSkinMeshRenderer.enabled = isShow;
        foreach (var temp in characterComponent.CharacterMeshRenderers) {
            temp.enabled = isShow;
        }
    }

    // 设置武器模型
    public void SetHoldWeaponModel(string weaponSign) {
        foreach (var curWeap in characterComponent.MyHoldWeapons) {
            if (curWeap.name.Equals(weaponSign)) {
                curWeap.SetActive(true);
            } else {
                curWeap.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 装载当前武器 到角色武器挂点
    /// </summary>
    public void InstallCurWeapon(int weapId) {
        if (weapId == 0) {
            return;
        }

        var weapGameObj = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(weapId);
        var weapComp = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponComponent(weapId);
        var weaponPlace = characterComponent.MyWeaponPlace;
        weapGameObj.SetWeaponPlace(weaponPlace, weapComp.MyCharacterHandlePos, weapComp.MyCharacterHandleRot);
    }

    public void UnInstallCurWeapon(int weapId) {
        if (weapId == 0) {
            return;
        }

        var weapGameObj = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(weapId);
        var weapComp = MyGame.MyGameSystem.MyWeaponSystem.GetWeaponComponent(weapId);
        weapGameObj.SetWeaponPlace(GameData.WeaponRoot, weapComp.MyCharacterHandlePos, weapComp.MyCharacterHandleRot);
    }

    private void CharacterAnimation() {
        // 当前武器不为空 播放手持武器动画 weapon 1 手枪 weapon 2 自动步枪
        bool hasCurWeap = MyGame.MyGameSystem.MyBackpackSystem.GetBackpackEntity(characterData.BackpackInstanceId).GetCurWeaponType(out WeaponType type);
        if (hasCurWeap) {
            if (type == WeaponType.MainWeapon) {
                characterComponent.AnimatorController.SetInteger("Weapon", 2);
            } else if(type == WeaponType.SideWeapon) {
                characterComponent.AnimatorController.SetInteger("Weapon", 1);
            }
            characterComponent.AnimatorController.SetBool("IsWeapon", true);
        } else {
            characterComponent.AnimatorController.SetBool("IsWeapon", false);
        }
    }

    private void CharacterMotion() {
        Vector3 vec = Vector3.zero;
        if (characterData.IsMainCharacter) {
            var pressAltDown = inputSystem.GetKey(KeyCode.LeftAlt) || inputSystem.GetKey(KeyCode.RightAlt);
            yRotate += inputSystem.GetAxis("Mouse X");
            if (!pressAltDown) {
                characterComponent.Body.transform.rotation = Quaternion.Euler(0, yRotate, 0);
            }

            Vector3 dir = Vector3.zero;

            var h = characterComponent.AnimatorController.GetFloat("Horizontal");
            var v = characterComponent.AnimatorController.GetFloat("Vertical");
            var speed = 10;
            if (inputSystem.GetKey(KeyCode.W)) {
                dir = characterComponent.Body.transform.forward;
                if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                    vec = dir * SOData.MySOCharacter.MyMoveInfo.RunSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 2, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Vertical", v);
                } else {
                    // 移动速度
                    vec = dir * SOData.MySOCharacter.MyMoveInfo.WalkSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 1, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Vertical", v);
                }
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.S)) {
                dir = -characterComponent.Body.transform.forward;
                vec = dir * SOData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, -1, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.A)) {
                dir = -characterComponent.Body.transform.right;
                vec = dir * SOData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, -1, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.D)) {
                dir = characterComponent.Body.transform.right;
                vec = dir * SOData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, 1, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else {
                h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", false);
            }
        }

        // 重力
        if (isStartGravity) {
            if (!characterComponent.CC.isGrounded) {
                vec.y = -SOData.MySOEnvironmentSetting.GravitySpeed;
            } else {
                vec.y = 0;
            }
        }

        // 控制器移动
        if (vec != Vector3.zero) {
            characterComponent.CC.Move(vec * Time.deltaTime);
        }

        isStartGravity = true;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    private void MoveBehaviour() {
        if (!characterData.IsMainCharacter) {
            return;
        }

        var inputSystem = game.MyGameSystem.MyInputSystem;
        // 获取不同的移动方向
        Vector3 dir = Vector3.zero;
        if (inputSystem.GetKey(KeyCode.W)) {
            dir = characterComponent.Body.transform.forward;
        } else if (inputSystem.GetKey(KeyCode.S)) {
            dir = -characterComponent.Body.transform.forward;
        } else if (inputSystem.GetKey(KeyCode.A)) {
            dir = -characterComponent.Body.transform.right;
        } else if (inputSystem.GetKey(KeyCode.D)) {
            dir = characterComponent.Body.transform.right;
        } else {
            characterComponent.AnimatorController.SetFloat("Move", 0);
        }

        // 未按下移动键
        if (dir == Vector3.zero) {
            return;
        }

        // 跳跃和降落获取空中移动速度
        var moveSpeed = 0f;
        if (characterData.IsJumping || characterData.IsLanding) {
            moveSpeed = SOData.MySOCharacter.MyMoveInfo.AirWalkSpeed;
        } else {
            if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                moveSpeed = SOData.MySOCharacter.MyMoveInfo.RunSpeed;
                characterComponent.AnimatorController.SetFloat("Move", 2);
            } else {
                moveSpeed = SOData.MySOCharacter.MyMoveInfo.WalkSpeed;
                characterComponent.AnimatorController.SetFloat("Move", 1);
            }
        }

        moveVector += dir * moveSpeed * Time.deltaTime;
    }

    private Vector3 JumpBehaviour(Vector3 moveVector) {
        // 计时中
        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
            characterData.IsJumping = true;
            moveVector += MyObj.transform.up * SOData.MySOCharacter.MyMoveInfo.JumpSpeed * Time.deltaTime;
            return moveVector;
        }

        // 降落中
        if (characterData.IsLanding) {
            moveVector -= MyObj.transform.up * SOData.MySOEnvironmentSetting.GravitySpeed * Time.deltaTime;
            // 落地降落停止
            if (characterComponent.CC.isGrounded) {
                characterData.IsLanding = false;
                jumpTimer = -1;
            }

            return moveVector;
        } else if (characterData.IsJumping || (!characterData.IsJumping && !characterComponent.CC.isGrounded)) {
            jumpTimer = 0;
            characterData.IsJumping = false;
            characterData.IsLanding = true;
        }

        // 按下跳跃键
        if (game.MyGameSystem.MyInputSystem.GetKeyDown(KeyCode.Space)) {
            if (characterData.IsMainCharacter) {
                jumpTimer = SOData.MySOCharacter.MyMoveInfo.JumpContinueTime;
            }
        }

        return moveVector;
    }
}