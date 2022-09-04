using System.Collections.Generic;
using UnityEngine;

// ShootBehaviour 继承自 GenericBehaviour 这个类对应于射击/装填/更改/添加/掉落武器的行为
// 由于其特性，无论当前状态(包括活动状态和重写状态)，此行为都会被调用
// 没有必要使用行为管理器来观察它。直接调用 MonoBehaviour 的所有基本函数
public class ShootBehaviour : GenericBehaviour {
    public string shootButton = "Fire1", // 默认射击武器按钮
        pickButton = "Interact", // 默认选择武器按钮
        changeButton = "Change", // 默认更改武器按钮
        reloadButton = "Reload", // 默认重装武器按钮
        dropButton = "Drop"; // 默认放下武器按钮

    public Texture2D aimCrosshair, shootCrosshair; // 瞄准和射击的准星纹理
    public GameObject muzzleFlash, shot, sparks; // 射击效果的游戏对象
    public Material bulletHole; // 子弹孔放置在靶上的材料
    public int maxBulletHoles = 50; // 能在现场画出最大的弹孔
    public float shotErrorRate = 0.01f; // Shooting error margin. 0 is most acurate.
    public float shotRateFactor = 1f; // Rate of fire parameter. Higher is faster rate.
    public float armsRotation = 8f; // Rotation of arms to align with aim, according player heigh.

    public LayerMask shotMask = ~((1 << 2) | (1 << 9) | // Layer mask to cast shots.
                                  (1 << 10) | (1 << 11));

    public LayerMask organicMask; // Layer mask to define organic matter.

    [Header("Advanced Rotation Adjustments")]
    public Vector3 LeftArmShortAim; // Local left arm rotation when aiming with a short gun.

    public Vector3 RightHandCover; // Local right hand rotation when holding a long gun and covering.
    public Vector3 LeftArmLongGuard; // Local left arm rotation when guarding with a long gun.
    private int activeWeapon = 0; //  Index of the active weapon.
    private int weaponTypeInt; // Animator variable related to the weapon type.
    private int changeWeaponTrigger; // Animator trigger for changing weapon.
    private int shootingTrigger; // Animator trigger for shooting weapon.
    private List<WeaponGameObj> weapons; // Weapons inventory.

    private int coveringBool,
        aimBool, // Animator variables related to covering and aiming.
        blockedAimBool, // Animator variable related to blocked aim.
        reloadBool; // Animator variable related to reloading.

    private bool isAiming, // Boolean to get whether or not the player is aiming.
        isAimBlocked; // Boolean to determine whether or not the aim is blocked.

    private Transform gunMuzzle; // World position of the gun muzzle.
    private float distToHand; // Distance from neck to hand.
    private Vector3 castRelativeOrigin; // Position of neck to cast for blocked aim test.
    private Dictionary<InteractiveWeapon.WeaponType, int> slotMap; // Map to designate weapon types to inventory slots.
    private Transform hips, spine, chest, rightHand, leftArm; // Avatar bone transforms.
    private Vector3 initialRootRotation; // Initial root bone local rotation.
    private Vector3 initialHipsRotation; // Initial hips rotation related to the root bone.
    private Vector3 initialSpineRotation; // Initial spine rotation related to the hips bone.
    private Vector3 initialChestRotation; // Initial chest rotation related to the spine bone.
    private float shotDecay, originalShotDecay = 0.5f; // Default shot lifetime. Use shotRateFactor to modify speed.
    private List<GameObject> bulletHoles; // Bullet holes scene buffer.
    private int bulletHoleSlot = 0; // Number of active bullet holes on scene.
    private int burstShotCount = 0; // Number of burst shots fired.
    private AimBehaviour aimBehaviour; // Reference to the aim behaviour.
    private Texture2D originalCrosshair; // Original unarmed aim behaviour crosshair.
    private bool isShooting = false; // Boolean to determine if player is holding shoot button.
    private bool isChangingWeapon = false; // Boolean to determine if player is holding change weapon button.
    private bool isShotAlive = false; // Boolean to determine if there is any active shot on scene.

