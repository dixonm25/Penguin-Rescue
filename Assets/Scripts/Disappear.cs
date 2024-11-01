using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            StartCoroutine(DelayDeactivate());
        }  
    }

    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
    }

}
