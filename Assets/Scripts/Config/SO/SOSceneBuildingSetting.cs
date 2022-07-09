using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneBuildingSetting")]
public class SOSceneBuildingSetting : ScriptableObject {
    public List<GameObject> MySceneBuildingPrefabInfoList;
    public List<SceneBuildingInfo> MySceneBuildingInfoList;

    public List<GameObject> MySceneBuildingOfficialPrefabInfoList;
    public List<SceneBuildingInfo> MySceneBuildingOfficialInfoList;

    public GameObject GetSceneBuildingBySign(string sign) {
        List<GameObject> tempList = GetSceneBuildingPrefabList();
        foreach (var building in tempList) {
            if (sign.Contains(building.name)) {
                return building;
            }
        }

        return null;
    }

    public List<GameObject> GetSceneBuildingPrefabList() {
        if (GameData.IsOfficial) {
            return MySceneBuildingOfficialPrefabInfoList;
        } else {
            return MySceneBuildingPrefabInfoList;
        }
    }
    
    public List<SceneBuildingInfo> GetSceneBuildingInfoList() {
        if (GameData.IsOfficial) {
            return MySceneBuildingOfficialInfoList;
        } else {
            return MySceneBuildingInfoList;
        }
    }
}

[Serializable]
public struct SceneBuildingInfo {
    public string Sign;
    public Vector3 Point;
    public Quaternion Quaternion;
}