using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenWaves = 10f;
    [SerializeField]
    private TextMeshProUGUI waveText;
    
    private List<Wave> waves;
    private Wave currentWave;
    private CountdownTimer nextWaveTimer;
    private int waveNum;

    private void Awake()
    {
        waveNum = 1;
        waves = new List<Wave>(GetComponentsInChildren<Wave>());
        nextWaveTimer = new CountdownTimer(timeBetweenWaves);
        currentWave = waves[waveNum - 1];
        //waveText.text = "Wave " + waveNum;
    }

    private void Start()
    {
        EventHandler.Instance.AddListener(EventType.WaveComplete, OnWaveComplete);
        currentWave.StartWave();
    }

    private void OnWaveComplete()
    {
        StartCoroutine(WaveSwitchRoutine());
    }

    private IEnumerator WaveSwitchRoutine()
    {
        while (!nextWaveTimer.Count())
        {
            //waveText.text = "Next wave in " + Mathf.Ceil(nextWaveTimer.Countdown);
            yield return null;
        }

        SwitchWave();
    }

    private void SwitchWave()
    {
        if (waveNum == waves.Count)
        {
            GameManager.Instance.EndGame();
        }
        else
        {
            waveNum++;
            currentWave = waves[waveNum - 1];
            nextWaveTimer.Reset();
            //waveText.text = "Wave " + waveNum;
            currentWave.StartWave();
        }
    }
}
