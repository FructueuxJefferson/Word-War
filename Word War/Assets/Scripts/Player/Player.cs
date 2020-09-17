using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    [SerializeField]
    public int id;
    [SerializeField]
    public string playerName;
    [SerializeField]
    public Color playerColor;
    [SerializeField]
    public int playerScore;
    [SerializeField]
    public Text playerNameText;
    [SerializeField]
    public GameObject floatingTextPrefab;
    [SerializeField]
    public Transform floatingFeedbacks;
    [SerializeField]
    public List<AudioClip> audioClips;

    public Buzzer buzzer;

    public void Awake()
    {
        if (id == 0)
        {
            playerColor = Runtime.player1Color;
            playerName = Runtime.player1Name;

            Runtime.player_1 = this;
        }
        else
        {
            playerColor = Runtime.player2Color;
            playerName = Runtime.player2Name;

            Runtime.player_2 = this;
        }

        playerNameText.text = playerName;
        playerNameText.color = playerColor;
    }

    private void Start()
    {

    }

    public void PrintInfo(string textToShow, float moveSpeed, float fadeSpeed, float destroyAfter, Color color)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab) as GameObject;

        if (floatingText.GetComponent<TextOnSpotScript>() != null)
        {

            var spotText = floatingText.GetComponent<TextOnSpotScript>();

            spotText.DisplayText = textToShow;
            spotText.Speed = moveSpeed;
            spotText.DestroyAfter = destroyAfter;
            spotText.color = color;
            spotText.fadeSpeed = fadeSpeed;
            spotText.cam = Camera.main.transform;

            floatingText.transform.position = floatingFeedbacks.position;
        }
    }

    private void Update()
    {
    }

    public void Won(int pts)
    {
        playerScore += pts;

        GetComponent<AudioSource>().clip = audioClips[0];
        GetComponent<AudioSource>().Play();
    }

    public void Lost(int pts)
    {
        playerScore -= pts;

        GetComponent<AudioSource>().clip = audioClips[1];
        GetComponent<AudioSource>().Play();
    }
}
