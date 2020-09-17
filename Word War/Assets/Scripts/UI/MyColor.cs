using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyColor
{
    public static Color Orange = new Color(1f, .64f, 0f);
    public static Color Gold = new Color(1f, .84f, 0f);
    public static Color Blue = new Color(0, 0.75f, .84f);
    public static Color Pink = new Color(1f, .41f, .70f);

    public static Color StringToColor(string color)
    {
        if (color.ToLower().Equals("orange"))
        {
            return Orange;
        }
        else if (color.ToLower().Equals("or"))
        {
            return Gold;
        }
        else if (color.ToLower().Equals("bleu"))
        {
            return Blue;
        }
        else if (color.ToLower().Equals("rose"))
        {
            return Pink;
        }
        else if (color.ToLower().Equals("jaune"))
        {
            return Color.yellow;
        }
        else if (color.ToLower().Contains("cyan"))
        {
            return Color.cyan;
        }

        return Color.white;
    }
}
