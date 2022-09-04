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
    private CharacterComponent charaComp;

    private InputSystem inputSystem {
        get { return game.MyGameSystem.InputS; }
    }

    private CharacterData characterData;
    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        characterData = (CharacterData) data;
        charaComp = (CharacterComponent) Comp;
        charaComp.Body.transform.localPosition = data.MyTranInfo.MyPos;
        charaComp.Body.transform.localRotation = data.MyTranInfo.MyRot;

        var camData = GS.CameraS.GetEntity(characterData.CameraInstanceId).GetData();
        var camComp = GS.CameraS.GetGO(characterData.CameraInstanceId).GetComp();
        camComp.ThirdPersonOrbitCam.SetPlayer(characterData.MyObj.transform, camComp.MyCamera.transform);
        charaComp.BasicBehaviour.SetCam(camComp.MyCamera.transform, camData.MyObj.transform);
    }

    public override void Update() {
        base.Update();
        //CharacterMotion();
        //CharacterAnimation();
        //CharacterWeaponAction();
    }

    private float weaponCameraFOVTarget;
    private float characterCameraFOVTarget;
    private void CharacterWeaponAction() {
        if (GS.InputS.GetMouseButtonDown(1)) {
            Aim();
        }

        if (GS.InputS.GetMouseButtonDown(0)) {
            Fire();
        }

        if (GS.InputS.GetKeyDown(KeyCode.G)) {
            GS.BackpackS.GetEntity(characterData.BackpackInstanceId).DropMainWeapon(0);
        }

        if (isLerpCharacterCameraFOV) {
            var characterCameraComponent = GS.CameraS.GetGO(characterData.CameraInstanceId).GetComp();
            characterCameraComponent.MyCamera.fieldOfView = Mathf.Lerp(characterCameraComponent.MyCamera.fieldOfView, characterCameraFOVTarget, Time.deltaTime * 5); 
        }

        if (isLerpWeaponCameraFOV) {
            var weaponCameraComponent = GS.CameraS.GetGO(GameData.WeaponCameraId).GetComp();
            weaponCameraComponent.MyCamera.fieldOfView = Mathf.Lerp(weaponCameraComponent.MyCamera.fieldOfView, weaponCameraFOVTarget, Time.deltaTime * 5);
        }
    }

    private void Fire() {
        var curWeapId = GS.BackpackS.GetEntity(characterData.BackpackInstanceId).GetCurWeaponId();
        var curWeapNull = curWeapId == 0;
        if (curWeapNull) {
            return;
        }

        var weaponData = GS.WeapS.GetEntity(curWeapId).GetData();

        //加载子弹
        var id = GS.EffectS.InstanceEffect(weaponData.MyFirePos.position, weaponData.MyFirePos.rotation);
        GS.EffectS.GetEffectGO(id).SetEffect(weaponData.MyFirePos.position + weaponData.MyFirePos.forward * 10);
    }

    private void Aim() {
        // 获取当前武器
        var curWeapId = GS.BackpackS.GetEntity(characterData.BackpackInstanceId).GetCurWeaponId();
        if (curWeapId == 0) {
            return;
        }

        var curWeapGO = GS.WeapS.GetGO(curWeapId);
        var curWeapComp = curWeapGO.GetComp();
        var curWeapData = GS.WeapS.GetEntity(curWeapId).GetData();
        var weapCamGO = GS.CameraS.GetGO(GameData.WeaponCameraId);

        if (isAim) {
            // 开启角色模型
            SetCharacterMeshActive(true);

            // 调整 CharacterCamera FOV 为 配置【相机默认】 FOV
            characterCameraFOVTarget = SoData.MySOCameraSetting.CharacterCameraDefaultFOV;
            isLerpCharacterCameraFOV = true;

            curWeapGO.Hide();// 隐藏开镜武器模型
            weapCamGO.Hide();// 隐藏 WeaponCamera

            isAim = false;
        } else {
            // 隐藏角色模型
            SetCharacterMeshActive(false);

            // 调整 CharacterCamera 【开镜 FOV】
            characterCameraFOVTarget = SoData.MySOCameraSetting.CharacterCameraAimFOV;
            isLerpCharacterCameraFOV = true;

            // 显示开镜武器模型 setActive 并至【开镜配置位置】
            curWeapGO.Display();
            curWeapGO.SetPos(SoData.MySOWeaponSetting.WeaponAimModelPoint);

            // 显示 WeaponCamera
            weapCamGO.Display();
            weapCamGO.SetPos(curWeapData.WeapParamInfo.WeaponCameraAimPoint);

            // 调整 WeaponCamera 【开镜 FOV】 并至【开镜位置】
            weaponCameraFOVTarget = curWeapData.WeapParamInfo.WeaponCameraAimFOV;
            isLerpWeaponCameraFOV = true;

            // 设置武器开镜旋转
            curWeapComp.MyWeaponRotation.SetTargetRotation();
            curWeapComp.MyWeaponPosition.SetTargetPosition();

            isAim = true;
        }
    }

    private void SetCharacterMeshActive(bool isShow) {
        charaComp.CharacterSkinMeshRenderer.enabled = isShow;
        foreach (var temp in charaComp.CharacterMeshRenderers) {
            temp.enabled = isShow;
        }
    }

    /// <summary>
    /// 装载当前武器 到角色武器挂点
    /// </summary>
    public void InstallCurWeapon(int weapId) {
        if (weapId == 0) {
            return;
        }

        var weapGameObj = GS.WeapS.GetGO(weapId);
        var weapData = GS.WeapS.GetEntity(weapId).GetData();
        var weapRoot = charaComp.MyWeaponPlace;
        weapGameObj.SetChild(weapRoot, weapData.WeapParamInfo.CharaWeapPot, weapData.WeapParamInfo.CharaWeapRot, true, true);
    }

    public void UninstallCurWeapon(int weapId) {
        if (weapId == 0) {
            return;
        }

        var weapGameObj = GS.WeapS.GetGO(weapId);
        var weapData = GS.WeapS.GetEntity(weapId).GetData();
        weapGameObj.SetChild(GameData.WeaponRoot, weapData.WeapParamInfo.CharaWeapPot, weapData.WeapParamInfo.CharaWeapRot, true, true);
    }

    private void CharacterAnimation() {
        // 当前武器不为空 播放手持武器动画 weapon 1 手枪 weapon 2 自动步枪
        bool hasCurWeap = GS.BackpackS.GetEntity(characterData.BackpackInstanceId).GetCurWeaponType(out WeaponType type);
        if (hasCurWeap) {
            if (type == WeaponType.MainWeapon) {
                charaComp.AnimatorController.SetInteger("Weapon", 2);
            } else if(type == WeaponType.SideWeapon) {
                charaComp.AnimatorController.SetInteger("Weapon", 1);
            }
            charaComp.AnimatorController.SetBool("IsWeapon", true);
        } else {
            charaComp.AnimatorController.SetBool("IsWeapon", false);
        }
    }

    private void CharacterMotion() {
        Vector3 vec = Vector3.zero;
        if (characterData.IsMainCharacter) {
            var pressAltDown = inputSystem.GetKey(KeyCode.LeftAlt) || inputSystem.GetKey(KeyCode.RightAlt);
            yRotate += inputSystem.GetAxis("Mouse X");
            if (!pressAltDown) {
                charaComp.Body.transform.rotation = Quaternion.Euler(0, yRotate, 0);
            }

            Vector3 dir = Vector3.zero;

            var h = charaComp.AnimatorController.GetFloat("Horizontal");
            var v = charaComp.AnimatorController.GetFloat("Vertical");
            var speed = 10;
            if (inputSystem.GetKey(KeyCode.W)) {
                dir = charaComp.Body.transform.forward;
                if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                    vec = dir * SoData.MySOCharacter.MyMoveInfo.RunSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 2, Time.deltaTime * speed);
                    charaComp.AnimatorController.SetFloat("Vertical", v);
                } else {
                    // 移动速度
                    vec = dir * SoData.MySOCharacter.MyMoveInfo.WalkSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 1, Time.deltaTime * speed);
                    charaComp.AnimatorController.SetFloat("Vertical", v);
                }
                charaComp.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.S)) {
                dir = -charaComp.Body.transform.forward;
                vec = dir * SoData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, -1, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Vertical", v);
                charaComp.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.A)) {
                dir = -charaComp.Body.transform.right;
                vec = dir * SoData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, -1, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Vertical", v);
                charaComp.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.D)) {
                dir = charaComp.Body.transform.right;
                vec = dir * SoData.MySOCharacter.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, 1, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Vertical", v);
                charaComp.AnimatorController.SetBool("IsMove", true);
            } else {
                h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                charaComp.AnimatorController.SetFloat("Vertical", v);
                charaComp.AnimatorController.SetBool("IsMove", false);
            }
        }

        // 重力
        if (isStartGravity) {
            if (!charaComp.CC.isGrounded) {
                vec.y = -SoData.MySOEnvironmentSetting.GravitySpeed;
            } else {
                vec.y = 0;
            }
        }

        // 控制器移动
        if (vec != Vector3.zero) {
            charaComp.CC.Move(vec * Time.deltaTime);
        }

        isStartGravity = true;
    }

    private void MoveBehaviour() {
        if (!characterData.IsMainCharacter) {
            return;
        }

        var inputSystem = game.MyGameSystem.InputS;
        // 获取不同的移动方向
        Vector3 dir = Vector3.zero;
        if (inputSystem.GetKey(KeyCode.W)) {
            dir = charaComp.Body.transform.forward;
        } else if (inputSystem.GetKey(KeyCode.S)) {
            dir = -charaComp.Body.transform.forward;
        } else if (inputSystem.GetKey(KeyCode.A)) {
            dir = -charaComp.Body.transform.right;
        } else if (inputSystem.GetKey(KeyCode.D)) {
            dir = charaComp.Body.transform.right;
        } else {
            charaComp.AnimatorController.SetFloat("Move", 0);
        }

        // 未按下移动键
        if (dir == Vector3.zero) {
            return;
        }

        // 跳跃和降落获取空中移动速度
        var moveSpeed = 0f;
        if (characterData.IsJumping || characterData.IsLanding) {
            moveSpeed = SoData.MySOCharacter.MyMoveInfo.AirWalkSpeed;
        } else {
            if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                moveSpeed = SoData.MySOCharacter.MyMoveInfo.RunSpeed;
                charaComp.AnimatorController.SetFloat("Move", 2);
            } else {
                moveSpeed = SoData.MySOCharacter.MyMoveInfo.WalkSpeed;
                charaComp.AnimatorController.SetFloat("Move", 1);
            }
        }

        moveVector += dir * moveSpeed * Time.deltaTime;
    }

    private Vector3 JumpBehaviour(Vector3 moveVector) {
        // 计时中
        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
            characterData.IsJumping = true;
            moveVector += MyObj.transform.up * SoData.MySOCharacter.MyMoveInfo.JumpSpeed * Time.deltaTime;
            return moveVector;
        }

        // 降落中
        if (characterData.IsLanding) {
            moveVector -= MyObj.transform.up * SoData.MySOEnvironmentSetting.GravitySpeed * Time.deltaTime;
            // 落地降落停止
            if (charaComp.CC.isGrounded) {
                characterData.IsLanding = false;
                jumpTimer = -1;
            }

            return moveVector;
        } else if (characterData.IsJumping || (!characterData.IsJumping && !charaComp.CC.isGrounded)) {
            jumpTimer = 0;
            characterData.IsJumping = false;
            characterData.IsLanding = true;
        }

        // 按下跳跃键
        if (game.MyGameSystem.InputS.GetKeyDown(KeyCode.Space)) {
            if (characterData.IsMainCharacter) {
                jumpTimer = SoData.MySOCharacter.MyMoveInfo.JumpContinueTime;
            }
        }

        return moveVector;
    }

    public CharacterComponent GetComp() {
        return base.GetComp() as CharacterComponent;
    }
}