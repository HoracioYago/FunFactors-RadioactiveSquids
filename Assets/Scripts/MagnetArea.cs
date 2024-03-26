using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Magnetize()
    {
        /*burstParticle.Play();

        foreach (RaycastHit hit in hits)
        {

            hit.transform.parent = magnet.transform;
            hit.rigidbody.useGravity = false;
            Instantiate(magnetParticle, hit.transform.position, Quaternion.identity);
            StartCoroutine(Delay());
            hit.transform.parent = null;
            hit.rigidbody.useGravity = true;
        }*/

        yield return null;

    }

}
