using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform moveTarget;
    private int wavePointIndex = 0;
    private PlayerStats playerStats;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        moveTarget = WayPoints.points[wavePointIndex];
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Vector3 dir = moveTarget.position - transform.position;
        transform.LookAt(moveTarget.position);
        //transform.LookAt()
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, moveTarget.position) <= 0.3f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            EndReached();
            return;
        }

        wavePointIndex++;
        moveTarget = WayPoints.points[wavePointIndex];
    }

    void EndReached()
    {
        playerStats.SubtractLives();
        WaveSpawner.numAliveEnemies--;
        Destroy(gameObject);
    }
}
