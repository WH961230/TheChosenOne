using UnityEngine;

public class CharacterGameObj : GameObj {
    private CharacterData characterData;
    private CharacterComponent characterComponent;
    private Game game;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        this.characterComponent = MyObj.transform.GetComponent<CharacterComponent>();
        characterData = (CharacterData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        // 视角旋转
        EyeBehaviour();
        // 移动行为
        MoveBehaviour();
        // 跳跃行为
        JumpBehaviour();
    }

    private void EyeBehaviour() {
        var mouseX = InputSystem.GetAxis("Mouse X");
        var mouseY = InputSystem.GetAxis("Mouse Y");
        characterComponent.Head.transform.rotation *= Quaternion.Euler(new Vector3(-mouseY, mouseX, 0));
    }

    private void MoveBehaviour() {
        if (InputSystem.GetKey(KeyCode.W)) {
            MyObj.transform.Translate(MyObj.transform.forward *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed * Time.deltaTime);
        } else if (InputSystem.GetKey(KeyCode.S)) {
            MyObj.transform.Translate(-MyObj.transform.forward *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed * Time.deltaTime);
        } else if (InputSystem.GetKey(KeyCode.A)) {
            MyObj.transform.Translate(-MyObj.transform.right *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed * Time.deltaTime);
        } else if (InputSystem.GetKey(KeyCode.D)) {
            MyObj.transform.Translate(MyObj.transform.right *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.MoveSpeed * Time.deltaTime);
        }
    }

    private void JumpBehaviour() {
        if (InputSystem.GetKeyDown(KeyCode.Space)) {
            MyObj.transform.Translate(MyObj.transform.up *
                                      game.MyGameSystem.MyCharacterSystem.MySoCharacterSetting.JumpSpeed * Time.deltaTime);
        }
    }
}