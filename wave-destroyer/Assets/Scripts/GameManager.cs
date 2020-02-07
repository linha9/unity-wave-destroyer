using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Scene deathScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        deathScene = SceneManager.GetSceneByName("DeadScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("DeadScene");
    }
}
