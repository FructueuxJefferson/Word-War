using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runtime
{
    public static Player player_1, player_2;
    public static List<DynamicLetter> askedWordLetters = new List<DynamicLetter>();
    public static int currentIndex = 0;

    public static DynamicLetter currentLetter;

    public static float timeToFadeAgain, timeToWaitAfterFade;

    public static int minLetterValue = 1, maxLetterValue = 10, lostOnFail, lostOnRubish;

    public static string player1Name, player2Name;

    public static Color player1Color, player2Color;

    public static int MaxScore;

    public static bool ShowTips;

    public static List<string> matchsHistory;
}
