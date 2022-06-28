using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 自动生成脚本工具
/// 弹出窗口
/// 根据输入的实体名称分别创建：
/// 【Data】【Entity】【Window】【Data】【Component】脚本
/// </summary>
public class GenerateScriptEditor : EditorWindow {
    private string text;
    [MenuItem("Assets/自动生成模板类")]
    public static void GenerateScriptWindow() {
        // 弹出创建类命
        Rect _rect = new Rect(1000, 1000, 500, 100);
        GenerateScriptEditor window = (GenerateScriptEditor)GetWindowWithRect(typeof(GenerateScriptEditor), _rect, true, "Window2 name");
        window.Show();
    }

    private void GenerateScript(string className) {
        if (string.IsNullOrEmpty(className)) {
            Debug.LogError("class Name is Null");
            return;
        }

        CreateStript(PathData.DataTemplatePath, PathData.DataPath, className, "Data");
        CreateStript(PathData.EntityTemplatePath, PathData.EntityPath, className, "Entity");
        CreateStript(PathData.GameObjTemplatePath, PathData.GameObjPath, className, "GameObj");
        CreateStript(PathData.WindowTemplatePath, PathData.WindowPath, className, "Window");
        CreateStript(PathData.ComponentTemplatePath, PathData.ComponentPath, className, "Component");
    }

    /// <summary>
    /// 创建脚本
    /// </summary>
    /// <param name="inputPath">输入模板路径</param>
    /// <param name="outputPath">输出模板路径</param>
    /// <param name="className">类名</param>
    /// <param name="striptType">脚本类型</param>
    private static void CreateStript(string inputPath, string outputPath, string className, string striptType) {
        if (inputPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(inputPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);
            log = Regex.Replace(log, "#ClassParamName#", className.ToLower());

            var createPath = $"{outputPath}{className}{striptType}.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
        }
    }

    private void OnGUI() {
        text = EditorGUILayout.TextField("输入文字:", text, GUILayout.Width(300));
        if (GUILayout.Button("生成脚本", GUILayout.Width(100))) {
            this.GenerateScript(text);
        }
    }
}