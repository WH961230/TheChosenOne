using UnityEngine;

public class SceneItemComponent : MonoBehaviour {
    public string SceneItemSign;
    public int MySceneItemNum;
    public SceneItemType MySceneItemType;
}

public enum SceneItemType {
    CONSUME,
    WEAPON,
    EQUIPMENT,
}