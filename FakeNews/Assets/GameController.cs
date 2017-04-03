using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;


enum Round
{
    subject,
    content,
    pause
}

public class GameController : MonoBehaviour {

    //UI
    [Header("UI")]
    public List<Button> cards;
    public GameObject back;
    public Text ArticleTitle;
    public Text pastArticles;
    public PostMaker postMaker;

    //Past

    [Header("Articles")]
    public List<Article> ArticlesWritten;

    //Current State
    Round currentRound = Round.subject;
    delegate void RoundFind(string[][] table);
    delegate void RoundSet();
    RoundFind roundFind;
    RoundSet roundSet;

    [Header("Current State")]
    public string chosenSubject = "";
    public string chosenContent = "";
    public List<string> activeEvents;


    //subject round
    [Header("Subject Round")]
    public string ns_FileName = "newsSubject.csv";
    public string topicFile = "topic.csv";
    public  List<string> potentialSubject;

    //content round
    [Header("Content Round")]
    public string nc_FileName = "newsContent.csv";
    public string subjectFile = "subject.csv";
    public  List<string> potentialContent;


    // Use this for initialization
    void Start() {
        postMaker.share.interactable = false;

        #region Init
        //Set start Events 
        activeEvents = new List<string>();
        activeEvents.Add("WaterGate");
        activeEvents.Add("Election");
        activeEvents.Add("ElectOver");
        activeEvents.Add("Housing Crisis");
        activeEvents.Add("general");

        ArticlesWritten = new List<Article>();

        #region Create Files
        if (!File.Exists(ns_FileName))
        {
            Debug.Log("Error: could not read file: "+ns_FileName);

            var sr = File.CreateText(ns_FileName);
            sr.WriteLine("News subject," + " NewsEvent ");

            for (int i = 0; i < 10; i++)
            {
                sr.WriteLine( "person"  + "," + "subject");
            }

            sr.Close();
        }

        if (!File.Exists(nc_FileName))
        {
            Debug.Log("Error: could not read file: "+nc_FileName);

            var sr = File.CreateText(nc_FileName);
            sr.WriteLine("Subject," + " Content ");

            for (int i = 0; i < 10; i++)
            {
                sr.WriteLine("person" + "," + "content");
            }

            sr.Close();
        }
        #endregion

        CreateCopyTo(ns_FileName, subjectFile);
        CreateCopyTo(nc_FileName, topicFile);


        #endregion Init

        StartSubjectRound();
        
    }

    //News Site interface
    #region Card Selection
    public void SelectCard(int i)
    {
        string text = cards[i].GetComponentInChildren<Text>().text;

        if (text == "xxxxx")
            return;

        if (currentRound == Round.subject)
        {
            chosenSubject = text;
            chosenContent = "";

            StartContentRound();
        }
        else if (currentRound == Round.content)
        {
            chosenContent = text;
            Article a=publishArticle(chosenSubject, chosenContent);
            ArticlesWritten.Add(a);
            
            pastArticles.text += "-" + a.subject + ": " + a.content + "\n";
            
            postMaker.articles.Add(a);
            postMaker.share.interactable = true;
  
            DeleteContent(topicFile,a.subject, a.content);

            StartSubjectRound();
        }

        ArticleTitle.text = chosenSubject + ": " + chosenContent;
        Debug.Log(chosenSubject + ": " + chosenContent);
    }

    private void StartRound(string fName)
    {
        var lines = File.ReadAllLines(fName).Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        csv.ToList().ForEach(j => roundFind(j));

        roundSet();
    }

    public void StartSubjectRound()
    {
        ArticleTitle.text = "";
        back.SetActive(false);

        currentRound = Round.subject;
        roundFind = FindSubject;
        roundSet = SetSubject;
        StartRound(subjectFile);
    }

    private void StartContentRound()
    {
        back.SetActive(true);
        currentRound = Round.content;
        roundFind = FindContent;
        roundSet = SetContent;
        StartRound(topicFile);
    }

    bool isContent = false;
    private void FindSubject(string[][] i)
    {

        string subject = i[0][0];
        string events  = i[1][0];

        foreach (string e in activeEvents)
        {
            if (events.Contains(e) && !potentialSubject.Contains(subject))
            {
                potentialSubject.Add(subject);
            }
        }
    }

    private void checkForContent(string[][] i)
    {
        string subject = i[0][0];
        string content = i[1][0];

        if (subject.Contains(chosenSubject) && !potentialContent.Contains(content))
            isContent = true;
    }


    private void FindContent(string[][] i)
    {

        string subject = i[0][0];
        string content = i[1][0];

        if (subject.Contains(chosenSubject) && !potentialContent.Contains(content))
            potentialContent.Add(content);

    }



