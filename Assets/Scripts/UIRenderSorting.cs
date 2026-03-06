using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRenderSorting : MonoBehaviour
{
	public int sort = 0;

	private void Awake()
	{
		this.gameObject.GetComponent<RectTransform>().SetSiblingIndex(sort);
	}
}
