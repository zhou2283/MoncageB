using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestFunction : MonoBehaviour
{
	private Transform mod;

	private Transform scenePositionMarkGroup;

	private List<Transform> scenePositionMarkList = new List<Transform>();
	// Use this for initialization
	void Start ()
	{
		mod = transform.Find("Mod");
		scenePositionMarkGroup = transform.Find("ScenePositionMarkGroup");
		foreach (Transform child in scenePositionMarkGroup)
		{
			scenePositionMarkList.Add(child);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z))
		{
			mod.DOMove(scenePositionMarkList[1].position, 1.5f).SetEase(Ease.InOutCubic);
			mod.DORotate(scenePositionMarkList[1].rotation.eulerAngles, 1.5f).SetEase(Ease.InOutCubic);
			mod.DOScale(scenePositionMarkList[1].localScale, 1.5f).SetEase(Ease.InOutCubic);
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			mod.DOMove(scenePositionMarkList[0].position, 1.5f).SetEase(Ease.InOutCubic);
			mod.DORotate(scenePositionMarkList[0].rotation.eulerAngles, 1.5f).SetEase(Ease.InOutCubic);
			mod.DOScale(scenePositionMarkList[0].localScale, 1.5f).SetEase(Ease.InOutCubic);
		}
	}
}
