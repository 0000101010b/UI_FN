using UnityEngine;
using System.Collections;
using UnityEditor;

public class EventCreator : EditorWindow
{

    Vector2 scrollPos;

    public string[] scope = new string[] { "LOCAL", "GLOBAL" };
    public int sIndex = 0;
    public string[] category = new string[] { "POLITICS", "BUSINESS", "SCIENCE", "ARTS", "HEALTH", "SPORTS" };
    public int cIndex = 0;

    public string topic;
    public string subjective;
    public string event_name;
    [MenuItem("Fake News/Event Creator")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(EventCreator));
        window.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(300), GUILayout.Height(300));


        EditorGUILayout.LabelField("Event", EditorStyles.boldLabel);

        event_name = EditorGUILayout.TextField("Event name", event_name);
        topic = EditorGUILayout.TextField("Topic", topic);
        subjective = EditorGUILayout.TextField("Subjective", subjective);

        EditorGUILayout.LabelField("Scope", EditorStyles.centeredGreyMiniLabel);
        sIndex = EditorGUILayout.Popup(sIndex, scope);

        EditorGUILayout.LabelField("Category", EditorStyles.centeredGreyMiniLabel);
        cIndex = EditorGUILayout.Popup(cIndex, category);

        //Thing.ShowEditGUI();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Create"))
            CreateArticle();
    }
    void CreateArticle()
    {

    }
}