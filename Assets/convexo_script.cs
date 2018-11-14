using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class convexo_script : MonoBehaviour
{
	private const float translationSpeed = 2;
	private const float virtualTranslationSpeed = 1.5f;
	//private const float focoMinPosition = 1.03f;
	//private const float focoMaxPosition = 1.15f;
	//private const float centroPosition = -1.23f; 
	//private const float realMultiplier = 0.99f;
	private const float virtualMultiplier = 0.992f;

	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;

	public GameObject objImagemVirtual;



	void Start()
	{

	}

	void Update ()
	{

		if (repeatPositionLeft) {
			PositionLeftButton();
		}

		if (repeatPositionRight) {
			PositionRightButton();
		}

	}

	public void PositionLeftButtonRepeat ()
	{
		repeatPositionLeft = true;
	}

	public void PositionRightButtonRepeat ()
	{
		repeatPositionRight = true;
	}


	public void PositionLeftButtonOff ()
	{
		repeatPositionLeft = false;
	}

	public void PositionRightButtonOff ()
	{
		repeatPositionRight = false;
	}


	public void PositionRightButton ()
	{
		const float boundaryRightLimit = 1.93f;


		float realPosition = objReal.transform.localPosition.x;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition < boundaryRightLimit) {

			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.Translate (-virtualTranslationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * ( 1 / virtualMultiplier), imgVirtualScaleZ);

		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = -2.6f; 

		float realPosition = objReal.transform.localPosition.x;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition > boundaryLeftLimit) {
			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.Translate (virtualTranslationSpeed * Time.deltaTime, 0, 0);
			objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * virtualMultiplier, imgVirtualScaleZ);

			}

	}


}