using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Customization: MonoBehaviour
{
    public TMP_InputField player1Name, player2Name;
    public HorizontalSelector player1Color, player2Color;

    public TMP_Text response;

    public GameObject panel;
    public ProgressBar loading;

    private void Start()
    {
        if(Runtime.player1Name != null)
        {
            player1Name.text = Runtime.player1Name;
        }
        if (Runtime.player2Name != null)
        {
            player2Name.text = Runtime.player2Name;
        }
    }

    public void CheckGame(ModalWindowManager window)
    {
        if (player1Name.text.Length <= 0)
        {
            response.text = "Le joueur 1 n'a pas saisi de nom";
            response.color = Color.green;
        }
        else if (player2Name.text.Length <= 0)
        {
            response.text = "Le joueur 2 n'a pas saisi de nom";
            response.color = Color.green;
        }
        else if (player1Name.text.Equals(player2Name.text))
        {
            response.text = "Les joueurs ne peuvent pas avoir les mêmes noms";
            response.color = Color.green;
        }
        else if (player1Color.index.Equals(player2Color.index))
        {
            response.text = "Les joueurs ne peuvent pas avoir les mêmes couleurs";
            response.color = Color.green;
        }
        else
        {
            window.OpenWindow();
        }
    }

    public void LaunchGame()
    {
        Runtime.player1Color = MyColor.StringToColor(player1Color.itemList[player1Color.index].itemTitle);
        Runtime.player2Color = MyColor.StringToColor(player2Color.itemList[player2Color.index].itemTitle);
        Runtime.player1Name = player1Name.text;
        Runtime.player2Name = player2Name.text;

        //SceneManager.LoadScene(2);

        panel.SetActive(false);

        StartCoroutine(LoadScene(2));
    }

    public void BackToMenu()
    {
        //SceneManager.LoadScene(0);

        panel.SetActive(false);

        StartCoroutine(LoadScene(0));
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
}
