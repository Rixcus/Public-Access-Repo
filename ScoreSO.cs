using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu (fileName = "ScoreSheet", menuName = "ScoreSheets")]

public class ScoreSO : ScriptableObject
{
    public int startScore = 0;
    public int startCount = 1;
    public int startMulti = 1;

    public int score;
    public int count;
    public int multi;


        

    public void SetStartValues()
    {
        score = startScore;
        count = startCount;
        multi = startMulti;
       
    }
    public void SetCannonValues()
    {
        score = startScore;
        count = 9; //10 total in text display 9 instantiated + cam ball
    }
    public void SetOtherValues()
    {
        score = startScore;
        count = 10;
        multi = startMulti;
    }


    //---------------------------- potential var data -------------------------
    //public string lv1Score; //string is temp assignment for ui text 
    //public string lv1Count;
    //public string lv1Multi;

    //public TextMeshProUGUI scoreText1;
    //public TextMeshProUGUI countText1;
    //public TextMeshProUGUI multiText1;

    //public string lv2Score;
    //public string lv2Count;

    //public string lv3Score;
    //public string lv3Count;

    //public string lv4Score;
    //public string lv4Count;

    //level 1 = you are ball, 2 = you are cannon, 3 = you are terrain, 4 = you are dropper(pachink)


    //scoreText1.text = "Score : " + score;
    //countText1.text = "Count : " + count;
    //multiText1.text = "M" + multi;


}
