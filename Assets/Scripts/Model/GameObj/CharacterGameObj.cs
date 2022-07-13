using UnityEngine;

public class CharacterGameObj : GameObj {
    private float moveSpeed = 0.0f; // 移动速度
    private float yRotate = 0.0f; // 视角参数
    private float jumpTimer = -1; // 跳跃参数
    private bool isStartGravity = false; // 延迟一帧执行重力，否则初始化位置有问题 会被拉回到原点坐标
    private Vector3 moveVector = Vector3.zero; // 移动向量
    private Vector3 gravityVector = Vector3.zero; // 重力向量
    private SOCharacterSetting soCharacterSetting;
    private CharacterComponent characterComponent;

    private InputSystem inputSystem {
        get { return game.MyGameSystem.MyInputSystem; }
    }

    private EnvironmentSystem environmentSystem {
        get { return game.MyGameSystem.MyEnvironmentSystem; }
    }

    private CharacterData characterData;
    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        characterData = (CharacterData) data;
        MyComponent = MyObj.transform.GetComponent<CharacterComponent>();
        characterComponent = (CharacterComponent) MyComponent;

        characterComponent.Body.transform.localPosition = MyData.MyTranInfo.MyPos;
        characterComponent.Body.transform.localRotation = MyData.MyTranInfo.MyRot;
        InitConfigParam();
    }

    private void InitConfigParam() {
        soCharacterSetting = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        Vector3 vec = Vector3.zero;

        if (characterData.IsMainCharacter) {
            // 视角 当玩家未按下 alt 视角移动角色 Body
            var pressAltDown = inputSystem.GetKey(KeyCode.LeftAlt) || inputSystem.GetKey(KeyCode.RightAlt);
            // var pressAltUp = inputSystem.GetKeyUp(KeyCode.LeftAlt) || inputSystem.GetKeyUp(KeyCode.RightAlt);
            yRotate += inputSystem.GetAxis("Mouse X");
            if (!pressAltDown) {
                characterComponent.Body.transform.rotation = Quaternion.Euler(0, yRotate, 0);
            }

            // 获取不同的移动方向
            Vector3 dir = Vector3.zero;

            var h = characterComponent.AnimatorController.GetFloat("Horizontal");
            var v = characterComponent.AnimatorController.GetFloat("Vertical");
            var speed = 10;
            if (inputSystem.GetKey(KeyCode.W)) {
                dir = characterComponent.Body.transform.forward;
                if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                    vec = dir * soCharacterSetting.MyMoveInfo.RunSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 2, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Vertical", v);
                } else {
                    // 移动速度
                    vec = dir * soCharacterSetting.MyMoveInfo.WalkSpeed;
                    h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                    v = Mathf.Lerp(v, 1, Time.deltaTime * speed);
                    characterComponent.AnimatorController.SetFloat("Vertical", v);
                }
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.S)) {
                dir = -characterComponent.Body.transform.forward;
                vec = dir * soCharacterSetting.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, -1, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.A)) {
                dir = -characterComponent.Body.transform.right;
                vec = dir * soCharacterSetting.MyMoveInfo.WalkSpeed;
                h = Mathf.Lerp(h, -1, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Horizontal", h);
                    
                v = Mathf.Lerp(v, 0, Time.deltaTime * speed);
                characterComponent.AnimatorController.SetFloat("Vertical", v);
                characterComponent.AnimatorController.SetBool("IsMove", true);
            } else if (inputSystem.GetKey(KeyCode.D)) {
                dir = characterComponent.Body.transform.right;
                vec = dir * soCharacterSetting.MyMoveInfo.WalkSpeed;
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
                vec.y = -environmentSystem.mySoEnvironmentSetting.GravitySpeed;
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
            moveSpeed = soCharacterSetting.MyMoveInfo.AirWalkSpeed;
        } else {
            if (inputSystem.GetKey(KeyCode.LeftShift) || inputSystem.GetKey(KeyCode.RightShift)) {
                moveSpeed = soCharacterSetting.MyMoveInfo.RunSpeed;
                characterComponent.AnimatorController.SetFloat("Move", 2);
            } else {
                moveSpeed = soCharacterSetting.MyMoveInfo.WalkSpeed;
                characterComponent.AnimatorController.SetFloat("Move", 1);
            }
        }

        moveVector += dir * moveSpeed * Time.deltaTime;
    }

    private Vector3 JumpBehaviour(Vector3 moveVector) {
        var config = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting;
        var environmentConfig = game.MyGameSystem.MyEnvironmentSystem.mySoEnvironmentSetting;
        // 计时中
        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
            characterData.IsJumping = true;
            moveVector += MyObj.transform.up * config.MyMoveInfo.JumpSpeed * Time.deltaTime;
            return moveVector;
        }

        // 降落中
        if (characterData.IsLanding) {
            moveVector -= MyObj.transform.up * environmentConfig.GravitySpeed * Time.deltaTime;
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
                jumpTimer = config.MyMoveInfo.JumpContinueTime;
            }
        }

        return moveVector;
    }
}