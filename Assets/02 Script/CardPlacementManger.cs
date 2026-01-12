using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO.Compression;

public class CardPlacementManger : MonoBehaviour
{
    [Header("스크립트연결")]
    [SerializeField] private List<GameObject> slots;  //카드슬롯
    [SerializeField] private GameManager gameManager; //게임매니저
    [SerializeField] private List<CardButton> Buttons; // 버튼 상호작용
    

    [Header("카드덱관련")]
    [SerializeField] private List<string> usingCard;  //사용할 카드덱
    [SerializeField] private List<string> rememberCard; // 제외시킨 은열쇠 기억용
    [SerializeField] private GameObject currentCard; //현재 사용할 카드
    [SerializeField] private Transform parent; //카드이미지 생성할 부모
    [SerializeField] private List<GameObject> cardimages;  //카드 이미지 프리펩

    [Header("카드이미지덱")] // 미리 카드 이미지를 활성화 시켜놓을 덱 = 분류로 나눠 찾기 쉽도록함.
    [SerializeField] private List<GameObject> bulletproofCard; //0 
    [SerializeField] private List<GameObject> hypnoticGasCard; //1
    [SerializeField] private List<GameObject> killersTrapCard; //2
    [SerializeField] private List<GameObject> sillverKeyCard;  //3
    [SerializeField] private List<GameObject> LittleProvisoCard;//4
    [SerializeField] private List<GameObject> EmptyRoomCard;    //5
    [SerializeField] private List<GameObject> ExitCard;         //6
    [SerializeField] private int cardNum; //카드 고유넘버

    [Header("은열쇠 관련")]
    [SerializeField] private int keyNum = 15;    //제외시킬 키의 리스트번호.
    [SerializeField] private int keyCount = 0;   //현재 얻은키의 갯수
    [SerializeField] private List<GameObject> Keyslots;

    [Header("넘겨 주기,받기용")]
    public int useCardNum; // 카드 고유넘버 넘겨받기용
    public int usekeyCount = 0;  //열쇠 갯수 넘겨주기용
    public CardSlot currentSlot; //슬롯 정보 넘겨받기용
    public CardButton currentButton; //버튼 정보 넘겨받기용

    [Header("버튼막는용 투명 패널")] 
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;

    private bool reversCardClose = false; // 카드를 다시 뒤집는지 여부 

    private void Awake()
    {   // usingCard,remembercard 리스트에 16장의 카드를 추가,이미지오브젝트 생성
        usingCard = new List<string>();
        rememberCard = new List<string>();
        RegistrationCard(usingCard);
        RegistrationCard(rememberCard);
        spawnCard();

    }

    public void UseingKeycount()
    {   //제외 카드수 넘겨주기용 .
        usekeyCount = keyCount;
    }
   
    public void useLogic()
    { //게임매니저로 시작시 카드 배치 넘겨주기용 
        Placement();
    }
    private void RegistrationCard(List<string>Card)
    {
        //0,1 살인마의 함정
        for (int i = 0; i <= 1; i++)
        {
            Card.Add("살인마의함정");
        }
        //2,3,4,5 최면가스
        for (int i = 0; i <= 3; i++)
        {
            Card.Add("최면가스");
        }
        // 6,7,8 작은단서
        for (int i = 0; i <= 2; i++)
        {
            Card.Add("작은단서");
        }
        // 9,10 아무것도없는방
        for (int i = 0; i <= 1; i++)
        {
            Card.Add("아무것도없는방");
        }
        Card.Add("방탄조끼");
        Card.Add("탈출구");
        //13,14,15 은열쇠
        for (int i = 0; i <= 2; i++)
        {
            Card.Add("은열쇠");
        }
    }
    private void spawnCard()
    {
        //위 RegistrationCard 은 다른 매서드에서도 사용됨으로, 스폰용은 따로 작성
        //0,1 살인마의 함정
        for (int i = 0; i <= 1; i++)
        {
            spawn(cardimages[4], killersTrapCard);
        }
        //2,3,4,5 최면가스
        for (int i = 0; i <= 3; i++)
        {
            spawn(cardimages[1],hypnoticGasCard);
        }
        // 6,7,8 작은단서
        for (int i = 0; i <= 2; i++)
        {
            spawn(cardimages[5], LittleProvisoCard);
        }
        // 9,10 아무것도없는방
        for (int i = 0; i <= 2; i++)
        {
            spawn(cardimages[2], EmptyRoomCard);
        }
        spawn(cardimages[0],bulletproofCard);
        spawn(cardimages[6],ExitCard);

        //13,14,15 은열쇠
        for (int i = 0; i <= 2; i++)
        {
            spawn(cardimages[3], sillverKeyCard);
        }
    }

    private void spawn(GameObject Prefabs, List<GameObject> Dack)
    { //화면밖에 소환 하고, 비활성화 시키기
        currentCard = Instantiate(Prefabs, parent);
        currentCard.transform.localPosition = Vector3.zero;
        Dack.Add(currentCard);
        currentCard.SetActive(false); 
    }

    private void OutCardNum()
    { //넘겨받은 숫자 카드넘에 수정되지 않도록 저장해두기
        cardNum = useCardNum;
    }

