using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMovableObject
{
    [UnityTest]
    public IEnumerator TestMovableObjectGetsCreated()
    {
        var movableObject = new MovableObject();
        Assert.AreEqual((float) 0, movableObject.GetPosition().x);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator TestMoveRight()
    {
        var movableObject = new MovableObject();
        
        movableObject.MoveRight(1f);
        Assert.AreEqual((float)-1, movableObject.GetPosition().x);
        yield return null;
    }  
    
    [UnityTest]
    public IEnumerator TestMoveLeft()
    {
        var movableObject = new MovableObject();
        
        movableObject.MoveLeft(1f);
        Assert.AreEqual((float) 1, movableObject.GetPosition().x);
        yield return null;
    }  
  
    
    
}