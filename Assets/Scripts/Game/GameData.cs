using System.Collections.Generic;
using UnityEngine;

public static class GameData {
    // Root
    public static Transform UIRoot;
    public static Transform CharacterRoot;
    public static Transform AudioRoot;
    public static Transform EnvironmentRoot;
    public static Transform CameraRoot;
    public static Transform LightRoot;
    
    // Camera
    public static int MainCamera = -1;
    public static Camera MainCharacterCamera;
    public static List<int> CharacterCamera = new List<int>();
    
    // Light
    public static Light MainLight;
}