using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

	public Image bar;

	public void SetBar(float percent){
		bar.fillAmount = percent;
	}
}
