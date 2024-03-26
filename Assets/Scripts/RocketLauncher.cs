using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class RocketLauncher : MonoBehaviour
{
    [SerializeField] float speed, camSensitivity;

    //[SerializeField] Camera cam;

    [SerializeField] ParticleSystem gatherPS;
    [SerializeField] Animator animator;

    [SerializeField] GameObject magnet;
    [SerializeField] Animator cubeAnimator;

    [SerializeField] WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    [SerializeField] WaitForSeconds magnetDuration = new WaitForSeconds(0.5f);
    [SerializeField] Material regularMaterial, atomizedMaterial;
    [SerializeField] GameObject atomRodsL, atomRodsR, atomBall;

    private AudioSource gunAudio;
    public AudioClip shotAudio;
    

    //private Vector3 cameraForward;
    public static bool isReloading;
    public static Transform playerRot;

    private float rodsIntensity = 0f, ballIntensity = 0f;
    void Start()
    {
        regularMaterial = atomBall.GetComponent<Renderer>().material;
        gunAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        
        //Vector3 aimOrigin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        float rotateHorizontal = Input.GetAxis("Horizontal");
        //float rotateHorizontal = Input.GetAxis("Mouse X");
        //float rotateVertical = Input.GetAxis("Mouse Y");
        Vector3 rotation = new Vector3(0, -rotateHorizontal * camSensitivity *Time.deltaTime, 0);
        //transform.eulerAngles -= new Vector3(0, -rotateHorizontal * camSensitivity, 0); 
        transform.eulerAngles -= rotation;

        transform.position += transform.forward*speed*Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && !isReloading) 
        {
            
            Debug.Log("SHOT");
            speed = 0;
            camSensitivity = 0;
            StartCoroutine(ShotEffect());
        } 
        else if (!isReloading)
        {   
            //atomRodsMaterial.DisableKeyword("_EMISSION");
            speed = 5;
            camSensitivity = 60;
            cubeAnimator.SetBool("Windup", false);
            
            atomRodsL.GetComponent<Renderer>().material = atomizedMaterial;
            atomRodsR.GetComponent<Renderer>().material = atomizedMaterial;
            atomBall.GetComponent<Renderer>().material = regularMaterial;
            //ballIntensity = Mathf.Lerp(0f, -3.5f, 5f * Time.deltaTime);
            //atomBallMaterial.DisableKeyword("_EMISSION");
        }

        if (Input.GetKey(KeyCode.UpArrow) && !isReloading)
        {
            speed = 15;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !isReloading)
        {
            speed = 0;
        }

        /*Color rodsColor = atomRodsMaterial.color;
        atomRodsMaterial.SetColor("_EmissionColor", rodsColor * rodsIntensity);
        Color ballColor = atomBallMaterial.color;
        atomBallMaterial.SetColor("_EmissionColor", ballColor * ballIntensity);
        */
    }

    private IEnumerator ShotEffect()
    {
        
        //rodsIntensity = Mathf.Lerp(0, 5f, 5f * Time.deltaTime);
        isReloading = true;
        animator.SetTrigger("Shot");
        StartCoroutine(Spin(0.2f));
        yield return new WaitForSeconds(0.5f);
        gunAudio.clip = shotAudio;
        gunAudio.Play();

        yield return null;
    }

    private IEnumerator Spin(float delay)
    {
        yield return new WaitForSeconds(delay);
        //atomBallMaterial.EnableKeyword("_EMISSION");
        //ballIntensity = Mathf.Lerp(0, 3.5f, 5f*Time.deltaTime);
        ParticleSystem burst = Instantiate(gatherPS, magnet.transform.position, Quaternion.identity);
        atomRodsL.GetComponent<Renderer>().material = regularMaterial;
        atomRodsR.GetComponent<Renderer>().material = regularMaterial;
        atomBall.GetComponent<Renderer>().material = atomizedMaterial;
        cubeAnimator.SetBool("Windup", true);
        
        StartCoroutine(ShotOut(0.8f));
        Destroy(burst, 3f);
        yield return null;
    }
    private IEnumerator ShotOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        //rodsIntensity = Mathf.Lerp(0f, -5f, 5f * Time.deltaTime);
        //harpoonAudio.clip = shotAudio;
        //harpoonAudio.Play();
        
        Ball.isShot = true;
        
    }


}


