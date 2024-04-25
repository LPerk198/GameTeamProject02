using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float creationTime;

    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
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
