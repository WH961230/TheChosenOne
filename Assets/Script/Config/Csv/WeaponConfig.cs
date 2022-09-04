using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class WeaponConfig {
    public class Weapon {
        public int id;
        public string name;
        public int type;
        public int mode;
        public int mag;
        public int totalBullet;
        public string prefabPath;
        public string shotSoundPath;
        public string reloadSoundPath;
        public string pickSoundPath;
        public string dropSoundPath;
        public string noBulletPath;
        public float bulletDamage;
    }

    private static List<Weapon> info = new List<Weapon>();
    public static void Init() {
        StreamReader stream = new StreamReader("Assets/Resources/Config/Csv/Weapon.csv");
        bool endFile = false;
        int index = 0;
        while (!endFile) {
            string data_String = stream.ReadLine();
            if (data_String == null) {
                endFile = true;
                break;
            }

            var data_Value = data_String.Split(',');
            if (index > 2) {
                info.Add(new Weapon() {
                    id = int.Parse(data_Value[0]),
                    name = data_Value[1],
                    type = int.Parse(data_Value[2]),
                    mode = int.Parse(data_Value[3]),
                    mag = int.Parse(data_Value[4]),
                    totalBullet = int.Parse(data_Value[5]),
                    prefabPath = data_Value[6],
                    shotSoundPath = data_Value[7],
                    reloadSoundPath = data_Value[8],
                    pickSoundPath = data_Value[9],
                    dropSoundPath = data_Value[10],
                    noBulletPath = data_Value[11],
                    bulletDamage = float.Parse(data_Value[12]),
                });
            }
            index++;
        }
    }

    public static List<Weapon> GetAll() {
        return info;
    }

    public static Weapon GetIndex(int index) {
        return info[index];
    }

    public static Weapon Get(int id) {
        foreach (var i in info) {
            if (i.id == id) {
                return i;
            }
        }

        return null;
    }
}