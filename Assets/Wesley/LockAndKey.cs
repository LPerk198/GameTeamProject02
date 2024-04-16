using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockAndKey : MonoBehaviour
{
    public GameObject door;
    public float doorSpeed;
    private Vector3 targetPosition;
    public float waitSeconds;
    public GameObject doorOpenTxt;
    public Image doorInd;
    public GameObject keyUI;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = door.transform.position + (Vector3.up * 3.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50f * Time.deltaTime, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveTheDoor());
        }
    }

    private IEnumerator MoveTheDoor()
    {
        yield return new WaitForSeconds(waitSeconds);
        gameObject.transform.position += Vector3.down * 1000f;
        keyUI.SetActive(true);
        doorOpenTxt.SetActive(true);
        doorInd.color = new Color(doorInd.color.r, doorInd.color.g, doorInd.color.b, 255);
        while (door.transform.position != targetPosition)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, doorSpeed * Time.deltaTime);
            yield return null;
        }
        doorOpenTxt.SetActive(false);
        Destroy(gameObject);
    }
}
