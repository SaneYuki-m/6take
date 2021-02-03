using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public void LifeUpdate(){
        this.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = (GameManager.life_point).ToString();
    }
}
