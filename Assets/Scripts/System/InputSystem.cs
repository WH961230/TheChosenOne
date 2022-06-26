using UnityEngine;

public static class InputSystem {
    public static bool GetKey(KeyCode keyCode) {
        return Input.GetKey(keyCode);
    }

    public static bool GetKeyDown(KeyCode keyCode) {
        return Input.GetKeyDown(keyCode);
    }
}