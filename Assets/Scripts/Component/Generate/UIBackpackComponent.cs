using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackpackComponent : MonoBehaviour {
    public Button MyUIBackpackBtn;
    public GameObject MyUIBackpackWindow;
    public Button MyUIBackpackCloseBtn;
    public Image[] MyUIBackpackMainWeaponImages = new Image[2];
    public Image MyUIBackpackSideWeaponImage;
    public Button MyUIBackpackMainWeaponDropBtn_1;
    public Button MyUIBackpackMainWeaponDropBtn_2;
    public Button MyUIBackpackSideWeaponDropBtn;
    public List<Image> MyUIBackpackConsumeImages = new List<Image>();
}