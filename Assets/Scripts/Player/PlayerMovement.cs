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
    private PlayerAnimation _Animation;
    
    private List<Vector2> _Path;
    private List<Node> _Checked_Nodes;
    private List<Node> _Waiting_Nodes;
    
    private AmmoOnScene _Ammo_On_Scene;
    [SerializeField] private LayerMask _Obstacle_Layer;

    public bool IsMove { get; private set; }

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

    public void Move()
    {
        if(!IsMove)
            StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        IsMove = true;
        _Animation.StartMove();
        while (_Ammo_On_Scene.AmmoList.Count > 0)
        {
            _Path = GetPath(_Ammo_On_Scene.AmmoList[0].transform.position);
            if (_Path.Count > 0)
            {
                Vector3 _vertical = new Vector2(transform.position.x, _Path[_Path.Count-1].y);
                Vector3 _horizontal = new Vector2(_Path[_Path.Count-1].x, transform.position.y);
                if (transform.position != _horizontal)
                {
                    if (transform.position.x < _horizontal.x)
                    {
                        _Animation.MoveRight(_horizontal.x);
                    }
                    else
                    {
                        _Animation.MoveLeft(_horizontal.x);
                    }
                    
                    transform.position = Vector2.MoveTowards(transform.position, _horizontal, _Speed * Time.deltaTime);
                }
                else
                {
                    if (transform.position.y < _vertical.y)
                    {
                       _Animation.MoveUp(_vertical.y);
                    }
                    else
                    {
                        _Animation.MoveDown(_vertical.y);
                    }
                    transform.position = Vector2.MoveTowards(transform.position, _vertical, _Speed * Time.deltaTime);
                }
            }

            if (transform.position == _Ammo_On_Scene.AmmoList[0].transform.position)
            {
                Destroy(_Ammo_On_Scene.AmmoList[0]);
                _Ammo_On_Scene.RemoveAmmo(_Ammo_On_Scene.AmmoList[0]);
                GameEvents.TakeAmmo();
            }
            yield return null;
        }
        IsMove = false;
        _Animation.StopMove();
    }

   private List<Vector2> GetPath(Vector2 _target)
    {
        _Path = new List<Vector2>();
        _Checked_Nodes = new List<Node>();
        _Waiting_Nodes = new List<Node>();

        Vector3 _start_Pos = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));
        Vector3 _target_Pos = new Vector2(Mathf.Round(_target.x),Mathf.Round(_target.y));
        if (_start_Pos == _target_Pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target, 2 * Time.deltaTime);
            return _Path;
        }
        
        Node _start_Node = new Node(_start_Pos, _target_Pos, null, 0);
        _Checked_Nodes.Add(_start_Node);
        _Waiting_Nodes.AddRange(NeighboursNodes(_start_Node));

        

        while (_Waiting_Nodes.Count > 0)
        {
            Node _node_To_Check = _Waiting_Nodes.Where(x => x.F == _Waiting_Nodes.Min(y => y.F)).FirstOrDefault();

            if (_node_To_Check._Position == _target_Pos)
            {
                return PathToNode(_node_To_Check);
            }

            bool _walkable = !Physics2D.OverlapCircle(_node_To_Check._Position, 0.1f, _Obstacle_Layer);
            if (!_walkable)
            {
                _Waiting_Nodes.Remove(_node_To_Check);
                _Checked_Nodes.Add(_node_To_Check);
            }
            else
            {
                _Waiting_Nodes.Remove(_node_To_Check);
                if (!_Checked_Nodes.Where(x => x._Position == _node_To_Check._Position).Any())
                {
                    _Checked_Nodes.Add(_node_To_Check);
                    _Waiting_Nodes.AddRange(NeighboursNodes(_node_To_Check));
                }
            }
            

        }

        return _Path;
    }

    private List<Vector2> PathToNode(Node _node)
    {
        List<Vector2> _path = new List<Vector2>();
        Node _current_Node = _node;

        while (_current_Node._Prev_Node != null)
        {
            _path.Add(new Vector2(_current_Node._Position.x, _current_Node._Position.y));
            _current_Node = _current_Node._Prev_Node;
        }

        return _path;
    }

    private List<Node> NeighboursNodes(Node _node)
    {
        List<Node> _neighbours = new List<Node>();

        _neighbours.Add(new Node(new Vector2(_node._Position.x + 1, _node._Position.y), _node._Target_Position,_node, _node.G + 1));
        _neighbours.Add(new Node(new Vector2(_node._Position.x - 1, _node._Position.y), _node._Target_Position,_node, _node.G + 1));
        _neighbours.Add(new Node(new Vector2(_node._Position.x, _node._Position.y + 1), _node._Target_Position,_node, _node.G + 1));
        _neighbours.Add(new Node(new Vector2(_node._Position.x, _node._Position.y - 1), _node._Target_Position,_node, _node.G + 1));
        
        return _neighbours;
    }

   

    private void ResetPosition()
    {
        transform.position = _Start_Position.position;
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
    public Vector3 _Position;
    public Vector3 _Target_Position;
    public Node _Prev_Node;
    public int F;
    public int G;
    public int H;

    public Node(Vector3 _position, Vector3 _targetPosition, Node _prevNode, int g)
    {
        _Position = _position;
        _Target_Position = _targetPosition;
        _Prev_Node = _prevNode;
        G = g;
        H = (int)Mathf.Abs(_targetPosition.x - _Position.x) + (int)Mathf.Abs(_targetPosition.y - _Position.y);
        F = G + H;
    }
}
