using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolsControl : MonoBehaviour
{
	public GameObject containerPrefab;
	public GameObject gotEffectPrefab;
	
	public GameObject invalidClickEffectPrefab;
	public GameObject validClickEffectPrefab;
	public GameObject needClickEffectPrefab;
	public GameObject findClickEffectPrefab;
	
	private const int maxSlotNum = 5;
	private float itemHeight = 1.4f;
	private float sideX = 5.5f;
	private float sideBuffer = 4f;

	private float panelWidth = 12.8f;
	private float panelHeight = 7.2f;
	
	public Material needItemMat;
	public Material findItemMat;

	private List<string> needItemList = new List<string>();
	private List<string> hasItemList = new List<string>();
	private  List<GameObject> slotList = new List<GameObject>();

	private GameObject currentItem = null;
	private float effectTweeningTime = 1f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown(KeyCode.M))
		{
			foreach (string child in needItemList)
			{
				print(child);
			}
		}
		*/
	}
	
	public bool NeedItem(string name)//check function
	{
		if (slotList.Count >= maxSlotNum)
		{
			Debug.Log("Slots are full!");
			return false;
		}
		foreach (string child in needItemList)
		{
			if (child == name)
			{
				return false;
			}
		}

		return true;
	}
	
	public bool NeedItemIsInList(string name)//check function
	{
		foreach (string child in needItemList)
		{
			if (child == name)
			{
				return true;
			}
		}

		return false;
	}
	
	public void AddNeedItem(GameObject needItem, Vector3 euler, Vector3 size)
	{
		needItemList.Add(needItem.name);

		for (int i = 0; i < slotList.Count; i++)
		{
			slotList[i].transform.DOMoveY(slotList[i].transform.position.y + itemHeight / 2f, 0.5f).SetEase(Ease.OutCubic);
		}


		GameObject newContainer = Instantiate(containerPrefab, transform);
		GameObject newNeedItem = Instantiate(needItem, newContainer.transform);
		newNeedItem.transform.localPosition = Vector3.zero;
		
		
		newContainer.transform.localPosition = new Vector3(sideX + sideBuffer, - slotList.Count * itemHeight / 2f,0);
		Debug.Log(newContainer.transform.localPosition);
		newContainer.transform.DOLocalMoveX(sideX, 0.5f).SetEase(Ease.OutCubic);
		
		newNeedItem.GetComponent<MeshRenderer>().material = needItemMat;
		newNeedItem.layer = 5;
		newNeedItem.name = needItem.name;
		newContainer.AddComponent<SelfRotation>();
		newNeedItem.transform.rotation = Quaternion.Euler(euler);
		newNeedItem.transform.localScale = size;
		newContainer.GetComponent<SelfRotation>().rotationSpeed = new Vector3(0,1,0);
		
		
		
		slotList.Add(newNeedItem);
	}



	public void FindItem(GameObject itemInScene)
	{
		itemInScene.transform.DOScale(0, 0.2f);
		
		needItemList.Remove(itemInScene.name);
		hasItemList.Add(itemInScene.name);
		foreach (GameObject child in slotList)
		{
			if (child.name == itemInScene.name)
			{

				
				var _effect = Instantiate(gotEffectPrefab, itemInScene.transform.position, Quaternion.identity);
				_effect.transform.parent = transform;
				Vector3 positionOnScreen = GameObject.Find("MainCameraBg").GetComponent<Camera>()
					.WorldToScreenPoint(itemInScene.transform.position);
				_effect.transform.localPosition = ScreenToUIPanel(positionOnScreen);

				
				_effect.GetComponent<GotEffectBase>().FlyFromSceneToUI(child, effectTweeningTime);

				currentItem = child;
				StartCoroutine(DelayToActiveItem(effectTweeningTime));
				
				return;
			}
		}
	}
	
	public bool HasItem(string name)//check function
	{
		foreach (string child in hasItemList)
		{
			if (child == name)
			{
				return true;
			}
		}

		return false;
	}

	public void RemoveItem(string name)
	{
		hasItemList.Remove(name);

		foreach (GameObject child in slotList)
		{
			if (child.name == name)
			{
				child.transform.parent.DOLocalMoveX(sideX + sideBuffer, 0.5f);
				slotList.Remove(child);
				ResortItemPosition();
				Destroy(child.transform.parent.gameObject,0.5f);
				return;
			}
			else
			{
				
			}
		}
	}

	void ResortItemPosition()
	{
		for (int i = 0; i < slotList.Count; i++)
		{
			slotList[i].transform.DOMoveY((slotList.Count - 1)/2f * itemHeight - i * itemHeight, 0.5f).SetEase(Ease.OutCubic);
		}
	}

	Vector3 ScreenToUIPanel(Vector3 _positionOnScreen)
	{
		var x = _positionOnScreen.x / (float) Screen.width * panelWidth - panelWidth / 2f;
		var y = _positionOnScreen.y / (float) Screen.height * panelHeight - panelHeight / 2f;
		return new Vector3(x,y,0);
	}

	public void ShowClickEffect(Vector3 _positionOnScreen, int _state)
	{
		if (_state == GameConst.INTERACTION_STATE_INVALID)
		{
			var _effect = Instantiate(invalidClickEffectPrefab, Vector3.zero, Quaternion.identity);
			_effect.transform.parent = transform;
			_effect.transform.localPosition = ScreenToUIPanel(_positionOnScreen);
		}
		else if (_state == GameConst.INTERACTION_STATE_VALID)
		{
			var _effect = Instantiate(validClickEffectPrefab, Vector3.zero, Quaternion.identity);
			_effect.transform.parent = transform;
			_effect.transform.localPosition = ScreenToUIPanel(_positionOnScreen);	
		}
		else if (_state == GameConst.INTERACTION_STATE_NEED)
		{
			var _effect = Instantiate(needClickEffectPrefab, Vector3.zero, Quaternion.identity);
			_effect.transform.parent = transform;
			_effect.transform.localPosition = ScreenToUIPanel(_positionOnScreen);				
		}
		else if (_state == GameConst.INTERACTION_STATE_ALREADY_NEED)
		{
			var _effect = Instantiate(needClickEffectPrefab, Vector3.zero, Quaternion.identity);
			_effect.transform.parent = transform;
			_effect.transform.localPosition = ScreenToUIPanel(_positionOnScreen);	
		}
		else if (_state == GameConst.INTERACTION_STATE_FIND)
		{
			var _effect = Instantiate(findClickEffectPrefab, Vector3.zero, Quaternion.identity);
			_effect.transform.parent = transform;
			_effect.transform.localPosition = ScreenToUIPanel(_positionOnScreen);				
		}
		else if (_state == GameConst.INTERACTION_STATE_USE)
		{
							
		}
		else if (_state == GameConst.INTERACTION_STATE_ALREADY_USED)
		{
							
		}
		
	}

	public IEnumerator DelayToActiveItem(float delay)
	{
		yield return new WaitForSeconds(delay);
		currentItem.GetComponent<MeshRenderer>().material = findItemMat;
	}
	
}
