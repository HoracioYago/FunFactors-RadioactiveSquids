using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] WaitForSeconds magnetDuration = new WaitForSeconds(0.5f);
    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] GameObject fishPrefab;
    [SerializeField] Rigidbody rb;
    [SerializeField] float hitForce;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }  

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (collision.gameObject.CompareTag("Harpoon"))
        {
            //instantiate more fish on hit
            waterParticle.Play();
            rb.AddForce(pos * hitForce, ForceMode.Impulse);
            for (int i = 0; i < 20; i++)
            {
                Instantiate(fishPrefab, pos, rot);
            }
            
            gameObject.SetActive(false);
        
        }
        if (collision.gameObject.CompareTag("Sea"))
        {
            Instantiate(waterParticle, pos, rot);
        }
            
    }

    private IEnumerator Delay()
    {
        yield return magnetDuration;
    }

}
