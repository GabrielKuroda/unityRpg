using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] potentialBattles;
    private bool inArea;
    public bool activateOnEnter, activateOnStay, activateOnExit;
    public float timeBetweenBattles;
    private float betweenBattleCounter;
    public bool deactivateAfterStarting;
    public bool cannotFlee;
    public bool shouldCompleteQuest;
    public string questToComplete;

    // Start is called before the first frame update
    void Start()
    {
        //Pega um tempo random entre batalhas
        betweenBattleCounter = Random.Range(timeBetweenBattles*.5f, timeBetweenBattles*1.5f);
        if(questToComplete != null && QuestManager.instance != null){
            for(int i = 0; i < QuestManager.instance.questMarkerNames.Length; i++){
                if(QuestManager.instance.questMarkerNames[i] == questToComplete){
                    if(QuestManager.instance.questMarkersComplete[i]){
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se está na Zona de batalha
        if(inArea && PlayerController.instance.canMove){
            //Verifica se o Player está andando
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
                //Começa acontagem
                betweenBattleCounter -= Time.deltaTime;
            }

            //Verifica se a contagem chegou a 0
            if(betweenBattleCounter <= 0)
            {
                //Set no intervalo de tempo
                betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
                //Começa a batalha
                StartCoroutine(StartBattleCo());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //Verifica se a batlaha ocorre ao entrar na area
            if (activateOnEnter)
            {
                //Começa batalha
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Verifica se a batalha ocorre ao sair
            if (activateOnExit)
            {
                //Começa batalha
                StartCoroutine(StartBattleCo());
            }
            else
            {
                inArea = false;
            }
        }
    }

    public IEnumerator StartBattleCo()
    {
        //Realiza a animação de troca de tela
        UIFade.instance.FadeToBlack();
        //Indica estar em batalha
        GameManager.instance.battleActive = true;
        //Pega uma batalha aleatoria
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        //Set os rewards
        BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.rewardXp = potentialBattles[selectedBattle].rewardExp;
        BattleManager.instance.rewardGold = potentialBattles[selectedBattle].rewardGold;
        //Aguarda 1.5s
        yield return new WaitForSeconds(1.5f);
        //Inicia a Batalha
        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies, cannotFlee);
        //Retorna a tela
        UIFade.instance.FadeFromBlack();
        //Verifica se não havera mais batalhas depois dessa
        if(deactivateAfterStarting)
        {
            //Desabilita a zona de batlaha
            gameObject.SetActive(false);
        }
        //Envia infos de quest
        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = questToComplete;
    }
}
