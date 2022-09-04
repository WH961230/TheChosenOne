using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : GameComp {
    public GameObject Head;
    public GameObject Body;
    public CharacterController CC;
    public Animator AnimatorController;
    public List<MeshRenderer> CharacterMeshRenderers;
    public SkinnedMeshRenderer CharacterSkinMeshRenderer;
    [Header("武器槽位")] public Transform MyWeaponPlace;
    [Header("武器模型")] public List<GameObject> MyHoldWeapons;
    public BasicBehaviour BasicBehaviour;
}