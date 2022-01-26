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
    Ray ray;

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


        ray = new Ray(firePoint.position, aimPoint.position - firePoint.position);

        if (Physics.SphereCast(ray.origin, line.endWidth/2, ray.direction, out (hit), maxLength))
        {
            //wont collide with boss
            if (hit.collider && hit.collider.gameObject.tag != "Boss")
            {
                line.SetPosition(1, hit.point);
            }
            // damamge to player
            if(hit.collider.gameObject.tag == "Player")
            {
                DamageHandler(hit.collider.gameObject.GetComponentInParent<Health>(), hit.collider.gameObject.GetComponentInParent<ElementMain>());
                Collided(hit.collider);
            }
            //destroy if it reaches the end object
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
