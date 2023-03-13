using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

public class CreateObstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] _Obstacles;
    [SerializeField] [Range(0, 100)] private int _DropChance;
    private List<Transform> _Tile_Positions = new List<Transform>();
    private List<GameObject> _Obstacles_On_Scene = new List<GameObject>();

    private PlayerMovement _Movement;
    
    [Inject]
    private void Construct(PlayerMovement _movement)
    {
        _Movement = _movement;
    }
    
    private void Awake()
    {
        GetTiles();
        
        DropChance();
    }

    private void OnEnable()
    {
        GetTiles();
        
        DropChance();
    }

    private void DropChance()
    {
        int _drop = Random.Range(0, 101);
        if (_drop < _DropChance)
        {
            CreateObstacle();
        }

        _Movement.Tiles = _Tile_Positions;
    }

    private void CreateObstacle()
    {
        int _obstacles_Count = Random.Range(3, 6);
        for (int i = 0; i < _obstacles_Count; i++)
        {
            int _obstacle_Index = Random.Range(0, _Obstacles.Length);
            int _tile_Index = Random.Range(0, _Tile_Positions.Count);

            GameObject _obstacle = Instantiate(_Obstacles[_obstacle_Index], _Tile_Positions[_tile_Index].position, Quaternion.identity);
            _Obstacles_On_Scene.Add(_obstacle);
            LockTiles(_tile_Index);
        }
    }

    private void GetTiles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _Tile_Positions.Add(transform.GetChild(i));
            UnlockTiles(i);
        }
    }

    private void LockTiles(int _index)
    {
        _Tile_Positions[_index].tag = "NotWalkable";
    }

    private void UnlockTiles(int _index)
    {
        _Tile_Positions[_index].tag = "Walkable";
    }

    private void OnDisable()
    {
        for (int i = 0; i < _Obstacles_On_Scene.Count; i++)
        {
            Destroy(_Obstacles_On_Scene[i]);
        }
        _Obstacles_On_Scene.Clear();
        _Tile_Positions.Clear();
    }
    
}
