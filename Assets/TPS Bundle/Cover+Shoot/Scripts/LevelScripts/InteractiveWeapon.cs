using UnityEngine;
using UnityEngine.UI;

// 这个类别对应于任何游戏内的武器互动
public class InteractiveWeapon : MonoBehaviour {
    public enum WeaponType { // 武器类型 与玩家的射击动画相关
        NONE, // 无
        SHORT, // 短
        LONG // 长
    }

    public enum WeaponMode { // 武器射击模式
        SEMI, // 半自动
        BURST, // 散弹
        AUTO // 自动
    }

    public WeaponType type = WeaponType.NONE; // 默认武器类型
    public WeaponMode mode = WeaponMode.SEMI; // 默认武器模式

    [SerializeField] 
    private int mag, totalBullets; // 当前的规格容量和所携带的子弹总数
    public int burstSize = 0; // 在爆发模式下开了多少枪
    private int fullMag, maxBullets; // 默认的磁石容量和总子弹头重置目的

    public string label; // 武器名称。不管游戏对象的名称是什么，相同的名称都会将武器视为相同的
    public AudioClip shotSound, reloadSound, pickSound, dropSound, noBulletSound;
    public Sprite sprite; // 武器srpite显示在屏幕HUD上
    public Vector3 rightHandPosition; // 相对于玩家的右手的位置偏移
    public Vector3 relativeRotation; // 相对于玩家的右手偏移
    public float bulletDamage = 10f; // 一次射击的伤害
    public float recoilAngle; // 武器反冲角度

    private GameObject player, gameController;
    private ShootBehaviour playerInventory;

    private SphereCollider interactiveRadius; // 与玩家的游戏内互动半径
    private BoxCollider col; // 武器碰撞体
    private Rigidbody rbody; // 武器刚体

    private WeaponUIManager weaponHud; // 参考屏幕上的武器 HUD
    private Transform pickupHUD; // 参考游戏中的拾取武器标签
    private bool pickable; // 是否可拾取

    void Awake() {
        // // 设置引用
        // gameObject.name = label;
        // gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //
        // foreach (Transform t in transform) {
        //     t.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        // }
        //
        // // 游戏控制器
        // gameController = GameObject.FindGameObjectWithTag("GameController");
        //
        // // 断言存在一个屏幕上的 HUD
        // if (GameObject.Find("ScreenHUD") == null) {
        //     Debug.LogError("没有找到ScreenHUD画布。在游戏控制器中创建ScreenHUD");
        // }
        //
        // // 武器 UI
        // weaponHud = GameObject.Find("ScreenHUD").GetComponent<WeaponUIManager>();
        //
        // // 拾取 UI
        // pickupHUD = gameController.transform.Find("PickupHUD");
        // pickupHUD.gameObject.SetActive(false);
        //
        // // 获取武器 盒子 碰撞体
        // col = transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
        //
        // // 设置交互半径
        // CreateInteractiveRadius(col.center);
        //
        // // 刚体
        // rbody = gameObject.AddComponent<Rigidbody>();
        // rbody.mass = 50;
        //
        // if (type == WeaponType.NONE) {
        //     Debug.LogWarning("Set correct weapon slot ( 1 - small/ 2- big)");
        //     type = WeaponType.SHORT;
        // }
        //
        // // 断言枪口是存在的
        // if (!transform.Find("muzzle")) {
        //     Debug.LogError(name + "没有枪口。创建一个名为“muzzle”的游戏对象，作为这个游戏对象的子对象");
        // }
        //
        // // 设置默认值
        // fullMag = mag;
        // maxBullets = totalBullets;
    }

    public void OnInit() {
        // 设置引用
        gameObject.name = label;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        foreach (Transform t in transform) {
            t.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        // 游戏控制器
        gameController = GameObject.FindGameObjectWithTag("GameController");

        // 断言存在一个屏幕上的 HUD
        if (GameObject.Find("ScreenHUD") == null) {
            Debug.LogError("没有找到ScreenHUD画布。在游戏控制器中创建ScreenHUD");
        }

        // 武器 UI
        weaponHud = GameObject.Find("ScreenHUD").GetComponent<WeaponUIManager>();

        // 拾取 UI
        pickupHUD = gameController.transform.Find("PickupHUD");
        pickupHUD.gameObject.SetActive(false);

        // 获取武器 盒子 碰撞体
        col = transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

        // 设置交互半径
        CreateInteractiveRadius(col.center);

        // 刚体
        rbody = gameObject.AddComponent<Rigidbody>();
        rbody.mass = 50;

        if (type == WeaponType.NONE) {
            Debug.LogWarning("Set correct weapon slot ( 1 - small/ 2- big)");
            type = WeaponType.SHORT;
        }

        // 断言枪口是存在的
        if (!transform.Find("muzzle")) {
            Debug.LogError(name + "没有枪口。创建一个名为“muzzle”的游戏对象，作为这个游戏对象的子对象");
        }

        // 设置默认值
        fullMag = mag;
        maxBullets = totalBullets;
    }

    // 创造与玩家的互动空间
    private void CreateInteractiveRadius(Vector3 center) {
        // 获取圆形碰撞体
        interactiveRadius = gameObject.AddComponent<SphereCollider>();
        interactiveRadius.center = center;
        interactiveRadius.radius = 1f;
        interactiveRadius.isTrigger = true;
    }

    void Update() {
        // if (player == null) {
        //     player = GameObject.FindGameObjectWithTag("Player");
        //     if (player != null) {
        //         playerInventory = player.GetComponent<ShootBehaviour>();
        //     }
        // }
        //
        // // 处理玩家挑选武器的动作
        // if (pickable && Input.GetButtonDown(playerInventory.pickButton)) {
        //     col.enabled = false;
        //
        //     // 设置武器并添加玩家库存
        //     playerInventory.AddWeapon(this);
        //     Destroy(interactiveRadius);
        //     Toggle(true);
        //     pickable = false;
        //
        //     // 改变现役武器HUD
        //     TooglePickupHUD(false);
        //     Destroy(rbody);
        //
        //     // 禁用武器物理
        //     transform.localPosition = Vector3.zero;
        //     transform.localRotation = Quaternion.identity;
        //     transform.localScale = Vector3.one;
        // }
    }
    
    public void OnUpdate() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {
                playerInventory = player.GetComponent<ShootBehaviour>();
            }
        }

