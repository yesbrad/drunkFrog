using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PPFXParticle : MonoBehaviour
{
	private Quaternion baseRotation;
	private ParticleSystem particle;
	private Pool pool;

	private void Awake()
	{
		particle = GetComponent<ParticleSystem>();
		baseRotation = transform.rotation;
	}

	public void Play (Pool currentPool)
	{
		pool = currentPool;
		transform.rotation = baseRotation;
		particle.Play();
		StartCoroutine(Stop());
	}
	
	IEnumerator Stop()
	{
		yield return new WaitForSeconds(particle.main.duration);
		pool.AddBackToPool(gameObject);
	}
}
