using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayParticleOnEnable : MonoBehaviour
{
	private void OnEnable()
	{
		GetComponent<ParticleSystem>().Play();
	}
}
