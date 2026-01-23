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
    [SerializeField] private SoundManager sound;
    [SerializeField] private List<CardButton> Buttons; // 버튼 상호작용


    [Header("카드덱관련")]
    [SerializeField] private List<string> usingCard;  //사용할 카드덱
    [SerializeField] private List<string> rememberCard; // 제외시킨 은열쇠 기억용
    [SerializeField] private List<string> restartingCard; // 재시작시 사용할 원본 카드덱
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
    [SerializeField] private List<GameObject> BrokenbulletproofCard; //7
    [SerializeField] private int cardNum; //카드 고유넘버

    [Header("은열쇠 관련")]
    [SerializeField] private int keyNum = 15;    //제외시킬 키의 리스트번호.
    [SerializeField] private int keyCount = 0;   //현재 얻은키의 갯수
    [SerializeField] private List<GameObject> Keyslots;

    [Header("넘겨 주기,받기용")]
    
    public int usekeyCount = 0;  //열쇠 갯수 넘겨주기용
    public CardSlot currentSlot; //슬롯 정보 넘겨받기용
    public CardSlot currentbulletProofSlot; // 방탄조끼가 어느슬롯인지 정보 넘겨받기용
    public CardButton currentButton; //버튼 정보 넘겨받기용
    public bool useBulletProof = false; // 방탄조끼 활성화 여부 넘겨주고받기용

    [Header("버튼막는용 투명 패널")]
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;

    [Header("작은단서")]
    [SerializeField] private GameObject activeProvisoButton;
    [SerializeField] private List<CardButton> ProvisoButtons;
    [SerializeField] private UnityEngine.UI.RawImage flesh;


    private bool reversCardClose = false; // 카드를 다시 뒤집는지 여부 
    [SerializeField] private bool bulletProof = false;
    private CardSlot bulletProofSlot;

    private void Awake()
    {   // usingCard,remembercard 리스트에 16장의 카드를 추가,이미지오브젝트 생성
        usingCard = new List<string>();
        rememberCard = new List<string>();
        restartingCard = new List<string>();
        RegistrationCard(usingCard);
        RegistrationCard(rememberCard);
        RegistrationCard(restartingCard);

        spawnCard();

    }
    public void UseRestartingReady()
    { //게임 매니저로 보내주기
        RestartingReady();
    }
    private void RestartingReady()
    { //게임 재시작을 위해  변수와 리스트 초기화.
        usingCard = new List<string>(restartingCard);
        rememberCard = new List<string>(restartingCard);

        useBulletProof = false;
        keyNum = 15;
        keyCount = 0;
        foreach (GameObject slot in Keyslots)
        {
            foreach (Transform child in slot.transform)
            {
                child.gameObject.SetActive(false);
                child.SetParent(parent, false);
                child.localPosition = Vector3.zero;
            }
        }
        AllReverse();
        Placement();
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

        spawn(cardimages[7],BrokenbulletproofCard);
    }

    private void spawn(GameObject Prefabs, List<GameObject> Dack)
    { //화면밖에 소환 하고, 비활성화 시키기
        currentCard = Instantiate(Prefabs, parent);
        currentCard.transform.localPosition = Vector3.zero;
        Dack.Add(currentCard);
        currentCard.SetActive(false); 
    }

   

    public void CallingMoveCardImage(int cardNum)
    { //슬롯에서 넘겨받은 데이터로 진행하기위한 매서드
        MoveCardImage(cardNum);
    }
    private void MoveCardImage(int cardNum)
    { // 슬롯에서 넘겨받은 0~6의 int 데이터에 따른 카드 이미지 처리
        sound.UsePlaySound(0);
        switch (cardNum)
        {
            case 0:
                //bulletproofCard
                bulletProof = true;
                ApplieBulletproof();
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
                gameManager.UseGameclaer();
                break;

        }
    }
    private GameObject SwichControll(List<GameObject> cardList)
    { // 소환된 이미지오브젝트을 활성화 해서 불러오고, 애니메이션 작동.
       
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
        sound.UsePlaySound(0);
        card.transform.DOScaleX(0, 0.5f).OnComplete(() =>
        {
            card.gameObject.SetActive(false);
            card.transform.localScale = new Vector3(1, 1, 1);
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
        sound.UsePlaySound(3);
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
            slot.Useindata(usingCard[i]);
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
        // 부서진방탄 카드 비활성화 풀로 돌려주기
        foreach (GameObject card in BrokenbulletproofCard)
        {
            card.SetActive(false);
            card.transform.SetParent(parent, false);
            card.transform.localPosition = Vector3.zero;
        }
        // 현재 찾은 은열쇠을 제외한 나머지 덱의 숫자만큼 뒤집힌 카드 활성화
        for (int i = 0; i < usingCard.Count; i++)
        {
            Buttons[i].gameObject.transform.localScale = Vector3.one;
            Buttons[i].gameObject.SetActive(true);
        }

        bulletProof = false; // 만약 방탄조끼 켜져있다면, 비활성화
        ApplieBulletproof(); // 만약을 위해 방탄조끼 활성화 여부 내보내기용 전역변수에 복사 메서드 호출


    }
    public void ApplieBulletproof()
    { // 방탄조끼 작동여부 내보내기 위한 업데이트 작업
        useBulletProof = bulletProof;
    }

    public void UesOperationBulletproof()
    { // 방탄조끼 뒷처리 바깥에서 호출용
        OperationBulletproof();
    }

    private void OperationBulletproof()
    {// 방탄조끼 뒷처리용
        bulletProof = false; // 비활성화
        ApplieBulletproof(); // 만약을 위해 방탄조끼 활성화 여부 내보내기용 전역변수에 복사 메서드 호출
        //방탄조끼 위치의 슬롯 찾기.
        bulletProofSlot = currentbulletProofSlot; //방탄조끼위치 잡기
        //방탄조끼 이미지 비활성화
        foreach (Transform child in bulletProofSlot.transform)
        {
            child.gameObject.SetActive(false);
        }
        //깨진 방탄조끼 이미지 활성화 하여, 해당 슬롯 위치로 보내기.
        Callingimageprefads(BrokenbulletproofCard,bulletProofSlot);

    }
    private void Callingimageprefads(List<GameObject> cardList,CardSlot targetSlot)
    { //카드 불러온 후 잠깐 커졌다 작아짐.
        foreach (GameObject card in cardList)
        {
            if (!card.activeSelf)
            {
                card.transform.SetParent(targetSlot.transform, true);
                card.transform.localPosition = Vector3.zero;
                card.gameObject.SetActive(true);

                flesh.gameObject.SetActive(true);
                flesh.gameObject.transform.DOScale(1.2f, 0.05f).OnComplete(() =>
                {
                    flesh.gameObject.SetActive(false);
                    card.gameObject.transform.DOScale(2f, 0.05f).OnComplete(() =>
                    {
                        card.gameObject.transform.DOScale(1.0f, 0.05f);
                    });
                });
                return;

            }
        }
    }

    
    
    public void UseLittelProviso(int cardNum)
    { //슬롯에서 넘겨받은 데이터로 진행하기위한 매서드
        LittelProviso(cardNum);
    }
    private void LittelProviso(int cardNum)
    { // 슬롯에서 넘겨받은 0~6의 int 데이터에 따른 카드 이미지 처리

        switch (cardNum)
        {
            case 0:

                callingProviso(bulletproofCard);

                break;
            case 1:

                callingProviso(hypnoticGasCard);
                break;
            case 2:

                callingProviso(killersTrapCard);
                break;
            case 3:

                callingProviso(sillverKeyCard);
                
                break;
            case 4:
               
                callingProviso(LittleProvisoCard);
                break;
            case 5:
              
                callingProviso(EmptyRoomCard);

                break;
            case 6:
                
                callingProviso(ExitCard);
                break;

        }
    }

    private GameObject callingProviso(List<GameObject> cardList)
    { // 소환된 이미지오브젝트을 활성화 해서 불러오고, 애니메이션 작동.

        foreach (GameObject card in cardList)
        {
            if (!card.activeSelf)
            {

                card.transform.SetParent(currentSlot.transform, true);

                StartCoroutine(Showproviso(card));
               
                return card;

            }

        }
        return null;
    }
    private IEnumerator Showproviso(GameObject card)
    {
        
        card.transform.localPosition = Vector3.zero;
        card.SetActive(true);
        yield return new WaitForSeconds(4f);
        card.gameObject.SetActive(false);
        shild2.gameObject.SetActive(false);

    }

}