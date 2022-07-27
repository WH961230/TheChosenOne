using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour {
    public GameObject Head;
    public GameObject Body;
    public CharacterController CC;
    public Animator AnimatorController;

    [Header("武器模型")] 
    public List<GameObject> MyHoldWeapons;
}