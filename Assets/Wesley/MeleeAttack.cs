using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float creationTime;

    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
        transform.Rotate(0, 0, 45f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(200f * Time.deltaTime, 0, 0, Space.World);
        if (creationTime + 1 < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
