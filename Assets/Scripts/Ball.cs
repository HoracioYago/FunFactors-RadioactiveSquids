using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Ball : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] ParticleSystem magnetParticle, gatherPS;
    [SerializeField] GameObject player, magnetZone, returnSpot, neonSquid;

    [SerializeField] float magnetSpeed, spawnNeonForce;

    Rigidbody rb;
    private bool isReturning;
    private Vector3 cameraForward;
    public static bool isShot;
    public static int caughtSquid;
    private Vector3 originalPosition;

    private AudioSource gunAudio;
    public AudioClip pulseAudio;
    public AudioClip reloadAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunAudio = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        cameraForward = player.transform.forward;
        if (isShot)
        {
            transform.position += cameraForward * magnetSpeed * Time.deltaTime;
        }
        /*if (isReturning)
        {
            //transform.position += -cameraForward * magnetSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, returnSpot.transform.position, 5f);
        }*/
        
        if (Vector3.Distance(transform.position, player.transform.position) > 30)
        {
            returnSpot.SetActive(true);
            StartCoroutine(Return(2f));
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BigSquid"))
        {
            StopAllCoroutines();
            //StartCoroutine(ReturnOff(0f));
            StartCoroutine(Magnetize(0f));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("BigSquid"))
        {
            StopAllCoroutines();
            //StartCoroutine(ReturnOff(0f));
            StartCoroutine(Magnetize(0f));
        }*/
        if (other.gameObject.CompareTag("ReturnSpot"))
        {
            StartCoroutine(Delay(1f));
            RocketLauncher.isReloading = false;
            StartCoroutine(ReturnOff(1f));
        }
    }

    private IEnumerator Magnetize(float delay)
    {
        yield return new WaitForSeconds(delay);
        gunAudio.clip = pulseAudio;
        gunAudio.Play();
        isShot = false;
        returnSpot.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject stasis = Instantiate(magnetZone, transform.position, Quaternion.identity);
        ParticleSystem atomizer = Instantiate(magnetParticle, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(3f);
        gunAudio.clip = reloadAudio;
        gunAudio.Play();
        ParticleSystem warp = Instantiate(gatherPS, returnSpot.transform.position, Quaternion.identity);
        transform.position = returnSpot.transform.position;

        Destroy(stasis, 1f);
        Destroy(atomizer, 100f*Time.deltaTime);
        Destroy(warp, 2f);
    }

    private IEnumerator Dispense(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        
        /* ABBANDONED METHOD FOR DISPENSING DISSOLVED SQUIDS 
        for (int i = 0; i < caughtSquid; )
        {
            Debug.Log("caughtSquid " + caughtSquid);
            GameObject neonMesh = Instantiate(neonSquid, new Vector3(transform.position.x,
            transform.position.y - 3f, transform.position.z), transform.rotation);
            //Rigidbody rb = neonMesh.GetComponent<Rigidbody>();
            //rb.AddForce(new Vector3(0, transform.position.y - spawnNeonForce, 0) , ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
            caughtSquid--;
        }*/
        //yield return new WaitForSeconds(delay);
        StartCoroutine(Return(2f));
       
    }
    private IEnumerator Return(float delay)
    {
        
        isShot = false;
        //returnSpot.SetActive(true);
        //returnSpot.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(delay);
        //Debug.Log("Return");
        gunAudio.clip = reloadAudio;
        gunAudio.Play();
        transform.position = Vector3.Lerp(transform.position, returnSpot.transform.position, 5f*Time.deltaTime);
        
        //isReturning = true;
    }
    private IEnumerator ReturnOff(float delay)
    {
        //isReturning = false;
        yield return new WaitForSeconds(delay);
        returnSpot.SetActive(false);
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
