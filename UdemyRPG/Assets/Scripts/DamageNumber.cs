using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{

    public Text damageText;
    public float lifetime = 1f;
    public float moveSpeed = 1f;
    public float placementJitter = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroi o Objeto
        Destroy(gameObject,lifetime);
        //Altera a posição do Objeto
        transform.position += new Vector3(0f,moveSpeed * Time.deltaTime,0f);
    }

    public void SetDamage(int damageAmount){
        //Set no valor do dano
        damageText.text = damageAmount.ToString();
        //Set na posição 
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }
}