    private void SetSubject()
    {
        List<int> randomNumbers = new List<int>();
        for(int i=0;i<cards.Count;i++)
        {
            int index;
            do
            {
                index = UnityEngine.Random.Range(0, potentialSubject.Count);
            } while (randomNumbers.Contains(index) && ! (randomNumbers.Count >= potentialSubject.Count));

            randomNumbers.Add(index);

            string subject = "xxxxx";

            if (potentialSubject.Count > i )
                subject = potentialSubject[index];

            cards[i].GetComponentInChildren<Text>().text=subject;
        }
        potentialSubject.Clear();
    }

    private void SetContent()
    {
        List<int> randomNumbers = new List<int>();
        for (int i = 0; i < cards.Count; i++)
        {
            int index;
            do
            {
                index = UnityEngine.Random.Range(0, potentialContent.Count);
            } while (randomNumbers.Contains(index) && !(randomNumbers.Count >= potentialContent.Count));

            randomNumbers.Add(index);

            string content = "xxxxx";

            if (potentialContent.Count > i)
                content = potentialContent[index];

            cards[i].GetComponentInChildren<Text>().text = content;
        }
        potentialContent.Clear();
    }
    #endregion


    public void DeleteContent(string file,string subject,string content)
    {
        List<string> newFile = new List<string>();
        var lines=File.ReadAllLines(file).Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> oldfile=csv.ToList();

        List<string> checkForSubject=new List<string>();

        for (int i = 0; i < oldfile.Count; i++)
        {
            string c = oldfile[i][1][0];
            string s = oldfile[i][0][0];
            if (c != content || s != subject)
            {
                checkForSubject.Add(s);
                List<string> strings = new List<string>();
                strings.Add(s);
                strings.Add(c);
                string line = string.Join(",", strings.ToArray());
                newFile.Add(line);
            }
        }

        if (!checkForSubject.Contains(subject))
            DeleteSubject(subject);
        
        File.WriteAllLines(file, newFile.ToArray());
    }

    public void DeleteSubject(string subject)
    {
        List<string> newFile = new List<string>();
        var lines = File.ReadAllLines(subjectFile).Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> oldfile = csv.ToList();
        for (int i = 0; i < oldfile.Count; i++)
        {
            string c = oldfile[i][1][0];
            string s = oldfile[i][0][0];
            if (s != subject)
            {
                List<string> strings = new List<string>();
                strings.Add(s);
                strings.Add(c);
                string line = string.Join(",", strings.ToArray());
                newFile.Add(line);
            }
        }
        Debug.Log("delete subject file");
        File.WriteAllLines(subjectFile, newFile.ToArray());
    }

    public void CreateCopyTo(string from, string to)
    {
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
                //string _2 = oldfile[i][1][0];
                //string _1 = oldfile[i][0][0];
                //List<string> strings = new List<string>();
                //strings.Add(_1);
                //strings.Add(_2);
            }
            string line = string.Join(",", strings.ToArray());
            newFile.Add(line);
        }

        File.WriteAllLines(to, newFile.ToArray());
    }


    public GameObject newsSite;
    public void offPage()
    {
        newsSite.SetActive(false);
    }

    public void onPage()
    {
        newsSite.SetActive(true);
    }
    public  Article publishArticle(string subject,string content)
    {
        Article A;


        List<string> newFile = new List<string>();
        var lines = File.ReadAllLines("articles.csv").Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> articles = csv.ToList();
        List<string> article = new List<string>();
        for (int i = 0; i < articles.Count; i++)
        {


            if (articles[i][0][0] == subject && articles[i][1][0] == content)
            {
                Debug.Log("subject" + subject);
                Debug.Log("content" + content);

                for (int j = 0; j < articles[i].Length; j++)
                    article.Add(articles[i][j][0]);
            }
        }

        Identity I = new Identity();

        I.g = (eGender) Enum.Parse(typeof(eGender), article[3]);
        I.ra =(eRace)Enum.Parse(typeof(eRace), article[4]);
        I.r= (eReligion)Enum.Parse(typeof(eReligion), article[5]);
        I.c= (eClass)Enum.Parse(typeof(eClass), article[6]);
        I.p= (ePolitics)Enum.Parse(typeof(ePolitics), article[7]);
        I.n= (eNationality)Enum.Parse(typeof(eNationality), article[8]);


        List<string> elements;
        List<float> result;

        elements = article[9].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_religion = result.ToArray();

        elements = article[10].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_ethnicity = result.ToArray();

        elements = article[11].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_gender = result.ToArray();

        elements = article[12].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_class = result.ToArray();

        elements = article[13].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_nationality = result.ToArray();

        elements = article[14].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_political = result.ToArray();



        A = new Article(subject, content, I);

        return A;
    }
}


