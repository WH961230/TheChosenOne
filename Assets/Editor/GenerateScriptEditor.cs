using UnityEditor;
using UnityEngine;

public class GenerateScriptEditor : EditorWindow {
    private const string DataPath = "Assets/Data/";
    private const string EntityPath = "Assets/Entity/";
    private const string GameObjPath = "Assets/GameObj/";
    
    private static string TemplatePath = "Assets/Configs/Template/GenerateScript.txt";
    
    private string text;
    [MenuItem("Assets/自动生成模板类")]
    public static void GenerateScript() {
        // 弹出创建类命
        Rect _rect = new Rect(1000, 1000, 500, 100);
        GenerateScriptEditor window = (GenerateScriptEditor)EditorWindow.GetWindowWithRect(typeof(GenerateScriptEditor), _rect, true, "Window2 name");
        window.Show();
        
        // // 获取选中的文件名称
        // var obj = Selection.objects;
        // for (var i = 0; i < obj.Length; i++) {
        //     var o = obj[i];
        //     // 获取路径
        //     var path = AssetDatabase.GetAssetPath(o.GetInstanceID());
        //     // 获取完整文件名
        //     var fileName = Path.GetFileName(path);
        //     // 筛选出文件名称后面作为类替换
        //     fileName = fileName.Substring(0, fileName.IndexOf('.'));
        //     if (path.EndsWith(".txt")) {
        //         // 字节流读取
        //         var streamReader = new StreamReader(path);
        //         var log = streamReader.ReadToEnd();
        //         streamReader.Close();
        //
        //         // 替换类名
        //         log = Regex.Replace(log, "#ClassName#", fileName);
        //         var createPath = $"Assets/{fileName}.cs";
        //
        //         // 写入文件
        //         var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
        //         streamWriter.Write(log);
        //         streamWriter.Close();
        //
        //         AssetDatabase.ImportAsset(createPath);
        //         Debug.Log(log);
        //     }
        // }
    }

    private void OnGUI() {
        text = EditorGUILayout.TextField("输入文字:", text, GUILayout.Width(300));
        if (GUILayout.Button("生成脚本", GUILayout.Width(100))) {
        }
    }
}