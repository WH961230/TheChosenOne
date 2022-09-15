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
    private bool isHaveSOSystemSetting; // 是否有系统配置
    private bool isCreateSOSystemSetting; // 是否有实体系统配置
    private static int currentProccess;
    private static int totalCount;

    #region 生成

    [MenuItem("点这里/创建/测试获取指定路径下的物体")]
    public static void AA() {
        string t = "Assets/Resources/Prefabs/Building";
        var s = AssetDatabase.FindAssets("t:prefab", new string[] {
            t
        });
        foreach (var id in s) {
            var guidToAssetPath = AssetDatabase.GUIDToAssetPath(id);
            var o = AssetDatabase.LoadAssetAtPath<GameObject>(guidToAssetPath);
            Instantiate(o);
            Debug.Log(guidToAssetPath);
        }
    }

    [MenuItem("点这里/创建/自动生成模板类 _F1")]
    public static void GenerateScriptWindow() {
        // 弹出创建类命
        Rect _rect = new Rect(1000, 1000, 500, 200);
        GenerateScriptEditor window = (GenerateScriptEditor)GetWindowWithRect(typeof(GenerateScriptEditor), _rect, true, "万物皆可一键创建");
        window.Show();
    }

    private void OnGUI() {
        className = EditorGUILayout.TextField("输入类文字 (注意大小写):", className, new []{GUILayout.Width(300)});
        isHaveWindow = EditorGUILayout.Toggle("是否创建窗口类", isHaveWindow);
        isHaveSOSetting = EditorGUILayout.Toggle("是否创建实体配置类", isHaveSOSetting);
        isHaveData = EditorGUILayout.Toggle("是否创建数据类", isHaveData);
        isHaveComponent = EditorGUILayout.Toggle("是否创建组件类", isHaveComponent);
        isHaveGameObj = EditorGUILayout.Toggle("是否创建游戏物体类", isHaveGameObj);
        isHaveEntity = EditorGUILayout.Toggle("是否创建实体类", isHaveEntity);
        if (GUILayout.Button("开始执行", GUILayout.Width(100))) {
            this.GenerateScript(className);
        }
        isHaveSOSystemSetting = EditorGUILayout.Toggle("是否创建系统配置类", isHaveSOSystemSetting);
        isCreateSOSystemSetting = EditorGUILayout.Toggle("是否创建系统配置实例(配置类编译完成后)", isCreateSOSystemSetting);
    }

    private void GenerateScript(string className) {
        if (string.IsNullOrEmpty(className)) {
            Debug.LogError("class Name is Null");
            return;
        }

        totalCount += isHaveData ? 1 : 0;
        totalCount += isHaveEntity ? 1 : 0;
        totalCount += isHaveGameObj ? 1 : 0;
        totalCount += isHaveWindow ? 1 : 0;
        totalCount += isHaveComponent ? 1 : 0;
        totalCount += isHaveSOSetting ? 1 : 0;

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

        if (isHaveSOSystemSetting) {
            CreateStript(PathData.SOSystemSettingTemplatePath, PathData.SOSettingPath, className, "SO", "SystemSetting");
        }

        if (isCreateSOSystemSetting) {
            CreateSetting(PathData.SOSystemSettingPath, className, "SO", "SystemSetting");
        }

        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog("创建成功", $"创建标志：【{className}】", "确定");
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
            ++currentProccess;
            EditorUtility.DisplayProgressBar("创建中 ...", "", (float)currentProccess / totalCount);
        }
    }

    private static void CreateSetting(string outputPath, string className, string front, string end) {
        var fullName = $"{front}{className}{end}";
        var type = System.Reflection.Assembly.Load("Assembly-CSharp").GetType(fullName);
        var soFile = (UnityEngine.Object) System.Activator.CreateInstance(type);
        AssetDatabase.CreateAsset(soFile, outputPath + fullName + ".asset");
    }

    #endregion
}