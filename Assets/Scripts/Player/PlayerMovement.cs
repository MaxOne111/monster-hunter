using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float _Speed;
    [SerializeField] private Transform _Start_Position;
    private AmmoOnScene _Ammo_On_Scene;
    private PlayerAnimation _Animation;

    [SerializeField] private List<Transform> _Around;
    [SerializeField] private Transform _Next;
    [field: SerializeField] public List<Transform> Tiles { get; set; }
    // public List<Vector2> PathToTarget;
    // private List<Node> _Checked_Nodes = new List<Node>();
    // private List<Node> _Waiting_Nodes = new List<Node>();
    // public GameObject Target;
    // public LayerMask _Wall;
    public bool IsMove { get; private set; }
    private bool _move_Back;
    private bool _move_Up;

    [Inject]
    private void Construct(AmmoOnScene _ammo)
    {
        _Ammo_On_Scene = _ammo;
    }
    
    private void Awake()
    {
        _Animation = GetComponent<PlayerAnimation>();
        GameEvents._Put_Ammo += Move;
        GameEvents._Start_Level += ResetPosition;

        
    }

    private void Start()
    {
        Debug.Log(Tiles.Count);
    }

    // private void Update()
    // {
    //     PathToTarget = GetPath(Target.transform.position);
    // }

    // public List<Vector2> GetPath(Vector2 _target)
    // {
    //     PathToTarget = new List<Vector2>();
    //     _Checked_Nodes = new List<Node>();
    //     _Waiting_Nodes = new List<Node>();
    //
    //     Vector2 StartPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    //     Vector2 TargetPos = new Vector2(Mathf.Round(Target.transform.position.x), Mathf.Round(Target.transform.position.y));
    //
    //     if (StartPos == TargetPos) return PathToTarget;
    //
    //     Node startNode = new Node(0, StartPos, TargetPos, null);
    //     _Checked_Nodes.Add(startNode);
    //     _Waiting_Nodes.AddRange(GetNeighbours(startNode));
    //
    //     while (_Waiting_Nodes.Count > 0)
    //     {
    //         Node _nodeToCheck = _Waiting_Nodes.Where(x => x.F == _Waiting_Nodes.Min(y => y.F)).FirstOrDefault();
    //
    //         if (_nodeToCheck.Position == TargetPos)
    //         {
    //             return CalculatePathFromNode(_nodeToCheck);
    //         }
    //
    //         var walkable = !Physics2D.OverlapCircle(_nodeToCheck.Position, 0.1f, _Wall);
    //         if (!walkable)
    //         {
    //             _Waiting_Nodes.Remove(_nodeToCheck);
    //             _Checked_Nodes.Add(_nodeToCheck);
    //         }
    //         else
    //         {
    //             _Waiting_Nodes.Remove(_nodeToCheck);
    //             if (_Checked_Nodes.Where(x => x.Position == _nodeToCheck.Position).Any())
    //             {
    //                 _Checked_Nodes.Add(_nodeToCheck);
    //                 _Waiting_Nodes.AddRange(GetNeighbours(_nodeToCheck));
    //             }
    //            
    //         }
    //     }
    //     
    //     
    //     return PathToTarget;
    // }
    //
    // public List<Vector2> CalculatePathFromNode(Node node)
    // {
    //     var path = new List<Vector2>();
    //     Node current = node;
    //     while (current.PrevNode != null)
    //     {
    //         path.Add(new Vector2(current.Position.x, current.Position.y));
    //         current = current.PrevNode;
    //     }
    //
    //     return path;
    // }
    //
    // private List<Node> GetNeighbours(Node node)
    // {
    //     var Neighbours = new List<Node>();
    //     
    //     Neighbours.Add(new Node(node.G+1,
    //         new Vector2(node.Position.x+1, node.Position.y),
    //         node.TargetPosition, node));
    //     Neighbours.Add(new Node(node.G+1,
    //         new Vector2(node.Position.x-1, node.Position.y),
    //         node.TargetPosition, node));
    //     Neighbours.Add(new Node(node.G+1,
    //         new Vector2(node.Position.x, node.Position.y+1),
    //         node.TargetPosition, node));
    //     Neighbours.Add(new Node(node.G+1,
    //         new Vector2(node.Position.x, node.Position.y-1),
    //         node.TargetPosition, node));
    //     return Neighbours;
    // }
    //
    // private void OnDrawGizmos()
    // {
    //     foreach (var item in PathToTarget)
    //     {
    //         if (PathToTarget != null)
    //         {
    //             Gizmos.color = Color.red;
    //             Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.2f);
    //         }
    //     }
    // }

    public void Move()
    {
        if(!IsMove)
            StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        IsMove = true;
        while (_Ammo_On_Scene.AmmoList.Count > 0)
        {
            Pathfinder();
            yield return null;
        }
        IsMove = false;
        _move_Back = false;
        _move_Up = false;
        _Animation.MoveAnimation(IsMove,_move_Back,_move_Up);
    }

    private void Pathfinder()
    {
        PathRadius();
        RaycastHit2D[] _hits = new RaycastHit2D[100];
        _hits = Physics2D.RaycastAll(transform.position,
            (_Ammo_On_Scene.AmmoList[0].transform.position - transform.position));

        Transform _temp;
        for (int i = 0; i < _Around.Count; i++)
        {
            for (int j = i+1; j < _Around.Count; j++)
            {
                if ((_Ammo_On_Scene.AmmoList[0].transform.position - _Around[i].position).magnitude >
                    (_Ammo_On_Scene.AmmoList[0].transform.position - _Around[j].position).magnitude)
                {
                    _temp = _Around[i];
                    _Around[i] = _Around[j];
                    _Around[j] = _temp;
                }
            }
        }

        _Next = _Around[0];

        Vector3 _horizontal = new Vector3(_Next.position.x, transform.position.y);
        Vector3 _vertical = new Vector3(transform.position.x, _Next.position.y);
        
        if(transform.position != _horizontal)
        {
            transform.position = Vector3.MoveTowards(transform.position, _horizontal, _Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _vertical, _Speed * Time.deltaTime);
        }
        
        if (transform.position == _Ammo_On_Scene.AmmoList[0].transform.position)
        {
            Destroy(_Ammo_On_Scene.AmmoList[0]);
            _Ammo_On_Scene.RemoveAmmo(_Ammo_On_Scene.AmmoList[0]);
            GameEvents.TakeAmmo();
        }
    }

    private void PathRadius()
    {
        float _offset = 0.1f;
        for (int i = 0; i < Tiles.Count; i++)
        {
            if ((Tiles[i].position - transform.position).magnitude <= Tiles[i].localScale.x + _offset && !Tiles[i].CompareTag("NotWalkable"))
            {
                if (!_Around.Contains(Tiles[i]))
                {
                    _Around.Add(Tiles[i]);
                }
            }
            else
            {
                if (_Around.Contains(Tiles[i]))
                {
                    _Around.Remove(Tiles[i]);
                }
            }
        }
    }

    private void ResetPosition()
    {
        transform.position = _Start_Position.position;
        if (_Around.Count>0)
        {
            for (int i = 0; i < _Around.Count; i++)
            {
                _Around.Remove(_Around[i]);
            }
        }
        
    }

    private void OnDisable()
    {
        GameEvents._Put_Ammo -= Move;
        GameEvents._Start_Level -= ResetPosition;
    }

    private void OnDestroy()
    {
        GameEvents._Put_Ammo -= Move;
        GameEvents._Start_Level -= ResetPosition;
    }
    
}

public class Node
{
    public Vector2 Position;
    public Vector2 TargetPosition;
    public Node PrevNode;
    public int F,G,H;
    public Node(int g, Vector2 _nodePosition,Vector2 _targetPosition, Node _prevNode)
    {
        TargetPosition = _targetPosition;
        Position = _nodePosition;
        PrevNode = _prevNode;
        G = g;
        H = (int)Mathf.Abs(_targetPosition.x - Position.x) + (int)Mathf.Abs(_targetPosition.y - Position.y);
        F = G + H;
    }
}
