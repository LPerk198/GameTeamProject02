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
        transform.Translate(Vector3.left * 0.03f, Space.Self);
        transform.Rotate(-90f, 0, 0, Space.Self);
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
