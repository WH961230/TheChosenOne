using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GenerateScriptEditor : EditorWindow {
    private string className;
    private bool isHaveData; // 是否有数据
    private bool isHaveEntity; // 是否有实体
    private bool isHaveGameObj; // 是否有游戏物体
    private bool isHaveWindow; // 是否有窗口
    private bool isHaveComponent; // 是否有组件
    private bool isHaveSOSetting; // 是否有配置

    #region 生成

    [MenuItem("Assets/生成/测试获取指定路径下的物体")]
    public static void AA() {
        string t = "Assets/Resources/Prefabs/Building";
        var s = AssetDatabase.FindAssets("t:prefab", new string[] {
            t
        });
        foreach (var id in s) {
            var guidToAssetPath = AssetDatabase.GUIDToAssetPath(id);
            var o = AssetDatabase.LoadAssetAtPath<GameObject>(guidToAssetPath);
            GameObject.Instantiate(o);
            Debug.Log(guidToAssetPath);
        }
    }

    [MenuItem("Assets/生成/自动生成模板类")]
    public static void GenerateScriptWindow() {
        // 弹出创建类命
        Rect _rect = new Rect(1000, 1000, 500, 200);
        GenerateScriptEditor window = (GenerateScriptEditor)GetWindowWithRect(typeof(GenerateScriptEditor), _rect, true, "Window2 name");
        window.Show();
    }

    private void GenerateScript(string className) {
        if (string.IsNullOrEmpty(className)) {
            Debug.LogError("class Name is Null");
            return;
        }

        if (isHaveData) {
            CreateStript(PathData.DataTemplatePath, PathData.DataPath, className, "", "Data");
        }

        if (isHaveEntity) {
            CreateStript(PathData.EntityTemplatePath, PathData.EntityPath, className, "", "Entity");
        }

        if (isHaveGameObj) {
            CreateStript(PathData.GameObjTemplatePath, PathData.GameObjPath, className, "", "GameObj");
        }

        if (isHaveWindow) {
            CreateStript(PathData.WindowTemplatePath, PathData.WindowPath, className, "", "Window");
        }

        if (isHaveComponent) {
            CreateStript(PathData.ComponentTemplatePath, PathData.ComponentPath, className, "", "Component");
        }

        if (isHaveSOSetting) {
            CreateStript(PathData.SOSettingTemplatePath, PathData.SOSettingPath, className, "SO", "Setting");
        }
    }

    private static void CreateStript(string inputPath, string outputPath, string className, string front, string end) {
        if (inputPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(inputPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);
            log = Regex.Replace(log, "#ClassParamName#", className.ToLower());

            var createPath = $"{outputPath}{front}{className}{end}.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
        }
    }

    #endregion

    private void OnGUI() {
        className = EditorGUILayout.TextField("输入文字:", className, GUILayout.Width(300));
        isHaveWindow = EditorGUILayout.Toggle("是否创建窗口类", isHaveWindow);
        isHaveSOSetting = EditorGUILayout.Toggle("是否创建配置类", isHaveSOSetting);
        isHaveData = EditorGUILayout.Toggle("是否创建数据类", isHaveData);
        isHaveComponent = EditorGUILayout.Toggle("是否创建组件类", isHaveComponent);
        isHaveGameObj = EditorGUILayout.Toggle("是否创建游戏物体类", isHaveGameObj);
        isHaveEntity = EditorGUILayout.Toggle("是否创建实体类", isHaveEntity);
        if (GUILayout.Button("生成脚本", GUILayout.Width(100))) {
            this.GenerateScript(className);
        }
    }
}