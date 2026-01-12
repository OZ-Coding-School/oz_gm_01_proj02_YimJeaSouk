using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
  
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
        //방탄조끼 : 게임 오버관련 임으로 살인마의 함정 이후작성
        Debug.Log("방탄조끼");
    }
    private void HypnoticGas()
    {
        //최면가스 : 기능 자체는 이미 되어있음.
        Debug.Log("최면가스");
    }
    private void EmptyRoom()
    {
        //아무것도없는방 - 확인 후 뒤집히도록 작성해보자. 

        Debug.Log("아무것도없는방.");
    }
    private void KillersTrap()
    {
        // 살인마의함정 게임오버 기능 - 게임매니저에 게임 오버 기능 넣고 호출
        Debug.Log("살인마의 함정");
    }
    private void SilverKey()
    {
        //은열쇠 - 사실상 지금 기능을 다 하고있다.
        Debug.Log("은열쇠");
    }
    private void LittleProviso()
    {
        //작은단서 - 어떻게 구현할까?
        //얻는 즉시 뒤집히지 않은 모든 버튼에 이팩트 주기
        //실드 위에 다른 버튼을 심어 뒤에 버튼 눌리지 않게 하기.
        //그상태로 원하는 버튼 클릭할떄까지 대기
        //버튼을 클릭하면 버튼 비활성화/ 해당 슬롯의 카드는 뒤집힘 없이 잠시동안, 반투명하게 보이게 하고 보이는것 끝날때, 쉴드 비활성화
        // 마지막 처리로, 해당 패널 데이터 '아무것도 없는 방' 으로 바꾸기.(섞일땐 따로 저장된 리스트에서 긁어오니 상관 X)


        Debug.Log("작은단서");
    }
    private void WayOut()
    {
        //탈출구 게임 클리어 기능 - 이미 3개의 은열쇠가 모여야 보이는 기믹 있음. 여기에 넣으면 될것이다.
        Debug.Log("탈출구");
    }
}
