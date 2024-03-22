using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private float minDistance = 0.3f;
    private LineRenderer line;
    private Vector3 prevPos;
    public List<GameObject> circles = new List<GameObject>();
    public LayerMask circleLayer;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        prevPos = transform.position;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;

            if(Vector3.Distance(currentPos, prevPos) > minDistance)
            {
                //If it is the first point
                if(prevPos == transform.position)
                {
                    line.SetPosition(0, currentPos);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPos);
                }
                CheckCircles();
                prevPos = currentPos;
            }
        }
    }

    public void CheckCircles()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float number_of_rays = 20;
        float totalAngle = 360;

        float delta = totalAngle / number_of_rays;

        for (int i = 0; i < number_of_rays; i++)
        {
            var dir = Quaternion.Euler(0, 0, i * delta) * transform.right;
            Debug.DrawRay(mousePos, dir * minDistance, Color.green);

            //Cast Rays in all 2D directions
            RaycastHit2D hit = (Physics2D.Raycast(mousePos, dir, minDistance, circleLayer));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Circle"))
                {
                    //Replace circle in the list if already added
                    for (int j = 0; j < circles.Count; j++)
                    {
                        if (hit.collider.gameObject == circles[j])
                        {
                            circles.Remove(circles[j]);
                        }
                    }
                    circles.Add(hit.collider.gameObject);
                }
            }
        }
    }
}
