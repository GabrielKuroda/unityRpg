using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{

    public bool isPlayer;
    public string[] movesAvailable;
    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, strength, defence,wpnPower, armrPower;
    public bool hasDied;
    public SpriteRenderer theSprite;
    public Sprite deadSprite;
    public Sprite aliveSprite;

    private bool shouldFade;
    public float fadSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se deve desaparecer
        if(shouldFade){
            //Modifica Color para desaparecer
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadSpeed* Time.deltaTime),Mathf.MoveTowards(theSprite.color.g, 0f, fadSpeed* Time.deltaTime),Mathf.MoveTowards(theSprite.color.b, 0f, fadSpeed* Time.deltaTime),Mathf.MoveTowards(theSprite.color.a, 0f, fadSpeed* Time.deltaTime));
            if(theSprite.color.a == 0){
                //Desativa o Char
                gameObject.SetActive(false);
            }
        }
    }

    public void EnemyFade(){
        shouldFade = true;
    }
}
