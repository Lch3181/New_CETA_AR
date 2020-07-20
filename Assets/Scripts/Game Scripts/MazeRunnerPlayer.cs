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

    /// <summary>
    /// FixedUpdate is called once per CPU cycle
    /// 
    /// Keyboard arrow keys checking to controll player movement
    /// </summary>
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

    /// <summary>
    /// On Player hits the wall, ends the game and bounce back
    /// </summary>
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

    /// <summary>
    /// On Player hits the points, speed++, update score
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {

            force += 1;
            managerCall.addPoint();
        }
    }

    /// <summary>
    /// Add Force to player
    /// </summary>
    public void movePlayer(float xForce, float zForce)
    {
        player.AddForce(xForce * Time.deltaTime, 0, zForce * Time.deltaTime,ForceMode.VelocityChange);
    }

    /// <summary>
    /// Set Player velocity to 0
    /// </summary>
    private void stopPlayer()
    {
        player.velocity = Vector3.zero;
    }

    /// <summary>
    /// Set Player direction to Up
    /// </summary>
    public void moveUp()
    {
        stopPlayer();
        playerDirection = direction.up;
    }

    /// <summary>
    /// Set Player Direction to Right
    /// </summary>
    public void moveRight()
    {
        stopPlayer();
        playerDirection = direction.right;
    }

    /// <summary>
    /// Set Player Direction to Left 
    /// </summary>
    public void moveLeft()
    {
        stopPlayer();
        playerDirection = direction.left;
    }

    /// <summary>
    /// Set Player Direction to Down
    /// </summary>
    public void moveDown()
    {
        stopPlayer();
        playerDirection = direction.down;
    }
}
