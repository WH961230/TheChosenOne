using UnityEngine;
using UnityEngine.UI;

public class WeaponGameObj : GameObj {
    private WeaponComponent wepComp;
    private InteractiveWeapon iw;
    private WeaponData wepData;

    public InteractiveWeapon.WeaponType type = InteractiveWeapon.WeaponType.NONE; // 默认武器类型
    public InteractiveWeapon.WeaponMode mode = InteractiveWeapon.WeaponMode.SEMI; // 默认武器模式

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
    public ShootBehaviour playerInventory;

    private SphereCollider interactiveRadius; // 与玩家的游戏内互动半径
    private BoxCollider col; // 武器碰撞体
    private Rigidbody rbody; // 武器刚体

    private WeaponUIManager weaponHud; // 参考屏幕上的武器 HUD
    private Transform pickupHUD; // 参考游戏中的拾取武器标签
    public bool pickable; // 是否可拾取

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        wepData = (WeaponData)data;
        wepComp = (WeaponComponent) Comp;
        wepComp.OnInit(this);

        // 设置引用
        MyObj.layer = LayerMask.NameToLayer("Ignore Raycast");

        foreach (Transform t in MyObj.transform) {
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
        col = MyObj.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

        // 设置交互半径
        CreateInteractiveRadius(col.center);

        // 刚体
        rbody = MyObj.AddComponent<Rigidbody>();
        rbody.mass = 50;

        if (type == InteractiveWeapon.WeaponType.NONE) {
            Debug.LogWarning("Set correct weapon slot ( 1 - small/ 2- big)");
            type = InteractiveWeapon.WeaponType.SHORT;
        }

        // 断言枪口是存在的
        if (!MyObj.transform.Find("muzzle")) {
            Debug.LogError(MyObj.name + "没有枪口。创建一个名为“muzzle”的游戏对象，作为这个游戏对象的子对象");
        }

        // 设置默认值
        fullMag = mag;
        maxBullets = totalBullets;
    }

    // 创造与玩家的互动空间
    private void CreateInteractiveRadius(Vector3 center) {
        // 获取圆形碰撞体
        interactiveRadius = MyObj.AddComponent<SphereCollider>();
        interactiveRadius.center = center;
        interactiveRadius.radius = 1f;
        interactiveRadius.isTrigger = true;
    }

    public void SetWeaponPlace(Transform root, Vector3 point, Quaternion rot) {
        wepData.MyObj.gameObject.transform.SetParent(root);
        wepData.MyObj.transform.localPosition = point;
        wepData.MyObj.transform.localRotation = rot;
    }

    // 管理射击行动
    public bool Shoot(bool firstShot = true) {
        if (mag > 0) {
            mag--;
            UpdateHud();
            return true;
        }

        if (firstShot && noBulletSound) {
            AudioSource.PlayClipAtPoint(noBulletSound, MyObj.transform.Find("muzzle").position, 5f);
        }
        return false;
    }

    public override void Update() {
        base.Update();
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {
                wepComp.SetPlayer(player);
                playerInventory = player.GetComponent<ShootBehaviour>();
            }
        }

        // 处理玩家挑选武器的动作
        if (pickable && Input.GetButtonDown(playerInventory.pickButton)) {
            col.enabled = false;

            // 设置武器并添加玩家库存
            playerInventory.AddWeapon(this);
            UnityEngine.GameObject.DestroyImmediate(interactiveRadius);
            Toggle(true);
            pickable = false;

            // 改变现役武器HUD
            TooglePickupHUD(false);
            UnityEngine.GameObject.DestroyImmediate(rbody);

            // 禁用武器物理
            MyObj.transform.localPosition = Vector3.zero;
            MyObj.transform.localRotation = Quaternion.identity;
            MyObj.transform.localScale = Vector3.one;
        }
    }

    // 重置弹药参数
    public void ResetBullets() {
        mag = fullMag;
        totalBullets = maxBullets;
    }

    // 结束重新装填动作(由射击行为调用)
    public void EndReload() {
        UpdateHud();
    }

    // 管理丢弃操作
    public void Drop() {
        MyObj.transform.SetParent(GameData.WeaponRoot);
        MyObj.SetActive(true);
        MyObj.transform.position += Vector3.up;

        rbody = MyObj.AddComponent<Rigidbody>();
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

    // 画出游戏内拾取武器的标签
    public void TooglePickupHUD(bool toogle) {
        pickupHUD.gameObject.SetActive(toogle);
        if (toogle) {
            pickupHUD.position = MyObj.transform.position + Vector3.up * 0.5f;
            Vector3 direction = player.GetComponent<BasicBehaviour>().playerCamera.forward;
            direction.y = 0f;
            pickupHUD.rotation = Quaternion.LookRotation(direction);
            pickupHUD.Find("Label").GetComponent<Text>().text = "Pick " + MyObj.name;
        }
    }

    // 管理武器激活状态
    public void Toggle(bool active) {
        // if (active) {
        //     AudioSource.PlayClipAtPoint(pickSound, MyObj.transform.position, 0.5f);
        // }

        weaponHud.Toggle(active);
        UpdateHud();
    }
    
    // 更新武器屏幕HUD
    private void UpdateHud() {
        weaponHud.UpdateWeaponHUD(sprite, mag, fullMag, totalBullets);
    }

    public WeaponComponent GetComp() {
        return base.GetComp() as WeaponComponent;
    }
}