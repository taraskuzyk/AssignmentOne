using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    private Cube _cube;
    private Sphere _sphere;
    private EegData _eegData;
    void Start()
    {
        InitialCubeAndSphere();
        var eegDataReader = new EegDataReader("epochs.txt");
        _eegData = new EegData(eegDataReader.GetMatrix());
    }

    private void InitialCubeAndSphere()
    {
        _cube = new Cube();
        _sphere = new Sphere();
        var scale = new Vector3(1, 1, 1);
        _cube.SetScale(scale);
        _sphere.SetScale(scale);

        var spherePosition = new Vector3(0, 1, 0);
        _sphere.SetPosition(spherePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRightKeyDown())
        {
            MoveSphereToEegCoordinates();
        }
    }

    private static bool IsRightKeyDown()
    {
        return Input.GetKeyDown("right");
    }

    private void MoveSphereToEegCoordinates()
    {
        var position = GetEegCoordinates();
        _sphere.SetPosition(position);
    }

    private Vector3 GetEegCoordinates()
    {
        const double factor = 10;
        var x = (float) (_eegData.GetDataByChannelNumber(channelNumber: 1).Average() * factor);
        var y = (float) (_eegData.GetDataByChannelNumber(channelNumber: 2).Average() * factor);
        var z = (float) (_eegData.GetDataByChannelNumber(channelNumber: 3).Average() * factor);
        return new Vector3(x, y, z);
    }

}

public class EegData
{
    private readonly double[,] _matrix;

    public EegData(double[,] matrix)
    {
        _matrix = matrix;
    }

    public double[] GetDataByChannelNumber(int channelNumber)
    {
        return Enumerable.Range(0, _matrix.GetLength(0))
            .Select(x => _matrix[x, channelNumber])
            .ToArray();
    }
    
}
