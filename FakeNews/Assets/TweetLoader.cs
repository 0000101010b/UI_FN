using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class TweetLoader {


    public static Tweet Tweet(int posterId,int index)
    {
        var lines = File.ReadAllLines("Tweets.csv").Select(a => a.Split(','));
        var csv = from line in lines
                  select (line.Select(a => a.Split(',')).ToArray());

        List<string[][]> file = csv.ToList();

        string[][] tweet = file[index];


        Identity I = new Identity();
        //I.pref_religion =;


        Tweet t = new Tweet(posterId,tweet[1][0],I);

        t.I=I;
        t.author = "bob";
        t.text = tweet[1][0]; 

        return t;
    }

}

