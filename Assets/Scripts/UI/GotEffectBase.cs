using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GotEffectBase : MonoBehaviour
{
	private bool isFollowing = false;

	private GameObject targetObj;

	private float minSizeMin = 0.5f;
	private float minSizeMax = 2f;
	private float minSize = 0.5f;
	private float maxSizeMin = 0.8f;
	private float maxSizeMax = 3f;
	private float maxSize = 0.8f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFollowing)
		{
			var _main = transform.GetComponent<ParticleSystem>().main;
			_main.startSize = new ParticleSystem.MinMaxCurve(minSize, maxSize);
		}
	}

	public void FlyFromSceneToUI(GameObject to, float duration)
	{
		transform.DOMove(to.transform.position, duration).SetEase(Ease.InCubic).OnComplete(GotExplosionEffect);
		DOTween.To(() => minSize, x => minSize = x, minSizeMax, duration);
		DOTween.To(() => maxSize, x => maxSize = x, maxSizeMax, duration);
		isFollowing = true;
	}
	
	public void FlyFromUIToScene()
	{
		
	}

	public void GotExplosionEffect()
	{
		var _em = transform.GetComponent<ParticleSystem>().emission;
		_em.enabled = false;
		_em = transform.Find("Tail").GetComponent<ParticleSystem>().emission;
		_em.enabled = false;
		transform.Find("GotEffectDeath").gameObject.SetActive(true);
		Destroy(gameObject,1f);
	}
}
