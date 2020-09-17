using UnityEngine;

public class SendMailCrossPlatform
{

    /// <param name="email"> myMail@something.com</param>
    /// <param name="subject">my subject</param>
    /// <param name="body">my body</param>
    public static void SendEmail(string email, string subject, string body)
    {
        subject = MyEscapeURL(subject);
        body = MyEscapeURL(body);
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    public static string MyEscapeURL(string url)
    {
#pragma warning disable CS0618 // Le type ou le membre est obsolète
        return WWW.EscapeURL(url).Replace("+", "%20");
#pragma warning restore CS0618 // Le type ou le membre est obsolète
    }
}