using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public int playerHP;
    public TMP_Text hpCount;

    // Start is called before the first frame update
    void Start()
    {
        hpCount.SetText("HP: " + playerHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            playerHP--;
            hpCount.SetText("HP: " + playerHP);
        }
    }
}
