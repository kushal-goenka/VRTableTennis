using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerside : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }



    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Collision here" + collision.gameObject.name);


        //Debug.Log("Do something here");

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Paddle")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Paddle fell here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            //Debug.Log("Do something else here");
        }
    }

}


