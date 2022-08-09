using UnityEngine;

public class BulletComponent : GameComp {
    public BulletType MyBulletType;
}

public enum BulletType {
    BULLET_556,
    BULLET_762,
    BULLET_12,
}