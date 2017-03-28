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
    public List<GameObject> newsfeedDisplay;
    List<Tweet> newsfeedTweets;
    public InputField iField;

    // Use this for initialization
    void Start()
    {

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
    public Identity makeIdentity() {
        Identity identity = new Identity();

        //probabilities
        //gender M-F
        float[] probaGender = new float[] { 0.491f, 0.509f };
        //race caucasian, negroid, asian, middleeastern, none
        float[] probaRace = new float[] { 0.673f, 0.122f, 0.047f, 0.163f, 0.031f };
        //nationnality decorrelated from race (need more nationnalities to correlate)
        //AMERICAN, BRITISH, SPANISH, CHINESE, IRISH, INDIAN, MALAYSIAN, FRENCH, KAZAKHSTANI
        float[] probaNationality = new float[] { 0.90f, 0.056f, 0.0063f, 0.0061f, 0.0051f, 0.0043f, 0.0042f, 0.000969f, 0.000936f, 0.000513f, 0.00546f, 0.000309f, 0.000309f };
        //age from race (found only for while, black & asian)
        //by 5 year step + random adjustement between 0-5 years
        float[][] probaAge = new float[][] {
            new float[] { 0.057f, 0.059f, 0.061f, 0.065f, 0.065f, 0.064f, 0.061f, 0.063f, 0.067f, 0.076f, 0.077f, 0.070f, 0.061f, 0.046f, 0.035f, 0.028f, 0.022f, 0.022f },
            new float[] { 0.075f, 0.074f, 0.078f, 0.089f, 0.080f, 0.072f, 0.068f, 0.067f, 0.069f, 0.073f, 0.069f, 0.057f, 0.043f, 0.030f, 0.022f, 0.016f, 0.011f, 0.010f },
            new float[] { 0.061f, 0.063f, 0.060f, 0.065f, 0.075f, 0.084f, 0.085f, 0.088f, 0.079f, 0.073f, 0.067f, 0.058f, 0.047f, 0.032f, 0.024f, 0.017f, 0.012f, 0.009f },
            new float[] { 0.118f, 0.105f, 0.097f, 0.096f, 0.090f, 0.088f, 0.081f, 0.072f, 0.063f, 0.054f, 0.043f, 0.031f, 0.022f, 0.014f, 0.010f, 0.007f, 0.004f, 0.003f } };
        //religion from race
        float[][] probaReligion = new float[][] {
            new float[] { 0.700f, 0.006f, 0.003f, 0.030f, 0.020f, 0.001f, 0.240f },
            new float[] { 0.790f, 0.020f, 0.003f, 0.003f, 0.010f, 0.004f, 0.170f },
            new float[] { 0.340f, 0.070f, 0.160f, 0.010f, 0.040f, 0.070f, 0.310f },
            new float[] { 0.770f, 0.003f, 0.003f, 0.010f, 0.004f, 0.010f, 0.200f },
            new float[] { 0.785f, 0.006f, 0.004f, 0.017f, 0.012f, 0.007f, 0.161f } };
        //social class
        float[] probaClass = new float[] { 0.48f, 0.46f, 0.06f };


        identity.g = (eGender)randomProba(probaGender);
        identity.ra = (eRace)randomProba(probaRace);
        identity.n = (eNationality)randomProba(probaNationality);
        switch (identity.ra)
        {
            case eRace.WHITE:
                identity.age = 5 * randomProba(probaAge[0]) + UnityEngine.Random.Range(0, 5);
                identity.r = (eReligion)randomProba(probaReligion[0]);
                break;
            case eRace.BLACK:
                identity.age = 5 * randomProba(probaAge[1]) + UnityEngine.Random.Range(0, 5);
                identity.r = (eReligion)randomProba(probaReligion[1]);
                break;
            case eRace.ASIAN:
                identity.age = 5 * randomProba(probaAge[2]) + UnityEngine.Random.Range(0, 5);
                identity.r = (eReligion)randomProba(probaReligion[2]);
                break;
            case eRace.HISPANIC:
                identity.age = 5 * randomProba(probaAge[3]) + UnityEngine.Random.Range(0, 5);
                identity.r = (eReligion)randomProba(probaReligion[3]);
                break;
            default:
                identity.age = UnityEngine.Random.Range(0, 90);
                identity.r = (eReligion)randomProba(probaReligion[4]);
                break;
        }
        //       if (age < 15)//make twitter users above 13 years olds (doesn't follow statistics but reflects that tweeter users are most ofter between 15 and 30)
        //           age += 15;
        identity.c = (eClass)randomProba(probaClass);

        //big 5 mean for white young man (needed)
        float[] meanB5 = new float[] { 0, 0, 0, 0, 0 };
        //big 5 correlation: Ext Agr Con Neu Ope CROSS Age female White Black Asian Hispanic Other
        float[][] correlationsB5 = new float[][]
        {
            new float[] {-0.04f, 0.12f, 0.32f, -0.1f, 0.01f },
            new float[] {0.18f, 0.17f, -0.12f, 0.14f, -0.2f },
            new float[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f },
            new float[] {-0.06f, -0.01f, -0.24f, -0.08f, -0.03f },
            new float[] {-0.12f, 0.05f, -0.1f, 0.02f, -0.05f },
            new float[] {0f, -0.06f, -0.23f, 0, -0.06f }
        };


        float[] personality = new float[5];
        //formula : personality[i] = mean[i]+SUM_j(correlation)+random value between -0.5 and 0.5) 
        for (int i = 0; i < 5; i++)
        {
            personality[i] = meanB5[i] + UnityEngine.Random.value - 0.5f;
            personality[i] += correlationsB5[0][i] * (float)(identity.age - 50) / 100f;
            if (identity.g == eGender.FEMALE)
                personality[i] += correlationsB5[1][i];
            switch (identity.ra)
            {
                case eRace.WHITE:
                    personality[i] += correlationsB5[2][i];
                    break;
                case eRace.BLACK:
                    personality[i] += correlationsB5[3][i];
                    break;
                case eRace.ASIAN:
                    personality[i] += correlationsB5[4][i];
                    break;
                case eRace.HISPANIC:
                    personality[i] += correlationsB5[5][i];
                    break;
                default:
                    personality[i] += correlationsB5[2][i];
                    break;
            }
            personality[i] = clamp(personality[i]);
        }

        //preferences
        float[] correlation_religiosity = new float[] { 0.1f, 0.2f, 0.17f, 0.0f, -0.06f };
        float[] correlation_religious_fundamentalism = new float[] { 0.09f, 0.13f, 0.09f, -0.12f, -0.14f };
        float[] correlation_politics = new float[] { -0.081f, 0.025f, -0.42f, -0.29f, 0.84f };
        //formula : pref_[own] = random value between -0.5 and 0.5 + SUM big5*correlation
        // pref_[other] = -pref_[own]
        float religiosity = UnityEngine.Random.value - 0.5f; //- to 1
        float religious_fundamentalism = 0f; //- to 1
        float politics = UnityEngine.Random.value - 0.5f; //-1 = very cons, 1 = very lib
        float ethnicity = 2 * UnityEngine.Random.value - 1f;
        float gender = 2f * UnityEngine.Random.value - 1f;
        float classPref = 2f * UnityEngine.Random.value - 1f;
        float nationalityPref = 2f * UnityEngine.Random.value - 1f;
        for (int i = 0; i < 5; ++i)
        {
            religiosity += personality[i] * 3f * correlation_religiosity[i];
            religious_fundamentalism += personality[i] * 10f * correlation_religious_fundamentalism[i];
            politics += personality[i] * correlation_politics[i];
        }
        religiosity = clamp(religiosity);
        religious_fundamentalism = clamp(religious_fundamentalism);
        politics = clamp(politics);


        identity.pref_religion = new float[probaReligion[0].Length];
        for (int i = 0; i < identity.pref_religion.Length; i++)
        {
            float atheist = 1.0f;
            if (identity.r == eReligion.ATHEIST)
                atheist = -1.0f;
            if ((eReligion)i == identity.r)
                identity.pref_religion[i] = atheist * religiosity;
            else
                identity.pref_religion[i] = -atheist * religious_fundamentalism * System.Math.Abs(atheist * religiosity);
        }

        identity.pref_ethnicity = new float[probaRace.Length];
        for (int i = 0; i < identity.pref_ethnicity.Length; i++)
            if ((eRace)i == identity.ra)
                identity.pref_ethnicity[i] = ethnicity;
            else
                identity.pref_ethnicity[i] = -ethnicity;

        identity.pref_gender = new float[2];
        for (int i = 0; i < 2; i++)
            if ((eGender)i == identity.g)
                identity.pref_gender[i] = gender;
            else
                identity.pref_gender[i] = -gender;

        identity.pref_class = new float[3];
        for (int i = 0; i < 3; i++)
            if ((eClass)i == identity.c)
                identity.pref_class[i] = classPref;
            else
                identity.pref_class[i] = -classPref;

        identity.pref_nationality = new float[13];
        for (int i = 0; i < identity.pref_nationality.Length; i++)
            if ((eNationality)i == identity.n)
                identity.pref_nationality[i] = nationalityPref;
            else
                identity.pref_nationality[i] = -nationalityPref;

        identity.pref_political = new float[4];
        if (politics < 0)//conservative/facist
            identity.p = (ePolitics)(2 * randomProba(new float[] { 0.75f, 0.25f }));
        else //liberal/communist
            identity.p = (ePolitics)(2 * randomProba(new float[] { 0.75f, 0.25f })) + 1;
        for (int i = 0; i < identity.pref_political.Length; i++)
            if ((ePolitics)i == identity.p)
                identity.pref_political[i] = System.Math.Abs(politics);
            else
                identity.pref_political[i] = -System.Math.Abs(politics);
        return identity;
    }
    public void MakePost()
    {
        while (newsfeedTweets.Count > 15)
            newsfeedTweets.RemoveAt(0);
        //get text from the input field

        string tweetText = iField.text;
        Tweet tweet = new Tweet(1,"",makeIdentity());
        tweet.author = "Bob";
        tweet.text = tweetText;
        s.tweets.Add(tweet);
        player.tweets_Ids.Add(s.tweets.Count-1);
        
        //adding to the array
        newsfeedTweets.Add(tweet);
        Draw();

    }

}