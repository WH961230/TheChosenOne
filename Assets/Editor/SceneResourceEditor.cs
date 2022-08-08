using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneResourceEditor : EditorWindow {
    private GameObject itemObj;
    private string itemName;
    private Texture itemPic;
    
    #region 收集

    [MenuItem("点这里/收集/收集【场景建筑位置】到【配置】")]
    public static void GatherBuildingInfoToConfig() {
        var buildingList = FindObjectsOfType<BuildingComponent>();
        var soBuilding = Resources.Load<SOBuildingSetting>(PathData.SOBuildingSettingPath);
        soBuilding.MyBuildingMapInfoList.Clear();
        // 遍历场景物体到集合
        foreach (var building in buildingList) {
            List<BuildingInfo> tempList = soBuilding.MyBuildingMapInfoList;
            var pos = building.transform.position;
            var rat = building.transform.rotation;
            tempList.Add(new BuildingInfo() {
                Sign = building.name, 
                Point = pos, 
                Quaternion = rat,
            });
        }

        EditorUtility.SetDirty(soBuilding);
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
    public static void GatherItemInfoToConfig() {
        SOData.Init();

        var itemList = FindObjectsOfType<ItemComponent>();
        SOData.MySOItemSetting.MyMapInfo.Clear();

        foreach (var item in itemList) {
            ItemType[] itemType = Enum.GetValues(typeof(ItemType)) as ItemType[];
            System.Random random = new System.Random();

            SOData.MySOItemSetting.MyMapInfo.Add(new ItemMapInfo() {
                Point = item.transform.position,
                Quaternion = item.transform.rotation,
                MyItemType = itemType[UnityEngine.Random.Range(0, itemType.Length)],
            });
        }

        EditorUtility.SetDirty(SOData.MySOItemSetting);
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
        itemObj = (GameObject)EditorGUILayout.ObjectField("拖入创建的物体", itemObj, typeof(GameObject) , GUILayout.Width(500));
        itemPic = (Texture)EditorGUILayout.ObjectField("物体图标", itemPic, typeof(GameObject), GUILayout.Width(800));
        itemName = EditorGUILayout.TextField("物体名称", GUILayout.Width(500));
        if (GUILayout.Button("创建场景物体到配置", GUILayout.Width(300))) {
            Debug.Log(itemObj);
        }
    }
}