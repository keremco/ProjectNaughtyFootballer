using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMovementChecker : MonoBehaviour
{

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    private bool touched = false;
    private int touchCount = 3;
    private int rollCount = 2;

    private int score = 0;

    private float Cooldown = 1f;
    private float coolDownTimer;

    AudioManager audioManager;
    Outline outline;

    bool takePointDone = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 3f;
    }
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        GameManager.instance.TotalObjectLife(touchCount);
    }

    void FixedUpdate()
    {

        if (!touched)
        {
            if (coolDownTimer > 0)
            {
                coolDownTimer -= Time.deltaTime;
            }

            if (coolDownTimer < 0)
            {
                coolDownTimer = 0;
            }

            if (IsTouchingBallObject() && touchCount > 0 && coolDownTimer == 0)
            {

                score += 1;

                if (rollCount > 0)
                {
                    Vector3 positionChange = transform.position - initialPosition;
                    float positionChangeScore = positionChange.magnitude * 1000f;
                    int positionScore = Mathf.RoundToInt(positionChangeScore);

                    Quaternion rotationChange = Quaternion.Euler(transform.rotation.eulerAngles - initialRotation.eulerAngles);
                    float rotationChangeScore = rotationChange.eulerAngles.magnitude * 2f;
                    int rotationScore = Mathf.RoundToInt(rotationChangeScore);

                    score += rotationScore + positionScore;
                    rollCount--;
                }
                touchCount--;
                coolDownTimer = Cooldown;
                touched = false;
                audioManager.PlaySFX(audioManager.collectPoints);
                GameManager.instance.UpdateScore(score);
                GameManager.instance.TotalObjectLife(touchCount);
                if (touchCount == 0) takePointDone = true;
            }

        }

        if (takePointDone)
        {
            outline.OutlineWidth = 0f;
            takePointDone = false;
            audioManager.PlaySFX(audioManager.takePointDone);

        }
        
    }

    bool IsTouchingBallObject()
    {

        GameObject[] topObjects = GameObject.FindGameObjectsWithTag("top");

        foreach (GameObject topObject in topObjects)
        {
            if (topObject.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                return true;
                
            }
        }
        return false;
    }
}