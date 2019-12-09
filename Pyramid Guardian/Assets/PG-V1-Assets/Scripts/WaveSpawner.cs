using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameMaster gameMaster;

    public static int numAliveEnemies;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenSpawns = 1f;
    public float timeBetweenWaves = 20f;
    private float cntDown = 2.5f;

    public Text waveCntDownTxt;

    private int waveIndex = 0;

    private void Update()
    {
        if(numAliveEnemies > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameMaster.LevelWin();
            this.enabled = false;
        }

        if (cntDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            cntDown = timeBetweenWaves;
            return;
        }

        cntDown -= Time.deltaTime;

        cntDown = Mathf.Clamp(cntDown, 0f, Mathf.Infinity);

        waveCntDownTxt.text = "Wave" + (waveIndex+1) + ": " + string.Format("{0:00}", cntDown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.roundsSurvived++;

        Wave wave = waves[waveIndex];

        numAliveEnemies = wave.enemySpawnCnt;

        for (int i = 0; i < wave.enemySpawnCnt; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;

        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
