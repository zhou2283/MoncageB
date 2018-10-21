using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneBase : MonoBehaviour
{

	public int moveState = 0;
	private Transform[] moveMarkArray;
	private Transform sceneBody;

	private CameraControl cameraControlScript;
	
	// Use this for initialization
	void Start ()
	{
		cameraControlScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		
		moveMarkArray = new Transform[transform.childCount - 1];//one is scene, others are move mark
		foreach (Transform child in transform)
		{
			if (child.name.Substring(0, 4) == "Move")
			{
				moveMarkArray[int.Parse(child.name[4].ToString())] = child;
			}
			else
			{
				sceneBody = child;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeMoveState(int _moveState, float duration, float delay)
	{
		moveState = _moveState;
		GameControlGlobal.Instance.INTERACTION_IS_ACTIVE = false;
		sceneBody.DOLocalMove(moveMarkArray[moveState].localPosition, duration).SetEase(Ease.InOutCubic).SetDelay(delay);
		sceneBody.DOLocalRotate(moveMarkArray[moveState].localRotation.eulerAngles, duration).SetEase(Ease.InOutCubic).SetDelay(delay).OnComplete(ActiveInteraction);
	}

	public Transform ChangeMoveStateManually(int _moveState)//return the target move mark Transform
	{
		return moveMarkArray[_moveState];
	}

	public void SetMoveState(int _moveState)//used in load process
	{
		if (_moveState >= transform.childCount)
		{
			Debug.Log("Error: wrong move state for scene - " + gameObject.name);
			return;
		}

		moveState = _moveState;
		sceneBody.localPosition = moveMarkArray[moveState].localPosition;
		sceneBody.localRotation = moveMarkArray[moveState].localRotation;
	}

	void ActiveInteraction()
	{
		GameControlGlobal.Instance.INTERACTION_IS_ACTIVE = true;
	}
}
