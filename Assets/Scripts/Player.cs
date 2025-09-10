using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);
        transform.Translate(0, moveV, 0);
    }
}
