using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public static class t
{
    public static float[] _pref_religion = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_ethnicity = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_gender = { 0.0f, 0.0f };
    public static float[] _pref_sexual_identity = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_class = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public static float[] _pref_nationality = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
        0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,
    0.0f,0.0f,0.0f};
    public static float[] _pref_political = { 0.0f, 0.0f, 0.0f, 0.0f };



#if UNITY_EDITOR
    public static void ShowEditGUI()
    {
        EditorGUILayout.LabelField("Preference Religion", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eReligion.HINDU + 1; i++)
        {
            _pref_religion[i] = EditorGUILayout.FloatField(((eReligion)i).ToString(), _pref_religion[i]);
        }
        EditorGUILayout.LabelField("Preference Gender", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eGender.FEMALE + 1; i++)
        {
            _pref_gender[i] = EditorGUILayout.FloatField(((eGender)i).ToString(), _pref_gender[i]);
        }
        EditorGUILayout.LabelField("Preference Nationality", EditorStyles.boldLabel);
        for (int i = 0; i < (int)eNationality.KAZAKHSTANI + 1; i++)
        {
            _pref_nationality[i] = EditorGUILayout.FloatField(((eNationality)i).ToString(), _pref_nationality[i]);
        }
        EditorGUILayout.LabelField("Preference Politics", EditorStyles.boldLabel);
        for (int i = 0; i < (int)ePolitics.COMMUNIST + 1; i++)
        {
            _pref_political[i] = EditorGUILayout.FloatField(((ePolitics)i).ToString(), _pref_political[i]);
        }
    }
#endif

}


[CustomEditor(typeof(t))]
[CanEditMultipleObjects]
public class TweetCreator : EditorWindow
{


    public  string fileName = "tweets.csv";
    Vector2 scrollPos;

    public string[] gender = new string[] { "MALE", "FEMALE" };
    public int gIndex = 0;
    public string[] religion = new string[] { "CHRISTIAN", "MUSLIM", "HINDU", "JEWISH", "BUDDHA", "OTHER", "ATHEIST" };
    public int rIndex = 0;
    public string[] politics = new string[] { "CONSERVATIVE", "LIBERAL", "FACIST", "COMMUNIST" };
    public int pIndex = 0;
    public string[] nationality = new string[] { "AMERICAN", "MEXICAN", "INDIAN", "CHINESE", "CANADIAN", "GERMAN", "BRITISH", "IRISH", "FRENCH", "SPANISH", "AFRICAN", "MALAYSIAN", "KAZAKHSTANI" };
    public int nIndex = 0;
    public string[] race = new string[] {  "WHITE",
    "BLACK",
    "ASIAN",
    "HISPANIC",
    "OTHER" };
    public int raIndex;
    public string[] _class = new string[] {
    "LOW",
    "MIDDLE",
    "HIGH" };
    public int cIndex;


    public string topic;
    public string subjective;
    public string event_;
    [MenuItem("Fake News/Tweet Creator")]
    static void Init()
    {
        
        if (!File.Exists("tweets.csv"))
        {
            Debug.Log("Error: could not read file: " + "tweets.csv");
            /*public string gender;
public string religion;
public string politics;
public string nationality;
public int age;
public eClass c;
public eRace ra;
public eGender g;
public eReligion r;
public ePolitics p;
public eNationality n;*/
            var sr = File.CreateText("Tweets.csv");
            sr.WriteLine( "Topic,"+"subjective,"+"Event,"+"Gender,"+"Race,"+"Religion,"+"class," +"Politics,"+"Nationality,"+ "Pref Religion," + "Pref_ethnicity ,"+"Pref_gender,"+"pref_class,"+"pref_nationality,"+"pref_political");
            sr.Close();
        }

        EditorWindow window = GetWindow(typeof(TweetCreator));
        window.Show();
    }
    void OnGUI()

    {
        
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(300), GUILayout.Height(600));


        EditorGUILayout.LabelField("Tweet", EditorStyles.boldLabel);

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

        EditorGUILayout.LabelField("Race", EditorStyles.centeredGreyMiniLabel);
        raIndex = EditorGUILayout.Popup(nIndex, race);


        t.ShowEditGUI();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create"))
            CreateTweet();


    }
    void CreateTweet()
    {
        string from = fileName;
        string to = fileName;
        List<string> newFile = new List<string>();
        var lines = File.ReadAllLines(from).Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> oldfile = csv.ToList();


        for (int i = 0; i < oldfile.Count; i++)
        {
            List<string> strings = new List<string>();
            for (int j = 0; j < oldfile[i].Length; j++)
            {
                strings.Add(oldfile[i][j][0]);
            }
            string line = string.Join(",", strings.ToArray());
            newFile.Add(line);
        }

        /*
           public float[] pref_religion;
    public float[] pref_ethnicity;
    public float[] pref_gender;
    public float[] pref_class;
    public float[] pref_nationality;
    public float[] pref_political;
    public string gender;
    public string religion;
    public string politics;
    public string nationality;
    public int age;
    public eClass c;
    public eRace ra;
    public eGender g;
    public eReligion r;
    public ePolitics p;
    public eNationality n;
    */

        List<string> l = new List<string>();
        List<string> insideCell=new List<string>();
        string cell;

        l.Add(topic);
        l.Add(subjective);
        l.Add(event_);
        l.Add(((eGender)gIndex).ToString());
        l.Add(((eRace)raIndex).ToString());
        l.Add(((eReligion)rIndex).ToString());
        l.Add(((eClass)rIndex).ToString());
        l.Add( ((ePolitics) pIndex).ToString() );
        l.Add(((eNationality)rIndex).ToString());

        //religion
        for (int i = 0; i < t._pref_religion.Length; i++)
        {
            insideCell.Add(t._pref_religion[i].ToString());
        }
        cell= string.Join(";", insideCell.ToArray());
        l.Add(cell);
        
        //ethnicity
        for (int i = 0; i < t._pref_ethnicity.Length; i++)
        {
            insideCell.Add(t._pref_ethnicity[i].ToString());
        }
        cell = string.Join(";", insideCell.ToArray());
        l.Add(cell);
        
        //gender
        for (int i = 0; i < t._pref_gender.Length; i++)
        {
            insideCell.Add(t._pref_gender[i].ToString());
        }
        cell = string.Join(";", insideCell.ToArray());
        l.Add(cell);

        //class
        for (int i = 0; i < t._pref_class.Length; i++)
        {
            insideCell.Add(t._pref_class[i].ToString());
        }
        cell = string.Join(";", insideCell.ToArray());
        l.Add(cell);

        //nationality
        for (int i = 0; i < t._pref_nationality.Length; i++)
        {
            insideCell.Add(t._pref_nationality[i].ToString());
        }
        cell = string.Join(";", insideCell.ToArray());
        l.Add(cell);

        //political
        for (int i = 0; i < t._pref_political.Length; i++)
        {
            insideCell.Add(t._pref_political[i].ToString());
        }
        cell = string.Join(";", insideCell.ToArray());
        l.Add(cell);



        string row = string.Join(",", l.ToArray());
        newFile.Add(row);

        File.WriteAllLines(to, newFile.ToArray());

        Debug.Log("Created Tweet");
    }
}
