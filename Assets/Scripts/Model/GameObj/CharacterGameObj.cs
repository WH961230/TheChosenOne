using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterGameObj : GameObj {
    private CharacterData characterData;
    private CharacterComponent characterComponent;
    private Game game;

    // 视角参数
    private float xRotate = 0.0f;
    private float yRotate = 0.0f;
    
    // 跳跃参数
    private float jumpTimer = -1;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        this.characterComponent = MyObj.transform.GetComponent<CharacterComponent>();
        characterData = (CharacterData)data;
        // 注册重力组件
        game.MyGameComponent.MyGravityComponent.Register(characterComponent.CC);
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        var moveVec = Vector3.zero;
        // 视角旋转
        EyeBehaviour();
        // 移动行为
        moveVec = MoveBehaviour(moveVec);
        // 跳跃行为
        moveVec = JumpBehaviour(moveVec);

        CCMove(moveVec);
    }

    private void EyeBehaviour() {
        yRotate += InputSystem.GetAxis("Mouse X");
        xRotate -= InputSystem.GetAxis("Mouse Y");
        characterComponent.Head.transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
    }

    private Vector3 MoveBehaviour(Vector3 moveVec) {
        var moveSpeed = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed;
        var tran = MyObj.transform;
        if (InputSystem.GetKey(KeyCode.W)) {
            moveVec += tran.forward * moveSpeed * Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.S)) {
            moveVec += -tran.forward * moveSpeed * Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.A)) {
            moveVec += -tran.right * moveSpeed * Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.D)) {
            moveVec += tran.right * moveSpeed * Time.deltaTime;
        }

        return moveVec;
    }

    private Vector3 JumpBehaviour(Vector3 moveVec) {
        var config = game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting;

        // 计时中
        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
            characterData.IsJumping = true;
            moveVec += MyObj.transform.up * config.JumpSpeed * Time.deltaTime;
            return moveVec;
        }

        jumpTimer = 0;
        characterData.IsJumping = false;
        characterData.IsLanding = true;

        if (characterComponent.CC.isGrounded) {
            characterData.IsLanding = false;
            jumpTimer = -1;
        }

        // 按下跳跃键
        if (InputSystem.GetKeyDown(KeyCode.Space)) {
            jumpTimer = config.JumpContinueTime;
        }

        return moveVec;
    }

    private void CCMove(Vector3 moveVec) {
        characterComponent.CC.Move(moveVec);
    }
}