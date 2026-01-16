using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CardEffectManager : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private List<GameObject> provisoButtons;
    [SerializeField] private List<provisoButton> buttonscript;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject smorke;
    [SerializeField] private List<RawImage> smorks;
    [SerializeField] private RawImage fullsmork;
    [SerializeField] private CardPlacementManger cardPlacement;

    private CardSlot currentSlot;
    private GameObject currentCardBack;

    public CardSlot useCurrentSlot;
    public GameObject useCurrentCardback;
    


    public void activate(string name)
    {
        Effect(name);
    }

    private void Effect(string effectName)
    { // 각 종류의 카드 효과를 일괄 관리
        switch(effectName)
        {
            case "방탄조끼":
                BulletProof();
                break;
            case "최면가스":
                HypnoticGas();
                break;
            case "아무것도없는방":
                EmptyRoom();
                break;
            case "은열쇠":
                SilverKey();
                break;
            case "살인마의함정":
                KillersTrap();
                break;
            case "작은단서":
                LittleProviso();
                break;
            case "탈출구":
                WayOut();
                break;

        }
    }

    private void BulletProof()
    {
        //다른곳에서 작성되었다. 없어도 되는 메서드 다만 설명용 텍스트가 뜨도록 할 예정이라면 내버려두자.
        Debug.Log("방탄조끼");
    }
    private void HypnoticGas()
    {
        //최면가스 : 기능 자체는 이미 되어있음 여기에 연결만 하면 됨.
        smorke.SetActive(true);
        foreach (RawImage smorges in smorks)
        {
            smorges.DOFade(1f, 2f);
        }
        StartCoroutine(waitImage());
        Debug.Log("최면가스");
    }
    private IEnumerator waitImage()
    {
        fullsmork.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        fullsmork.DOFade(1f, 1f).OnComplete(() =>
        {
            cardPlacement.useRePlaceMent();
            fullsmork.DOFade(0f, 1f);

        });
        yield return new WaitForSeconds(2f);
        foreach (RawImage smorges in smorks)
        {
            smorges.DOFade(0f, 1f);
        }
        yield return new WaitForSeconds(2f);
        smorke.SetActive(false);
        fullsmork.gameObject.SetActive(false);

    }
    private void EmptyRoom()
    {
        //아무것도없는방 - 확인 후 뒤집히도록 작성해보자. 
        // 이 기능은 여기서 사용하지 않음- 필요 없는 메서드 임으로, 없애도록 하자. 

        Debug.Log("아무것도없는방.");
    }
    private void KillersTrap()
    {
        gameManager.UseGameover();
        Debug.Log("살인마의 함정");
    }
    private void SilverKey()
    {
        //은열쇠 - 사실상 지금 기능을 다 하고있다. 필요 없는 메서드 다만 설명용 텍스트가 뜨도록 할 예정이라면 내버려두자.
        Debug.Log("은열쇠");
    }
    private void LittleProviso()
    {
        shild.gameObject.SetActive(true);

        StartCoroutine(provisoActive());

        Debug.Log("작은단서");
    }
    private void WayOut()
    {
        //탈출구 게임 클리어 기능 - 이미 3개의 은열쇠가 모여야 보이는 기믹 여기서 처리하지 않음.다만 설명용 텍스트가 뜨도록 할 예정이라면 내버려두자.
        Debug.Log("탈출구");
    }

    public void UseprovisoDeactive()
    {
        provisoDeactive();
    }
    private void provisoDeactive()
    {
        foreach (GameObject buttons in provisoButtons)
        {
            buttons.GetComponent<Image>().color = new Color(0.6726527f, 1, 0.5330188f, 0.5f);
            buttons.SetActive(false);
        }
    }
    public void UseprovisoNotSee()
    {
        provisoNotSee();
    }
    private void provisoNotSee()
    {
        foreach (GameObject buttons in provisoButtons)
        {
            buttons.GetComponent<Image>().color = new Color(0.6726527f, 1, 0.5330188f, 0f);
        }
    }
    private IEnumerator provisoActive()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < buttonscript.Count; i++)
        {
            buttonscript[i].UseactiveButton();
        }
        
    }
}
