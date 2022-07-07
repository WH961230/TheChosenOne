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

    public static int CharacterCamera = -1;
    public static CameraComponent CharacterCameraComponent;

    // Light
    public static Light MainLight;

    // Log Open or not
    public static bool IfShowLog;

    // MainCharacter
    public static int MainCharacater = -1;
    public static CharacterComponent MainCharacterComponent;
}