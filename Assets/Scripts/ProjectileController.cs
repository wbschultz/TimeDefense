using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;

    public float speed = 30f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        SeekTarget();
    }

    private void HitTarget()
    {
        Destroy(gameObject);
    }

    private void SeekTarget()
    {
        Vector3 targetDir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (targetDir.magnitude <= distanceThisFrame)
        {
            // About to hit target
            HitTarget();
            return;
        }

        transform.Translate(targetDir.normalized * distanceThisFrame, Space.World);
    }
}
