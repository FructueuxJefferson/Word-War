using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static List<int> PickRandomNumbers(int min, int max, int count)
    {
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < count; ++i)
        {
            int randomNumber;

            do
            {
                randomNumber = Random.Range(min, max);
            } while (randomNumbers.Contains(randomNumber));

            randomNumbers.Add(randomNumber);
        }

        return randomNumbers;
    }

    public static char[] PickRandomChars(int count)
    {
        char[] chars = new char[count];
        string[] Alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "@", "%", "#", "&" };

        for (int i = 0; i < count; ++i)
        {
            char pickedChar;

            char.TryParse(Alphabet[Random.Range(0, Alphabet.Length)], out pickedChar);

            chars[i] = pickedChar;
        }

        return chars;
    }

    public static string PickRandomLetter()
    {
        string[] Alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "+1", "+2", "+3", "+4", "+5", "+6", "+7", "+8", "+9", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "$", "%", "?", "!" };

        string pickedLetter;

        pickedLetter = Alphabet[Random.Range(0, Alphabet.Length)];

        return pickedLetter.ToUpper();
    }

    public static string PickRandomLetter(List<string> list)
    {
        string pickedLetter;

        pickedLetter = list[Random.Range(0, list.Count)];

        return pickedLetter.ToUpper();
    }

    public static Color IntToColor(int color)
    {
        switch (color)
        {           
            case 0:
                return Color.yellow;
            case 1:
                return Color.cyan;
            case 2:
                return Color.white;
            case 3:
                return Color.white;

            default:
                return Color.clear;
        }
    }

    public static Player GetOpponentPlayer(Player player)
    {
        if(player.id == 0)
        {
            return Runtime.player_2;
        }
        else
        {
            return Runtime.player_1;
        }
    }
}
