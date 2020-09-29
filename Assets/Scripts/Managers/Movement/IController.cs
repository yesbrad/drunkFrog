using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
	Pawn Pawn { get; }
	Vector3 Position { get; }
}
