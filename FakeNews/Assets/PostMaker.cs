using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostMaker : MonoBehaviour
{
    Player player;
    Society s;

    public GameObject newsfeed;
    public GameObject prefabTweet;
    public GameObject n_followers;
    public List<GameObject> newsfeedDisplay;
    List<Tweet> newsfeedTweets;
    public InputField iField;

    public Button share;

    public List<Article> articles;

    // Use this for initialization
    void Start()
    {
        articles = new List<Article>();

        GameObject society = GameObject.Find("society");
        s=society.GetComponent<Society>();
        player=s.player;
        /*
        for (int i = 0; i < 20; i++)
        {
            s.agents[i].followingList.Add(-1);
   
        }*/
   

        newsfeedTweets = new List<Tweet>();
        for (int i = 0; i < 20; i++)
        {
            Tweet tweet = new Tweet(i,"",new Identity());
            tweet.author = "tom";
            tweet.text = "I'm a unicorn number" + i;
            tweet.no_hearts = 2;
            tweet.no_retweets = 3;

            newsfeedTweets.Add(tweet);
        }

        Draw();
        //MakePost();
        //Draw();

    }

    // Update is called once per frame
    void Update()
    {


    }

    /// <summary>
    /// Drawing the gameobjects on a display
    /// </summary>
    void Draw()
    {
        //clear the display
        foreach (GameObject g in newsfeedDisplay)
            Destroy(g);

        newsfeedDisplay.Clear();
        //assigning each tweet its gameobject 
        for (int i = newsfeedTweets.Count - 1; i >= 0; i--)
        {

            GameObject tweet = Instantiate(prefabTweet, transform.position, Quaternion.identity) as GameObject;
            tweet.transform.SetParent(newsfeed.transform, false);
            Text[] children = tweet.GetComponentsInChildren<Text>();
            children[0].text = newsfeedTweets[i].author + ":";
            children[1].text = newsfeedTweets[i].text;
            children[2].text = newsfeedTweets[i].no_retweets + "k";
            children[3].text = newsfeedTweets[i].no_hearts + "k";
            //adding to the array                   
            newsfeedDisplay.Add(tweet);
        }
        n_followers.GetComponent<Text>().text = player.nb_followers.ToString();
    }
    /// <summary>
    /// Invokes after clicking on Post Button
    /// </summary>
    /// 
    private int randomProba(float[] probas)
    {
        float proba = UnityEngine.Random.value;
        float sum = 0;
        for (int i = 0; i < probas.Length; ++i)
        {
            sum += probas[i];
            if (proba < sum)
                return i;
        }
        return probas.Length - 1;
    }
    private float clamp(float f)
    {
        if (f > 1f)
            return 1f;
        if (f < -1f)
            return -1f;
        return f;
    }
   
    public void MakePost(string author,string text,Identity I)
    {
        while (newsfeedTweets.Count > 15)
            newsfeedTweets.RemoveAt(0);
        //get text from the input field

        string tweetText = text;
        Tweet tweet = new Tweet(-1,"",I);
        tweet.author = "Bob";
        tweet.text = tweetText;
        s.tweets.Add(tweet);
        player.tweets_Ids.Add(s.tweets.Count-1);
        
        //adding to the array
        newsfeedTweets.Add(tweet);
        Draw();

    }
    public void shareArticle()
    {
        if(articles.Count>0)
        {
            MakePost("The Failing New York Times", articles[0].subject +" "+ articles[0].content, articles[0].identity);
            articles.RemoveAt(0);
        }
        if (articles.Count == 0)
            share.interactable = false;

    }
        

}