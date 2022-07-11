using UnityEngine;

public static class LogSystem {
    public static void Print(string content) {
        if (GameData.IsShowLog == 1) {
            Debug.Log($"【LOG:{content}】");
        }
    }

    public static void PrintE(string content) {
        if (GameData.IsShowLog == 2) {
            Debug.LogError($"【ERROR:{content}】");
        }
    }
}