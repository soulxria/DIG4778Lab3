using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 2;
    public float avoidanceStrength = 2f;
    public float avoidanceRadius = 1f;
    public float speed;
    public float distance;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 toPlayer = (player.position - transform.position).normalized;
        distance = toPlayer.magnitude;

        Vector2 toPlayerNormalized = toPlayer.normalized;
        Vector2 orbitDir = new Vector2(-toPlayer.y, toPlayer.x).normalized;
        speed = baseSpeed + distance * 0.5f;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, avoidanceRadius);
    
        if (hit != null && hit.gameObject != gameObject)
            {
                Vector2 awayvariable = (transform.position - hit.transform.position).normalized;
                orbitDir += awayvariable * avoidanceStrength;
            }

        orbitDir.Normalize();


        Vector2 velocity = orbitDir * speed * Time.deltaTime;
        rb.MovePosition(velocity + rb.position);

        float angleToPlayer = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetrot, 5 * Time.deltaTime);

    }
}
