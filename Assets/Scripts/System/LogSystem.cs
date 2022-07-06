using UnityEngine;

public static class LogSystem {
    public static void Print(string content) {
        if (GameData.IfShowLog) {
            Debug.Log(content);
        }
    }

    public static void PrintE(string content) {
        if (GameData.IfShowLog) {
            Debug.LogError(content);
        }
    }
}