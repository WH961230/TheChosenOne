using System;
using UnityEngine;

public class WeaponComponent : GameComp {
    public string MyWeaponSign;
    public WeaponType MyWeaponType;
    public TweenRotation MyWeaponRotation;
    public TweenPosition MyWeaponPosition;
    public Transform MyFirePos;

    
    private GameObject player;
    private WeaponGameObj weaponGameObj;
    public void OnInit(WeaponGameObj weaponGameObj) {
        this.weaponGameObj = weaponGameObj;
    }

    public void SetPlayer(GameObject player) {
        this.player = player;
    }

    // 处理武器与环境的碰撞
    private void OnCollisionEnter(Collision collision) {
        if (player == null) {
            return;
        }
        // if (collision.collider.gameObject != player && Vector3.Distance(transform.position, player.transform.position) <= 5f) {
        //     AudioSource.PlayClipAtPoint(weaponGameObj.dropSound, transform.position, 0.5f);
        // }
    }

    // 处理玩家退出互动范围
    private void OnTriggerExit(Collider other) {
        if (player == null) {
            return;
        }
        if (other.gameObject == player) {
            weaponGameObj.pickable = false;
            weaponGameObj.TooglePickupHUD(false);
        }
    }

    // 在互动范围内处理玩家
    void OnTriggerStay(Collider other) {
        if (player == null) {
            return;
        }
        if (other.gameObject == player && weaponGameObj.playerInventory && weaponGameObj.playerInventory.isActiveAndEnabled) {
            weaponGameObj.pickable = true;
            weaponGameObj.TooglePickupHUD(true);
        }
    }
}

