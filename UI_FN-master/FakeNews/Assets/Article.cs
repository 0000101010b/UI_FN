using UnityEngine;
using System.Collections;

public class Article {

    //header
    public string subject;
    public string content;

    ArticleIdentity identity;

    public Article(string _subject,string _content,ArticleIdentity _identity)
    {
        subject = _subject;
        content = _content;
        identity = _identity;
    }
}

public class ArticleIdentity
{
    float[] pref_religion;
    float[] pref_ethnicity;
    float[] pref_gender;
    float[] pref_sexual_identity;
    float[] pref_class;
    float[] pref_nationality;
    float[] pref_political;

    public eGender g;
    public eReligion r;
    public ePolitics p;
    public eNationality n;

    public ArticleIdentity(){

    }

    public ArticleIdentity(
    float[] _pref_religion,
    float[] _pref_ethnicity,
    float[] _pref_gender,
    float[] _pref_sexual_identity,
    float[] _pref_class,
    float[] _pref_nationality,
    float[] _pref_political,
    eGender _g,
    eReligion _r,
    ePolitics _p,
    eNationality _n)
    {
        pref_religion=_pref_religion;
        pref_ethnicity= _pref_ethnicity;
        pref_gender= _pref_gender;
        pref_sexual_identity=_pref_sexual_identity;
        pref_class= _pref_class;
        pref_nationality= _pref_nationality;
        pref_political= _pref_political;
        g = _g;
        r = _r;
        p = _p;
        n = _n;
    }

}
