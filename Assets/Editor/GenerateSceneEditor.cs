using UnityEditor;
using UnityEngine;

public class GenerateSceneEditor : MonoBehaviour {
    [MenuItem("Assets/收集场景信息到配置")]
    public static void GenerateSceneInfoToConfig() {
        var sceneItemList = FindObjectsOfType<SceneItemComponent>();
        var soSceneItem = Resources.Load<SOSceneItemSetting>(PathData.SOSceneItemSettingPath);
        soSceneItem.SceneItemInfoList.Clear();
        // 遍历场景物体到集合
        foreach (var item in sceneItemList) {
            var pos = item.transform.position;
            var rat = item.transform.rotation;
            foreach (var itemPrefab in soSceneItem.SceneItemPrefabInfoList) {
                if (string.Equals(item.SceneItemSign, itemPrefab.MyItemSign)) {
                    soSceneItem.SceneItemInfoList.Add(new SceneItemInfo() {
                        MyItemSign = item.SceneItemSign,
                        MySceneItemVector3 = pos,
                        MySceneItemQuaternion = rat,
                    });
                }
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}