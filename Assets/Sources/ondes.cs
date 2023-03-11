using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ondes : MonoBehaviour
{
    public static int resolution = 10000;
    public float flightDuration = 300;
    public float deadTime = 10;
    private Vector3[] linePoints = new Vector3[resolution];
    private Vector2[] linePoints2 = new Vector2[resolution];
    private float[] deltafloat = new float[resolution];
    private float xStartPosition,yStartPosition;
    public Vector2 direction;
    public Vector2 directionCur;
    public float horizontalVelocity = 1;
    private LineRenderer lineRenderer;
    private EdgeCollider2D collider2D;
    private float previoustime,startTime;
    public GameObject shooter;
    // Start is called before the first frame update
    void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;
        if (direction == Vector2.zero) direction = new Vector2(1f, 0f);
        lineRenderer = GetComponent<LineRenderer>();
        collider2D = GetComponent<EdgeCollider2D>();
        startTime = Time.time;
        for (int i = 0;i < resolution; i++)
        {
            if( i== 0)
            {
                deltafloat[i] += Random.Range(-0.0001f, 0.0001f);
            }else if ( i < 17)
                deltafloat[i] += deltafloat[i-1] + Random.Range(-0.0001f*i, 0.0001f*i);
            else
                deltafloat[i] += deltafloat[i - 1] + Random.Range(-0.0001f * i * 5, 0.0001f * i * 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 0.8f)
            GameObject.Destroy(this.gameObject);
        int i = 0;
        previoustime = Time.time;
        float timeStep = flightDuration / resolution;
        for (i = 0; i/1000f < previoustime - startTime; i++)
        {
            float time = i * timeStep;
            float height = GetProjectileHeight(i);
            float tx = (direction * height).x;
            float ty = (direction * height).y;
            if (direction.y >= 0.5) {
                linePoints[i] = new Vector3(transform.position.x + (direction * height).x + deltafloat[i], transform.position.y + (direction * height).y  , 0);
                linePoints2[i] = new Vector2((direction * height).x + deltafloat[i], (direction * height).y);
            }
            else
            {
                linePoints[i] = new Vector3(transform.position.x + (direction * height).x, transform.position.y + (direction * height).y + deltafloat[i], 0);
                linePoints2[i] = new Vector2((direction * height).x, (direction * height).y + deltafloat[i]);
            }
        }
        for (int j = i; j < resolution ; j++)
        {
            float height = GetProjectileHeight(i);
            linePoints[j] = new Vector3(transform.position.x + (direction * height).x , transform.position.y + (direction * height).y +constY, 0);
            linePoints2[j] = new Vector2((direction * height).x, (direction * height).y);
        }
        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(linePoints);
        collider2D.SetPoints(new List<Vector2>(linePoints2));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject != shooter)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Player>();
            }

        }
    }

    float GetProjectileHeight(float t)
    {
            return t * 0.1f;
    }
    float constX=0, constY=0;
    float deltax()
    {
        constX += Random.Range(-10f, 10f);
        return constX;
    }

    float deltay()
    {
        constY += Random.Range(-10f, 10f);
        return constY;
    }
}

