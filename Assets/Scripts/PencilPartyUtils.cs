using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class PencilPartyUtils
{
    public static int RoundUp(int numToRound, int multiple)
    {
        if (multiple == 0)
            return numToRound;

        int remainder = Mathf.Abs(numToRound) % multiple;
        if (remainder == 0)
            return numToRound;

        if (numToRound < 0)
            return -(Mathf.Abs(numToRound) - remainder);
        else
            return numToRound + multiple - remainder;
    }

	public static Vector3 RoundAnglesToNearest90(Transform rotationTranform)
	{
		Vector3 newAngle = new Vector3(0, Mathf.Round(rotationTranform.eulerAngles.y / 90) * 90, 0);

		if(newAngle.y == 360)
			newAngle.y = 0;

		return newAngle;
	}

    public static T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new System.Exception("The type must be serializable.");
        }

        // Don't serialize a null object, simply return the default for that object
        if (UnityEngine.Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
}
