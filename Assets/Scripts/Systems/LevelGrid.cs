using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private GameObject[] _Obstacles;
    [SerializeField] [Range(0, 100)] private int _DropChance;
    
    private List<Transform> _Level_Tiles = new List<Transform>();
    [SerializeField] private List<Transform> _Locked_Tiles;
    private List<GameObject> _Obstacles_On_Scene = new List<GameObject>();

    public List<Transform> LockedTiles{get=>_Locked_Tiles;}
    private AdReward _Reward;

    [Inject]
    private void Construct(AdReward _reward)
    {
        _Reward = _reward;
    }

    private void OnEnable()
    {
        SrartLevelTiles();
        
        DropChance();
    }

    private void DropChance()
    {
        int _drop = Random.Range(0, 101);
        if (_drop < _DropChance)
        {
            CreateObstacle();
        }
    }

    private void CreateObstacle()
    {
        int _obstacles_Count = Random.Range(3, 6);
        for (int i = 0; i < _obstacles_Count; i++)
        {
            int _obstacle_Index = Random.Range(0, _Obstacles.Length);
            int _tile_Index = Random.Range(0, _Level_Tiles.Count);

            GameObject _obstacle = Instantiate(_Obstacles[_obstacle_Index],
                _Level_Tiles[_tile_Index].position,
                Quaternion.identity,
                _Level_Tiles[_tile_Index]);
            
            _Obstacles_On_Scene.Add(_obstacle);
            LockCustomTile(_tile_Index);
        }

        _Reward.LevelGrid = this;
    }

    private void SrartLevelTiles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _Level_Tiles.Add(transform.GetChild(i));
            UnlockAllTiles(i);
        }
    }

    private void LockCustomTile(int _index)
    {
        _Level_Tiles[_index].tag = "NotWalkable";
        _Level_Tiles[_index].gameObject.layer = LayerMask.NameToLayer("Obstacle");
        _Locked_Tiles.Add(_Level_Tiles[_index]);
    }

    private void UnlockAllTiles(int _index)
    {
        _Level_Tiles[_index].tag = "Walkable";
        _Level_Tiles[_index].gameObject.layer = 0;
        if(_Locked_Tiles.Contains(_Level_Tiles[_index]))
            _Locked_Tiles.Remove(_Level_Tiles[_index]);
    }

    public void UnlockCustomTile(int _index)
    {
        if (_Level_Tiles[_index])
        {
            _Locked_Tiles[_index].tag = "Walkable";
            _Locked_Tiles[_index].gameObject.layer = 0;
            Destroy(_Locked_Tiles[_index].GetChild(0).gameObject);
            _Locked_Tiles.Remove(_Locked_Tiles[_index]);
        }
    }
    
    private void OnDisable()
    {
        for (int i = 0; i < _Obstacles_On_Scene.Count; i++)
        {
            Destroy(_Obstacles_On_Scene[i]);
        }
        _Obstacles_On_Scene.Clear();
        _Level_Tiles.Clear();
        _Locked_Tiles.Clear();
    }
    
}
