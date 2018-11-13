using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class plano_script : MonoBehaviour
{
	private float translationSpeed = 2;
	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;
	public GameObject objImagem;



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
		const float boundaryRightLimit = 3.82f; 
		//Debug.Log (objReal.transform.localPosition.x);
		if (objReal.transform.localPosition.x < boundaryRightLimit) {
			
			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
			objImagem.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = 0.42f; 

		if (objReal.transform.localPosition.x > boundaryLeftLimit) {
			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
			objImagem.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
		}
	}

}