using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackpackComponent : GameComp {
    [Header("背包按钮")]
    public Button MyUIBackpackBtn;
    [Header("背包关闭按钮")]
    public Button MyUIBackpackCloseBtn;
    [Header("背包窗口")]
    public GameObject MyUIBackpackWindow;

    [Header("主武器图片")]
    public MyContent[] MyUIBackpackMainWeaponImages = new MyContent[2];
    [Header("副武器图片")]
    public MyContent MyUIBackpackSideWeaponImage;
    [Header("消耗品图片")]
    public List<MyContent> MyUIBackpackConsumeImages = new List<MyContent>();
    [Header("装备图片")]
    public MyContent[] MyUIBackpackEquipmentImages = new MyContent[4];
}

[Serializable]
public struct MyContent {
    public Button MyButton;
}