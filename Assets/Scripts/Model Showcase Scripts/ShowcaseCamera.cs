using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseCamera : MonoBehaviour
{
    public Camera cam;
    public GameObject[] Floors;
    public float stageTimer;
    public float speed;

    private int currentStage;
    private float NextStageTime;

    private void Start()
    {
        currentStage = Floors.Length - 1;
        NextStageTime += stageTimer;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);

        //swap stage
        if(Time.time > NextStageTime && currentStage > 0)
        {
            // increase cooldown
            NextStageTime += stageTimer;

            // set camera focus to new floor
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 9f, transform.localPosition.z);
            if (currentStage == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 25f, transform.localPosition.y, transform.localPosition.z);
            }

            // disable floor
            Floors[currentStage].gameObject.SetActive(false);
            currentStage -= 1;
        }
    }
}
