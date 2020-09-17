using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options: MonoBehaviour
{
    [Header("Backround Music")]
    public AudioSource backgroundMusicSource;
    public RadialSlider backgroundMusicSlider;
    [Header("Grapchics")]
    public HorizontalSelector horizontalSelector;
    [Header("Options Panel")]
    public AnimatedIconHandler showOptionsPanelBtn;
    public WindowManager optionsWindows;
    public GameObject optionsPanel;

    public LoadingBehavior loading;
    void Start()
    {

    }

    void Update()
    {

    }

    public void ShowOptionsPanel()
    {
        LoadSettings();
    }

    public void HideOptionsPanel()
    {

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("_Quality", horizontalSelector.index);
        PlayerPrefs.SetFloat("_BGMusicVolume", backgroundMusicSlider.currentValue);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("_Quality"));

        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("_Quality") && PlayerPrefs.HasKey("_BGMusicVolume"))
        {
            horizontalSelector.defaultIndex = PlayerPrefs.GetInt("_Quality");
            horizontalSelector.index = PlayerPrefs.GetInt("_Quality");
            backgroundMusicSlider.currentValue = PlayerPrefs.GetFloat("_BGMusicVolume");

            Debug.Log("" + PlayerPrefs.GetInt("_Quality") + "\t" + horizontalSelector.index);
        }
        else
        {
            PlayerPrefs.SetInt("_Quality", 1);
            PlayerPrefs.SetFloat("_BGMusicVolume", .5f);
            PlayerPrefs.Save();

            horizontalSelector.defaultIndex = PlayerPrefs.GetInt("_Quality");
            horizontalSelector.index = PlayerPrefs.GetInt("_Quality");
            backgroundMusicSlider.currentValue = PlayerPrefs.GetFloat("_BGMusicVolume");
        }  

        if (showOptionsPanelBtn != null)
            showOptionsPanelBtn.gameObject.SetActive(false);
        optionsPanel.SetActive(true);
        optionsWindows.OpenFirstTab();
    }

    public void BackgroundMusicSlideChanger()
    {
        backgroundMusicSource.volume = backgroundMusicSlider.currentValue / 100;
    }

    public void LoadMenu()
    {
        StartCoroutine(loading.GetComponent<LoadingBehavior>().LoadScene(0));
        //SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        StartCoroutine(loading.GetComponent<LoadingBehavior>().LoadScene(1));
        //SceneManager.LoadSceneAsync(1);
    }
}
