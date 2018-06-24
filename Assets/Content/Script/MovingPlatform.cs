using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    private bool _goingToA;
    private bool _wait;
    private Rigidbody2D _myBody;
    private float _currentTimeToWait;
    public float TimeToReach;
    public float TimeToWait;
    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        _goingToA = false;
        _wait = true;
        _currentTimeToWait = TimeToWait;
        _myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 myPos = transform.position;
        Vector3 target = _goingToA ? pointA : pointB;
        Vector3 destination = target - myPos;
        destination.z = 0;

        if (_wait)
        {
            _currentTimeToWait -= Time.deltaTime;
            if (!(_currentTimeToWait <= 0)) return;
            _wait = false;
            _currentTimeToWait = TimeToWait;
            _myBody.velocity = new Vector2(destination.x / TimeToReach, destination.y / TimeToReach);
            return;
        }

        if (!isArrived(myPos, target)) return;
        _goingToA = !_goingToA;
        _wait = true;
        _myBody.velocity = new Vector2(0, 0);
    }

    private static bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }


}
