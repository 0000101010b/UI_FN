using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[ExecuteInEditMode]
public static class a
{
    public static float[] _pref_religion = { 0.0f, 0.0f, 0.0f, 0.0f,0.0f };
    public static float[] _pref_ethnicity = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_gender = { 0.0f, 0.0f };
    public static float[] _pref_sexual_identity = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_class = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_nationality = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,0.0f };
    public static float[] _pref_political = { 0.0f, 0.0f, 0.0f, 0.0f };



#if UNITY_EDITOR
    public static void ShowEditGUI()
    {
        EditorGUILayout.LabelField("Preference Religion", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eReligion.HINDU+1; i++)
        {
            _pref_religion[i] = EditorGUILayout.FloatField(((eReligion)i).ToString() , _pref_religion[i]);
        }
        EditorGUILayout.LabelField("Preference Gender", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eGender.FEMALE+1; i++)
        {
            _pref_gender[i] = EditorGUILayout.FloatField(((eGender)i).ToString(), _pref_gender[i]);
        }
        EditorGUILayout.LabelField("Preference Nationality", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eNationality.KAZAKHSTANI+1; i++)
        {
            _pref_nationality[i] = EditorGUILayout.FloatField(((eNationality)i).ToString(), _pref_nationality[i]);
        }
        EditorGUILayout.LabelField("Preference Politics", EditorStyles.boldLabel);
        for (int i = 0; i < (int)ePolitics.COMMUNIST+1; i++)
        {
            _pref_political[i] = EditorGUILayout.FloatField(((ePolitics)i).ToString(), _pref_political[i]);
        }
    }
#endif
}





[CustomEditor(typeof(a))]
[CanEditMultipleObjects]
public class ArticleCreator : EditorWindow {

    Vector2 scrollPos;

    public string[] gender = new string[] { "MALE", "FEMALE","NONE"};
    public int gIndex = 0;
    public string[] religion = new string[] { "CHRISTIAN", "MUSLIM", "JEWISH", "BUDDHA", "HINDU","NONE" };
    public int rIndex = 0;
    public string[] politics = new string[] { "CONSERVATIVE", "LIBERAL", "FACIST", "COMMUNIST","NONE" };
    public int pIndex = 0;
    public string[] nationality = new string[] { "AMERICAN", "BRITISH", "SPANISH", "CHINESE", "IRISH", "INDIAN", "MALAYSIAN", "FRENCH", "KAZAKHSTANI","NONE" };
    public int nIndex = 0;

    public string topic;
    public string subjective;
    public string event_;
    [MenuItem("Fake News/Article Creator")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(ArticleCreator));
        window.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(300), GUILayout.Height(600));


        EditorGUILayout.LabelField("Article", EditorStyles.boldLabel);

        topic = EditorGUILayout.TextField("Topic", topic);
        subjective = EditorGUILayout.TextField("Subjective", subjective);
        event_ = EditorGUILayout.TextField("Event", event_);

        EditorGUILayout.LabelField("Gender", EditorStyles.centeredGreyMiniLabel);
        gIndex = EditorGUILayout.Popup(gIndex, gender);

        EditorGUILayout.LabelField("Religion", EditorStyles.centeredGreyMiniLabel);
        rIndex = EditorGUILayout.Popup(rIndex, religion);

        EditorGUILayout.LabelField("Politics", EditorStyles.centeredGreyMiniLabel);
        pIndex = EditorGUILayout.Popup(pIndex, politics);

        EditorGUILayout.LabelField("Nationality", EditorStyles.centeredGreyMiniLabel);
        nIndex = EditorGUILayout.Popup(nIndex, nationality);

        a.ShowEditGUI();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create"))
            CreateArticle();


    }
    void CreateArticle()
    {
       
    }
}
