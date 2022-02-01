using UnityEngine;

public class MovableObject
{
    private readonly GameObject _movableObject;

    public MovableObject()
    {
        _movableObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    public MovableObject(PrimitiveType type)
    {
        _movableObject = GameObject.CreatePrimitive(type);
    }

    public MovableObject(string findByName)
    {
        _movableObject = GameObject.Find(findByName);
    }
    
    public Vector3 GetPosition()
    {
        return _movableObject.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        _movableObject.transform.position = position;
    }

    public Vector3 GetScale()
    {
        return _movableObject.transform.localScale;
    }

    public void SetScale(Vector3 scale)
    {
        _movableObject.transform.localScale = scale;
    }
    
    public void MoveLeft(float delta)
    {
        MoveX(delta);
    }

    public void MoveRight(float delta)
    {
        MoveX(-delta);
    }

    private void MoveX(float delta)
    {
        var newPosition = GetPosition();
        newPosition.x += delta;
        SetPosition(newPosition);
    }
 
}