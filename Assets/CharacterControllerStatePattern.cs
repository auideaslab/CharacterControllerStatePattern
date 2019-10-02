using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStatePattern : MonoBehaviour
{

    PLAYER_STATE state;
    Animator anim;
    Rigidbody rigid;

    // implementing states as enumeration
    enum PLAYER_STATE
    {
        S_JUMP,
        S_WALK,
        S_IDLE,
        S_RUN
    };

    void Start()
    {
        state = PLAYER_STATE.S_IDLE;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Walk()
    {
        state = PLAYER_STATE.S_WALK;
        anim.SetTrigger("walk");
    }

    void Stop()
    {
        state = PLAYER_STATE.S_IDLE;
        anim.SetTrigger("stop");
    }


    void TurnLeft()
    {
        transform.Rotate(new Vector3(0, -1f, 0));
    }

    void TurnRight()
    {
        transform.Rotate(new Vector3(0, 1f, 0));
    }

    void Run()
    {
        state = PLAYER_STATE.S_RUN;
        anim.SetTrigger("run");
    }

    void Jump()
    {
        state = PLAYER_STATE.S_JUMP;
        rigid.AddForce((Vector3.up) * 5, ForceMode.Impulse);
        anim.SetTrigger("jump");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (state == PLAYER_STATE.S_JUMP)
        {
            Debug.Log("Landed!");
            Walk();
        }
    }

    void Update()
    {

        /* Finite State Machine:

         */
        //Debug.Log (state);

        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }

        switch (state)
        {

            case PLAYER_STATE.S_IDLE:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("Walk!");
                    Walk();
                }
                break;

            case PLAYER_STATE.S_WALK:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Run();
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    Stop();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    Debug.Log("Jumping!");
                }

                break;

            case PLAYER_STATE.S_RUN:
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Walk();
                    Debug.Log("From Run To Walk");
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    Stop();
                    Debug.Log("From Run To Stop");
                }
                break;

            case PLAYER_STATE.S_JUMP:
                if (Input.GetKeyUp(KeyCode.W))
                {
                    Stop();
                }

                break;
        }

    }
}
