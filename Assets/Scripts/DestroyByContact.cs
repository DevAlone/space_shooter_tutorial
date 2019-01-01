using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosionVFX;
    public GameObject playerExplosionVFX;
    public uint scoreValue;

    private GameController gameController;

    private void Start()
    {
        var gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (this.tag == "Enemy" && other.tag == "EnemyBolt")
        {
            return;
        }*/

        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosionVFX, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);

        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
