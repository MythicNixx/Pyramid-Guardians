using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform hitPoint;

    public float startSpeed = 20f;
    public float animStartSpeed = 0.5f;

    [HideInInspector] public float speed;

    public float startHealth = 100;
    private float health;

    private bool isDed = false;

    public GameObject deathEffect;
    public int moneyValue = 20;

    [Header("Unity Stuff")]
    public Image healthBar;
    public Animator anim;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !isDed)
        {
            Die();
        }
    }

    public void Slow(float slowPercentage)
    {
        anim.SetFloat("speed", (1f - slowPercentage));
        speed = startSpeed * (1f - slowPercentage);
    }

    void Die()
    {
        isDed = true;

        PlayerStats.Money += moneyValue;
        WaveSpawner.numAliveEnemies--;

        GameObject effect = (GameObject) Instantiate(deathEffect, hitPoint.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

}
