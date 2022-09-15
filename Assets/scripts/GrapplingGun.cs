using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer _line;
    private Vector3 grapplingPoint;
    [SerializeField] private LayerMask whatisGrappable;
    [SerializeField] private Transform gunTip, _camera, player;
    [SerializeField] private float maxDistance = 1000;
    private SpringJoint springJoint;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }


    private void LateUpdate()
    {
        DrawRope();
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            if(!PauseController.isPaused)
                GetComponent<AudioSource>().Play();
        }
        else if (Input.GetMouseButtonUp(0))
            StopGrapple();

    }

    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance, whatisGrappable))
        {
            grapplingPoint = hit.point;
            springJoint = player.gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = grapplingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplingPoint);

            springJoint.maxDistance = distanceFromPoint * 0.8f;
            springJoint.minDistance = distanceFromPoint * 0.25f;

            springJoint.spring = 4.5f;
            springJoint.damper = 7f;
            springJoint.massScale = 4.5f;

            _line.positionCount = 2;

        }
    }

    private void DrawRope()
    {
        if (!springJoint) return;

        _line.SetPosition(0, gunTip.position);
        _line.SetPosition(1, grapplingPoint);
    }

    private void StopGrapple()
    {
        _line.positionCount = 0;
        Destroy(springJoint);
    }
}