        // 处理玩家挑选武器的动作
        if (pickable && Input.GetButtonDown(playerInventory.pickButton)) {
            col.enabled = false;

            // 设置武器并添加玩家库存
            // playerInventory.AddWeapon(this);
            Destroy(interactiveRadius);
            Toggle(true);
            pickable = false;

            // 改变现役武器HUD
            TooglePickupHUD(false);
            Destroy(rbody);

            // 禁用武器物理
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }

    // // 处理武器与环境的碰撞
    // private void OnCollisionEnter(Collision collision) {
    //     if (collision.collider.gameObject != player && Vector3.Distance(transform.position, player.transform.position) <= 5f) {
    //         AudioSource.PlayClipAtPoint(dropSound, transform.position, 0.5f);
    //     }
    // }
    //
    // // 处理玩家退出互动范围
    // private void OnTriggerExit(Collider other) {
    //     if (other.gameObject == player) {
    //         pickable = false;
    //         TooglePickupHUD(false);
    //     }
    // }
    //
    // // 在互动范围内处理玩家
    // void OnTriggerStay(Collider other) {
    //     if (other.gameObject == player && playerInventory && playerInventory.isActiveAndEnabled) {
    //         pickable = true;
    //         TooglePickupHUD(true);
    //     }
    // }

    // 画出游戏内拾取武器的标签
    private void TooglePickupHUD(bool toogle) {
        pickupHUD.gameObject.SetActive(toogle);
        if (toogle) {
            pickupHUD.position = transform.position + Vector3.up * 0.5f;
            Vector3 direction = player.GetComponent<BasicBehaviour>().playerCamera.forward;
            direction.y = 0f;
            pickupHUD.rotation = Quaternion.LookRotation(direction);
            pickupHUD.Find("Label").GetComponent<Text>().text = "Pick " + gameObject.name;
        }
    }

    // 管理武器激活状态
    public void Toggle(bool active) {
        if (active) {
            AudioSource.PlayClipAtPoint(pickSound, transform.position, 0.5f);
        }

        weaponHud.Toggle(active);
        UpdateHud();
    }

    // 管理丢弃操作
    public void Drop() {
        gameObject.transform.SetParent(GameData.WeaponRoot);
        gameObject.SetActive(true);
        transform.position += Vector3.up;

        rbody = gameObject.AddComponent<Rigidbody>();
        rbody.useGravity = true;
        rbody.isKinematic = false;

        CreateInteractiveRadius(col.center);
        col.enabled = true;
        weaponHud.Toggle(false);
    }

    // 启动重新加载动作(由射击行为调用)
    public bool StartReload() {
        if (mag == fullMag || totalBullets == 0)
            return false;
        else if (totalBullets < fullMag - mag) {
            mag += totalBullets;
            totalBullets = 0;
        } else {
            totalBullets -= fullMag - mag;
            mag = fullMag;
        }

        return true;
    }

    // 结束重新装填动作(由射击行为调用)
    public void EndReload() {
        UpdateHud();
    }

    // 管理射击行动
    public bool Shoot(bool firstShot = true) {
        if (mag > 0) {
            mag--;
            UpdateHud();
            return true;
        }

        if (firstShot && noBulletSound) {
            AudioSource.PlayClipAtPoint(noBulletSound, transform.Find("muzzle").position, 5f);
        }
        return false;
    }

    // 重置弹药参数
    public void ResetBullets() {
        mag = fullMag;
        totalBullets = maxBullets;
    }

    // 更新武器屏幕HUD
    private void UpdateHud() {
        weaponHud.UpdateWeaponHUD(sprite, mag, fullMag, totalBullets);
    }
}