using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : Projectile
{
    public Transform firePoint;

    public Transform startPoint;
    public Transform endPoint;

    public Transform aimPoint;

    public LineRenderer line;

    public float speed;
    public float maxLength;

    RaycastHit hit;

    private void Start()
    {
        aimPoint.position = startPoint.position;
        line.SetPosition(0, firePoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        aimPoint.Translate((endPoint.position - startPoint.position) * speed * Time.deltaTime);
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, aimPoint.position);


        Ray ray = new Ray(firePoint.position, aimPoint.position - firePoint.position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Physics.SphereCast(ray.origin, line.endWidth/2, ray.direction, out (hit), maxLength))
        {

            if (hit.collider && hit.collider.gameObject.tag != "Boss")
            {
                line.SetPosition(1, hit.point);
            }
            if(hit.collider.gameObject.tag == "Player")
            {
                Collided(hit.collider);
            }
            if(hit.collider.gameObject.tag == "Finish")
            {
                Destroy(gameObject);
            }
        }
    }
    public void BeamTarget(Transform fire, Transform start, Transform end)
    {
        firePoint = fire;
        startPoint = start;
        endPoint = end;
    }


}
