using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    private const string InputFileName = "epochs.txt";
    private const string OutputFileName = "xyz.txt";
    private Cube _cube;
    private Sphere _sphere;
    private EegData _eegData;
    private string _rootDirectory;
    void Start()
    {
        InitialCubeAndSphere();
        var eegDataReader = new EegDataReader(InputFileName);
        _eegData = new EegData(eegDataReader.GetMatrix());
        _rootDirectory = eegDataReader.GetProjectRootDirectory();
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
        var x = _eegData.GetDataByChannelNumber(channelNumber: 1).Average();
        var y = _eegData.GetDataByChannelNumber(channelNumber: 2).Average();
        var z = _eegData.GetDataByChannelNumber(channelNumber: 3).Average();
        
        SaveXYZ(x, y, z);
        return new Vector3((float)(x * factor),(float) (y*factor), (float) (z*factor));
    }

    private void SaveXYZ(double x, double y, double z)
    {
        var outputFilePath = Path.Combine(_rootDirectory, "Assets", "Files", OutputFileName);
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            outputFile.WriteLine($"{x} {y} {z}");
        }
        
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
