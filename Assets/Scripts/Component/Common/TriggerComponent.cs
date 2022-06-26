using System;
using UnityEngine;
/// <summary>
/// 处理触发器相关的
/// </summary>
public class TriggerComponent : MonoBehaviour {
    public Action<Collider> OnTriggerEnterAction;
    public Action<Collider> OnTriggerExitAction;
    public Action<Collider> OnTriggerStayAction;
    
    public Action<Collision> OnColliderEnterAction;
    public Action<Collision> OnColliderExitAction;
    public Action<Collision> OnColliderStayAction;

    private void OnTriggerEnter(Collider other) {
        if (null != OnTriggerEnterAction) {
            OnTriggerEnterAction(other);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (null != OnTriggerStayAction) {
            OnTriggerStayAction(other);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (null != OnTriggerExitAction) {
            OnTriggerExitAction(other);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (null != OnColliderEnterAction) {
            OnColliderEnterAction(other);
        }
    }

    private void OnCollisionStay(Collision other) {
        if (null != OnColliderStayAction) {
            OnColliderStayAction(other);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (null != OnColliderExitAction) {
            OnColliderExitAction(other);
        }
    }
}