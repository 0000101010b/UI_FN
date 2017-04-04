using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Society : MonoBehaviour {
	private static Society instance;
	public static Society Instance{
		get { return instance; }
	}

    public Player player;
    public List<Agent> agents;
    public List<Tweet> tweets;

	private void Awake(){
		if (instance == null){
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	// Use this for initialization
	void Start () {

        player = new Player();

        tweets = new List<Tweet>();
        agents = new List<Agent>();
		for(int i=0; i<Generator.NUMBER_OF_AGENTS; i++)
        {
            agents.Add(new Agent(i));
        }

		for (int i = 0; i < Generator.NUMBER_OF_AGENTS; i++)
        {
            agents[i].followingList.Add(-1);
			agents[i].Awake();
        }
		AgentLoader.saveToFile(agents, "Assets/Resources/agents2.json");
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
	void round(int hour) {
		for (int i = 0; i < Generator.NUMBER_OF_AGENTS; i++)
		{
			if (hour == 8)
				agents[i].ChangeState(Working.Instance);
			else if (hour == 17)
				agents[i].ChangeState(Free.Instance);
			agents[i].Update();
		}
        /*for (int i = 0; i < 100; i++)
        {
            agents[i].MakeTweet(ref tweets);
        }*/

//        for (int i = 0; i < 100; i++)
//        {
//            agents[i].ReadNewsFeed(player, agents, ref tweets);
//        }
//
//        for (int i = 0; i < tweets.Count; i++)
//        {
//            Debug.Log(tweets[i].text + " " + tweets[i].no_hearts);
//        }
    }

    IEnumerator LinearTime()
    {
        int hour = 8;
        while (true)
        {
            hour %= 24;
            Debug.Log(hour + ":00");
            round(hour);
            yield return new WaitForSeconds(1.0f);
            hour++;
        }
    }
}
