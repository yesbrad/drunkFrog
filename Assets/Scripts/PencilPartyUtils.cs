using UnityEngine;

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
}
