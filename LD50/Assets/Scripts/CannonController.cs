using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CannonController : InteractableGeneric
{
    // Start is called before the first frame update
    public bool cannonStatus;
    public float gunpowderStack;
    public float depletionRate;
    public int enemySpawnFactor;
    public GameObject ship;

    private bool activated;

    public override void Interaction(GameObject player)
    {
        if (!activated)
        {
            activated = true;
            Camera.main.transform.parent = GameObject.Find("Center").transform;
            Camera.main.transform.position = GameObject.Find("GunpowderStorage").transform.position;
            Camera.main.transform.position += new Vector3(0, 0, -10);
        }
        else
        {
            activated = false;
            Camera.main.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            Camera.main.transform.position = GameObject.Find("Player").transform.position;
            Camera.main.transform.position += new Vector3(0, 0, -10);
            GameObject.Find("Player").GetComponent<PlayerController>().interacting = false;
        }
    }


    void Start()
    {
        cannonStatus = true;
        gunpowderStack = 100;
        depletionRate = .1f;
        enemySpawnFactor = 2;
        StartCoroutine(depleteCoroutine());
        activated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gunpowderStack< 1)
        {
            cannonStatus = false;
            gunpowderStack = 0;
            ship.GetComponent<ShipController>().enemySpawnChance *=enemySpawnFactor;
            
        }
        
    }
    IEnumerator depleteCoroutine()
    {
        while (gunpowderStack>0)
        {
            gunpowderStack *= 1 - depletionRate;
            yield return new WaitForSeconds(1.0f);
            
        }
    }

}
