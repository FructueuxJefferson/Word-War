using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buzzer: MonoBehaviour
{
    public Game game;
    public Player player;

    [Header("UI")]
    public Text buzzerText;

    private void Awake()
    {
        if (player.id == 0)
        {
            GetComponent<Image>().color = Runtime.player1Color;
        }
        else
        {
            GetComponent<Image>().color = Runtime.player2Color;
        }
    }

    private void Start()
    {

    }

    public void Buzz()
    {
        if (game.currentAskedLetter.gameObject.activeSelf == true)
        {
            if (Runtime.askedWordLetters[Runtime.currentIndex].GetLetter().Equals(game.currentAskedLetter.GetLetter()))
            {
                int gain = Runtime.askedWordLetters[Runtime.currentIndex].GetValue();

                Debug.Log(gain);

                player.PrintInfo("+" + gain + "pts", 25f, 0.25f, 1.5f, Color.green);

                player.Won(gain);

                game.PlayerFoundCurrentLetter(player);
            }
            else if (game.currentAskedLetter.GetLetter().Contains("+"))
            {
                int gain = 0;
                string res = game.currentAskedLetter.GetLetter().Replace("+", "");

                Debug.Log(res);

                gain = int.Parse(res);

                player.PrintInfo("+" + gain + "pts", 25f, 0.25f, 1.5f, Color.green);

                player.Won(gain);

                game.HideCurrentAskedLetter();
            }
            else if (game.currentAskedLetter.GetLetter().Contains("-"))
            {
                int gain = 0;
                string res = game.currentAskedLetter.GetLetter().Replace("-", "");

                Debug.Log(res);

                gain = int.Parse(res);

                player.PrintInfo("-" + gain + "pts", 25f, 0.25f, 1.5f, Color.red);

                player.Lost(gain);

                game.HideCurrentAskedLetter();
            }
            else if(game.currentAskedLetter.GetLetter().Equals("$"))
            {
                Player opponent = Utils.GetOpponentPlayer(player);
                opponent.PrintInfo("-"+Runtime.lostOnFail+"pts", 25f, 0.25f, 1.5f, Color.red);

                opponent.Lost(Runtime.lostOnFail);

                game.HideCurrentAskedLetter();
            }
            else if (game.currentAskedLetter.GetLetter().Equals("%"))
            {
                Player opponent = Utils.GetOpponentPlayer(player);
                opponent.PrintInfo("-" + Runtime.lostOnRubish + "pts", 25f, 0.25f, 1.5f, Color.red);

                opponent.Lost(Runtime.lostOnRubish);

                game.HideCurrentAskedLetter();
            }
            else if (game.currentAskedLetter.GetLetter().Equals("?"))
            {
                Player opponent = Utils.GetOpponentPlayer(player);
                opponent.PrintInfo("-" + Runtime.lostOnFail + "pts", 25f, 0.25f, 1.5f, Color.red);

                opponent.Lost(Runtime.lostOnFail);

                player.PrintInfo("+" + Runtime.lostOnFail + "pts", 25f, 0.25f, 1.5f, Color.green);

                player.Won(Runtime.lostOnFail);

                game.HideCurrentAskedLetter();
            }
            else if (game.currentAskedLetter.GetLetter().Equals("!"))
            {
                Player opponent = Utils.GetOpponentPlayer(player);
                opponent.PrintInfo("-" + Runtime.lostOnRubish + "pts", 25f, 0.25f, 1.5f, Color.red);

                opponent.Lost(Runtime.lostOnRubish);

                player.PrintInfo("+" + Runtime.lostOnRubish + "pts", 25f, 0.25f, 1.5f, Color.green);

                player.Won(Runtime.lostOnRubish);

                game.HideCurrentAskedLetter();
            }
            else
            {
                int gain = Runtime.askedWordLetters[Runtime.currentIndex].GetValue();

                player.PrintInfo("-" + gain + "pts", 25f, 0.25f, 1.5f, Color.red);

                player.Lost(gain);

                game.HideCurrentAskedLetter();
            }
        }
        else
        {
            player.PrintInfo("-" + Runtime.lostOnRubish + "pts", 25f, 0.25f, 1.5f, Color.red);

            player.Lost(Runtime.lostOnRubish);
        }
    }

    private void Update()
    {
        buzzerText.text = "" + player.playerScore;
    }
}
