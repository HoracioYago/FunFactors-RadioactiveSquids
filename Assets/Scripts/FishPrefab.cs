using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishPrefab : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] WaitForSeconds magnetDuration = new WaitForSeconds(0.5f);
    [SerializeField] Rigidbody rb;
    [SerializeField] float spawnForce;
    [SerializeField] GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Random.onUnitSphere * spawnForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        
       

    }
    private IEnumerator Delay()
    {
        yield return magnetDuration;
    }

    private IEnumerator Magnetize()
    {
        rb.useGravity = false;
        transform.parent = ball.transform;
        StartCoroutine(Delay());
        transform.parent = null;
        rb.useGravity = true;
        yield return 0;
    }
}
