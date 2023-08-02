using UnityEngine;

public class PlayerInputLookAtController : MonoBehaviour {
    [Header("距离玩家节点的偏移高度")] public float OffHeightWithPlayer;
    private Transform playerTr;
    private float followTargetRotationY;
    private float followTargetRotationX;

    private void Start() {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        playerTr.root.GetComponent<PlayerInputMovementController>().FollowTargetTr = this.transform;
    }

    private void Update() {
        transform.position = playerTr.transform.position + Vector3.up * OffHeightWithPlayer;

        followTargetRotationY += CustomInputSystem.GetAxis_MouseX;
        followTargetRotationX -= CustomInputSystem.GetAxis_MouseY;
        transform.rotation = Quaternion.Euler(followTargetRotationX, followTargetRotationY, 0);
    }
}