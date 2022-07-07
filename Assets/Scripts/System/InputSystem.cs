using System;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : GameSys {
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
    }

    public override void Update() {
        base.Update();
    }

    public override void Clear() {
        base.Clear();
    }

    public bool GetKey(KeyCode keyCode) {
        return Input.GetKey(keyCode);
    }

    public bool GetKeyDown(KeyCode keyCode) {
        return Input.GetKeyDown(keyCode);
    }

    public float GetAxis(string name) {
        return Input.GetAxis(name);
    }
}