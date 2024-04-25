using System.Collections;
using UnityEngine;
using FMOD.Studio;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    private Rigidbody rb;

    private bool isGrounded = false;

    private bool disableMovement = false;

    //audio
    private EventInstance playerFootsteps;

    // Update is called once per frame

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        if (disableMovement)
        {
            rb.velocity = Vector3.zero;
            UpdateSound();
            return;
        }

        UpdateIsGrounded();

        UpdateSound();
    }

    Vector3 velocity;

    private void UpdateIsGrounded()
    {
        
    }

    private void UpdateSound()
    {
        if (rb.velocity.x != 0 && isGrounded)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

}
