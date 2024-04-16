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
        Debug.Log("Create");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(200f * Time.deltaTime, 0, 0, Space.Self);
        if (creationTime + 1 < Time.time)
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}
