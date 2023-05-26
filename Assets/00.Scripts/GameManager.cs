using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GameObject fadeObj = null;
    public Image fadeImg = null;
    private Color fadeTransparency = new Color(0, 0, 0, 0.04f);
    public bool isGameOver = false;

    public float fadeSpeed = 0.02f;
    private string currentScene;

    private AsyncOperation async;
    
    private void Awake()
    {
        if (inst == null)
        {
            DontDestroyOnLoad(this.gameObject);
            inst = GetComponent<GameManager>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(this.gameObject);
    }

    public void LoadScene(string _sceneName)
    {
        if (_sceneName == "Play")
            AudioManager.inst.PlaySFX("Start");
        else
            AudioManager.inst.PlaySFX("Main");

        inst.StartCoroutine(Load(_sceneName));
        inst.StartCoroutine(FadeOut(inst.fadeObj, inst.fadeImg));
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name); // 현재씬 재 리로드.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // scene 로드될때마다 실행.
    {
        currentScene = scene.name;
        Debug.Log(currentScene);
        AudioManager.inst.PlayBGM(currentScene);
        inst.StartCoroutine(FadeIn(inst.fadeObj, inst.fadeImg));
    }

    IEnumerator FadeIn(GameObject _fadeObj, Image fadeimg)
    {
        _fadeObj.SetActive(true);
        while (fadeimg.color.a > 0)
        {
            fadeimg.color -= fadeTransparency;
            yield return new WaitForSeconds(fadeSpeed);
        }
        fadeObj.SetActive(false);
    }

    IEnumerator FadeOut(GameObject _fadeObj, Image fadeimg)
    {
        _fadeObj.SetActive(true);
        while(fadeimg.color.a < 1)
        {
            fadeimg.color += fadeTransparency;
            yield return new WaitForSeconds(fadeSpeed);
        }
        ActivateScene();
    }

    IEnumerator Load(string _sceneName)
    {
        async = SceneManager.LoadSceneAsync(_sceneName);
        async.allowSceneActivation = false; // 곧 바로 화면전환되는 것을 막음. true 가 되면 다시 활성화.
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true; // scene 활성화.
    }
}
