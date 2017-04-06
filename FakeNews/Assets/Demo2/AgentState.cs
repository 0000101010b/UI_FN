using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public sealed class AgentState : State<Agent>
{

	static readonly AgentState instance = new AgentState();

	public static AgentState Instance
	{
		get
		{
			return instance;
		}
	}

	static AgentState() { }
	private AgentState() { }

	public override void Enter(Agent agent)
	{
	}

	public override void Execute(Agent agent)
	{
	}

	public override void Exit(Agent agent)
	{
	}
}

public sealed class Working : State<Agent>
{

	static readonly Working instance = new Working();

	public int tweetingProb = 100;
	public static Working Instance
	{
		get
		{
			return instance;
		}
	}

	static Working() { }
	private Working() { }

	public override void Enter(Agent agent)
	{
	}

	public override void Execute(Agent agent)
	{
		if (Random.Range (0, 10000) < tweetingProb) {
			Debug.Log ("Have a break, have a kitkat and tweeting~");
            Tweet t = agent.MakeTweet(ref Society.Instance.tweets, 1);//i miss obama
            Society.Instance.newsFeed.MakePost(t);
            agent.ReadNewsFeed (Society.Instance.player, Society.Instance.agents, ref Society.Instance.tweets);
		}
	}

	public override void Exit(Agent agent)
	{
	}
}

public sealed class Free : State<Agent>
{

	static readonly Free instance = new Free();

	public int tweetingProb = 800;

	public static Free Instance
	{
		get
		{
			return instance;
		}
	}

	static Free() { }
	private Free() { }

	public override void Enter(Agent agent)
	{

	}

	public override void Execute(Agent agent)
	{
		if (Random.Range (0, 10000) < tweetingProb) {
			Debug.Log ("I am not tweeting all the time, okay?");
			agent.ReadNewsFeed (Society.Instance.player, Society.Instance.agents, ref Society.Instance.tweets);
		}
	}

	public override void Exit(Agent agent)
	{
	}
}

public sealed class Angry : State<Agent>
{

	static readonly Angry instance = new Angry();

	public static Angry Instance
	{
		get
		{
			return instance;
		}
	}

	static Angry() { }
	private Angry() { }

	public override void Enter(Agent agent)
	{
	}

	public override void Execute(Agent agent)
	{
	}

	public override void Exit(Agent agent)
	{
	}
}

public sealed class Happy : State<Agent>
{

	static readonly Happy instance = new Happy();

	public static Happy Instance
	{
		get
		{
			return instance;
		}
	}

	static Happy() { }
	private Happy() { }

	public override void Enter(Agent agent)
	{

	}

	public override void Execute(Agent agent)
	{
	}

	public override void Exit(Agent agent)
	{
	}
}