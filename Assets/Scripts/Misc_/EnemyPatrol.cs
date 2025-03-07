using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    int current;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        current = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == points[current].position)
        {
            increaseCurrentInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
    }

    void increaseCurrentInt()
    {
        current++;
        if (current >= points.Length)
        {
            current = 0;
        }
    }
}
