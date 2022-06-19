using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GenerateScriptEditor : EditorWindow {
    private const string DataPath = "Assets/Scripts/Data/";
    private const string EntityPath = "Assets/Scripts/Entity/";
    private const string GameObjPath = "Assets/Scripts/GameObj/";
    private const string WindowPath = "Assets/Scripts/Window/";

    private static string TemplateDataPath = "Assets/Configs/Template/GenerateDataScript.txt";
    private static string TemplateEntityPath = "Assets/Configs/Template/GenerateEntityScript.txt";
    private static string TemplateGameObjPath = "Assets/Configs/Template/GenerateGameObjScript.txt";
    private static string TemplateWindowPath = "Assets/Configs/Template/GenerateWindowScript.txt";

    private string text;
    [MenuItem("Assets/自动生成模板类")]
    public static void GenerateScriptWindow() {
        // 弹出创建类命
        Rect _rect = new Rect(1000, 1000, 500, 100);
        GenerateScriptEditor window = (GenerateScriptEditor)EditorWindow.GetWindowWithRect(typeof(GenerateScriptEditor), _rect, true, "Window2 name");
        window.Show();
    }

    private void GenerateScript(string className) {
        if (string.IsNullOrEmpty(className)) {
            Debug.LogError("class Name is Null");
            return;
        }

        if (TemplateDataPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(TemplateDataPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);

            var createPath = $"{DataPath}{className}Data.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
        }

        if (TemplateEntityPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(TemplateEntityPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);
            log = Regex.Replace(log, "#ClassParamName#", className.ToLower());

            var createPath = $"{EntityPath}{className}Entity.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
        }

        if (TemplateGameObjPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(TemplateGameObjPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);
            log = Regex.Replace(log, "#ClassParamName#", className.ToLower());

            var createPath = $"{GameObjPath}/{className}GameObj.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
        }

        if (TemplateWindowPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(TemplateWindowPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();

            log = Regex.Replace(log, "#ClassName#", className);

            var createPath = $"{WindowPath}/{className}Window.cs";
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