// EnemyFSM.cs
// Maintainer: Qingzhe-Song
// Summary: Uses a NavMeshAgent and sight sensor to approach and attack the base or player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState {GoToBase, AttackBase, ChasePlayer, AttackPlayer}
    [Header("State")]
    [Tooltip("Current behavior selected by the enemy's state transitions.")]
    public EnemyState currentState;

    [Header("Targeting")]
    [Tooltip("Sight component that provides the currently detected player collider.")]
    public Sight sightSensor;
    [Tooltip("Base aim point; Awake replaces this with the object named BaseDamagePoint.")]
    public Transform baseTransform;
    [Tooltip("Base attack range in world units; the approach check adds a four-unit offset.")]
    public float baseAttackDistance;
    [Tooltip("Player attack range in world units; chasing resumes beyond 110% of this range.")]
    public float playerAttackDistance;
    private NavMeshAgent agent;

    [Header("Weapon")]
    [Tooltip("Projectile spawned from this component's position and rotation.")]
    public GameObject bulletPrefab;
    [InspectorName("Shot Interval (Seconds)")]
    [Tooltip("Minimum elapsed time between shots. Smaller values fire more frequently.")]
    public float fireRate;

    [Header("Shot Timing")]
    [InspectorName("Last Shot Time (Seconds)")]
    [Tooltip("Time.time recorded for the previous shot, used to enforce the shot interval.")]
    public float lastShootTime;
    // Start is called before the first frame update
    void Awake()
    {
        // Spawned enemies resolve the scene's shared base aim point by its object name.
        baseTransform = GameObject.Find("BaseDamagePoint").transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.GoToBase)
        {
            GoToBase();
        }
        else if (currentState == EnemyState.AttackBase)
        {
            AttackBase();
        }
        else if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void GoToBase()
    {
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            return;
        }

        float distanceTobase = Vector3.Distance(transform.position, baseTransform.position);

        // Include the existing four-unit approach offset when deciding to attack the base.
        if (distanceTobase < baseAttackDistance + 4)
        {
            currentState = EnemyState.AttackBase;
        }
    }

    void AttackBase()
    {
        agent.isStopped = true;
        LookTo(baseTransform.position);
        Shoot();
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }

        agent.SetDestination(sightSensor.detectedObject.transform.position);
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        // The wider exit range prevents rapid switching at the attack-distance boundary.
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

    void Shoot()
    {
        var timeSinceLastShot = Time.time - lastShootTime;
        if (timeSinceLastShot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        // Turn the parent body on the ground plane so aiming does not tilt the enemy.
        Vector3 directionToPosition  = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
