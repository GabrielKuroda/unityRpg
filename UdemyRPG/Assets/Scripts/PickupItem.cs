using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Se puder pegar, o botão foi precionado e o player está fora de Menu
        if(canPickup && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove){
            //Add o item no inventario
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            //Apaga o Item da Scene
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Verifica se oq entrou foi o Player
        if(other.tag == "Player"){
            canPickup= true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        //Verifica se oq saiu foi o Player
        if(other.tag == "Player"){
            canPickup= false;
        }
    }
}
