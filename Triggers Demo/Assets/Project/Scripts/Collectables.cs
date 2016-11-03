using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour
{
    /*
    Triggers Assignment - Plays a sound when you acquire a collectable, and incremenets the score

    Josh Bellyk - 100526009
    Owen Meier  - 100538643    
    */
    public int scoreInc = 10;
    public AudioSource collectItemAudioSource;


    void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player)
        {
            collectItemAudioSource.Play();
            collectItemAudioSource.transform.SetParent(null, true);
            Destroy(gameObject);

            player.score += scoreInc;
            player.updateScore();

        }
    }
}
