using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ondes : MonoBehaviour
{
    public static int resolution = 1000;
    public float flightDuration = 300;
    public float deadTime = 10;
    private Vector3[] linePoints = new Vector3[resolution];
    private Vector2[] linePoints2 = new Vector2[resolution];
    private float xStartPosition,yStartPosition;
    public Vector2 direction;
    public Vector2 directionCur;
    public float horizontalVelocity = 1;
    private LineRenderer lineRenderer;
    private EdgeCollider2D collider2D;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;
        if (direction == Vector2.zero) direction = new Vector2(1f, 0f);
        directionCur = new Vector2(transform.eulerAngles.x, transform.eulerAngles.y).normalized;
        if (directionCur == Vector2.zero) directionCur = new Vector2(0f, 1f);
        lineRenderer = GetComponent<LineRenderer>();
        collider2D = GetComponent<EdgeCollider2D>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("dir : " + directionCur);
        if (Time.time - time > deadTime)
            GameObject.Destroy(this.gameObject);
        float timeStep = flightDuration / resolution;
        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            float height = GetProjectileHeight(i);
            linePoints[i] = new Vector3(transform.position.x + (direction * height).x, transform.position.y + (direction * height).y,0); 
            linePoints2[i] = new Vector2((direction * height).x, (direction * height).y);
        }
        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(linePoints);
        collider2D.SetPoints(new List<Vector2>(linePoints2));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hey !");
    }

    float GetProjectileHeight(float t)
    {
        return t * 0.5f;
    }
}

