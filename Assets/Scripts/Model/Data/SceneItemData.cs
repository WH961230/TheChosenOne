﻿using System;
using UnityEngine;

public class SceneItemData : Data {
    public string MySceneItemSign;
    public Sprite MyBackpackSprite;
    public SceneItemType MySceneItemType;
}

public enum SceneItemType {
    MainWeapon,
    SideWeapon,
    Consume,
    Equipment,
}