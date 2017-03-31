using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class EventCreator : EditorWindow
{

    public string fileName="events.csv";
    Vector2 scrollPos;

    public string topic;
    public string event_name;
    [MenuItem("Fake News/Event Creator")]
    static void Init()
    {
        if (!File.Exists("events.csv"))
        {
            Debug.Log("Error: could not read file: " + "events.csv");

            var sr = File.CreateText("events.csv");
            sr.WriteLine("Topic,"+"Event");
            sr.Close();
        }
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

        //Thing.ShowEditGUI();
        
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Create"))
            CreateEvent();
    }
    void CreateEvent()
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
        List<string> l = new List<string>();
        l.Add(topic);
        l.Add(event_name);


        string row = string.Join(",", l.ToArray());
        newFile.Add(row);
        File.WriteAllLines(to, newFile.ToArray());

        Debug.Log("created Event");
    }
}