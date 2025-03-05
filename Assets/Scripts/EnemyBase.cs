using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;
    public int maxHealth = 15;
    private int currentHealth;
    protected Transform player;
    protected bool isUsingAbility = false;

    public float patrolSpeed = 1f;
    public float patrolRange = 3f;
    public float patrolWaitTime = 2f;
    private Vector2 patrolTarget;
    private bool isPatrolling = false;

    public Slider healthBar;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        currentHealth = maxHealth;
        UpdateHealthBar();
        StartCoroutine(PatrolRoutine()); // ✅ Start patrolling
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && !isUsingAbility)
        {
            isPatrolling = false; // Stop patrolling when chasing
            MoveTowardPlayer();
        }
        else if (!isPatrolling)
        {
            StartCoroutine(PatrolRoutine()); // Resume patrolling if player is out of range
        }

        // Ensure ability is only triggered if it isn't already being used
        if (!isUsingAbility && ShouldUseAbility(distanceToPlayer))
        {
            StartCoroutine(UseAbility());
        }
    }


    protected virtual void MoveTowardPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private IEnumerator PatrolRoutine()
    {
        isPatrolling = true;

        while (player == null || Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            patrolTarget = (Vector2)transform.position + Random.insideUnitCircle * patrolRange;
            yield return StartCoroutine(MoveToPatrolPoint(patrolTarget));

            yield return new WaitForSeconds(patrolWaitTime); // Pause before choosing new point
        }

        isPatrolling = false; // Stop patrolling when player is detected
    }

    private IEnumerator MoveToPatrolPoint(Vector2 target)
    {
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    protected abstract bool ShouldUseAbility(float distanceToPlayer);
    protected abstract IEnumerator UseAbility();
}
