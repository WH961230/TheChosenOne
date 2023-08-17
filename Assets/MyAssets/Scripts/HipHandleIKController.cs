using UnityEngine;

public class HipHandleIKController : MonoBehaviour {
    public Transform IkRotateFollowTr;
    void Start() {
    }

    void Update() {
        Vector3 thisEulerAngles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(IkRotateFollowTr.eulerAngles.x, thisEulerAngles.y, thisEulerAngles.z);
    }
}