using UnityEngine;

public abstract class SystemSettingBase : ScriptableObject {
    public virtual GameSys OnInit(GameSystem gameSystem) {
        return null;
    }
}