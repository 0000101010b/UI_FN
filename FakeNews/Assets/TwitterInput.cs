using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Tweet t=TweetLoader.Tweet(1);
        Debug.Log(t.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
