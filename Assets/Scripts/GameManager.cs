using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject bigSquid, player, signalLight;
    public float randX = 300, randZ = 300;
    public static float squidCount = 0;
    private float time, timeDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timeDelay = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = time + timeDelay * Time.deltaTime;
        if (time >= timeDelay )
        {
            Debug.Log(squidCount);
            if (squidCount < 3)
            {
                squidCount++;
                StartCoroutine(SpawnSquid());
            }
            time = 0;
        }     
    }
    private IEnumerator SpawnSquid()
    {   
        float x = Random.Range(-randX,randX);
        float z = Random.Range(-randZ, randZ);
        GameObject light = Instantiate(signalLight, new Vector3(x, 9, z), Quaternion.identity);
        yield return new WaitForSeconds(100f * Time.deltaTime);
        Destroy(light, 30f * Time.deltaTime);
        Instantiate(bigSquid, new Vector3(x, -8, z), Quaternion.identity);
        yield return null;
    }
}
