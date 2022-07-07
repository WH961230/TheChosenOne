using System;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : GameSys {
    private List<string> axis = new List<string>();
    private GameSystem gameSystem;
    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
        InstanceAxis();
    }

    public override void Update() {
        base.Update();
        foreach (var code in Enum.GetValues(typeof(KeyCode))) {
            var tmpCode = (KeyCode)code;
            if (GetKey(tmpCode)) {
                gameSystem.MyGameMessageCenter.Dispather(GameMessageConstants.INPUT_KEY, tmpCode);
            }

            if (GetKeyDown(tmpCode)) {
                gameSystem.MyGameMessageCenter.Dispather(GameMessageConstants.INPUT_KEYDOWN, tmpCode);
            }
        }

        foreach (var a in axis) {
            var val = GetAxis(a);
            Debug.Log($"val {val}");
        }
    }

    public override void Clear() {
        base.Clear();
    }

    private void InstanceAxis() {
        axis.Add("Horizontal");
        axis.Add("Vertical");
    }

    private bool GetKey(KeyCode keyCode) {
        return Input.GetKey(keyCode);
    }

    private bool GetKeyDown(KeyCode keyCode) {
        return Input.GetKeyDown(keyCode);
    }

    public float GetAxis(string name) {
        return Input.GetAxis(name);
    }
}