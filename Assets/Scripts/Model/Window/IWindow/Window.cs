using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Window : IWindow {
    protected List<GameObject> TempGO = new List<GameObject>(); 
    protected Game MyGame;
    protected GameSystem MyGS;

    public virtual void Init(Game game, GameObj gameObj) {
        MyGame = game;
        MyGS = game.MyGameSystem;
    }

    protected virtual void ActiveGO(GameObject go, bool active) {
        go.SetActive(active);
    }

    public virtual void OpenAll() {
        foreach (var temp in TempGO) {
            ActiveGO(temp, true);
        }
    }

    public virtual void CloseAll() {
        foreach (var temp in TempGO) {
            ActiveGO(temp, false);
        }
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void LateUpdate() {
    }

    public virtual void Clear() {
    }
    
    protected virtual void RegisterEvent(Button.ButtonClickedEvent e, UnityAction action) {
        e.AddListener(action);
    }

    protected virtual void UnRegisterEvent(Button.ButtonClickedEvent e, UnityAction action) {
        e.RemoveListener(action);
    }

    protected virtual void Invoke(Button.ButtonClickedEvent e) {
        e.Invoke();
    }
}