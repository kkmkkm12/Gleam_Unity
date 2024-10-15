/*using UnityEditor;
using UnityEngine;

public class HariboEditorScript
{
    [MenuItem("Tools/My Custom Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(HariboEditorWindow));
    }
}

public class HariboEditorWindow : EditorWindow
{
    private string myString = "출력하고 싶은거 쓰삼(테스트용)";

    void OnGUI()
    {
        GUILayout.Label("My Custom Editor Tool", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Input:", myString);

        if (GUILayout.Button("Print"))
        {
            Debug.Log(myString);
        }
    }
}*/

