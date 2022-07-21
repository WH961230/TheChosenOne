using UnityEngine;

public static class GameData {
    public static Transform UIRoot;
    public static Transform CharacterRoot;
    public static Transform AudioRoot;
    public static Transform EnvironmentRoot;
    public static Transform ItemRoot;
    public static Transform CameraRoot;
    public static Transform LightRoot;

    public static int MainCamera = 0;
    public static int MainCharacterCamera = 0;

    public static Light MainLight;

    public static int IsShowLog;

    public static int MainCharacterId = 0;

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