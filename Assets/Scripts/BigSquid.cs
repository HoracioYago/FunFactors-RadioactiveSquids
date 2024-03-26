using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BigSquid : MonoBehaviour
{
    public Animator anim;
    public float impulse, hitForce;
    public GameObject player, lilSquid;
    
    private Collider col;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();

        rb = GetComponent<Rigidbody>();
        rb.AddForce (new Vector3 (0, impulse, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(-player.transform.position);
        if (transform.position.y >= 20)
        {
            col.enabled = true;
            anim.SetBool("Falling", true);
            rb.mass = 0.5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (collision.gameObject.CompareTag("Cube"))
        {
            Debug.Log("Hit");
            rb.AddForce(pos * hitForce, ForceMode.Impulse);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(lilSquid, pos, rot);
            }
            GameManager.squidCount-=0.5f;
            Sea.score += 500;
            //Sea.backBar.SetTrigger("Score");
            //Sea.frontBar.SetTrigger("Score");
            Sea.scoreBar.SetTrigger("Score");
            Destroy(gameObject);

        }
    }

}
