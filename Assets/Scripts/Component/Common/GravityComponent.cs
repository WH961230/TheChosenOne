using UnityEngine;

public class GravityComponent : GameComp {
    private Transform MyTarget;
    private CharacterController MyCC;
    private SOEnvironmentSetting soEnvironmentSetting;
    public override void Init(Game game) {
        base.Init(game);
        soEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
    }

    public void Register(Transform target) {
        MyTarget = target;
    }

    public void Register(CharacterController cc) {
        MyCC = cc;
    }

    public override void Update() {
        base.Update();
        if (MyTarget) {
            MyTarget.transform.Translate(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
        }

        if (MyCC) {
            if (!MyCC.isGrounded) {
                MyCC.Move(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
            }
        }
    }

    public override void Clear() {
        MyTarget = null;
        MyCC = null;
        base.Clear();
    }
}