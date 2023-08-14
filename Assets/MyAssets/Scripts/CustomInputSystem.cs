using UnityEngine;

public class CustomInputSystem {
    public static float GetAxis_MouseX => Input.GetAxis("Mouse X");
    public static float GetAxis_MouseY => Input.GetAxis("Mouse Y");
    public static float GetAxis_Vertical => Input.GetAxis("Vertical");
    public static float GetAxis_Horizontal => Input.GetAxis("Horizontal");
    public static bool GetKey_LeftShift => Input.GetKey(KeyCode.LeftShift);
    public static bool GetKey_LeftCtrl => Input.GetKey(KeyCode.LeftControl);
    public static bool GetKey_W => Input.GetKey(KeyCode.W);
    public static bool GetKey_S => Input.GetKey(KeyCode.S);
    public static bool GetKey_A => Input.GetKey(KeyCode.A);
    public static bool GetKey_D => Input.GetKey(KeyCode.D);
    public static bool GetKeyDown_Space => Input.GetKeyDown(KeyCode.Space);
    public static bool GetMouse_Left => Input.GetMouseButton(0);
    public static bool GetMouse_Right => Input.GetMouseButton(1);
    public static bool GetMouse_Middle => Input.GetMouseButton(2);
}