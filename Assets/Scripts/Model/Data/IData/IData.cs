using UnityEngine;

public interface IData {
    T GetComponent<T>() where T : MonoBehaviour;
}