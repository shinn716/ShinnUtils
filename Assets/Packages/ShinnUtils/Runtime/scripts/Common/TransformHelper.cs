using UnityEngine;

public static partial class ExtensionMethods
{
    public static void SetLocalPositionX(this Transform t, float x)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(x, position.y, position.z);
    }
    public static void SetLocalPositionY(this Transform t, float y)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(position.x, y, position.z);
    }
    public static void SetLocalPositionZ(this Transform t, float z)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(position.x, position.y, z);
    }

    public static void SetLocalRotation(this Transform t, float x)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(new Vector3(x, rotation.eulerAngles.y, rotation.eulerAngles.z));
    }

    public static void SetLocalRotationY(this Transform t, float y)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(new Vector3(rotation.eulerAngles.x, y, rotation.eulerAngles.z));
    }

    public static void SetLocalRotationZ(this Transform t, float z)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, z));
    }
}