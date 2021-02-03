using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour{

    //設定
    [SerializeField] public int card_num;
    [SerializeField] private bool hand_card_flg;
    private GameObject hand_space_obj;
    private GameObject triangle_space_obj;
    private GameObject player;

    private GameObject game_manager_obj;
    private float next_time;
	public float interval = 0.1f;	// 点滅周期

    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("Player1");
        next_time = Time.time;
        game_manager_obj = GameObject.Find("GameManager");
        hand_space_obj = GameObject.Find("HandSpace");
        triangle_space_obj = GameObject.Find("GetCardButtonSpace");
    }

    void Update(){
        if (hand_card_flg&&card_num == GameManager.selected_hand_card&&Time.time > next_time ){
			GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
			next_time += interval;
		}
        if(hand_card_flg&&card_num != GameManager.selected_hand_card&&!GetComponent<Image>().enabled){
            GetComponent<Image>().enabled = true;
        }
    }

    public void CardPushed(){
        if(hand_card_flg)GameManager.selected_hand_card = card_num;
        if(hand_card_flg)GameManager.selected_hand_row_num = this.gameObject.transform.GetSiblingIndex();
    }

    public void CardMove(){
        int card_row_num = 5;
        int most_near_row = -1;
        int most_small_dis = 104;
        int big_num = 0;
        //一番近いカードの判定
        for(int i = 0;i<card_row_num;i++){
            GameObject g = GameObject.Find("CardRow"+(i+1));
            int c_num_in_row = g.transform.childCount;
            int c_num = g.transform.GetChild(c_num_in_row-1).gameObject.GetComponent<Card>().card_num;
            if(card_num>c_num && card_num-c_num<most_small_dis){
                most_small_dis = card_num-c_num;
                most_near_row = i+1;
            }else if(card_num<c_num){
                //全カードより小さい場合の判定
                big_num++;
            }
        }

        if(big_num<5){
            Transform g = GameObject.Find("CardRow"+most_near_row).transform;
            if(g.transform.childCount==5){
                CardCollect(most_near_row-1);
            }else{
                Transform card = g.GetChild(g.transform.childCount-1);
                this.gameObject.transform.SetParent(g);
                this.gameObject.transform.localPosition = new Vector3(
                    card.transform.localPosition.x+100,card.transform.localPosition.y,card.transform.localPosition.z
                );
            }
            hand_card_flg = false;
            GetComponent<Image>().enabled = true;
            GameManager.selected_hand_card = -1;
            GameManager.selected_hand_row_num = -1;
        }else{
            for(int i=0;i<triangle_space_obj.transform.childCount;i++){
                triangle_space_obj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    public void CardCollect(int collect_row_num){
        GameObject selected_row_obj = GameObject.Find("CardSpace").transform.GetChild(collect_row_num).gameObject;
        int collect_card_num = selected_row_obj.transform.childCount;
        int damage = 0;
        for(int i = 0;i<collect_card_num;i++){
            damage += selected_row_obj.transform.GetChild(i).gameObject.GetComponent<Card>().card_num;
            Destroy(selected_row_obj.transform.GetChild(i).gameObject);
        }
        GameManager.life_point -= damage;
        player.GetComponent<Player>().LifeUpdate();
        this.gameObject.transform.SetParent(selected_row_obj.transform);
        this.gameObject.transform.localPosition = new Vector3(0,0,0);

        hand_card_flg = false;
        GetComponent<Image>().enabled = true;
        GameManager.selected_hand_card = -1;
        GameManager.selected_hand_row_num = -1;
    }
}
