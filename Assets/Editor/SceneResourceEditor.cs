using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneResourceEditor : EditorWindow {
    private GameObject sceneItemObj;
    private string sceneItemName;
    private Texture sceneItemPic;
    
    #region 收集

    [MenuItem("点这里/收集/收集【场景建筑位置】到【配置】")]
    public static void GatherSceneBuildingInfoToConfig() {
        var sceneBuildingList = FindObjectsOfType<SceneBuildingComponent>();
        var soSceneBuilding = Resources.Load<SOSceneBuildingSetting>(PathData.SOSceneBuildingSettingPath);
        soSceneBuilding.MySceneBuildingOfficialInfoList.Clear();
        // 遍历场景物体到集合
        foreach (var building in sceneBuildingList) {
            List<SceneBuildingInfo> tempList = soSceneBuilding.MySceneBuildingOfficialInfoList;
            var pos = building.transform.position;
            var rat = building.transform.rotation;
            tempList.Add(new SceneBuildingInfo() {
                Sign = building.name, 
                Point = pos, 
                Quaternion = rat,
            });
        }

        EditorUtility.SetDirty(soSceneBuilding);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("点这里/收集/收集【玩家初始点】到【配置】")]
    public static void GatherCharacterInfoToConfig() {
        // 角色生成点信息
        var character = FindObjectOfType<CharacterComponent>();
        var soCharacter = Resources.Load<SOCharacterSetting>(PathData.SOCharacterSettingPath);
        soCharacter.MyCharacterInfo.MyCharacterPoint = character.transform.position;
        soCharacter.MyCharacterInfo.MyCharacterQuaternion = character.transform.rotation;
        EditorUtility.SetDirty(soCharacter);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("点这里/收集/收集【场景物资位置】到【配置】")]
    public static void GatherSceneItemInfoToConfig() {
        SOData.Init();

        var sceneItemList = FindObjectsOfType<SceneItemComponent>();

        SOData.MySOSceneItemSetting.MySceneItemMapInfo.Clear();
        SOData.MySOWeaponSetting.MyWeaponMapInfo.Clear();
        SOData.MySOEquipmentSetting.MyEquipmentMapInfo.Clear();

        foreach (var item in sceneItemList) {
            var rand = Random.Range(0, 3);
            if (rand == 0) {
                SOData.MySOSceneItemSetting.MySceneItemMapInfo.Add(new SceneItemMapInfo() {
                    Point = item.transform.position, Quaternion = item.transform.rotation,
                });
            } else if(rand == 1){
                SOData.MySOWeaponSetting.MyWeaponMapInfo.Add(new WeaponMapInfo() {
                    Point = item.transform.position,
                    Quaternion = item.transform.rotation,
                });
            } else if (rand == 2) {
                SOData.MySOEquipmentSetting.MyEquipmentMapInfo.Add(new EquipmentMapInfo() {
                    Point = item.transform.position,
                    Quaternion = item.transform.rotation,
                });
            }
        }

        EditorUtility.SetDirty(SOData.MySOSceneItemSetting);
        EditorUtility.SetDirty(SOData.MySOWeaponSetting);
        EditorUtility.SetDirty(SOData.MySOEquipmentSetting);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("点这里/收集/收集【灯光位置】到【配置】")]
    public static void GatherLightInfoToConfig() {
        var component = FindObjectOfType<LightComponent>();
        SOData.MySOLightSetting.MainLightInfo.position = component.transform.position;
        SOData.MySOLightSetting.MainLightInfo.rotation = component.transform.rotation;
        EditorUtility.SetDirty(SOData.MySOLightSetting);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    #endregion

    #region 创建

    [MenuItem("点这里/创建/创建场景物品信息到配置")]
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