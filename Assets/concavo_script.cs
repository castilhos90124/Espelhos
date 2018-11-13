using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class concavo_script : MonoBehaviour
{
	private const float translationSpeed = 2;
	private const float virtualTranslationSpeed = 5;
	private const float focoMinPosition = 1.03f;
	private const float focoMaxPosition = 1.15f;
	private const float centroPosition = -1.23f; 
	private const float realMultiplier = 0.99f;
	private const float virtualMultiplier = 0.98f;

	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;
	public GameObject objImagemReal;
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
		const float boundaryRightLimit = 2.1f;
		const float atrasCentroMultiplier = 0.99f;

		float realPosition = objReal.transform.localPosition.x;

		float imgRealScaleX = objImagemReal.transform.localScale.x;
		float imgRealScaleY = objImagemReal.transform.localScale.y;
		float imgRealScaleZ = objImagemReal.transform.localScale.z;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition < boundaryRightLimit) {

			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);

			if (realPosition < focoMinPosition) { 	//Objeto atrás do foco
				objImagemVirtual.SetActive (false);
				objImagemReal.SetActive (true);
				objImagemReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
				objImagemReal.transform.localScale = new Vector3 (imgRealScaleX, imgRealScaleY * ( 1 / realMultiplier), imgRealScaleZ);
			}
			if (realPosition >= focoMinPosition && realPosition <= focoMaxPosition) {	//Objeto no foco
				objImagemReal.SetActive (false);
				objImagemVirtual.SetActive (false);

			}
			if(realPosition > focoMaxPosition){			//Objeto entre o foco e o vertice
				objImagemVirtual.SetActive (true);
				objImagemVirtual.transform.Translate (-virtualTranslationSpeed * Time.deltaTime, 0, 0);
				objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * virtualMultiplier, imgVirtualScaleZ);

			}
		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = -3.42f; 
		const float atrasCentroMultiplier = 0.99f;

		float realPosition = objReal.transform.localPosition.x;

		float imgRealScaleX = objImagemReal.transform.localScale.x;
		float imgRealScaleY = objImagemReal.transform.localScale.y;
		float imgRealScaleZ = objImagemReal.transform.localScale.z;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition > boundaryLeftLimit) {
			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);

			if (realPosition < focoMinPosition) { 	//Objeto atrás do foco
				objImagemVirtual.SetActive (false);
				objImagemReal.SetActive (true);

				objImagemReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
				objImagemReal.transform.localScale = new Vector3 (imgRealScaleX, imgRealScaleY * realMultiplier, imgRealScaleZ);
			}
			if (realPosition >= focoMinPosition && realPosition <= focoMaxPosition) {	//Objeto no foco
				objImagemReal.SetActive (false);
				objImagemVirtual.SetActive (false);
			}
			if(realPosition > focoMaxPosition){		//Objeto entre o foco e o vertice
				objImagemVirtual.SetActive (true);
				objImagemVirtual.transform.Translate (virtualTranslationSpeed * Time.deltaTime, 0, 0);
				objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, imgVirtualScaleY * (1/virtualMultiplier), imgVirtualScaleZ);

			}

		}
	}

}