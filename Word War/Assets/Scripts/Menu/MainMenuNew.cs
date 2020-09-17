using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;
using System.IO;
using System.Collections.Generic;

public class MainMenuNew: MonoBehaviour
{

    Animator CameraObject;

    [Header("Panels")]
    [Tooltip("The UI Panel parenting all sub menus")]
    public GameObject mainCanvas;


    [Tooltip("The UI Panel that holds the GAME window tab")]
    public GameObject PanelGame;

    public AudioSource bgSound;

    public HorizontalSelector difficulty, timing;

    public Toggle showTips;

    public GameObject panel;
    public ProgressBar loading;




    // campaign button sub menu
    [Header("Menus")]
    [Tooltip("The Menu for when the MAIN menu buttons")]
    public GameObject mainMenu;
    [Tooltip("The Menu for when the PLAY button is clicked")]
    public GameObject playMenu;
    [Tooltip("The Menu for when the EXIT button is clicked")]
    public GameObject exitMenu;

    public Transform matchScoreList;
    public GameObject matchScorePrefab;



    /*[Header("Choose Panel")]
    public ChoosePanel choosePanel;*/

    void Start()
    {
        CameraObject = transform.GetComponent<Animator>();

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("_BGMusicVolume") / 100;



        if (File.Exists(Application.persistentDataPath + "/MatchHistory/matchHistory.txt"))
        {
            Runtime.matchsHistory = IO.ReadBinary("MatchHistory/matchHistory.txt", 0).words;

            for (int i = 0; i < Runtime.matchsHistory.Count; ++i)
            {
                GameObject matchScore = Instantiate(matchScorePrefab, matchScoreList);
                matchScore.GetComponent<MatchScore>().AddMatch(Runtime.matchsHistory[i]);
            }

            Debug.Log(Runtime.matchsHistory.Count);
        }
        else
        {
            List<string> scores = new List<string>();
            scores.Add("RG vs Jovane\n" +
                "DOPE\n" +
                "14 vs -15");
            IO.CreateDirectory("MatchHistory");

            IO.WriteBinary(scores, "MatchHistory/matchHistory.txt", 0);

            Debug.Log(Application.persistentDataPath);
        }

    }


    public void Position2()
    {
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }

    public void AreYouSure()
    {
        exitMenu.gameObject.SetActive(true);
    }

    public void No()
    {
        exitMenu.gameObject.SetActive(false);
    }
    public void Yes()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        switch (difficulty.index)
        {
            case 0:
                Runtime.timeToFadeAgain = .8f;
                Runtime.timeToWaitAfterFade = 1.5f;
                Runtime.maxLetterValue = 5;

                Runtime.lostOnFail = 5;
                Runtime.lostOnRubish = 10;
                break;
            case 1:
                Runtime.timeToFadeAgain = .7f;
                Runtime.timeToWaitAfterFade = 1f;
                Runtime.maxLetterValue = 7;

                Runtime.lostOnFail = 5;
                Runtime.lostOnRubish = 8;
                break;
            case 2:
                Runtime.timeToFadeAgain = .6f;
                Runtime.timeToWaitAfterFade = .75f;
                Runtime.maxLetterValue = 8;

                Runtime.lostOnFail = 4;
                Runtime.lostOnRubish = 6;
                break;
            case 3:
                Runtime.timeToFadeAgain = .5f;
                Runtime.timeToWaitAfterFade = .5f;
                Runtime.maxLetterValue = 10;

                Runtime.lostOnFail = 2;
                Runtime.lostOnRubish = 3;
                break;
        }

        Runtime.MaxScore = int.Parse(timing.itemList[timing.index].itemTitle);

        Runtime.ShowTips = showTips.isOn;

        panel.SetActive(false);
        StartCoroutine(LoadScene(1));
        //SceneManager.LoadSceneAsync(1);
        //Fader.
    }

    public void EmptyMatchHistory()
    {
        IO.WriteBinary(new List<string>(), "MatchHistory/matchHistory.txt", 0);

        for(int i = 1; i < matchScoreList.childCount;  ++i)
        {
            Destroy(matchScoreList.GetChild(i).gameObject);
        }
    }

    IEnumerator LoadScene(int scene)
    {
        loading.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);

            loading.currentPercent = progress;

            yield return null;
        }
    }

    public void SendEmail()
    {
        SendMailCrossPlatform.SendEmail("brightcompany@gmail.com", "Jeu Word war", "Concernant le jeu Word War ...");
    }

}