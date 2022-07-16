using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SOSceneBuildingSetting")]
public class SOSceneBuildingSetting : ScriptableObject {
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
        return MySceneBuildingOfficialPrefabInfoList;
    }
    
    public List<SceneBuildingInfo> GetSceneBuildingInfoList() {
        return MySceneBuildingOfficialInfoList;
    }
}

[Serializable]
public struct SceneBuildingInfo {
    public string Sign;
    public Vector3 Point;
    public Quaternion Quaternion;
}