using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using gameplay;

public class ballbounce : MonoBehaviour {

    // Use this for initialization

    public int enemy_score;
    public int our_score;

    public GameObject player_text;

    public GameObject result_text;

    public static int followPlayer;
    private string currentCollision;

    public GameObject Ball;

    public static bool enemyFlag;
    public static bool resetFlag;
    public GameObject enemyPaddle;
    private float time;

    public GameObject cheer_furniture;
    public GameObject boo_furniture;

    public int serving; //0 playing, 1 player serving, 2 enemy serving

    public enum gameState
    {
   
        enemyStart,
        playerStart,

        enemyPaddle,
        playerPaddle,

        playerSide,
        enemySide,

        losePoint,
        winPoint


    }

    public static gameState state;

    private void Start()
    {
        state = gameState.playerStart;
        followPlayer =0;
        time = -1f;
        serving = 1;
        enemyFlag = false;
        resetFlag = false;
        enemy_score = 0;
        our_score = 0;
    }

    private void Update()
    {
        Debug.Log(state);
        Debug.Log(serving);

        if (enemy_score >= 12)
        {
            result_text.GetComponent<UnityEngine.UI.Text>().text = "YOU LOSE :(";
        }
        else if (our_score >= 12)
        {
            result_text.GetComponent<UnityEngine.UI.Text>().text = "YOU WIN! :)";
        }

        if (state == gameState.playerStart)
        {
            if (gameplay.playerPaddleCollision == 1)
            {
                state = gameState.playerPaddle;
                gameplay.playerPaddleCollision = 0;
            }
            enemyFlag = false;
            resetFlag = true;
        }

        else if (state == gameState.enemyStart)
        {
            Debug.Log("WAITING");
            if (time == -1f)
            {
                time = Time.time;
            }
            else if (Time.time - time > 2f)
            {
                //serve
                Debug.Log("SERVING");
                Ball.transform.position = enemyPaddle.transform.position + new Vector3(0, 0.3f, -0.15f);
                Ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);
                time = -1f;
                state = gameState.enemyPaddle;
            }


            if (gameplay.enemyPaddleCollision == 1)
            {
                state = gameState.enemyStart;
                gameplay.enemyPaddleCollision = 0;
            }
            enemyFlag = false;
            resetFlag = true;
        }

