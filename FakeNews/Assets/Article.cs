using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Article {

    //header
    public string subject;
    public string content;

    public Identity identity;

    public Article(string _subject,string _content,Identity _identity)
    {
        subject = _subject;
        content = _content;
        identity = _identity;
    }
}
