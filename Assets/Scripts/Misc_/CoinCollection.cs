using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;

    [SerializeField] private AudioClip pickUpCoin;

    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Coin")
        {
            SoundFXManager.instance.PlaySoundFXClip(pickUpCoin, transform, 1f);
            Coin++;
            coinText.text = "Coins:" + Coin.ToString();
            Debug.Log(Coin);
            Destroy(other.gameObject);
        }
    }
}
