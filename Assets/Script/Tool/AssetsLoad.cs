using UnityEditor;
using UnityEngine;

public static class AssetsLoad {
    public static Object Load<T>(string path) {
        return AssetDatabase.LoadAssetAtPath(path, typeof(T));
    } 
}