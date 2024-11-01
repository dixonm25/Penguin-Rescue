using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float hideDelay = 1f;
    public float showDelay = 2f;

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.name == "Player")
        {
            HideAndShow();       
        } 
    }

    private void HideAndShow()
    {
        Invoke("Hide", hideDelay);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

        Invoke("Show", showDelay);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
