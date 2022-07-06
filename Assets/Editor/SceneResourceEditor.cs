using UnityEditor;
using UnityEngine;

public class SceneResourceEditor : EditorWindow {
    private GameObject sceneItemObj;
    private string sceneItemName;
    private Texture sceneItemPic;
    
    #region 收集

    [MenuItem("Assets/收集/收集场景建筑信息到配置")]
    public static void GatherSceneBuildingInfoToConfig() {
        var sceneBuildingList = FindObjectsOfType<SceneBuildingComponent>();
        var soSceneBuilding = Resources.Load<SOSceneBuildingSetting>(PathData.SOSceneBuildingSettingPath);
        soSceneBuilding.MySceneBuildingInfoList.Clear();
        // 遍历场景物体到集合
        foreach (var building in sceneBuildingList) {
            var pos = building.transform.position;
            var rat = building.transform.rotation;
            foreach (var buildingInfo in soSceneBuilding.MySceneBuildingPrefabInfoList) {
                if (building.name.Contains(buildingInfo.name)) {
                    soSceneBuilding.MySceneBuildingInfoList.Add(new SceneBuildingInfo() {
                        Sign = buildingInfo.name,
                        Point = pos,
                        Quaternion = rat,
                    });
                }
            }
        }
        EditorUtility.SetDirty(soSceneBuilding);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/收集/收集玩家信息到配置")]
    public static void GatherCharacterInfoToConfig() {
        // 角色生成点信息
        var character = FindObjectOfType<CharacterComponent>();
        var soCharacter = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        soCharacter.CharacterInfo.MyCharacterPoint = character.transform.position;
        soCharacter.CharacterInfo.MyCharacterQuaternion = character.transform.rotation;
        EditorUtility.SetDirty(soCharacter);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/收集/收集场景物资信息到配置")]
    public static void GatherSceneItemInfoToConfig() {
        var sceneItemList = FindObjectsOfType<SceneItemComponent>();
        var soSceneItem = Resources.Load<SOSceneItemSetting>(PathData.SOSceneItemSettingPath);
        soSceneItem.MySceneItemInfoList.Clear();
        foreach (var item in sceneItemList) {
            soSceneItem.MySceneItemInfoList.Add(new SceneItemInfo() {
                Point = item.transform.position,
                Quaternion = item.transform.rotation,
            });
        }
        EditorUtility.SetDirty(soSceneItem);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    #endregion

    #region 创建

    [MenuItem("Assets/创建/创建场景物品信息到配置")]
    public static void CreateSceneItemToConfig() {
        Rect _rect = new Rect(1000, 1000, 500, 200);
        SceneResourceEditor window = (SceneResourceEditor)GetWindowWithRect(typeof(SceneResourceEditor), _rect, true, "Window2 name");
        window.Show();
    }

    #endregion

    public void OnGUI() {
        sceneItemObj = (GameObject)EditorGUILayout.ObjectField("拖入创建的物体", sceneItemObj, typeof(GameObject) , GUILayout.Width(500));
        sceneItemPic = (Texture)EditorGUILayout.ObjectField("物体图标", sceneItemPic, typeof(GameObject), GUILayout.Width(800));
        sceneItemName = EditorGUILayout.TextField("物体名称", GUILayout.Width(500));
        if (GUILayout.Button("创建场景物体到配置", GUILayout.Width(300))) {
            Debug.Log(sceneItemObj);
        }
    }
}