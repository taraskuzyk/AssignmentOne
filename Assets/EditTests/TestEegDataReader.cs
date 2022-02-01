using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEegDataReader
{
    
    [Test]
    public void TestReadFileLines()
    {
        var dataReader = new EegDataReader("test_file.txt");
        var lines = dataReader.ReadFileLines();
        Assert.AreEqual("0.855635099	0.558540913	1.012277892	0.24675363	", lines[0]);
    }

    [Test]
    public void TestParseLineWithNumbers()
    {
        var parsed = EegDataReader.ParseLine("0.855635099	0.558540913	1.012277892	0.24675363	");
        var expected = new[] {0.855635099, 0.558540913, 1.012277892, 0.24675363};
        Assert.AreEqual(expected, parsed);
    }
    
    [Test]
    public void TestParseLineWithOutNumbers()
    {
        const string mixOfTabsAndSpaces = "	       ";
        var parsed = EegDataReader.ParseLine(mixOfTabsAndSpaces);
        var expected = new double[] {};
        Assert.AreEqual(expected, parsed);
    }

    [Test]
    public void TestGetMatrix()
    {
        var dataReader = new EegDataReader("test_file.txt");
        var expected = new double[,]
        {
            {0.855635099, 0.558540913, 1.012277892, 0.24675363},
            {0.855635099, 0.558540913, 1.012277892, 0.24675363},
            {0.855635099, 0.558540913, 1.012277892, 0.24675363}
            
        };
        var matrix = dataReader.GetMatrix();
        Assert.AreEqual(expected, matrix);
    }
    
    
}
