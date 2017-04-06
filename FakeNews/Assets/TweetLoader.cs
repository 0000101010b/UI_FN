using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class TweetLoader {


    public static Tweet Tweet(int posterId,int index,string name)
    {
        var lines = File.ReadAllLines("Tweets.csv").Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> file = csv.ToList();

        string[][] _tweet = file[index];


        List<string> tweet=new List<string>();
        for(int i=0;i<_tweet.Length;i++)
        {
            tweet.Add(_tweet[i][0]);
        }


        Identity I = new Identity();

        I.g = (eGender)Enum.Parse(typeof(eGender), tweet[3]);
        I.ra = (eRace)Enum.Parse(typeof(eRace), tweet[4]);
        I.r = (eReligion)Enum.Parse(typeof(eReligion), tweet[5]);
        I.c = (eClass)Enum.Parse(typeof(eClass), tweet[6]);
        I.p = (ePolitics)Enum.Parse(typeof(ePolitics), tweet[7]);
        I.n = (eNationality)Enum.Parse(typeof(eNationality), tweet[8]);


        List<string> elements;
        List<float> result;

        elements = tweet[9].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_religion = result.ToArray();

        elements = tweet[10].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_ethnicity = result.ToArray();

        elements = tweet[11].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_gender = result.ToArray();

        elements = tweet[12].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_class = result.ToArray();

        elements = tweet[13].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_nationality = result.ToArray();

        elements = tweet[14].Split(';').ToList();
        result = elements.Select(x => float.Parse(x)).ToList();
        I.pref_political = result.ToArray();



        Tweet t = new Tweet(1, tweet[1], I);
        t.author = name;
 

        return t;
    }

}

