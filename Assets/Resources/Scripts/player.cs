using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Vector3 moveDirection;
    private Vector3 lastMousePosition;
    private bool isColidingL = false;
    private bool isColidingR = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPosition = transform.position;

        if (Input.anyKey)
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !isColidingL)
            {
                currentPosition.x -= 0.2F;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !isColidingR)
            {
                currentPosition.x += 0.2F;
            }
        }
        else
        {
            if (Input.mousePosition != lastMousePosition)
            {
                currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPosition.y = -4.725F;
                currentPosition.z = 0;
            }
        }

        if (currentPosition.x > 7.65F)
        {
            moveDirection.x = 0;
            currentPosition.x = 7.65F;
        }
        if (currentPosition.x < -7.65F)
        {
            moveDirection.x = 0;
            currentPosition.x = -7.65F;
        }

        this.transform.position = currentPosition;
        lastMousePosition = Input.mousePosition;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector3 contactPoint = coll.contacts[0].point;

        if (coll.gameObject.CompareTag("Background"))
        {

            bool right = contactPoint.x > 0;
            bool left = contactPoint.x < 0;

            if (right)
            {
                isColidingR = true;
            }
            if (left)
            {
                isColidingL = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        isColidingR = false;
        isColidingL = false;
    }

}