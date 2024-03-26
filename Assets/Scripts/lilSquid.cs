using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Rendering.DebugUI.Table;

public class lilSquid : MonoBehaviour
{
    [SerializeField] float spawnForce;
    [SerializeField] GameObject whiteSquid;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Random.onUnitSphere * spawnForce, ForceMode.Impulse);
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("MagnetZone"))
        {
            rb.useGravity = false;
            Ball.caughtSquid++;
            GameObject dissolveMesh = Instantiate(whiteSquid, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(dissolveMesh, 5f);
        }
    }
}
