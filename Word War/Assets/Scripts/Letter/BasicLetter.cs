using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicLetter: MonoBehaviour
{
    public Text text;
    void Start()
    {
        GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLetter(char letter)
    {
        text.text = ("" + letter).ToUpper();
    }

    public char GetLetter()
    {
        char letter;
        char.TryParse(text.text, out letter);

        return letter;
    }

    public void Hide()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r,
                                                GetComponent<Image>().color.g,
                                                GetComponent<Image>().color.b,
                                                0);

        text.text = "";
    }

    public void Show()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r,
                                                GetComponent<Image>().color.g,
                                                GetComponent<Image>().color.b,
                                                255);

        text.text = "";
    }

    public void Found()
    {
        GetComponent<Button>().interactable = true;
    }
}
