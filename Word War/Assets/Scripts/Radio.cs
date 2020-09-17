using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("_BGMusicVolume") / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
