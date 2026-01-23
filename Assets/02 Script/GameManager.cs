
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private CardPlacementManger cardPlacement;
    [SerializeField] private CardEffectManager cardEffect;
    [SerializeField] private SoundManager Sound;
    [SerializeField] private List<CardSlot> slots;
    [SerializeField] private List<CardButton> Buttons;
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private UnityEngine.UI.RawImage Blood;
    [SerializeField] private UnityEngine.UI.RawImage flesh;
    [SerializeField] private TMP_Text gameOver;
    [SerializeField] private TMP_Text gameClear;
    [SerializeField] private UnityEngine.UI.Image gameClearBack;
    [SerializeField] private TMP_Text restartButtonText;
    [SerializeField] private UnityEngine.UI.Button restartButton;

    [Header("엔딩연출용")]
    [SerializeField] private List<UnityEngine.UI.Image> openDoorImages;
    [SerializeField] private GameObject openDoors;
    [SerializeField] private UnityEngine.UI.Image openDoorback;
    

    //[SerializeField] private bool isGameOver = false;
    //[SerializeField] private bool isGameClear = false;

    private bool bulletProof = false;




    private void Start()
    {
        cardPlacement.useLogic();
    }
   
    public void UseRestart()
    {
        ReStartGame();
    }

    public void UseGameover()
    {
        StartCoroutine(GameoverSlow());
    }
    private IEnumerator GameoverSlow()
    {
        yield return new WaitForSeconds(1.3f);
        GameOver();
    }
    private void GameOver()
    {
        BulletProofUpdate(); // 방탄 조끼 작동여부 업데이트
        Sound.UsePlaySound(4);
        if (bulletProof == false)
        {
            // 작동중이 아니라면, 게임오버 절차
            flesh.gameObject.SetActive(true);
            flesh.gameObject.transform.DOScale(1.2f, 0.05f).OnComplete(() =>
            {
                flesh.gameObject.SetActive(false);
                Blood.gameObject.transform.localScale = Vector3.one;
                Blood.gameObject.SetActive(true);
                Blood.gameObject.transform.DOScale(1.2f, 0.05f).OnComplete(() =>
                {
                   StartCoroutine(LateSound());
                });
                
            });
            
            
        }
        else if(bulletProof == true)
        {
            //방탄조끼 작동중 이라면 게임오버 안됨.
            Sound.UsePlaySound(5);
            bulletProof = false;
            cardPlacement.UesOperationBulletproof();
            
        }
    }
    private IEnumerator LateSound()
    {
        yield return new WaitForSeconds(1f);
        gameOver.alpha = 0f;
        gameOver.gameObject.SetActive(true);
        gameOver.DOFade(1f, 1f).SetEase(Ease.InCirc).OnComplete(() =>
        { 
            restartButton.gameObject.SetActive(true);
            restartButton.image.DOFade(1f,1.0f);
            restartButtonText.DOFade(1f,1.0f);
        });
        Sound.UsePlaySound(9);
    }
    public void UseGameclaer()
    {
        StartCoroutine(OpenDoor());
    }
    private void GameClear() 
    {
        gameClear.alpha = 0f;
        Sound.UsePlaySound(8);
        gameClearBack.color = new UnityEngine.Color(1, 1, 1, 0);
        gameClear.gameObject.SetActive(true);
        gameClearBack.gameObject.SetActive(true);
        gameClear.DOFade(1f, 1f).SetEase(Ease.InCirc);
        gameClearBack.DOFade(1f, 1f).OnComplete(()=> 
        {
        restartButton.gameObject.SetActive(true);
        restartButton.image.DOFade(1f, 1f);
        restartButtonText.DOFade(1f, 1f);
        });

    }
    private void ReStartGame()
    {
        gameClear.gameObject.SetActive(false);
        gameClearBack.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        Blood.gameObject.SetActive(false);
        cardPlacement.UseRestartingReady();
        openDoors.SetActive(false);
        restartButton.gameObject.SetActive(false);
        openDoorImages[2].color = new UnityEngine.Color(1, 1, 1, 0);
        openDoorback.color = new UnityEngine.Color(1, 1, 1, 0);
        restartButton.image.color = new UnityEngine.Color(1,1,1,0);
        restartButtonText.alpha = 0;
    }

    private void BulletProofUpdate()
    {
        cardPlacement.ApplieBulletproof(); // 방탄조끼 작동여부 내보내기 위한 업데이트 작업
        bulletProof = cardPlacement.useBulletProof; // 방탄조끼 여부를 bulletProof 에 복사해넣기
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1.3f);
        openDoors.SetActive(true);
        openDoorback.DOFade(1f, 0.3f);
        Sound.UsePlaySound(7);
        openDoorImages[0].DOFade(1f, 0.5f).OnComplete(()=>
        {
            openDoorImages[1].color = new UnityEngine.Color(1, 1, 1, 1);
            openDoorImages[2].color = new UnityEngine.Color(1, 1, 1, 1);
        });
        
        yield return new WaitForSeconds(1f);
        Sound.UsePlaySound(6);
        openDoorImages[0].DOFade(0f, 0.5f);
        yield return new WaitForSeconds(1.2f);
        openDoorImages[1].DOFade(0f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        GameClear();
    }

}
