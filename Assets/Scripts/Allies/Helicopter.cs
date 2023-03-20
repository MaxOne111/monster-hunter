using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private AnimationCurve _Path_X;
    [SerializeField] private AnimationCurve _Path_Y;

    private void Update()
    {
        for (int i = 0; i < _Path_X.length; i++)
        {
            for (int j = 0; j < _Path_Y.length; j++)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(_Path_X.keys[i].value, _Path_Y.keys[j].value), Time.deltaTime);
                Debug.Log($"{_Path_X.keys[i].value}/{_Path_Y.keys[j].value}");
            }
        }
    }
}
