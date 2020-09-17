using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLetter: MonoBehaviour
{
    public bool blinkLetter = false;
    public Text text;
    public Text valueText;
    public int value;

    public Color defaultColor;

    void Start()
    {
        defaultColor = GetComponent<Image>().color;
        GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLetter(string letter)
    {
        text.text = "" + letter;
        text.text = text.text.ToUpper();
    }

    public void SetValue(int value)
    {
        this.value = value;
        valueText.text = "" + this.value;
    }

    public string GetLetter()
    {
        return text.text;
    }

    public int GetValue()
    {
        return this.value;
    }

    public void Mute()
    {
        GetComponent<Image>().color = Color.grey;
    }

    public void Currentize()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void SetIsPlus()
    {
        GetComponent<Image>().color = Color.green;
    }

    public void SetIsMinus()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void SetIsSpecial()
    {
        GetComponent<Image>().color = new Color(0f, 1f, 0.6f);
    }

    public void SetIsNormal()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void Gain(Player player)
    {
        GetComponent<Image>().color = player.playerColor;
        valueText.GetComponent<Text>().color = player.playerColor;
    }

    public void Remove()
    {
        transform.SetParent(null);
        Destroy(this);
    }
}
