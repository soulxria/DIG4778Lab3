using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 2f;
    public float orbitFactor = 0.7f; // how much is sideways vs inward pull
    public float avoidanceStrength = 2f;
    public float avoidanceRadius = 1f;
    public float speed;
    public float distance;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 toPlayer = (player.position - transform.position);
        distance = toPlayer.magnitude;
        Vector2 toPlayerNormalized = toPlayer.normalized;

        // Orbit direction (perpendicular to player vector)
        Vector2 orbitDir = new Vector2(-toPlayerNormalized.y, toPlayerNormalized.x);

        // Mix orbit and pull so enemy doesn't drift away
        Vector2 moveDir = (toPlayerNormalized * (1f - orbitFactor)) + (orbitDir * orbitFactor);

        // Speed scales a little with distance
        speed = baseSpeed + distance * 0.3f;

        // Avoidance from other enemies
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
        foreach (Collider2D h in hits)
        {
            if (h != null && h.gameObject != gameObject)
            {
                Vector2 away = (transform.position - h.transform.position).normalized;
                moveDir += away * avoidanceStrength;
            }
        }

        moveDir.Normalize();

        // Move enemy
        Vector2 newPos = rb.position + moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        // Rotate to face movement direction
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}