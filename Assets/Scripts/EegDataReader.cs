using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.SocialPlatforms;

public class EegDataReader
{
    private static readonly char[] DelimiterChars = {' ', '\t'};
    private const string AssetsRelativeDirectory = "Assets";
    private const string FilesRelativeDirectory = "Files";
    private readonly string _filePath;

    public EegDataReader(string fileName)
    {
        var rootDirectory = GetProjectRootDirectory();
        _filePath = Path.Combine(rootDirectory, AssetsRelativeDirectory, FilesRelativeDirectory, fileName);
    }

    public string GetProjectRootDirectory()
    {
        var rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        rootDirectory = rootDirectory.Replace("\\Library\\ScriptAssemblies", "");
        rootDirectory = rootDirectory.Replace("/Library/ScriptAssemblies", "");
        rootDirectory = rootDirectory.Replace("file:/", "");
        rootDirectory = rootDirectory.Replace("file:\\", "");
        return rootDirectory;
    }
    
    public double[,] GetMatrix()
    {
        var lines = ReadFileLines();
        var rows = GetNotEmptyRows(lines);
        var lengthsOfRows = rows.Select(row => row.Length).ToArray();
        var maxColumns = lengthsOfRows.Max();
        var matrix = new double[rows.Length, maxColumns];
        foreach (var row in Enumerable.Range(0, matrix.GetLength(0)))
        {
            foreach (var column in Enumerable.Range(0, maxColumns))
            {
                matrix[row, column] = rows[row][column];
            }
        }
        return matrix;
    }

    private static double[][] GetNotEmptyRows(string[] lines)
    {
        var rows = lines.Select(ParseLine).ToArray();
        return rows.Where(row => row.Length != 0).ToArray();
    }

    public string[] ReadFileLines()
    {
        return File.ReadAllLines(_filePath);
    }

    public static double[] ParseLine(string line)
    {
        var wordsAndEmptyStrings = line.Split(DelimiterChars);
        var words = wordsAndEmptyStrings.Where(word => word != "").ToArray();
        return Array.ConvertAll(words, double.Parse);
    }

}