    // Start is always called after any Awake functions.
    public void OnStart(SOTps config) {
        // Set up the references.
        weaponTypeInt = Animator.StringToHash("Weapon");
        aimBool = Animator.StringToHash("Aim");
        coveringBool = Animator.StringToHash("Cover");
        blockedAimBool = Animator.StringToHash("BlockedAim");
        changeWeaponTrigger = Animator.StringToHash("ChangeWeapon");
        shootingTrigger = Animator.StringToHash("Shooting");
        reloadBool = Animator.StringToHash("Reload");
        weapons = new List<WeaponGameObj>(new WeaponGameObj[3]);
        aimBehaviour = this.GetComponent<AimBehaviour>();
        bulletHoles = new List<GameObject>();
        foreach (var VARIABLE in GameObject.Find("ShotEffects").transform) {
            var go = VARIABLE as Transform;
            if (go.gameObject.name == "flash") {
                muzzleFlash = go.gameObject;
            } else if (go.gameObject.name == "tracer") {
                shot = go.gameObject;
            } else if (go.gameObject.name == "sparks") {
                sparks = go.gameObject;
            }
        }

        aimCrosshair = config.AimCrosshair;
        shootCrosshair = config.ShootCrosshair;
        bulletHole = config.BulletHole;
        shotErrorRate = 0.02f;
        LeftArmShortAim = new Vector3(-4, 0, 2);
        RightHandCover = new Vector3(15, 0, 0);
        LeftArmLongGuard = new Vector3(0, 0, 0);

        // Hide shot effects on scene.
        muzzleFlash.SetActive(false);
        shot.SetActive(false);
        sparks.SetActive(false);

        // 创建武器插槽。不同的武器类型可以添加在同一个槽-例如:(LONG_SPECIAL, 2)为火箭发射器
        slotMap = new Dictionary<InteractiveWeapon.WeaponType, int> {
            {
                InteractiveWeapon.WeaponType.SHORT, 1
            }, {
                InteractiveWeapon.WeaponType.LONG, 2
            }
        };

        // 获得玩家角色的骨骼变形为IK
        Transform neck = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.Neck);
        if (!neck) {
            neck = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.Head).parent;
        }

        hips = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.Hips);
        spine = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.Spine);
        chest = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.Chest);
        rightHand = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.RightHand);
        leftArm = behaviourManager.GetAnim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        Transform root = hips.parent;

        // 正确的髋部和根骨
        if (spine.parent != hips) {
            root = hips;
            hips = spine.parent;
        }

        initialRootRotation = (root == transform) ? Vector3.zero : root.localEulerAngles;
        initialHipsRotation = hips.localEulerAngles;
        initialSpineRotation = spine.localEulerAngles;
        initialChestRotation = chest.localEulerAngles;
        originalCrosshair = aimBehaviour.crosshair;
        shotDecay = originalShotDecay;
        castRelativeOrigin = neck.position - this.transform.position;
        distToHand = (rightHand.position - neck.position).magnitude * 1.5f;
    }

    // Update is used to set features regardless the active behaviour.
    public void OnUpdate() {
        // Handle shoot weapon action.
        if (Input.GetAxisRaw(shootButton) != 0 && !isShooting && activeWeapon > 0 && burstShotCount == 0) {
            isShooting = true;
            ShootWeapon(activeWeapon);
        } else if (isShooting && Input.GetAxisRaw(shootButton) == 0) {
            isShooting = false;
        }
        // Handle reload weapon action.
        else if (Input.GetButtonUp(reloadButton) && activeWeapon > 0) {
            if (weapons[activeWeapon].StartReload()) {
                AudioSource.PlayClipAtPoint(weapons[activeWeapon].reloadSound, gunMuzzle.position, 0.5f);
                behaviourManager.GetAnim.SetBool(reloadBool, true);
            }
        }
        // Handle drop weapon action.
        else if (Input.GetButtonDown(dropButton) && activeWeapon > 0) {
            // End reload paramters, drop weapon and change to another one in inventory.
            EndReloadWeapon();
            int weaponToDrop = activeWeapon;
            ChangeWeapon(activeWeapon, 0);
            weapons[weaponToDrop].Drop();
            weapons[weaponToDrop] = null;
        }
        // Handle change weapon action.
        else {
            if ((Input.GetAxisRaw(changeButton) != 0 && !isChangingWeapon)) {
                isChangingWeapon = true;
                int nextWeapon = activeWeapon + 1;
                ChangeWeapon(activeWeapon, (nextWeapon) % weapons.Count);
            } else if (Input.GetAxisRaw(changeButton) == 0) {
                isChangingWeapon = false;
            }
        }

        // Manage shot parameters after shooting action.
        if (isShotAlive)
            ShotDecay();
        isAiming = behaviourManager.GetAnim.GetBool(aimBool);
    }

    private void ShootWeapon(int weapon, bool firstShot = true) {
        if (!isAiming || isAimBlocked || behaviourManager.GetAnim.GetBool(reloadBool) ||
            !weapons[weapon].Shoot(firstShot)) {
            return;
        } else {
            burstShotCount++;
            behaviourManager.GetAnim.SetTrigger(shootingTrigger);
            aimBehaviour.crosshair = shootCrosshair;
            behaviourManager.GetCamScript.BounceVertical(weapons[weapon].recoilAngle);

            // 通过射击找到目标
            Vector3 imprecision = Random.Range(-shotErrorRate, shotErrorRate) * behaviourManager.playerCamera.right;
            Ray ray = new Ray(behaviourManager.playerCamera.position, behaviourManager.playerCamera.forward + imprecision);
            RaycastHit hit = default(RaycastHit);
            // 目标被击中
            if (Physics.Raycast(ray, out hit, 500f, shotMask)) {
                if (hit.collider.transform != this.transform) {
                    // 目标是有机的吗
                    bool isOrganic = (organicMask == (organicMask | (1 << hit.transform.root.gameObject.layer)));
                    // Handle shot effects on target.
                    DrawShoot(weapons[weapon].GetObj(), hit.point, hit.normal, hit.collider.transform, !isOrganic, !isOrganic);

                    // 如果存在，调用目标的伤害行为
                    if (hit.collider) {
                        hit.collider.SendMessageUpwards("HitCallback", new HealthManager.DamageInfo(hit.point, ray.direction, weapons[weapon].bulletDamage, hit.collider),
                            SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            // No target was hit.
            else {
                Vector3 destination = (ray.direction * 500f) + ray.origin;
                // Handle shot effects without a specific target.
                DrawShoot(weapons[weapon].GetObj(), destination, Vector3.up, null, false, false);
            }

            // Play shot sound.
            AudioSource.PlayClipAtPoint(weapons[weapon].shotSound, gunMuzzle.position, 5f);
            // Trigger alert callback
            GameObject.FindGameObjectWithTag("GameController").SendMessage("RootAlertNearby", ray.origin,
                SendMessageOptions.DontRequireReceiver);
            // Reset shot lifetime.
            shotDecay = originalShotDecay;
            isShotAlive = true;
        }
    }

    // Manage the shot visual effects.
    private void DrawShoot(GameObject weapon, Vector3 destination, Vector3 targetNormal, Transform parent,
        bool placeSparks = true, bool placeBulletHole = true) {
        Vector3 origin = gunMuzzle.position - gunMuzzle.right * 0.5f;

        // Draw the flash at the gun muzzle position.
        muzzleFlash.SetActive(true);
        muzzleFlash.transform.SetParent(gunMuzzle);
        muzzleFlash.transform.localPosition = Vector3.zero;
        muzzleFlash.transform.localEulerAngles = Vector3.back * 90f;

        // Create the shot tracer and smoke trail particle.
        GameObject instantShot = Object.Instantiate<GameObject>(shot);
        instantShot.SetActive(true);
        instantShot.transform.position = origin;
        instantShot.transform.rotation = Quaternion.LookRotation(destination - origin);
        instantShot.transform.parent = shot.transform.parent;

        // Create the shot sparks at target.
        if (placeSparks) {
            GameObject instantSparks = Object.Instantiate<GameObject>(sparks);
            instantSparks.SetActive(true);
            instantSparks.transform.position = destination;
            instantSparks.transform.parent = sparks.transform.parent;
        }

        // Put bullet hole on the target.
        if (placeBulletHole) {
            Quaternion hitRotation = Quaternion.FromToRotation(Vector3.back, targetNormal);
            GameObject bullet = null;
            if (bulletHoles.Count < maxBulletHoles) {
                // Instantiate new bullet if an empty slot is available.
                bullet = GameObject.CreatePrimitive(PrimitiveType.Quad);
                bullet.GetComponent<MeshRenderer>().material = bulletHole;
                bullet.GetComponent<Collider>().enabled = false;
                bullet.transform.localScale = Vector3.one * 0.07f;
                bullet.name = "BulletHole";
                bulletHoles.Add(bullet);
            } else {
                // Cycle through bullet slots to reposition the oldest one.
                bullet = bulletHoles[bulletHoleSlot];
                bulletHoleSlot++;
                bulletHoleSlot %= maxBulletHoles;
            }

            bullet.transform.position = destination + 0.01f * targetNormal;
            bullet.transform.rotation = hitRotation;
            bullet.transform.SetParent(parent);
        }
    }

    // Change the active weapon.
    private void ChangeWeapon(int oldWeapon, int newWeapon) {
        // Previously armed? Disable weapon.
        if (oldWeapon > 0) {
            weapons[oldWeapon].GetObj().SetActive(false);
            gunMuzzle = null;
            weapons[oldWeapon].Toggle(false);
        }

        // Cycle trought empty slots to find next existing weapon or the no weapon slot.
        while (weapons[newWeapon] == null && newWeapon > 0) {
            newWeapon = (newWeapon + 1) % weapons.Count;
        }

        // Next weapon exists? Activate it.
        if (newWeapon > 0) {
            weapons[newWeapon].GetObj().SetActive(true);
            gunMuzzle = weapons[newWeapon].GetObj().transform.Find("muzzle");
            weapons[newWeapon].Toggle(true);
        }

        activeWeapon = newWeapon;

        // Call change weapon animation if new weapon type is different.
        if (oldWeapon != newWeapon) {
            behaviourManager.GetAnim.SetTrigger(changeWeaponTrigger);
            behaviourManager.GetAnim.SetInteger(weaponTypeInt, weapons[newWeapon] != null ? (int) weapons[newWeapon].type : 0);
        }

        // Set crosshair if armed.
        SetWeaponCrosshair(newWeapon > 0);
    }

    // 在生命周期内处理 shot 参数
    private void ShotDecay() {
        // 更新即将中弹死亡的参数
        if (shotDecay > 0.2) {
            shotDecay -= shotRateFactor * Time.deltaTime;
            if (shotDecay <= 0.4f) {
                // 将准星调回正常瞄准模式，隐藏射击闪光
                SetWeaponCrosshair(activeWeapon > 0);
                muzzleFlash.SetActive(false);
                if (activeWeapon > 0) {
                    // 设置相机反冲返回。
                    behaviourManager.GetCamScript.BounceVertical(-weapons[activeWeapon].recoilAngle * 0.1f);

                    // 处理下一个镜头的爆发或自动模式
                    if (shotDecay <= (0.4f - 2 * Time.deltaTime)) {
                        // 自动模式，在按下射击按钮时继续射击
                        if (weapons[activeWeapon].mode == InteractiveWeapon.WeaponMode.AUTO && Input.GetAxisRaw(shootButton) != 0) {
                            ShootWeapon(activeWeapon, false);
                        }
                        // 爆发模式，持续射击直到达到武器爆发能力
                        else if (weapons[activeWeapon].mode == InteractiveWeapon.WeaponMode.BURST && burstShotCount < weapons[activeWeapon].burstSize) {
                            ShootWeapon(activeWeapon, false);
                        }
                        // 重置其他模式的爆发计数
                        else if (weapons[activeWeapon].mode != InteractiveWeapon.WeaponMode.BURST) {
                            burstShotCount = 0;
                        }
                    }
                }
            }
        }
        // 枪毙了，重置参数
        else {
            isShotAlive = false;
            behaviourManager.GetCamScript.BounceVertical(0);
            burstShotCount = 0;
        }
    }

    // 在库存中添加一件新武器(由武器对象调用)
    public void AddWeapon(WeaponGameObj obj) {
        // Position new weapon in player's hand.
        obj.GetObj().transform.SetParent(rightHand);
        obj.GetObj().transform.localPosition = obj.rightHandPosition;
        obj.GetObj().transform.localRotation = Quaternion.Euler(obj.relativeRotation);

        // Handle inventory slot conflict.
        if (this.weapons[slotMap[obj.type]] != null) {
            // Same weapon type, recharge bullets and destroy duplicated object.
            if (this.weapons[slotMap[obj.type]].label == obj.label) {
                this.weapons[slotMap[obj.type]].ResetBullets();
                ChangeWeapon(activeWeapon, slotMap[obj.type]);
                GameObject.Destroy(obj.GetObj());
                return;
            }
            // Different weapon type, grab the new one and drop the weapon in inventory.
            else {
                this.weapons[slotMap[obj.type]].Drop();
            }
        }

        // Call change weapon action.
        this.weapons[slotMap[obj.type]] = obj;
        ChangeWeapon(activeWeapon, slotMap[obj.type]);
    }

    // Handle reload weapon end (called by animation).
    public void EndReloadWeapon() {
        behaviourManager.GetAnim.SetBool(reloadBool, false);
        weapons[activeWeapon].EndReload();
    }

    // Change HUD crosshair when aiming.
    private void SetWeaponCrosshair(bool armed) {
        if (armed)
            aimBehaviour.crosshair = aimCrosshair;
        else
            aimBehaviour.crosshair = originalCrosshair;
    }

    // Check if aim is blocked by obstacles.
    private bool CheckforBlockedAim() {
        isAimBlocked = Physics.SphereCast(this.transform.position + castRelativeOrigin, 0.1f,
            behaviourManager.GetCamScript.transform.forward, out RaycastHit hit, distToHand - 0.1f);
        isAimBlocked = isAimBlocked && hit.collider.transform != this.transform;
        behaviourManager.GetAnim.SetBool(blockedAimBool, isAimBlocked);
        Debug.DrawRay(this.transform.position + castRelativeOrigin,
            behaviourManager.GetCamScript.transform.forward * distToHand, isAimBlocked ? Color.red : Color.cyan);
        return isAimBlocked;
    }

    // Manage inverse kinematic parameters.
    public void OnAnimatorIK(int layerIndex) {
        if (isAiming && activeWeapon > 0) {
            if (CheckforBlockedAim())
                return;

            // Orientate upper body where camera  is targeting.
            Quaternion targetRot = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            targetRot *= Quaternion.Euler(initialRootRotation);
            targetRot *= Quaternion.Euler(initialHipsRotation);
            targetRot *= Quaternion.Euler(initialSpineRotation);
            // Set upper body horizontal orientation.
            behaviourManager.GetAnim.SetBoneLocalRotation(HumanBodyBones.Spine,
                Quaternion.Inverse(hips.rotation) * targetRot);

            // Keep upper body orientation regardless strafe direction.
            float xCamRot = Quaternion.LookRotation(behaviourManager.playerCamera.forward).eulerAngles.x;
            targetRot = Quaternion.AngleAxis(xCamRot + armsRotation, this.transform.right);
            if (weapons[activeWeapon] != null && weapons[activeWeapon].type == InteractiveWeapon.WeaponType.LONG) {
                // Correction for long weapons.
                targetRot *= Quaternion.AngleAxis(9f, this.transform.right);
                targetRot *= Quaternion.AngleAxis(20f, this.transform.up);
            }

            targetRot *= spine.rotation;
            targetRot *= Quaternion.Euler(initialChestRotation);
            // Set upper body vertical orientation.
            behaviourManager.GetAnim.SetBoneLocalRotation(HumanBodyBones.Chest,
                Quaternion.Inverse(spine.rotation) * targetRot);
        }
    }

    // Manage post animation step corrections.
    private void LateUpdate() {
        if (!isAiming || isAimBlocked) {
            // Correct right hand position when covering.
            if (behaviourManager.GetAnim.GetBool(coveringBool)
                && behaviourManager.GetAnim.GetFloat(Animator.StringToHash("Crouch")) > 0.5f) {
                rightHand.Rotate(RightHandCover);
            }
            // Correct left arm position when guarding with LONG gun.
            else if (weapons[activeWeapon] != null && weapons[activeWeapon].type == InteractiveWeapon.WeaponType.LONG) {
                leftArm.localEulerAngles += LeftArmLongGuard;
            }
        }

        // Correct left arm position when aiming with a SHORT gun.
        else if (isAiming && weapons[activeWeapon] != null &&
                 weapons[activeWeapon].type == InteractiveWeapon.WeaponType.SHORT) {
            leftArm.localEulerAngles += LeftArmShortAim;
        }
    }
}