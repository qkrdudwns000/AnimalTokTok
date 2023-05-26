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
        LoadScene(SceneManager.GetActiveScene().name); // ����� �� ���ε�.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // scene �ε�ɶ����� ����.
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
        async.allowSceneActivation = false; // �� �ٷ� ȭ����ȯ�Ǵ� ���� ����. true �� �Ǹ� �ٽ� Ȱ��ȭ.
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true; // scene Ȱ��ȭ.
    }
}
