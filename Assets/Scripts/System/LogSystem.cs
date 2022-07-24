using UnityEngine;

public static class LogSystem {
    public static void Print(string content) {
        Debug.Log($"【LOG:{content}】");
    }

    public static void PrintE(string content) {
        Debug.LogError($"【ERROR:{content}】");
    }
}