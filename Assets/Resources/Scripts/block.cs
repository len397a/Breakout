using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour {

    // Use this for initialization
    //public Transform prefab;
    public GameObject prefab;
    void Start()
    {
        prefab = Resources.Load("blockPrefab") as GameObject;
        for (int i = 0; i < 10; i++)
        {
            //Instantiate(prefab, new Vector3(-6.75F+(i * 1.5F), 4, 0), Quaternion.identity);
            for (int j = 0; j < 10; j++)
            {
                Instantiate(prefab, new Vector3(-6.75F + (i * 1.5F), 4F-j*0.5F, 0), Quaternion.identity);
            }
        }
    }
    // Update is called once per frame
    //public Rigidbody projectile;
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Rigidbody clone;
        //    clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        //    clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        //}
    }
}