    public void CallingMoveCardImage()
    { //슬롯에서 넘겨받은 데이터로 진행하기위한 매서드
        MoveCardImage();
    }
    private void MoveCardImage()
    { // 슬롯에서 넘겨받은 0~6의 int 데이터에 따른 카드 이미지 처리
        OutCardNum();
        switch (cardNum)
        {
            case 0:
                //bulletproofCard
                SwichControll(bulletproofCard);
                break;
            case 1:
                //hypnoticGasCard
                SwichControll(hypnoticGasCard);
                break;
            case 2:
                //killersTrapCard
                SwichControll(killersTrapCard);
                break;
            case 3:
                //sillverKeyCard
                keyCount++;
                GameObject keyCard = SwichControll(sillverKeyCard);
                if (keyCard != null)
                {
                    ExcludingKey();
                    Cueingslot(keyCard);
                }
                break;
            case 4:
                //LittleProvisoCard
                SwichControll(LittleProvisoCard);
                break;
            case 5:
                //EmptyRoomCard
                 reversCardClose = true;
                SwichControll(EmptyRoomCard);
            
                break;
            case 6:
                //ExitCard
                SwichControll(ExitCard);
                break;

        }
    }
    private GameObject SwichControll(List<GameObject> cardList)
    {
       
        foreach (GameObject card in cardList)
        {
            if (!card.activeSelf)
            {
                
                card.transform.SetParent(currentSlot.transform, true);
                
                StartCoroutine(ShowCard(card));
                if (reversCardClose == true)
                {
                    
                    StartCoroutine(CloseCard(card));
                   
                }
                return card;

            }
            
        }
        return null;
    }
    private IEnumerator ShowCard(GameObject card)
    {
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(0, 1, 1);
        card.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        card.transform.DOScaleX(1, 0.5f).OnComplete(() =>
        {
            shild.gameObject.SetActive(false);
        });
        
    }
  
    private IEnumerator CloseCard(GameObject card)
    {
        
        shild2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        card.transform.DOScaleX(0, 0.5f).OnComplete(() =>
        {
            card.gameObject.SetActive(false);
            currentButton.UseRevers();
        });
        reversCardClose = false;
    }

    private void Cueingslot(GameObject card)
    {
        for (int i = 0; i < Keyslots.Count; i++)
        {
            if (Keyslots[i].transform.childCount == 0)
            {
                // 열쇠슬롯의 자식 오브젝트의 갯수가 0이라면, SlotMove를 실행한다.
                //해당 번호수의 키슬롯으로 이동 시킨다.
                StartCoroutine(MovingSlot(card.transform, Keyslots[i].transform));
                return;
            }
        }

        Debug.Log("열쇠 슬롯이 없습니다. 비정상적인 열쇠 갯수입니다.");

    }

    private IEnumerator MovingSlot(Transform target, Transform newParent)
    {

        shild2.gameObject.SetActive(true); // 패널로 가려서 버튼이 안눌리게
        Vector3 wordposition = target.position;

        yield return new WaitForSeconds(1f);

        target.DOMove(newParent.position, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            target.SetParent(newParent, true);
            shild2.gameObject.SetActive(false); //끝나고나서 패널 치워서 버튼 눌리게
        });


    }

    private void shuffle<T>(List<T> list)
    {
        for(int i=0; i<list.Count; i++)
        {
            int rand= UnityEngine.Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

   private void Placement() 
    {
        // 16장의 카드 셔플
        shuffle(usingCard);

        // 각 뒤집혀진 카드에 데이터를 배치하기 
        for (int i = 0; i<usingCard.Count;i++)
        {
            CardSlot slot = slots[i].GetComponent<CardSlot>();
            slot.Indata(usingCard[i]);
        }

    }

    
    private void ExcludingKey()
    {
        // RememberCard에 찾은 은열쇠를 제외하기.
        if(keyNum == 15)
        {
            rememberCard.Remove(rememberCard[keyNum]);
            keyNum--;
        }
        else if (keyNum == 14)
        {
            rememberCard.Remove(rememberCard[keyNum]);
            keyNum--;
        }
        else if (keyNum == 13)
        {
            rememberCard.Remove(rememberCard[keyNum]);
            keyNum--;
        }

    }

    public void useRePlaceMent()
    {// 이 매서드를 이팩트 매니저의  최면가스에 넣으면 된다. (현재는 테스트를 위해 버튼으로 컨트롤)
        RePlaceMent();
    }
    private void RePlaceMent()
    {
        //usingCard에 은열쇠가 제외된 rememberCard를 넣음
        usingCard = new List<string>(rememberCard);
         
        // 모든 카드를 뒤집기 - 비활성화된 카드뒷면 버튼의 스케일 복원 및 활성화
        AllReverse();
        // 셔플 후 각 뒤집혀진 카드에 데이터를 배치하기 
        Placement();
    }
    private void AllReverse()
    { 
        //모든 버튼 비활성화
        foreach (CardButton Buttons in Buttons)
        {
            Buttons.gameObject.SetActive(false);
        }

        //모든 슬롯의 카드 비활성화
        foreach (GameObject Slot in slots)
        {
            foreach (Transform child in Slot.transform)
            {
                child.gameObject.SetActive(false);
                child.SetParent(parent, false);
                child.transform.localPosition = Vector3.zero;
            }
        }
        // 현재 찾은 은열쇠을 제외한 나머지 덱의 숫자만큼 뒤집힌 카드 활성화
        for (int i = 0; i < usingCard.Count; i++)
        {
            Buttons[i].gameObject.transform.localScale = Vector3.one;
            Buttons[i].gameObject.SetActive(true);
        }
    }
}