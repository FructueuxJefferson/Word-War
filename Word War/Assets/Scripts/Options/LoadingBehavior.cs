using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;
    public ProgressBar loading;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public IEnumerator LoadScene(int scene)
    {
        panel.SetActive(false);
        loading.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);

            loading.currentPercent = progress;

            yield return null;
        }
    }
}
