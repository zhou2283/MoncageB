using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameControlGlobal : UnitySingleton<GameControlGlobal>
{
	public bool INTERACTION_IS_ACTIVE = true;
	private Transform waitingIcon;

	private void Start()
	{
		waitingIcon = GameObject.Find("WaitingIcon").transform;
	}

	private void Update()
	{
		if (waitingIcon == null)
		{
			waitingIcon = GameObject.Find("WaitingIcon").transform;
			return;
		}
		if (INTERACTION_IS_ACTIVE)
		{
			waitingIcon.GetComponent<Image>().DOFade(0f, 0.2f);
		}
		else
		{
			waitingIcon.GetComponent<Image>().DOFade(0.4f, 0.2f).SetDelay(0.1f);
		}
	}
}
