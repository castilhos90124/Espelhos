using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class concavo_script : MonoBehaviour
{
	private const float translationSpeed = 2;
	private const float virtualTranslationSpeed = 14;
	private const float focoMinPosition = 0.47f;
	private const float focoMaxPosition = 1.39f;
	private const float centroPosition = -1.23f; 
	private const float realMultiplier = 0.4f;
	private const float virtualMultiplier = 0.27f;
	private const float offsetParalelo = 0.00048f;

	private const float multiplierAngle = 58.5f;
	private const float objHeight = 2.7f;
	private const float multiplierScale = 0.195f;
	private const float offsetOrigem = 3.1f;
	//private const float offsetImgOrigem = 0.3f;
	private const float offsetImgOrigem = 0.5f;
	//private const float offset = 1.3f;
	private const float offset = 3f;
	private const float multiplierImgDistanceToMirror = 1.1f;
	private const float focoOffset = 1.21f;
	private const float correcaoTamanhoImgReal = 0.14f;
	private const float offsetVirtualImg = 0.1f;
	private const float offsetVirtualMultiplier = 0.8f;

	private float distanceToMirror;
	private float distanceOrigem;
	private float distanceImgToMirror;
	private float tamanhoImg;

	bool repeatPositionLeft = false;
	bool repeatPositionRight = false;

	public GameObject objReal;
	public GameObject objImagemReal;
	public GameObject objImagemVirtual;
	public GameObject raioIncidenteParalelo;
	public GameObject raioIncidenteOrigem;
	public GameObject raioRefletido;
	public GameObject tracejado;



	void Start()
	{

	}

	void Update ()
	{
		distanceToMirror = -objReal.transform.localPosition.x + offsetOrigem;
		distanceOrigem = Mathf.Sqrt (Mathf.Pow (distanceToMirror, 2) + Mathf.Pow (objHeight, 2));
		distanceImgToMirror = (-(1 / (1 / (((focoMinPosition + focoMaxPosition) / 2)+ focoOffset) - (1 / distanceToMirror))) + offsetImgOrigem) * multiplierImgDistanceToMirror;
		tamanhoImg = ((-distanceImgToMirror / distanceToMirror) * objHeight) + correcaoTamanhoImgReal;
		//Debug.Log((((focoMinPosition + focoMaxPosition) / 2)+ focoOffset));
		//Debug.Log(distanceImgToMirror);
		//Debug.Log(distanceToMirror);
		//Debug.Log(tamanhoImg);

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

		float imgRealPositionX = objImagemReal.transform.localPosition.x;
		float imgRealPositionY = objImagemReal.transform.localPosition.y;
		float imgRealPositionZ = objImagemReal.transform.localPosition.z;

		float imgVirtualPositionX = objImagemVirtual.transform.localPosition.x;
		float imgVirtualPositionY = objImagemVirtual.transform.localPosition.y;
		float imgVirtualPositionZ = objImagemVirtual.transform.localPosition.z;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition < boundaryRightLimit) {

			objReal.transform.Translate (translationSpeed * Time.deltaTime, 0, 0);
			raioIncidenteParalelo.transform.localScale -= new Vector3 (offsetParalelo,0,0);

			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)); 
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset + 180));




			if (realPosition < focoMinPosition) { 	//Objeto atrás do foco
				objImagemVirtual.SetActive (false);
				objImagemReal.SetActive (true);
				//objImagemReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
				//objImagemReal.transform.localScale = new Vector3 (imgRealScaleX, imgRealScaleY * ( 1 / realMultiplier), imgRealScaleZ);
				objImagemReal.transform.localPosition = new Vector3 (distanceImgToMirror,imgRealPositionY,imgRealPositionZ);
				objImagemReal.transform.localScale = new Vector3 (imgRealScaleX, tamanhoImg * realMultiplier, imgRealScaleZ);
			}
			if (realPosition >= focoMinPosition && realPosition <= focoMaxPosition) {	//Objeto no foco
				objImagemReal.SetActive (false);
				objImagemVirtual.SetActive (false);

			}
			if(realPosition > focoMaxPosition){			//Objeto entre o foco e o vertice
				objImagemVirtual.SetActive (true);
				//objImagemVirtual.transform.Translate (-virtualTranslationSpeed * Time.deltaTime, 0, 0);
				objImagemVirtual.transform.localPosition = new Vector3 ((distanceImgToMirror + offsetVirtualImg)*offsetVirtualMultiplier,imgVirtualPositionY,imgVirtualPositionZ);
				objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, tamanhoImg * virtualMultiplier, imgVirtualScaleZ);

			}
		}
	}

	public void PositionLeftButton ()
	{
		const float boundaryLeftLimit = -3.22f; 
		const float atrasCentroMultiplier = 0.99f;

		float realPosition = objReal.transform.localPosition.x;

		float imgRealScaleX = objImagemReal.transform.localScale.x;
		float imgRealScaleY = objImagemReal.transform.localScale.y;
		float imgRealScaleZ = objImagemReal.transform.localScale.z;

		float imgRealPositionX = objImagemReal.transform.localPosition.x;
		float imgRealPositionY = objImagemReal.transform.localPosition.y;
		float imgRealPositionZ = objImagemReal.transform.localPosition.z;

		float imgVirtualPositionX = objImagemVirtual.transform.localPosition.x;
		float imgVirtualPositionY = objImagemVirtual.transform.localPosition.y;
		float imgVirtualPositionZ = objImagemVirtual.transform.localPosition.z;

		float imgVirtualScaleX = objImagemVirtual.transform.localScale.x;
		float imgVirtualScaleY = objImagemVirtual.transform.localScale.y;
		float imgVirtualScaleZ = objImagemVirtual.transform.localScale.z;

		//Debug.Log (objReal.transform.localPosition.x);
		if (realPosition > boundaryLeftLimit) {
			objReal.transform.Translate (-translationSpeed * Time.deltaTime, 0, 0);
			raioIncidenteParalelo.transform.localScale += new Vector3 (offsetParalelo,0,0);

			raioIncidenteOrigem.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle));
			raioIncidenteOrigem.transform.localScale = new Vector3 (1,distanceOrigem * multiplierScale,1);

			raioRefletido.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -(Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle)+offset));

			tracejado.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, - (Mathf.Asin(distanceToMirror/distanceOrigem) * multiplierAngle) +offset + 180));


			if (realPosition < focoMinPosition) { 	//Objeto atrás do foco
				objImagemVirtual.SetActive (false);
				objImagemReal.SetActive (true);


				objImagemReal.transform.localPosition = new Vector3 (distanceImgToMirror,imgRealPositionY,imgRealPositionZ);

				objImagemReal.transform.localScale = new Vector3 (imgRealScaleX, tamanhoImg * realMultiplier, imgRealScaleZ);
			}
			if (realPosition >= focoMinPosition && realPosition <= focoMaxPosition) {	//Objeto no foco
				objImagemReal.SetActive (false);
				objImagemVirtual.SetActive (false);
			}
			if(realPosition > focoMaxPosition){		//Objeto entre o foco e o vertice
				objImagemVirtual.SetActive (true);
				//objImagemVirtual.transform.Translate (virtualTranslationSpeed * Time.deltaTime, 0, 0);
				objImagemVirtual.transform.localPosition = new Vector3 ((distanceImgToMirror + offsetVirtualImg)*offsetVirtualMultiplier,imgVirtualPositionY ,imgVirtualPositionZ);
				objImagemVirtual.transform.localScale = new Vector3 (imgVirtualScaleX, tamanhoImg * virtualMultiplier, imgVirtualScaleZ);

			}

		}
	}

}