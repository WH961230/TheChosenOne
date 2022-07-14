using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackpackComponent : MonoBehaviour {
    [Header("背包按钮")]
    public Button MyUIBackpackBtn;
    [Header("背包关闭按钮")]
    public Button MyUIBackpackCloseBtn;
    [Header("背包窗口")]
    public GameObject MyUIBackpackWindow;

    [Header("主武器图片")]
    public Image[] MyUIBackpackMainWeaponImages = new Image[2];
    [Header("副武器图片")]
    public Image MyUIBackpackSideWeaponImage;
    [Header("消耗品图片")]
    public List<Image> MyUIBackpackConsumeImages = new List<Image>();
    [Header("装备图片")]
    public Image[] MyUIBackpackEquipmentImages = new Image[4];

    [Header("武器丢弃按钮")]
    public Button MyUIBackpackMainWeaponDropBtn_1;
    public Button MyUIBackpackMainWeaponDropBtn_2;
    public Button MyUIBackpackSideWeaponDropBtn;

    [Header("装备丢弃按钮")]
    public Button MyUIBackpackEquipment_1;
    public Button MyUIBackpackEquipment_2;
    public Button MyUIBackpackEquipment_3;
    public Button MyUIBackpackEquipment_4;
}