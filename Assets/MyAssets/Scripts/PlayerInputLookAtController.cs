using System;
using UnityEngine;

public class PlayerInputLookAtController : MonoBehaviour {
    [Header("距离玩家节点的偏移高度")] public float OffHeightWithPlayer;
    [Header("玩家朝向物体")] public Transform LookAtTr;
    [Header("玩家朝向速度")] public float LookAtLerpSpeed;
    [Header("MouseX 速度")] public float MouseXSpeed;
    [Header("MouseY 速度")] public float MouseYSpeed;
    private Transform playerTr;
    private float followTargetRotationY;
    private float followTargetRotationX;

    private void Start() {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        playerTr.root.GetComponent<PlayerInputMovementController>().FollowTargetTr = this.LookAtTr;
        LookAtTr.rotation = transform.rotation;
    }

    private void Update() {
        transform.position = playerTr.transform.position + Vector3.up * OffHeightWithPlayer;

        followTargetRotationY += CustomInputSystem.GetAxis_MouseX * MouseXSpeed;
        followTargetRotationX -= CustomInputSystem.GetAxis_MouseY * MouseYSpeed;
        transform.rotation = Quaternion.Euler(followTargetRotationX, followTargetRotationY, 0);

        Vector3 VecW = CustomInputSystem.GetKey_W ? transform.forward : Vector3.zero;
        Vector3 VecS = CustomInputSystem.GetKey_S ? -transform.forward : Vector3.zero;
        Vector3 VecA = CustomInputSystem.GetKey_A ? -transform.right : Vector3.zero;
        Vector3 VecD = CustomInputSystem.GetKey_D ? transform.right : Vector3.zero;

        Vector3 tempVec = Vector3.ProjectOnPlane(VecW + VecS + VecA + VecD, Vector3.up);
        LookAtTr.position = transform.position;
        LookAtTr.forward = Vector3.Lerp(LookAtTr.forward, tempVec, Time.deltaTime * LookAtLerpSpeed);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(LookAtTr.position, LookAtTr.position + LookAtTr.forward);
    }
#endif
}