using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : Projectile
{
    public Transform firepoint;

    public Transform start, end;

    public Transform aimPoint;

    public LineRenderer line;

    public float speed;
    public float maxLength;

    RaycastHit hit;

    public LayerMask beamMask;

    private void Start()
    {
        aimPoint.position = start.position;
        line.SetPosition(0, firepoint.position);

    }

    // Update is called once per frame
    void Update()
    {
        aimPoint.Translate((end.position-start.position) * speed * Time.deltaTime);
        line.SetPosition(0, firepoint.position);
        line.SetPosition(1, aimPoint.position);


        Ray ray = new Ray(firepoint.position, aimPoint.position);

        if (Physics.Raycast(ray.origin, ray.direction, out (hit), maxLength))
        {
            if (hit.collider)
            {
                line.SetPosition(1, hit.point);
            }
        }

        if (Physics.Raycast(ray.origin, ray.direction, out (hit), maxLength, beamMask))
        {
            if (hit.collider)
            {
                Debug.Log("pew");
            }
        }
    }

}
