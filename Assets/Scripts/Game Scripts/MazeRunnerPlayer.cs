using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerPlayer : MonoBehaviour
{
    public MazeRunnerManager managerCall;
    public Rigidbody player;

    enum direction{up,down,left,right};
    direction playerDirection = direction.up;

    private float force = 15;

    public bool isPlaying = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPlaying)
        {
            switch (playerDirection)
            {
                case direction.up:
                    movePlayer(0, force);
                    break;
                case direction.down:
                    movePlayer(0, -force);
                    break;
                case direction.left:
                    movePlayer(-force, 0);
                    break;
                case direction.right:
                    movePlayer(force, 0);
                    break;
            }
        }
        else
        {
            stopPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            if(isPlaying)
            {
                isPlaying = !isPlaying;
                force = 15f;
                managerCall.gameEnd();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {

            force += 1;
            managerCall.addPoint();
        }
    }

    public void movePlayer(float xForce, float zForce)
    {
        player.AddForce(xForce * Time.deltaTime, 0, zForce * Time.deltaTime,ForceMode.VelocityChange);
    }

    private void stopPlayer()
    {
        player.velocity = Vector3.zero;
    }

    public void moveUp()
    {
        stopPlayer();
        playerDirection = direction.up;
    }

    public void moveRight()
    {
        stopPlayer();
        playerDirection = direction.right;
    }

    public void moveLeft()
    {
        stopPlayer();
        playerDirection = direction.left;
    }

    public void moveDown()
    {
        stopPlayer();
        playerDirection = direction.down;
    }
}
