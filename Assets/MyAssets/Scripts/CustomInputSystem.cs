using UnityEngine;

public class CustomInputSystem {
    public static float GetAxis_MouseX => Input.GetAxis("Mouse X");
    public static float GetAxis_MouseY => Input.GetAxis("Mouse Y");
    public static bool GetKey_LeftShift => Input.GetKey(KeyCode.LeftShift);
    public static float GetAxis_Vertical => Input.GetAxis("Vertical");
    public static float GetAxis_Horizontal => Input.GetAxis("Horizontal");
}