using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Game: MonoBehaviour
{
    [Header("Players")]
    public Player player1;
    public Player player2;
    public ModalWindowManager player1WinPopUp;
    public ModalWindowManager playerWinPopUp;
    public TMP_Text MaxScoreText;

    [Header("Customization")]
    public TextAsset textfile;

    public int minLetter = 3, maxLetter = 7, letterNoise = 10;
    public bool onlyImpair = true;
    public bool alwaysLetter = false;
    public bool onlyCurrentLetter = true;


    public float timeToFadeAgain;
    public float timeToWaitAfterFade;


    public GameObject letterPrefab;
    public Transform askedWord;
    public Transform dustbin;

    [Header("Existed")]
    public DynamicLetter currentAskedLetter;
    public AnimatedIconHandler showOptionsPanelBtn, listOfScores;
    public Transform matchScoreList;
    public GameObject matchScorePrefab;
    public GameObject matchHistory;


    [Header("Counter")]
    public int count;
    public Text showCount;

    [Header("Letter")]
    public int minLetterValue = 1, maxLetterValue = 10;

    [Header("Tips")]
    public ModalWindowManager tipsWindow;


    #region Private
    private List<string> words = new List<string>();
    private bool canUpdate = false, hasBeenFaded = false, gameGenerated = false, tipsShowed = false, gameOver = false;
    private float currentTime;



    string pickedWord;

    #endregion


    private void Awake()
    {
        IO.CreateDirectory("Words");
        
        byte[] bytes = Encoding.Default.GetBytes(textfile.text);
        Debug.Log(textfile.text);
        File.WriteAllBytes(Application.persistentDataPath + "/Words/words.txt", bytes);


        words = IO.ReadText("words.txt", minLetter, maxLetter, onlyImpair);
    }
    void Start()
    {
        HideBuzzers();

        if (Runtime.MaxScore != int.MaxValue)
        {
            MaxScoreText.text = "" + Runtime.MaxScore;
        }
        else
        {
            MaxScoreText.text = "Inf";
        }

        timeToFadeAgain = Runtime.timeToFadeAgain;
        timeToWaitAfterFade = Runtime.timeToWaitAfterFade;
        maxLetterValue = Runtime.maxLetterValue;

        PrepareGame();
    }

    void PrepareGame()
    {
        if (Parameter.IsAssigned(askedWord) && Parameter.IsAssigned(currentAskedLetter))
        {
            Runtime.askedWordLetters.Clear();
            Runtime.currentIndex = 0;
            currentAskedLetter.gameObject.SetActive(false);

            GiveWord();
        }
        else
        {
            Debug.Log("Can't start the game without all needded objects assigned");
        }
    }

    void GiveWord()
    {
        if (words.Count > 0)
        {
            if (gameGenerated == false)
            {
                int pickedIndex = Random.Range(0, words.Count - 1);

                pickedWord = words[pickedIndex];

                Debug.Log(pickedWord);

                for (int i = 0; i < pickedWord.Length; ++i)
                {

                    GameObject letter = Instantiate(letterPrefab, askedWord);
                    letter.GetComponent<DynamicLetter>().SetLetter("" + pickedWord[i]);
                    letter.GetComponent<DynamicLetter>().Mute();
                    letter.GetComponent<DynamicLetter>().SetValue(Random.Range(minLetter, maxLetterValue + 1));

                    Runtime.askedWordLetters.Add(letter.GetComponent<DynamicLetter>());
                }

                Runtime.askedWordLetters[Runtime.currentIndex].Currentize();

                if (tipsShowed == false)
                    ShowTips();
                else
                    StartCoroutine(Counter());
            }
            else
            {

            }
        }
    }

    public void StartCounter()
    {
        StartCoroutine(Counter());
    }

    public void ShowTips()
    {
        tipsWindow.OpenWindow();
        tipsWindow.titleText = "Aides";
        tipsWindow.descriptionText = "Chaque joueur a son buzzer, peint avec sa couleur.\n" +
            "En cas de mauvais buzz le joueur perd " + Runtime.lostOnFail + "pts\n" +
            "En cas de buzz inutile, le joueur perd " + Runtime.lostOnRubish + "pts\n" +
            "Joker $ : L'opposant perd " + Runtime.lostOnFail + "pts\n" +
            "Joker % : L'opposant perd " + Runtime.lostOnRubish + "pts\n" +
            "Joker ? : L'opposant perd " + Runtime.lostOnFail + "pts et le joueur gagne " + Runtime.lostOnFail + "pts\n" +
            "Joker ! : L'opposant perd " + Runtime.lostOnRubish + "pts et le joueur gagne " + Runtime.lostOnRubish + "pts\n";


        tipsWindow.UpdateUI();

        tipsShowed = true;
    }

    IEnumerator Counter()
    {
        showCount.gameObject.SetActive(true);
        showCount.text = "" + count;

        int startCount = 0;
        while (startCount < count)
        {
            yield return new WaitForSeconds(1);
            startCount++;
            int res = count - startCount;
            showCount.text = "" + res;
        }

        showCount.gameObject.SetActive(false);
        gameGenerated = true;

        canUpdate = true;

        ShowBuzzers();
    }

    void SetPickedWord(string word)
    {
        this.pickedWord = word;

        GiveWord();
    }

    void FixedUpdate()
    {
        if (canUpdate)
        {
            if (!hasBeenFaded)
            {
                if (currentTime < timeToFadeAgain)
                {
                    currentTime += Time.deltaTime;

                    float restOfTimeBeforeFade = (float) (timeToFadeAgain - currentTime);
                }
                else
                {
                    List<string> probLetter = new List<string>();

                    for (int i = 0; i < letterNoise; ++i)
                    {
                        probLetter.Add(Utils.PickRandomLetter());
                    }

                    if (onlyCurrentLetter)
                    {
                        probLetter.Add(Runtime.askedWordLetters[Runtime.currentIndex].GetLetter().ToUpper());
                    }
                    else
                    {
                        int remaining = Runtime.askedWordLetters.Count - Runtime.currentIndex;
                        for (int i = 0; i < remaining; ++i)
                        {
                            probLetter.Add(Runtime.askedWordLetters[i].GetComponent<DynamicLetter>().GetLetter());
                        }
                    }



                    currentAskedLetter.gameObject.SetActive(true);

                    if (!alwaysLetter)
                        currentAskedLetter.SetLetter(Utils.PickRandomLetter(probLetter));
                    else
                        currentAskedLetter.SetLetter(Runtime.askedWordLetters[Runtime.currentIndex].GetLetter());


                    if (Runtime.ShowTips)
                    {
                        if (currentAskedLetter.GetLetter().Contains("+"))
                        {
                            currentAskedLetter.SetIsPlus();
                        }
                        else if (currentAskedLetter.GetLetter().Contains("-"))
                        {
                            currentAskedLetter.SetIsMinus();
                        }
                        else if (currentAskedLetter.GetLetter().Equals("$")
                            || currentAskedLetter.GetLetter().Equals("%")
                            || currentAskedLetter.GetLetter().Equals("?")
                            || currentAskedLetter.GetLetter().Equals("!"))
                        {
                            currentAskedLetter.SetIsSpecial();
                        }
                        else
                        {
                            currentAskedLetter.SetIsNormal();
                        }
                    }
                    else
                    {
                        currentAskedLetter.SetIsNormal();
                    }

                    currentTime = 0;
                    hasBeenFaded = true;
                }
            }
            else
            {
                if (currentTime < timeToWaitAfterFade)
                {
                    currentTime += Time.deltaTime;

                    float restOfTimeAfterFade = (float) (timeToWaitAfterFade - currentTime);
                }
                else
                {
                    hasBeenFaded = false;
                    currentTime = 0;

                    currentAskedLetter.gameObject.SetActive(false);
                }

            }
        }
    }

    public void PlayerFoundCurrentLetter(Player player)
    {
        currentTime = 0;
        hasBeenFaded = false;

        Runtime.askedWordLetters[Runtime.currentIndex].Gain(player);

        currentAskedLetter.gameObject.SetActive(false);

        Runtime.currentIndex++;

        if (Runtime.currentIndex >= Runtime.askedWordLetters.Count)
        {
            gameGenerated = false;
            canUpdate = false;

            GameObject matchScore = Instantiate(matchScorePrefab, matchScoreList);
            matchScore.GetComponent<MatchScore>().AddMatch(pickedWord, player1.playerName, player2.playerName, player1.playerScore, player2.playerScore);

            Runtime.matchsHistory.Add(matchScore.GetComponent<MatchScore>().text.text);
            IO.WriteBinary(Runtime.matchsHistory, "MatchHistory/matchHistory.txt", 0);


            if (player1 != null && player1.playerScore >= Runtime.MaxScore && player2.playerScore < Runtime.MaxScore)
            {
                ShowWinner(player1.playerName, player2.playerName);

                gameOver = true;
            }
            else if (player2 != null && player2.playerScore >= Runtime.MaxScore && player1.playerScore < Runtime.MaxScore)
            {
                ShowWinner(player2.playerName, player1.playerName);

                gameOver = true;
            }
            else if (player1 != null && player2 != null && player1.playerScore >= Runtime.MaxScore && player2.playerScore >= Runtime.MaxScore)
            {
                if (player1.playerScore > player2.playerScore)
                {
                    ShowWinner(player1.playerName, player2.playerName);
                }
                else if (player2.playerScore > player1.playerScore)
                {
                    ShowWinner(player2.playerName, player1.playerName);
                }
                else
                {
                    ShowEquality();
                }

                gameOver = true;
            }
            else
            {
                NewWord();

                gameOver = false;
            }


        }
        else
        {
            Runtime.askedWordLetters[Runtime.currentIndex].Currentize();
        }
    }

    public void HideCurrentAskedLetter()
    {
        currentTime = 0;
        hasBeenFaded = false;

        currentAskedLetter.gameObject.SetActive(false);
    }

    private void NewWord()
    {
        Runtime.askedWordLetters.Clear();
        Runtime.currentIndex = 0;

        while (askedWord.childCount > 0)
        {
            askedWord.GetChild(0).transform.SetParent(dustbin);
        }
        for (int i = 0; i < dustbin.childCount; ++i)
        {
            Destroy(dustbin.GetChild(i).gameObject);
        }

        currentAskedLetter.gameObject.SetActive(false);

        GiveWord();
    }

    public void Pause()
    {
        canUpdate = false;
    }

    public void Resume()
    {
        canUpdate = true;
    }

    public void ShowWinner(string winnerName, string loserName)
    {
        playerWinPopUp.OpenWindow();

        playerWinPopUp.windowTitle.text = "Fin de jeu";
        playerWinPopUp.windowDescription.text = "Gagnant : " + winnerName + "\n" +
            "Perdant : " + loserName;
    }
    public void ShowEquality()
    {
        playerWinPopUp.OpenWindow();

        playerWinPopUp.windowTitle.text = "Fin de jeu";
        playerWinPopUp.windowDescription.text = "Egalité entre les joueurs";
    }

    public void ShowBuzzers()
    {
        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);

        showOptionsPanelBtn.gameObject.SetActive(true);
        listOfScores.gameObject.SetActive(true);
    }

    public void HideBuzzers()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);

        showOptionsPanelBtn.gameObject.SetActive(false);
        listOfScores.gameObject.SetActive(false);
    }

    public void ShowMatchHistory()
    {
        if (!gameOver)
        {
            Pause();
            HideBuzzers();
        }
        matchHistory.gameObject.SetActive(true);
    }

    public void HideMatchHistory()
    {
        matchHistory.gameObject.SetActive(false);

        if (!gameOver)
        {
            Resume();
            ShowBuzzers();
        }
    }
}
