using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Society : MonoBehaviour {

    public Player player;
    public List<Agent> agents;
    public List<Tweet> tweets;

	// Use this for initialization
	void Start () {

        player = new Player();

        tweets = new List<Tweet>();
        agents = new List<Agent>();
        for(int i=0;i<100;i++)
        {
            agents.Add(new Agent(i));
        }

        for (int i = 0; i < 20; i++)
        {
            agents[i].followingList.Add(-1);
        }
        StartCoroutine("LinearTime");
        /*
        for (int i = 0; i < 10; i++)
        {
            agents[i].MakeTweet(ref tweets);
        }

        for (int i = 0; i < 10; i++)
        {
            agents[i].ReadNewsFeed(agents,ref tweets);
        }

        Debug.Log(agents[5].religion.ToString());
        for (int i = 0; i<agents[5].following.Count; i++)
        {
            Debug.Log(agents[5].following[i]);
        }
        for (int i = 0; i < tweets.Count; i++)
        {
            Debug.Log(tweets[i].religion +" "+ tweets[i].text + " " + tweets[i].likes);
        } */
    }
    void round() {

        /*for (int i = 0; i < 100; i++)
        {
            agents[i].MakeTweet(ref tweets);
        }*/

        for (int i = 0; i < 100; i++)
        {
            agents[i].ReadNewsFeed(player, agents, ref tweets);
        }

        for (int i = 0; i < tweets.Count; i++)
        {
            Debug.Log(tweets[i].text + " " + tweets[i].no_hearts);
        }
    }

    IEnumerator LinearTime()
    {
        int hour = 8;
        while (true)
        {
            hour %= 24;
            Debug.Log(hour + ":00");
            round();
            yield return new WaitForSeconds(1.0f);
            hour++;
        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
