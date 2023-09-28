using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireCursorController : MonoBehaviour {
    [SerializeField] private RectTransform CursorTr;
    [SerializeField] private AnimationCurve CursorCurve;
    private Vector3 rectScale;
    public static FireCursorController Ins;
    public float TotalTime;
    public bool isOpen;
    public float time;

    private void Awake() {
        Ins = this;
    }

    void Start() {
        rectScale = CursorTr.localScale;
    }

    void Update() {
        if (isOpen) {
            if (time < TotalTime) {
                time += Time.deltaTime;
                float value = CursorCurve.Evaluate(time);
                CursorTr.localScale = new Vector3(value, value, value);
            } else {
                FireCursor(false);
            }
        } else {
            CursorTr.localScale = Vector3.Lerp(CursorTr.localScale, rectScale, Time.deltaTime);
        }
    }

    public void FireCursor(bool isOpen) {
        if (isOpen) {
            this.isOpen = true;
            time = 0;
        } else {
            this.isOpen = false;
        }
    }
}