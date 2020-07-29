using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class gameplay : MonoBehaviour {

    public static int playerPaddleCollision = 0;
    public static int enemyPaddleCollision = 0;

    public GameObject playerside;
    public GameObject enemyside;
    public GameObject avatar;
    

    private GameObject right_hand;
    private GameObject left_hand;

    public GameObject paddle;

    public GameObject ball;

    public BoxCollider paddleBox;
    public BoxCollider enemypaddleBox;

    public GameObject text_score;

    public GameObject enemyPaddle;

    public float speed;

    // Use this for initialization
    void Start () {

        
}
	
	// Update is called once per frame
	void Update () {
        if (ballbounce.enemyFlag)
        {
            follow_ball();
        }
        if (ballbounce.resetFlag)
        {
            reset_enemy_paddle();
        }

    // to create a new instance of a ball
        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.2f)
        {
            if (avatar.transform.childCount > 1 && ballbounce.state == ballbounce.gameState.playerStart)
            {
                left_hand = avatar.transform.GetChild(0).gameObject;
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                ball.transform.position = left_hand.transform.position;
            }
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.2f)
        {

            if (avatar.transform.childCount > 1)
            {

                right_hand = avatar.transform.GetChild(1).gameObject;

                left_hand = avatar.transform.GetChild(0).gameObject;

                //put paddle to hand
                paddle.transform.position = right_hand.transform.position + paddle.transform.forward * -0.1f;
                
                paddle.transform.rotation = right_hand.transform.rotation;
                paddle.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
                
                // this makes the paddle look like its actually behing held properly
                paddle.transform.Rotate(new Vector3(-45f, 0, 0));

                // Enemy following

                if (ballbounce.followPlayer == 1)
                //if (false)
                {
                    enemyPaddle.transform.position = paddle.transform.forward * -0.1f + new Vector3(right_hand.transform.position.x, right_hand.transform.position.y, -1.5f * right_hand.transform.position.z);

                    //enemyPaddle.transform.rotation = right_hand.transform.rotation;

                    enemyPaddle.transform.localRotation = new Quaternion(right_hand.transform.localRotation.x, right_hand.transform.localRotation.y * -1.0f, right_hand.transform.localRotation.z * -1.0f, right_hand.transform.localRotation.w);

                    enemyPaddle.GetComponent<Rigidbody>().velocity = (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));

                    
                }

                

                // this makes the paddle look like its actually behing held properly
                //enemyPaddle.transform.Rotate(new Vector3(-45f, 0, 0));



                paddleBox = paddle.GetComponent<BoxCollider>();

                Vector3 closestPoint = paddleBox.ClosestPointOnBounds(ball.transform.position);
                float distance = Vector3.Distance(closestPoint, ball.transform.position);

                if (distance < 0.05)
                {

                    
                    Vector3 fromPaddleToBall = ball.transform.position - paddle.transform.position;
                    if (Vector3.Dot(fromPaddleToBall, paddle.transform.up) > 0)
                    {
                        ball.GetComponent<Rigidbody>().velocity = paddle.transform.up * 3;
                    }
                    else
                    {
                        ball.GetComponent<Rigidbody>().velocity = -paddle.transform.up * 3;

                    }
                    


                    playerPaddleCollision = 1;
                }



            }
        }
        else if(OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) < 0.2f)
        {
            if (avatar.transform.childCount > 1)
            {
                //paddle.GetComponent<Rigidbody>().useGravity = true;

                paddle.transform.position = new Vector3(0.723f, 0.732f, -1.316f);

            }


        }

/*
        if(ballbounce.followPlayer == 0)
        {

            Debug.Log("AI in Action");
            speed = 5.0f;
            //track ball smoothly until it crosses the halfway line on the table (or another arbitrary point) and then instantaneously towards ball position 
            // dont follow ball position until a.p., follow ball in (x+a, y+b, z+c) translated to the end of the table where (x, y, z) are coordinates of current ball position.
            float step = speed * Time.deltaTime;
            enemyPaddle.transform.position = Vector3.MoveTowards(enemyPaddle.transform.localPosition, ball.transform.localPosition, step);
        }


    */

    }
    public void follow_ball()
    {
        if (ballbounce.followPlayer == 0)
        {

            Debug.Log("AI in Action");
            speed = 5.0f;
            //track ball smoothly until it crosses the halfway line on the table (or another arbitrary point) and then instantaneously towards ball position 
            // dont follow ball position until a.p., follow ball in (x+a, y+b, z+c) translated to the end of the table where (x, y, z) are coordinates of current ball position.
            float step = speed * Time.deltaTime;
            enemyPaddle.transform.position = Vector3.MoveTowards(enemyPaddle.transform.localPosition, ball.transform.localPosition, step);

            enemypaddleBox = enemyPaddle.GetComponent<BoxCollider>();

            Vector3 closestPointEnemy = enemypaddleBox.ClosestPointOnBounds(ball.transform.position);
            float enemydistance = Vector3.Distance(closestPointEnemy, ball.transform.position);

            if (enemydistance < 0.2)
            {


                Vector3 fromPaddleToBall = ball.transform.position - enemyPaddle.transform.position;
                //if (Vector3.Dot(fromPaddleToBall, enemyPaddle.transform.up) > 0)
                //{
                ball.GetComponent<Rigidbody>().velocity = enemyPaddle.transform.up * 3 + enemyPaddle.transform.forward * 3;
                //}
                /*else
                {
                    ball.GetComponent<Rigidbody>().velocity = -enemyPaddle.transform.up * 3 + enemyPaddle.transform.forward * 2;

                }
                */


                enemyPaddleCollision = 1;
            }
        }

    }

    public void reset_enemy_paddle()
    {
        enemyPaddle.transform.position = new Vector3(0.7f, 0.7f, 1.2f);
    }

}

