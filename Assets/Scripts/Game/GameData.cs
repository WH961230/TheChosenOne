using UnityEngine;

public static class GameData {
    public static Transform UIRoot;
    public static Transform CharacterRoot;
    public static Transform WeaponRoot;
    public static Transform ConsumeRoot;
    public static Transform EquipmentRoot;
    public static Transform AudioRoot;
    public static Transform EnvironmentRoot;
    public static Transform ItemRoot;
    public static Transform CameraRoot;
    public static Transform LightRoot;
    public static Transform EntityRoot;

    public static Light MainLight;

    // 全局参数 ID
    public static int MainCharacterId = 0;
    public static int WeaponCameraId = 0;
    public static int AnimatorId = 0;

    #region 通用方法

    public static Vector3 GetGround(Vector3 point) {
        RaycastHit hit;
        Ray ray = new Ray(point, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10)) {
            return hit.point;
        }

        return default;
    }

    #endregion
}