        else if (state == gameState.playerPaddle)
        {
            
            if (gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.playerPaddle;
                }
                else
                {
                    state = gameState.playerPaddle;
                }
                
                gameplay.playerPaddleCollision = 0;
            }
            else if (gameplay.enemyPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.losePoint;
                }
                else
                {
                    state = gameState.winPoint;
                }
                gameplay.enemyPaddleCollision = 0;
            }
            if (serving == 2)
            {
                serving = 0;
            }
            enemyFlag = false;
            resetFlag = false;
        }

        else if (state == gameState.enemyPaddle)
        {
            
            if (gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.winPoint;
                }
                else
                {
                    state = gameState.losePoint;
                }

                gameplay.playerPaddleCollision = 0;
            }
            else if (gameplay.enemyPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemyPaddle;
                }
                else
                {
                    state = gameState.enemyPaddle;
                }
                gameplay.enemyPaddleCollision = 0;
            }
            if (serving == 1)
            {
                serving = 0;
            }
            enemyFlag = false;
            resetFlag = false;
        }

        else if (state == gameState.playerSide)
        {

            if (gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.playerPaddle;
                }
                else
                {
                    state = gameState.playerPaddle;
                }

                gameplay.playerPaddleCollision = 0;
            }
            else if (gameplay.enemyPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.winPoint;
                }
                else
                {
                    state = gameState.enemyPaddle; //UNDEF
                }
                gameplay.enemyPaddleCollision = 0;
            }
            enemyFlag = false;
            resetFlag = false;
        }

        else if (state == gameState.enemySide)
        {

            if (gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.losePoint;
                }
                else
                {
                    state = gameState.playerPaddle; //UNDEF
                }

                gameplay.playerPaddleCollision = 0;
            }
            else if (gameplay.enemyPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemyPaddle;
                }
                else
                {
                    state = gameState.enemyPaddle;
                }
                gameplay.enemyPaddleCollision = 0;
            }
            if(serving == 0 || serving == 1)
            {
                enemyFlag = true;
                resetFlag = false;
            }
            else
            {
                enemyFlag = false;
                resetFlag = false;
            }
            
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        //Bounce(collision.contacts[0].normal);
        currentCollision = collision.gameObject.name;
        //Debug.Log("Collided object1234" + collision.collider.gameObject.name);
        //Debug.Log(currentCollisionCollision);
        //Debug.Log("");

        AudioSource audioData = GetComponent<AudioSource>();

        AudioSource audio_cheer = cheer_furniture.GetComponent<AudioSource>();

        if (state == gameState.playerStart)
        {
            followPlayer = 0;
            //Debug.Log("Player Paddle Variable" + gameplay.playerPaddleCollision);
            if (currentCollision == "playerside")
            {
                state = gameState.playerStart;
            }
            else if (currentCollision == "enemyside")
            {
                state = gameState.playerStart;
            }
            else if (currentCollision == "floor")
            {
                state = gameState.playerStart;
                //Debug.Log("Not Playing the game");
            }
            else if (currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {
                state = gameState.playerPaddle;
                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {
                state = gameState.playerStart;
            }
        }
/*
        else if (state == gameState.enemyStart)
        {

            //Debug.Log("Player Paddle Variable" + gameplay.playerPaddleCollision);
            if (currentCollision == "playerside")
            {
                state = gameState.enemyPaddle;
            }
            else if (currentCollision == "enemyside")
            {
                state = gameState.enemyPaddle;
            }
            else if (currentCollision == "floor")
            {
                state = gameState.enemyPaddle;
                //Debug.Log("Not Playing the game");
            }
            else if (currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {
                state = gameState.enemyPaddle;
                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {
                state = gameState.enemyPaddle;

            }

        }
*/
        else if (state == gameState.playerPaddle)
        {
            followPlayer = 0;
            audioData.Play(0);
            Debug.Log("Hit Player Paddle");
            
            if (currentCollision == "playerside")
            {
                if(serving == 1 || serving == 2)
                {
                    state = gameState.playerSide;
                }
                else
                {
                    state = gameState.losePoint;
                }
            }
            else if (currentCollision == "enemyside")
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.losePoint;
                }
                else
                {
                    state = gameState.enemySide;
                }
            }
            else if(currentCollision == "floor")
            {
                state = gameState.losePoint;
                //Debug.Log("Not Playing the game");
            }
            else if(currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.playerPaddle;
                }
                else
                {
                    state = gameState.playerPaddle;
                }
                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.losePoint;
                }
                else
                {
                    state = gameState.winPoint;
                }
            }
            if (serving == 2)
            {
                serving = 0; //playing
            }
        }

        else if (state == gameState.enemyPaddle)
        {
            followPlayer = 0;
            Debug.Log("Hit something after hitting Enemy Paddle");
            audioData.Play(0);
            

            if (currentCollision == "playerside")
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.winPoint;
                }
                else
                {
                    state = gameState.playerSide;
                }

                
            }
            else if (currentCollision == "enemyside")
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemySide;
                }
                else
                {
                    state = gameState.winPoint;
                }
            }
            else if (currentCollision == "floor")
            {
                state = gameState.winPoint;
                //Debug.Log("Not Playing the game");
            }
            else if (currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {

                if (serving == 1 || serving == 2)
                {
                    state = gameState.winPoint;
                }
                else
                {
                    state = gameState.losePoint;
                }
                
                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {

                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemyPaddle;
                }
                else
                {
                    state = gameState.enemyPaddle;
                }

                //state = gameState.winPoint;
            }
            if (serving == 1)
            {
                serving = 0;
            }
        }

        else if (state == gameState.playerSide)
        {

            Debug.Log("Detected bounce on player side");

            followPlayer =0;
            audioData.Play(0);



            if (currentCollision == "playerside")
            {

                state = gameState.losePoint;
            }
            else if (currentCollision == "enemyside")
            {
                state = gameState.enemySide;

                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemySide;
                }
                else
                {
                    state = gameState.enemySide;
                }

            }
            else if (currentCollision == "floor")
            {
                state = gameState.losePoint;
                //Debug.Log("Not Playing the game");
            }
            else if (currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {
                
                if (serving == 1 || serving == 2)
                {
                    state = gameState.playerPaddle;
                }
                else
                {
                    state = gameState.playerPaddle;
                }

                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {
               

                if (serving == 1 || serving == 2)
                {
                    state = gameState.winPoint;
                }
                else
                {
                    state = gameState.enemyPaddle;
                }

            }
        }

        else if (state == gameState.enemySide)
        {
            followPlayer = 0;
            audioData.Play(0);

            Debug.Log("Detected bounce on enemy");
            if (currentCollision == "playerside")
            {
                // TODO this is undefined
               

                if (serving == 1 || serving == 2)
                {
                    state = gameState.playerSide;
                }
                else
                {
                    // UNDEFINED
                    state = gameState.playerSide;
                }

            }
            else if (currentCollision == "enemyside")
            {
                state = gameState.winPoint;
            }
            else if (currentCollision == "floor")
            {
                state = gameState.winPoint;
                //Debug.Log("Not Playing the game");
            }
            else if (currentCollision == "Paddle"  || gameplay.playerPaddleCollision == 1)
            {
                if (serving == 1 || serving == 2)
                {
                    state = gameState.losePoint;
                }
                else
                {
                    // UNDEFINED
                    state = gameState.playerPaddle;
                }
                gameplay.playerPaddleCollision = 0;
            }
            else if (currentCollision == "EnemyPaddle")
            {
               
                if (serving == 1 || serving == 2)
                {
                    state = gameState.enemyPaddle;
                }
                else
                {
                    state = gameState.enemyPaddle;
                }

            }
            enemyFlag = true;
        }

        else if (state == gameState.losePoint)
        {
            serving = 2;

            

            Debug.Log("Lose point");
            enemy_score += 1;
            player_text.GetComponent<UnityEngine.UI.Text>().text = "* Enemy Score: " + enemy_score.ToString() + "\r\n Our Score: "+ our_score.ToString();
            state = gameState.enemyStart;
        }

        else if (state == gameState.winPoint)
        {

            audio_cheer.Play(0);
            serving = 1;
            our_score += 1;
            player_text.GetComponent<UnityEngine.UI.Text>().text = "Enemy Score: " + enemy_score.ToString() + "\r\n * Our Score: " + our_score.ToString();

            Debug.Log("gain point");
            state = gameState.playerStart;
        }



    }



}
