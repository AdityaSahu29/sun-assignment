using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    public Camera mainCamera;
    private Vector2 StartMousePosition;
    private Vector2 mousePosition;
    public GameObject restartScreen;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, new Vector3(StartMousePosition.x, StartMousePosition.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 0f));

            Vector2[] linePositions = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                linePositions[i] = lineRenderer.GetPosition(i);
            }

            edgeCollider.points = linePositions;
        }
        if(Input.GetMouseButtonUp(0))
        {
            DisplayRestartScreen();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            Destroy(collision.gameObject);
        }
    }

    void DisplayRestartScreen()
    {
        restartScreen.SetActive(true);
    }

}
