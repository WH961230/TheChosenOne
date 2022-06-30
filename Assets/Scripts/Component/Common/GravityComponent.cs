using UnityEngine;

public class GravityComponent {
    private Transform MyTarget;
    private CharacterController MyCC;
    private SOEnvironmentSetting soEnvironmentSetting;
    public void Start(Transform target) {
        MyTarget = target;
        soEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
    }

    public void Start(CharacterController cc) {
        MyCC = cc;
        soEnvironmentSetting = Resources.Load<SOEnvironmentSetting>(PathData.SOEnvironmentSettingPath);
    }

    public void Update() {
        if (MyTarget) {
            MyTarget.transform.Translate(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
        }

        if (MyCC) {
            MyCC.Move(Vector3.down * soEnvironmentSetting.GravitySpeed * Time.deltaTime);
        }
    }

    public void Stop() {
        MyTarget = null;
        MyCC = null;
    }
}