using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping = false;

    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();

        trail.enabled = false;
        col.enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        HandleMouseBehaviour();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Target collisionTarget = collision.gameObject.GetComponent<Target>();
        if (collisionTarget) collisionTarget.KillTarget();
    }


    void HandleMouseBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateSwipeState(true);
        } else if (Input.GetMouseButtonUp(0))
        {
            UpdateSwipeState(false);
        }

        if (swiping) 
        {
            UpdateMousePosition();
        }
    }

    void UpdateSwipeState(bool state)
    {
        swiping = state;
        trail.enabled = state;
        col.enabled = state;
    }

    void UpdateMousePosition()
    {
        Vector3 actualMousePosition = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            10.0f
        );
        mousePos = cam.ScreenToWorldPoint(actualMousePosition);
        transform.position = mousePos;
    }
}
