using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentManager : MonoBehaviour
{
    public float transparency;
    Image img;
    public static TransparentManager instance;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
        img = GameObject.Find("BloodEffect").GetComponent<Image>();
    }

    void Update()
    {
		Color color = img.color;
        color.a = transparency;
        img.color = color;

        if(transparency > 0)
        {
            transparency -= Time.deltaTime * 0.15f;
		}

        if (transparency > 1)
        {
            transparency = 1;
        }
	}
}
