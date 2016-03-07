using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public Text	moneyText;

	[HideInInspector] public int money;

	public void addMoney(int amount) {
		money += amount;
		moneyText.text = "$" + money;
	}
}
