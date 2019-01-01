using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tiltHorizontal;
    public float tiltVertical;
    public Boundary boundary;
    public GameObject shot;
    public float fireRate;
    public Transform shotSpawnTransform;
    public AudioSource audioSource;

    private float nextFireTimestamp;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(rb.velocity.z * tiltVertical, 0, rb.velocity.x * -tiltHorizontal);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTimestamp)
        {
            Instantiate(shot, shotSpawnTransform.position, shotSpawnTransform.rotation);
            audioSource.Play();
            nextFireTimestamp = Time.time + fireRate;
        }
    }
}
