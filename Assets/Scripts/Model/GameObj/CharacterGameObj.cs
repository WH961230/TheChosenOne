﻿using UnityEngine;

public class CharacterGameObj : GameObj {
    private float moveSpeed = 0.0f; // 移动速度
    private float xRotate = 0.0f; // 视角参数
    private float yRotate = 0.0f; // 视角参数
    private float jumpTimer = -1; // 跳跃参数
    private Vector3 moveVector = Vector3.zero; // 

    private SOCharacterSetting soCharacterSetting;

    private CharacterComponent characterComponent;
    public CharacterComponent MyCharacterComponent {
        get { return characterComponent; }
    }

    private CharacterData characterData;

    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        characterData = (CharacterData)data;
        characterData.MyComponent = MyObj.transform.GetComponent<CharacterComponent>();
        characterComponent = (CharacterComponent)characterData.MyComponent;

        characterComponent.Body.transform.position = MyData.MyTranInfo.MyPos;
        characterComponent.Body.transform.rotation = MyData.MyTranInfo.MyRot;

        InitConfigParam();
        RegisterMessage();
    }

    private void RegisterMessage() {
        game.MyGameMessageCenter.Register<KeyCode>(GameMessageConstants.INPUT_KEY, MoveBehaviour);
    }

    private void InitConfigParam() {
        soCharacterSetting = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        Debug.Log($"jump:{characterData.IsJumping} land:{characterData.IsLanding}");
        BehaviourCenter();
        characterComponent.CC.Move(moveVector);
    }

    private void BehaviourCenter() {
        // 视角旋转
        EyeBehaviour();
        // 跳跃行为
        JumpBehaviour();
    }

    private void EyeBehaviour() {
        // var pressAlt = InputSystem.GetKey(KeyCode.LeftAlt) || InputSystem.GetKey(KeyCode.RightAlt);
        // yRotate += InputSystem.GetAxis("Mouse X");
        // xRotate -= InputSystem.GetAxis("Mouse Y");
        //
        // Transform rotTran = null;
        // if (pressAlt) {
        //     rotTran = characterComponent.Head.transform;
        // } else {
        //     rotTran = characterComponent.Body.transform;
        // }
        //
        // rotTran.rotation = Quaternion.Euler(xRotate, yRotate, 0);
    }

    private bool CheckCanMove() {
        return true;
    }

    private void MoveBehaviour(KeyCode keyCode) {
        if (!CheckCanMove()) {
            return;
        }

        // 获取不同的移动方向
        Vector3 dir;
        switch (keyCode) {
            case KeyCode.W:
                dir = characterComponent.Body.transform.forward;
                break;
            case KeyCode.S:
                dir = -characterComponent.Body.transform.forward;
                break;
            case KeyCode.A:
                dir = -characterComponent.Body.transform.right;
                break;
            case KeyCode.D:
                dir = characterComponent.Body.transform.right;
                break;
            default:
                return;
        }

        // 跳跃和降落获取空中移动速度
        var moveSpeed = 0f;
        if (characterData.IsJumping || characterData.IsLanding) {
            moveSpeed = soCharacterSetting.AirMoveSpeed;
        } else {
            moveSpeed = soCharacterSetting.GroundMoveSpeed;
        }

        moveVector += dir * moveSpeed * Time.deltaTime;
    }

    private void JumpBehaviour() {
        var config = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting;
        var environmentConfig = game.MyGameSystem.MyEnvironmentSystem.mySoEnvironmentSetting;
        // 计时中
        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
            characterData.IsJumping = true;
            moveVector += MyObj.transform.up * config.JumpSpeed * Time.deltaTime;
            return;
        }

        // 降落中
        if (characterData.IsLanding) {
            moveVector -= MyObj.transform.up * environmentConfig.GravitySpeed * Time.deltaTime;
            // 落地降落停止
            if (characterComponent.CC.isGrounded) {
                characterData.IsLanding = false;
                jumpTimer = -1;
            }
        } else if (characterData.IsJumping || (!characterData.IsJumping && !characterComponent.CC.isGrounded)) {
            jumpTimer = 0;
            characterData.IsJumping = false;
            characterData.IsLanding = true;
        }

        // // 按下跳跃键
        // if (InputSystem.GetKeyDown(KeyCode.Space)) {
        //     jumpTimer = config.JumpContinueTime;
        // }
    }
}