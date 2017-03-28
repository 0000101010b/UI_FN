using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class TweetLoader {


    public static Tweet Tweet(int index)
    {
        var lines = File.ReadAllLines("Tweets.csv").Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> file = csv.ToList();

        string[][] tweet = file[index];


        Identity I = new Identity();
        //I.pref_religion =;


        Tweet t = new Tweet();
        t.I=I;
        t.name = "bob";
        t.text = tweet[1][0]; 

        return t;
    }

}

public class Identity
{
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

    public void setEnums()
    {
        g = (eGender)System.Enum.Parse(typeof(eGender), gender);
        r = (eReligion)System.Enum.Parse(typeof(eReligion), religion);
        p = (ePolitics)System.Enum.Parse(typeof(ePolitics), politics);
        n = (eNationality)System.Enum.Parse(typeof(eNationality), nationality);
    }

    public void prepEnumStrings()
    {
        gender = g.ToString();
        religion = r.ToString();
        politics = p.ToString();
        nationality = n.ToString();
    }
}
