using UnityEngine;

public static class GameData {
    // Root
    public static Transform UIRoot;
    public static Transform CharacterRoot;
    public static Transform AudioRoot;
    public static Transform EnvironmentRoot;
    public static Transform ItemRoot;
    public static Transform CameraRoot;
    public static Transform LightRoot;

    // Camera
    public static int MainCamera = -1;
    public static CameraComponent MainCameraComponent;

    public static int MainCharacterCamera = -1;
    public static CameraComponent CharacterCameraComponent;

    // Light
    public static Light MainLight;

    // Log
    public static int IsShowLog;

    // MainCharacter
    public static int MainCharacater = -1;
    public static CharacterComponent MainCharacterComponent;

    // switch official
    public static bool IsOfficial;

    public static Vector3 GetGround(Vector3 point) {
        RaycastHit hit;
        Ray ray = new Ray(point, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10)) {
            return hit.point;
        }

        return default;
    }
}