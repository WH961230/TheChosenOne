using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/SOBuildingSetting")]
public class SOBuildingSetting : ScriptableObject {
    public List<GameObject> MyBuildingPrefabInfoList;
    public List<BuildingInfo> MyBuildingMapInfoList;

    public GameObject GetBuildingBySign(string sign) {
        List<GameObject> tempList = GetBuildingPrefabList();
        foreach (var building in tempList) {
            if (sign.Contains(building.name)) {
                return building;
            }
        }

        return null;
    }

    private List<GameObject> GetBuildingPrefabList() {
        return MyBuildingPrefabInfoList;
    }
}

[Serializable]
public struct BuildingInfo {
    public string Sign;
    public Vector3 Point;
    public Quaternion Quaternion;
}