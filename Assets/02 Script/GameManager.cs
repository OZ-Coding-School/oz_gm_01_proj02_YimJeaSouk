
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardPlacementManger cardPlacement;
    [SerializeField] private CardEffectManager cardEffect;
    [SerializeField] private List<CardSlot> slots;
    [SerializeField] private List<CardButton> Buttons;
    [SerializeField] private int ReversCount;
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private UnityEngine.UI.RawImage Blood;
    [SerializeField] private TMP_Text gameOver;
    [SerializeField] private TMP_Text gameClear;

    //[SerializeField] private bool isGameOver = false;
    //[SerializeField] private bool isGameClear = false;





    private void Start()
    {
        cardPlacement.useLogic();
    }
   
    public void GameOver()
    {
        Blood.gameObject.transform.localScale = Vector3.one;
        Blood.gameObject.SetActive(true);
        Blood.gameObject.transform.DOScale(1.2f,0.05f).OnComplete(()=>
        {
            gameOver.alpha = 0f;
            gameOver.gameObject.SetActive(true);
            gameOver.DOFade(1f, 1f).SetEase(Ease.InCirc);
        });
    }

    public void GameClear()
    {
        gameClear.alpha = 0f;
        gameClear.gameObject.SetActive(true);
        gameClear.DOFade(1f, 1f).SetEase(Ease.InCirc);
    }
    private void ReStartGame()
    {

    }
}
