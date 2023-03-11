using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ondes : MonoBehaviour
{
    public static int resolution = 200;
    public float flightDuration = 0.8f;
    public float deadTime = 10;
    private Vector3[] linePoints = new Vector3[resolution];
    private Vector2[] linePoints2 = new Vector2[resolution];
    private Vector2[] linePointTotal = new Vector2[resolution];
    private float[] deltafloat = new float[resolution];
    private float xStartPosition,yStartPosition;
    public Vector2 direction;
    public Vector2 directionCur;
    public float horizontalVelocity = 1;
    private LineRenderer lineRenderer;
    private EdgeCollider2D collider2D;
    private float currentTime = 0;
    private Collider2D otherObject;
    public GameObject shooter;
    public int damage = 50;
    public int mutationValue = 50;

    private bool first = true;
    private float distance;

    private bool interrupted = false;
    // Start is called before the first frame update
    void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;
        if (direction == Vector2.zero) direction = new Vector2(1f, 0f);
        lineRenderer = GetComponent<LineRenderer>();
        collider2D = GetComponent<EdgeCollider2D>();
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
        currentTime += Time.deltaTime;
        int index = Mathf.FloorToInt(currentTime / flightDuration * resolution);
        if (index >= resolution)
            GameObject.Destroy(this.gameObject);
        if (interrupted)
        {
            
            RenderLineCut();

        } else
        {
            RenderLine(resolution, true);
        }
    }

    private void RenderLine(int position, bool hitbox)
    {
        linePoints = new Vector3[position];
        if(hitbox)linePoints2 = new Vector2[position];

        for (int i = 0; i < position; i++)
        {
            float height = GetProjectileHeight(i);
            if (direction.y >= 0.5)
            {
                linePoints[i] = new Vector3(transform.position.x + (direction * height).x + deltafloat[i], transform.position.y + (direction * height).y, 0);
                if (hitbox) linePoints2[i] = new Vector2((direction * height).x + deltafloat[i], (direction * height).y);
                if (first) linePointTotal[i] = new Vector2(transform.position.x + (direction * height).x + deltafloat[i], transform.position.y + (direction * height).y);
            }
            else
            {
                linePoints[i] = new Vector3(transform.position.x + (direction * height).x, transform.position.y + (direction * height).y + deltafloat[i], 0);
                if (hitbox) linePoints2[i] = new Vector2((direction * height).x, (direction * height).y + deltafloat[i]);
                if (first) linePointTotal[i] = new Vector2(transform.position.x + (direction * height).x, transform.position.y + (direction * height).y + deltafloat[i]);
            }
        }
        first = false;
        lineRenderer.positionCount = position;
        lineRenderer.SetPositions(linePoints);
        if (hitbox)collider2D.SetPoints(new List<Vector2>(linePoints2));
    }

    private void RenderLineCut()
    {
        int i = 0;
        for (i = 0; i < resolution; i++)
        {
            if (Vector2.Distance(new Vector2(linePointTotal[0].x, linePointTotal[0].y), linePointTotal[i]) > distance)
            {
                break;
            }
        }
        RenderLine(i, false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.gameObject != shooter)
        {
            otherObject = other;
            distance = Vector2.Distance(new Vector2(linePointTotal[0].x, linePointTotal[0].y), other.gameObject.transform.position);
            if (other.tag == "Player" && !interrupted)
            {
                other.GetComponent<Player>().TakeDamage(damage);
                other.GetComponent<Player>().TakeMutation(mutationValue);
            }
            interrupted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != shooter)
        {
            interrupted = false;
        }
    }


    float GetProjectileHeight(float t)
    {
            return t * 0.1f;
    }

}

