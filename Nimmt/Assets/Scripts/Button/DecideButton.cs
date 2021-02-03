using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideButton : MonoBehaviour{
    [SerializeField] private GameObject hand_space_obj;
    [SerializeField] private GameObject play_hand_card;
    
    public void DecideButtonPushed(){
        play_hand_card.SetActive(true);
        Card card = hand_space_obj.transform.GetChild(GameManager.selected_hand_row_num)
            .gameObject.GetComponent<Card>();
        card.CardMove();
    }
}