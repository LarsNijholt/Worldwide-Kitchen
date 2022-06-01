using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointSystemObjectController : MonoBehaviour
{
    public Transform myAreaRoot;
    private RootController _myRootController;
    private PointEventSystem _myPointEvents;
    [SerializeField] private float speed;

    private List<Point> _areaPoints = new List<Point>();

    private bool _canMove;
    private bool _goBack;
    private Vector2 _destination;

    private int _iteration = 0;
    
    private void Awake()
    {
        for (int i = 0; i < myAreaRoot.childCount; i++)
        {
            _areaPoints.Add(myAreaRoot.GetChild(i).GetComponent<Point>());
        }
        transform.position = _areaPoints[0].transform.position;

        _myRootController = myAreaRoot.GetComponent<RootController>();
        _myPointEvents = myAreaRoot.GetComponent<PointEventSystem>();

        if (_myPointEvents.goOnAwake)
        {
            SetDestination(_areaPoints[_iteration]);
            StartMoving();
        }
    }
    private Vector2 _pos;
    
    private void SetDestination(Point point)
    {
        if (_goBack == true)
        {
            _pos = point.previousPoint.transform.localPosition;
        }
        else
        {
            _pos = point.nextPoint.transform.localPosition;
        }
        _destination = new Vector2(_pos.x, _pos.y);
    }
    private void Update()
    {
        if (_canMove) MoveTo();
    }
    private void MoveTo()
    {
        float dist = Vector2.Distance(_destination, transform.localPosition);
        if (dist < 0.005f)
        {
            try
            {
                if (_goBack)
                {
                    _iteration--;
                    SetDestination(_areaPoints[_iteration]);
                }
                else
                {
                    _iteration++;
                    SetDestination(_areaPoints[_iteration]);
                }
            }
            catch
            {
                if (_myRootController.loop)
                {
                    _myPointEvents.Loop();
                    _iteration = 0;
                    SetDestination(_areaPoints[_iteration]);
                }
                else if (_myRootController.pingpong)
                {
                    switch (_goBack)
                    {
                        case true:
                            _myPointEvents.PingPong();
                            _goBack = false;
                            SetDestination(_areaPoints[_iteration]);
                            break;
                        case false:
                            _myPointEvents.PingPong();
                            _goBack = true;
                            SetDestination(_areaPoints[_iteration]);
                            break;
                    }
                }
                else
                {
                    _canMove = false;
                    _myPointEvents.Oneway();
                    return;
                }
            }
        }
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, _destination, speed * Time.deltaTime);
    }

    #region Events
    public void StartMoving()
    {
        _myPointEvents.StartMove();
        _canMove = true;
    }
    public void StopMoving()
    {
        _myPointEvents.StopMove();
        _canMove = false;
    }

    public void ResetPath()
    {
        _myPointEvents.ResetPath();
        _goBack = false;
        _iteration = 0;
        SetDestination(_areaPoints[_iteration]);

        transform.position = _areaPoints[0].transform.position;
    }
    public void SetCanMove(bool input)
    {
        _canMove = input;
    }
    #endregion
}
