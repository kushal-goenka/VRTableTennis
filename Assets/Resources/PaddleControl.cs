using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour {
    public GameObject paddle;
    public GameObject avatar;
    public GameObject player;
    private GameObject right_hand;
    private GameObject left_hand;
    public GameObject ball;
    public bool holdingPaddleRight;
    public bool holdingPaddleLeft;
    private bool collidingBall;
    private GameObject CollidingObject;
    // Use this for initialization
    void Start () {
        //right_hand = avatar.transform.GetChild(1).gameObject;
        collidingBall = false;
        holdingPaddleRight = false;
        holdingPaddleLeft = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //set up hand collider
        if (avatar.transform.childCount > 1)
        {
            left_hand = avatar.transform.GetChild(0).gameObject;
            right_hand = avatar.transform.GetChild(1).gameObject;
            if (!right_hand.GetComponent(typeof(Collider)))
            {
                right_hand.AddComponent<BoxCollider>();
                right_hand.GetComponent<Collider>().isTrigger = true;
                right_hand.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
            }
            if (!left_hand.GetComponent(typeof(Collider)))
            {
                left_hand.AddComponent<BoxCollider>();
                left_hand.GetComponent<Collider>().isTrigger = true;
                left_hand.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }

        //Paddle holding/releasing
        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.2f && CollidingObject && CollidingObject.name == "hand_right")
        {
            //PADDLE TRACKING
            avatar.transform.position = player.transform.position;
            //if hand is visible
            if (avatar.transform.childCount > 1)
            {
                paddle.GetComponent<Rigidbody>().isKinematic = true;
                paddle.GetComponent<Rigidbody>().useGravity = false;

                right_hand = avatar.transform.GetChild(1).gameObject;
                //put paddle to hand
                paddle.transform.position = right_hand.transform.position + paddle.transform.forward * -0.1f;
                //paddle.GetComponent<Rigidbody>().MovePosition((right_hand.transform.position + paddle.transform.forward * -0.1f)*Time.fixedDeltaTime);
                paddle.transform.rotation = right_hand.transform.rotation;
                paddle.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
                //Debug.Log(paddle.GetComponent<Rigidbody>().velocity);
                paddle.transform.Rotate(new Vector3(-45f, 0, 0));
                holdingPaddleRight = true;
                holdingPaddleLeft = false;
                //Debug.Log(Vector3.Distance(ball.transform.position, paddle.transform.position));
                if (Vector3.Distance(ball.transform.position, paddle.transform.position) < 0.25)
                {
                    ball.GetComponent<Rigidbody>().velocity = Vector3.Reflect(ball.GetComponent<Rigidbody>().velocity, paddle.transform.up) + paddle.GetComponent<Rigidbody>().velocity;
                }
            }
        }
        else if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.2f && CollidingObject && CollidingObject.name == "hand_left")
        {
            //PADDLE TRACKING
            avatar.transform.position = player.transform.position;
            //if hand is visible
            if (avatar.transform.childCount > 1)
            {
                paddle.GetComponent<Rigidbody>().isKinematic = true;
                paddle.GetComponent<Rigidbody>().useGravity = false;

                left_hand = avatar.transform.GetChild(0).gameObject;
                //put paddle to hand
                paddle.transform.position = left_hand.transform.position + paddle.transform.forward * -0.1f;
                //paddle.GetComponent<Rigidbody>().MovePosition((right_hand.transform.position + paddle.transform.forward * -0.1f)*Time.fixedDeltaTime);
                paddle.transform.rotation = left_hand.transform.rotation;
                paddle.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
                //Debug.Log(paddle.GetComponent<Rigidbody>().velocity);
                paddle.transform.Rotate(new Vector3(-45f, 0, 0));
                holdingPaddleLeft = true;
                holdingPaddleRight = false;
                //Debug.Log(Vector3.Distance(ball.transform.position, paddle.transform.position));
                if (Vector3.Distance(ball.transform.position, paddle.transform.position) < 0.25)
                {
                    ball.GetComponent<Rigidbody>().velocity = Vector3.Reflect(ball.GetComponent<Rigidbody>().velocity, paddle.transform.up) + paddle.GetComponent<Rigidbody>().velocity;
                }
            }
        }
        else
        {
            paddle.GetComponent<Rigidbody>().isKinematic = false;
            paddle.GetComponent<Rigidbody>().useGravity = true;
            holdingPaddleRight = false;
            holdingPaddleLeft = false;
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
        if (other.gameObject.name == "Ball")
        {
            //Debug.Log(other.gameObject.name);
            //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
            collidingBall = true;
            //TODO:
            //Ball Physics:
            //When ball enters trigger
            //Ball's velocity = Vector3.Reflect(Ball's velocity, Paddle's Normal) + (((Paddle's Velocity)))
            //if(Vector3.Magnitude(ball.GetComponent<Rigidbody>().velocity) < 1)
            //{
            //    ball.GetComponent<Rigidbody>().velocity = 5 * paddle.transform.up;
            //}
            //else
            //{
            //    ball.GetComponent<Rigidbody>().velocity = Vector3.Reflect(ball.GetComponent<Rigidbody>().velocity, paddle.transform.up);
            //}
            ball.GetComponent<Rigidbody>().velocity = Vector3.Reflect(ball.GetComponent<Rigidbody>().velocity, paddle.transform.up) + paddle.GetComponent<Rigidbody>().velocity;
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
        if ((other.gameObject.name == "hand_right" && CollidingObject && CollidingObject.name == "hand_right") || (other.gameObject.name == "hand_left" && CollidingObject && CollidingObject.name == "hand_left"))

        {

            CollidingObject = null;

        }

    }
}
