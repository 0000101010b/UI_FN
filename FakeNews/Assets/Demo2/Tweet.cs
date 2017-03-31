using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tweet {
    public int posterId;
    public int id;


    public string author;

    public List<int> reposterIds;
    public string text;
    public int no_hearts;
    public int no_retweets;

    public List<string> hashtags;
    public List<int> _at;

    /*public eClass socialClass;
    public eReligion religion;
    */
    public Identity I;

    public Tweet(int _posterId,string _text,Identity _identity)
    {
        posterId = _posterId;
        reposterIds = new List<int>();
        text = _text;
        I = _identity;
    }

    public void LikeTweet()
    {
        no_hearts++;
    }
    public void repostTweet(int _reposterId)
    {
        reposterIds.Add(_reposterId);
        no_retweets++;
    }
}
