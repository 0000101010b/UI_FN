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
    public Text ArticleTitle;
    public Text pastArticles;

    //Past
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
    public  List<string> potentialSubject;

    //content round
    [Header("Content Round")]
    public string nc_FileName = "newsContent.csv";
    public  List<string> potentialContent;


    // Use this for initialization
    void Start() {
        #region Init
        //Set start Events 
        activeEvents = new List<string>();
        activeEvents.Add("WaterGate");
        activeEvents.Add("Election");

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

        StartSubjectRound();
        #endregion Init
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
            Article a = new Article(chosenSubject, chosenContent);
            ArticlesWritten.Add(a);
            pastArticles.text += "-" + a.subject + ": " + a.content + "\n";

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

    private void StartSubjectRound()
    {
        currentRound = Round.subject;
        roundFind = FindSubject;
        roundSet = SetSubject;
        StartRound(ns_FileName);
    }

    private void StartContentRound()
    {
        currentRound = Round.content;
        roundFind = FindContent;
        roundSet = SetContent;
        StartRound(nc_FileName);
    }

    private void FindSubject(string[][] i)
    {

        string subject = i[0][0];
        string events  = i[1][0];

        foreach (string e in activeEvents)
        {
            if (events.Contains(e) && !potentialSubject.Contains(subject))
                potentialSubject.Add(subject);
        }
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

    //Twitter interface : Create Separate File for
    /*To Do:
     * -Share Article
     * -Load NewsFeed
     * */



}
