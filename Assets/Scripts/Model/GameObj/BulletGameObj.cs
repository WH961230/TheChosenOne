﻿using UnityEditor;
using UnityEngine;

public class BulletGameObj : GameObj {
    private BulletData bulletData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        bulletData = (BulletData)data;
    }
}