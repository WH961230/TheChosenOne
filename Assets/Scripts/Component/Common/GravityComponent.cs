using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : GameComp {
    private List<Transform> MyTarget = new List<Transform>();
    private List<CharacterController> MyCC = new List<CharacterController>();
    private SOEnvironmentSetting soEnvironmentSetting;
    public override void Init(Game game) {
        base.Init(game);
        soEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
    }

    public void Register(Transform target) {
        MyTarget.Add(target);
    }

    public void Register(CharacterController cc) {
        MyCC.Add(cc);
    }

    public void Unregister(Transform target) {
        MyTarget.Remove(target);
    }

    public void Unregister(CharacterController cc) {
        MyCC.Remove(cc);
    }

    public override void Update() {
        base.Update();
        for (var i = 0; i < MyTarget.Count; i++) {
            MyTarget[i].transform.Translate(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
        }

        for (var i = 0; i < MyCC.Count; i++) {
            var tempCC = MyCC[i];
            if (!tempCC.isGrounded) {
                tempCC.Move(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
            }
        }
    }

    public override void Clear() {
        MyTarget = null;
        MyCC = null;
        base.Clear();
    }
}