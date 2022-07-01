using UnityEngine;

public class CharacterGameObj : GameObj {
    private CharacterData characterData;
    private CharacterComponent characterComponent;
    private Game game;

    private float xRotate = 0.0f;
    private float yRotate = 0.0f;
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
        if (InputSystem.GetKey(KeyCode.W)) {
            moveVec += MyObj.transform.forward *
                                       game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed *
                                       Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.S)) {
            moveVec += -MyObj.transform.forward *
                       game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed * Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.A)) {
            moveVec += -MyObj.transform.right *
                                       game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed *
                                       Time.deltaTime;
        } else if (InputSystem.GetKey(KeyCode.D)) {
            moveVec += MyObj.transform.right *
                       game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed *
                       Time.deltaTime;
        }

        return moveVec;
    }

    private Vector3 JumpBehaviour(Vector3 moveVec) {
        if (InputSystem.GetKey(KeyCode.Space)) {
            moveVec += MyObj.transform.up *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.JumpSpeed * Time.deltaTime;
        }

        if (InputSystem.GetKeyDown(KeyCode.R)) {
            game.MyGameComponent.MyGravityComponent.Clear();
        }

        if (InputSystem.GetKeyDown(KeyCode.T)) {
            game.MyGameComponent.MyGravityComponent.Register(characterComponent.CC);
        }

        return moveVec;
    }

    private void CCMove(Vector3 moveVec) {
        characterComponent.CC.Move(moveVec);
    }
}