using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private List<MobSpawner> mobSpawners;
    [SerializeField]
    private int tMobsAmount;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private TextMeshProUGUI mobsAliveText;

    private const float T_SPAWN_ROUTINE = .1f;

    private int mobsSpawned;
    public int MobsAlive { get; private set; }
    public bool IsWaveComplete { get; private set; }
    private CountdownTimer spawnTimer;

    public void StartWave()
    {
        if (!IsWaveComplete)
        {
            //mobsAliveText.text = tMobsAmount.ToString();
            MobsAlive = tMobsAmount;
            EventHandler.Instance.AddListener(EventType.EnemyDeath, OnMobDeath);
            spawnTimer = new CountdownTimer(spawnDelay);
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {   
        while (mobsSpawned < tMobsAmount)
        {
            if (spawnTimer.Count(T_SPAWN_ROUTINE))
            {

                PickRandomSpawner().Spawn().transform.parent = transform;
                mobsSpawned++;
                spawnTimer.Reset();
            }

            yield return new WaitForSeconds(T_SPAWN_ROUTINE);
        }
    }

    private void OnMobDeath()
    {
        MobsAlive--;
        mobsAliveText.text = MobsAlive.ToString();

        if (mobsSpawned >= tMobsAmount && MobsAlive == 0)
        {
            FinishWave();
        }
    }

    private void FinishWave()
    {
        IsWaveComplete = true;
        EventHandler.Instance.Invoke(EventType.WaveComplete);
        EventHandler.Instance.DeleteListener(EventType.EnemyDeath, OnMobDeath);
    }

    private MobSpawner PickRandomSpawner()
    {
        return mobSpawners[Random.Range(0, mobSpawners.Count)];
    }
}
