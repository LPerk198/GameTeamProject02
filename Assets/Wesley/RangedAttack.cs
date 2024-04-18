using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    private float creationTime;

    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
        transform.Rotate(90f, 0, 0, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if (creationTime + 10 < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
