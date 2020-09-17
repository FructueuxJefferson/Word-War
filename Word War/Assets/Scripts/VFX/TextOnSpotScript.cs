using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextOnSpotScript: MonoBehaviour
{

    public string DisplayText;
    public Text TextPrefab;
    public float Speed;
    public float DestroyAfter;
    private float Timer;

    public Color color;

    public Transform cam;

    public float fadeSpeed;

    // Use this for initialization
    void Start()
    {
        Timer = DestroyAfter;
        TextPrefab = GetComponentInChildren<Text>();
        TextPrefab.color = color;
        transform.LookAt(2 * transform.position - cam.position);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Destroy(gameObject);
        }

        if (DisplayText != null)
        {
            TextPrefab.text = DisplayText;
        }

        if (Speed > 0)
        {
            TextPrefab.GetComponent<RectTransform>().transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
        }

        if (TextPrefab.color.a > 0 && fadeSpeed > 0)
        {
            float alpha = TextPrefab.color.a - fadeSpeed * Time.deltaTime;
            TextPrefab.color = new Color(color.r, color.g, color.b, alpha);
        }
    }
}
