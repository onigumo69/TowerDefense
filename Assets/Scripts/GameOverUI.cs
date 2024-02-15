using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("wavesSurviveText").GetComponent<TextMeshProUGUI>().
            SetText("You survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
