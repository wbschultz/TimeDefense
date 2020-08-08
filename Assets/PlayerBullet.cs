using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    int despawnDelay = 5;

    private Coroutine despawn;
    // Start is called before the first frame update
    void Start()
    {
        despawn = StartCoroutine(KillSelfAfterSeconds(despawnDelay));
    }

    private void OnDisable()
    {
        StopCoroutine(despawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Do enemy stuff
            print("hit!");
            // kill projectile
            Destroy(gameObject);
        }
    }

    IEnumerator KillSelfAfterSeconds(int numSeconds)
    {
        yield return new WaitForSeconds(numSeconds);
        Destroy(gameObject);
    }
}
