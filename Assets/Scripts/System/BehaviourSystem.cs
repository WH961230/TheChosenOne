using UnityEngine;

public class BehaviourSystem : GameSys {
    private float countFrame;
    private GameSystem gameSystem;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    // 相机中心打出射线获取物体
    public void OnCameraScreenCenterRayRecognize() {
        ++countFrame;
        if (countFrame % 10 != 0) {
            return;
        }

        // 获取角色相机
        var cameraGameObj = gameSystem.MyGameObjFeature.Get<CameraGameObj>(GameData.MainCharacterCamera);
        var cameraComponent = cameraGameObj.GetData<CameraData>().GetComponent<CameraComponent>();
        var camera = cameraComponent.MyCamera;

        // 打射线
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        var uiTipWindow = gameSystem.MyWindowFeature.Get<UITipWindow>();
        if (null == uiTipWindow) {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9)) {
            Debug.DrawLine(cameraComponent.MyCamera.transform.position, hit.point, Color.red);
            var id = hit.collider.gameObject.GetInstanceID();
            var name = gameSystem.MyGameObjFeature.Get<SceneItemGameObj>(id).GetData<SceneItemData>().MySceneItemSign;
            uiTipWindow.SetTipDescription(UITipType.ItemNameTip, name);
            uiTipWindow.SetTipDescription(UITipType.ItemKeycode, "拾取[F]");
            uiTipWindow.Open();

            if (gameSystem.MyInputSystem.GetKeyDown(KeyCode.F)) {
                // game.MyGameSystem.MyItemSystem.OnPickUpItem(id, GameData.MainCharacater);
            }

            Debug.Log("检测物体 " + name + " InstanceId: " + hit.collider.gameObject.GetInstanceID());
        } else {
            uiTipWindow.Close();
        }
    }
}