using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolding : MonoBehaviour {
    public GameObject avatar;
    public GameObject paddle;
    private GameObject right_hand;
    private GameObject left_hand;
    private GameObject CollidingObject;
    private bool holdingPaddleLeft;
    private bool holdingPaddleRight;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        holdingPaddleLeft = paddle.GetComponent<PaddleControl>().holdingPaddleLeft;
        holdingPaddleRight = paddle.GetComponent<PaddleControl>().holdingPaddleRight;
        if (avatar.transform.childCount > 1)
        {
            left_hand = avatar.transform.GetChild(0).gameObject;
            right_hand = avatar.transform.GetChild(1).gameObject;
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.2f && CollidingObject && CollidingObject.name == "hand_right" && !holdingPaddleRight)
        {
            //if hand is visible
            if (avatar.transform.childCount > 1)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                right_hand = avatar.transform.GetChild(1).gameObject;
                //put gameObject to hand
                gameObject.transform.position = right_hand.transform.position + gameObject.transform.forward * -0.1f;
                //gameObject.GetComponent<Rigidbody>().MovePosition((right_hand.transform.position + gameObject.transform.forward * -0.1f)*Time.fixedDeltaTime);
                gameObject.transform.rotation = right_hand.transform.rotation;
                gameObject.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
                //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
                //gameObject.transform.Rotate(new Vector3(-45f, 0, 0));
            }
        }
        else if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.2f && CollidingObject && CollidingObject.name == "hand_left" && !holdingPaddleLeft)
        {
            //if hand is visible
            if (avatar.transform.childCount > 1)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;

                left_hand = avatar.transform.GetChild(0).gameObject;
                //put gameObject to hand
                gameObject.transform.position = left_hand.transform.position + gameObject.transform.forward * -0.1f;
                //gameObject.GetComponent<Rigidbody>().MovePosition((right_hand.transform.position + gameObject.transform.forward * -0.1f)*Time.fixedDeltaTime);
                gameObject.transform.rotation = left_hand.transform.rotation;
                gameObject.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
                //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
                //gameObject.transform.Rotate(new Vector3(-45f, 0, 0));
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }



    }

    //TODO: Implement holding Ball as well
    void OnTriggerEnter(Collider other)

    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "hand_right" && !CollidingObject)
        {

            CollidingObject = right_hand;

        }
        if (other.gameObject.name == "hand_left" && !CollidingObject)
        {
            CollidingObject = left_hand;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (CollidingObject == null)
        {
            if (other.gameObject.name == "hand_right")
            {
                CollidingObject = right_hand;
            }
            else if (other.gameObject.name == "hand_left")
            {
                CollidingObject = left_hand;
            }
        }
    }
    void OnTriggerExit(Collider other)

    {
        //Debug.Log(other.gameObject.name);
        if (CollidingObject && (other.gameObject.name == "hand_right" && CollidingObject.name == "hand_right") || (other.gameObject.name == "hand_left" && CollidingObject.name == "hand_left"))

        {

            CollidingObject = null;

        }

    }
}
