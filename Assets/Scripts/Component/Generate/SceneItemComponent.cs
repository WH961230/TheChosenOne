using UnityEngine;

public class SceneItemComponent : MonoBehaviour {
    public int MySceneItemId;
    public string SceneItemSign;
    public int MySceneItemNum;
}

public enum SceneItemType {
    CONSUME,
    WEAPON,
    EQUIPMENT,
}