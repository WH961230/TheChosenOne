using UnityEngine;

public class BulletController : MonoBehaviour {
    public LayerMask LayerMask;
    public float DestroyTime;

    private float DeployDestroyTime;
    private Vector3 lastVec;
    private bool raycast;

    void Start() {
        DeployDestroyTime = 0;
        lastVec = transform.position;
    }

    void Update() {
        if (raycast) {
            transform.gameObject.SetActive(false);
            return;
        }

        float distance = Vector3.Distance(lastVec, transform.position);
        raycast = Physics.Raycast(transform.position, (lastVec - transform.position).normalized, distance, 1 << LayerMask);
        lastVec = transform.position;

        DeployDestroyTime += Time.deltaTime;
        if (DeployDestroyTime > DestroyTime) {
            Destroy(transform.gameObject);
        }
    }
}