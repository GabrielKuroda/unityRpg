using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public SpriteRenderer theChest;
    public string itemInside;
    public Sprite openSprite;
    public bool canOpen;
    public string chestName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsEmpty(chestName)){
            theChest.sprite = openSprite;
        }
        if(canOpen && Input.GetButtonDown("Fire1")){
            if(!GameManager.instance.IsEmpty(chestName)){
                OpenChest();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
        }
    }

    private void OpenChest(){
        theChest.sprite = openSprite;
        ChestReward.instance.OpenChest(itemInside,chestName);
    }

}
