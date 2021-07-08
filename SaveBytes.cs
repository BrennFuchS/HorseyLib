using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>Saves classes with BinaryFormatters</summary>
public static class SaveBytes
{
    static BinaryFormatter bf = new BinaryFormatter();

    /// <summary>Save a list of data to the save file</summary>
    public static void save(string saveFile, params object[] data)
    {
        if (data == null) return;

        var fs = new FileStream(saveFile, FileMode.Create);
        bf.Serialize(fs, fixArray(data));
        fs.Close();
    }

    /// <summary>Load and return a list of data from the save file</summary>
    public static object[] load(string saveFile, object[] ifFail = null)
    {
        try
        {
            var fs = new FileStream(saveFile, FileMode.Open);
            var data = (object[])bf.Deserialize(fs);
            fs.Close();
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ifFail;
        }
    }

    /// <summary>Replace all unity classes that can be saved with their saveable variants</summary>
    public static object[] fixArray(object[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            if (array[i] == null) continue;

            if (array[i] is Color color) array[i] = new sColor(color);
            else if (array[i] is Quaternion quaternion) array[i] = new sQuaternion(quaternion);
            else if (array[i] is Vector2 vector) array[i] = new sVector2(vector);
            else if (array[i] is Vector3 vector1) array[i] = new sVector3(vector1);
            else if (array[i] is Vector4 vector2) array[i] = new sVector4(vector2);
            else if (array[i] is GameObject gameObject) array[i] = new sGameObject(gameObject);
            else if (array[i] is Transform transform) array[i] = new sTransform(transform);
        }
        return array;
    }
}

/// <summary>Saved class of UnityEngine.Color</summary>
/// <remarks>Saves r, g, b, and a</remarks>
[Serializable]
public class sColor
{
    public float r;
    public float g;
    public float b;
    public float a;

    internal sColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }

    /// <summary>Get the data as a Color</summary>
    public Color get() => new Color(r, g, b, a);

    public override string ToString() => get().ToString();
}

/// <summary>Saved class of UnityEngine.Quaternion</summary>
/// <remarks>Saves x, y, z, and w</remarks>
[Serializable]
public class sQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    internal sQuaternion(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    /// <summary>Get the data as a Quaternion</summary>
    public Quaternion get() => new Quaternion(x, y, z, w);

    public override string ToString() => get().ToString();
}

/// <summary>Saved class of UnityEngine.Vector4</summary>
/// <remarks>Saves x, y, z, and w</remarks>
[Serializable]
public class sVector4
{
    public float x;
    public float y;
    public float z;
    public float w;

    internal sVector4(Vector4 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        w = vector.w;
    }

    /// <summary>Get the data as a Vector4</summary>
    public Vector4 get() => new Vector4(x, y, z, w);

    public override string ToString() => get().ToString();
}

/// <summary>Saved class of UnityEngine.Vector3</summary>
/// <remarks>Saves x, y, and z</remarks>
[Serializable]
public class sVector3
{
    public float x;
    public float y;
    public float z;

    internal sVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    /// <summary>Get the data as a Vector3</summary>
    public Vector3 get() => new Vector3(x, y, z);

    public override string ToString() => get().ToString();
}

/// <summary>Saved class of UnityEngine.Vector2</summary>
/// <remarks>Saves x and y</remarks>
[Serializable]
public class sVector2
{
    public float x;
    public float y;
    public float z;
    public float w;

    internal sVector2(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }

    /// <summary>Get the data as a Vector2</summary>
    public Vector2 get() => new Vector2(x, y);

    public override string ToString() => get().ToString();
}

/// <summary>Saved class of UnityEngine.GameObject</summary>
/// <remarks>Saves name, active, and transform</remarks>
[Serializable]
public class sGameObject
{
    public sTransform transform;
    public string name;
    public bool active;

    internal sGameObject(GameObject gameObject)
    {
        transform = new sTransform(gameObject.transform);
        name = gameObject.name;
        active = gameObject.activeSelf;
    }

    /// <summary>Apply the data to a GameObject</summary>
    public void apply(GameObject gameObject)
    {
        transform.apply(gameObject.transform);
        gameObject.name = name;
        gameObject.SetActive(active);
    }
}

/// <summary>Saved class of UnityEngine.Transform</summary>
/// <remarks>Saves local position, local rotation, and local scale</remarks>
[Serializable]
public class sTransform
{
    public sVector3 position;
    public sVector3 rotation;
    public sVector3 scale;

    internal sTransform(Transform transform)
    {
        position = new sVector3(transform.localPosition);
        rotation = new sVector3(transform.localEulerAngles);
        scale = new sVector3(transform.localScale);
    }

    /// <summary>Apply the data to a Transform</summary>
    public void apply(Transform transform)
    {
        transform.localPosition = position.get();
        transform.localEulerAngles = rotation.get();
        transform.localScale = scale.get();
    }
}