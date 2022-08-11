using UnityEngine;

public class BulletSystem : GameSys {
    private bool isShoot;
    private GameObject go;
    public override void Update() {
        base.Update();
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     go = GameObject.Instantiate(SOData.MyEffectSetting.MyBulletFX);
        //     go.transform.position = new Vector3(0,0,0);
        //     go.transform.localScale = Vector3.one * 0.2f;
        //     isShoot = true;
        // }
        //
        // if (isShoot) {
        //     go.transform.position += go.transform.forward * Time.deltaTime;
        //     if (Input.GetKeyDown(KeyCode.P)) {
        //         isShoot = false;
        //     }
        // }
    }

    public void InstanceBullet() {
    }
    
    private void InstanceBullet(BulletData data) {
        MyGameSystem.InstanceGameObj<BulletGameObj, BulletEntity>(data);
    }
}