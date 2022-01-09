using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMember : MonoBehaviour
{
    public bool canJoin;
    public string memberName, textToSay;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++){
            if(GameManager.instance.playerStats[i].charName == memberName){
                gameObject.SetActive(!GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canJoin && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove){
            StartCoroutine(AddMemberCo());
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Verifica se o player entrou no Tregger
        if(other.tag == "Player"){
            canJoin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        //Verifica se o Player saiu do Trigger
        if(other.tag == "Player"){
            canJoin = false;
        }
    }

    public IEnumerator AddMemberCo()
    {
        GameManager.instance.addingNewMember = true;
        AudioManager.instance.PlayBgm(6);
        NewMemberUI.instance.ShowMemberMessage(memberName,textToSay);
        yield return new WaitForSeconds(2f);
        NewMemberUI.instance.NotifyNewMEmber(memberName);
        yield return new WaitForSeconds(2f);
        NewMemberUI.instance.Close();
        gameObject.SetActive(false);
        AudioManager.instance.PlayBgm(FindObjectOfType<CameraController>().musicToPlay);
        GameManager.instance.addingNewMember = false;
    }
}
