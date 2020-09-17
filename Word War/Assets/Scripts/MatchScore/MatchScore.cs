using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchScore: MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMatch(string word, string player1Name, string player2Name, int score1, int score2)
    {
        text.text = System.DateTime.Now.ToString("dd/MM/yyyy")+"\n"+player1Name + " vs " + player2Name + "\n" + word.ToUpper() + "\n" +
            score1 + " vs " + score2;
    }

    public void AddMatch(string matchSave)
    {
        text.text = matchSave;
    }
}
