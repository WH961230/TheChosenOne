using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] public PlayerStateController stateController;
    public static PlayerController Ins;

    private void Awake() {
        Ins = this;
        stateController = GetComponent<PlayerStateController>();
    }

    void Start() {
    }

    void Update() {
    }
}