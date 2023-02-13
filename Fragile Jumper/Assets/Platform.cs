using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private bool playerTouched = true;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (playerTouched)
        {
            if (other.gameObject.tag == "Player")
            {
                Vector3 newPos = transform.position;
                newPos.y += UnityEngine.Random.Range(2f,4f); // Offset the position in the y-direction
                Instantiate(prefabToInstantiate, newPos, Quaternion.identity).name = "Platform";
                playerTouched = false;
            }
            
        }
        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
