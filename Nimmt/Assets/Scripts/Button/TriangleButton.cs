using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleButton : MonoBehaviour
{
    private GameObject hand_space_obj;

    void Start(){
        hand_space_obj = GameObject.Find("HandSpace");
    }
    public void TriangleButtonPushed(){
        GameObject selected_card_obj = hand_space_obj.transform.GetChild(GameManager.selected_hand_row_num).gameObject;
        selected_card_obj.GetComponent<Card>().CardCollect(this.gameObject.transform.GetSiblingIndex());
    }
}
