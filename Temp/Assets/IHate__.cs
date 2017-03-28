using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IHate__ : MonoBehaviour {


    public Text text;
    public List<string> strings;
     
	// Use this for initialization
	void Start () {
        StartCoroutine("speech");
	}
	IEnumerator speech()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < strings.Count; i++)
        {
            text.text = strings[i];
            yield return new WaitForSeconds(1.5f);
        }
    }

}
