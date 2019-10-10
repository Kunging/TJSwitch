using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TJSwitch : MonoBehaviour
{
    public bool isOn;                                     // TODO: 스위치 온/오프 상태
    [Range(0, 3)]
    public float moveDuration = 3f;                       // TODO: 스위치 이동 애니메이션 시간

    //Color
    public Color handleColor = Color.white;
    public Color offBackgroundColor =Color.green;
    public Color onBackgroundColor= Color.blue;

    const float totalHandleMoveLength = 72f;
    const float halfMoveLenght = totalHandleMoveLength / 2;

    Image handleImage;                                   // TODO: 스위치 핸들 이미지
    Image backgroundImage;                               // TODO: 스위치 배경 이미지
    RectTransform handleRectTransform;                   // TODO: 스위치 핸들RectTransform

    // Coroutine
    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;            // 배경색 변경 코루틴
    void Start()
    {
        GameObject handleObject = transform.Find("Handle").gameObject;

        handleRectTransform = handleObject.GetComponent<RectTransform>();

        //Handle Image
        handleImage = handleObject.GetComponent<Image>();
        handleImage.color = handleColor;

        //Background Image
        backgroundImage = GetComponent<Image>();
        backgroundImage.color = offBackgroundColor;

        if (isOn)

            handleRectTransform.anchoredPosition = new Vector2(halfMoveLenght, 0);
        else
            handleRectTransform.anchoredPosition = new Vector2(-halfMoveLenght, 0);
    }
    
       public void OnClickSwitch()
        {
            isOn = !isOn;

            Vector2 fromPosition = handleRectTransform.anchoredPosition;
            Vector2 toPosition = (isOn) ? new Vector2(halfMoveLenght, 0) : new Vector2(-halfMoveLenght, 0);
            Vector2 distance = toPosition - fromPosition;

            float ratio = Mathf.Abs(distance.x) / totalHandleMoveLength;
            float duration = moveDuration * ratio;

        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
           
            MoveCoroutine(moveHandle(fromPosition,toPosition, duration));
        //백그라운드 컬러 체인지 코루틴
        if(changeBackgroundColorCoroutine !=null)
        {
            StopCoroutine(changeBackgroundColorCoroutine);
            changeBackgroundColorCoroutine = null;
        }
        changeBackgroundColorCoroutine
            = StartCoroutine(changeBackgroundColor(fromColor, toColor, duration));
        


        }
    /// <summary>
    /// 스위치 배경색 변경 함수
    /// </summary>
    /// <param name="fromPosition"></param>
    /// <param name="toPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
       IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
        {
            float currentTime = 0f;
          while(currentTime < duration)
            {
                float t = currentTime / duration;
                Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
                handleRectTransform.anchoredPosition = newPosition;

                currentTime += Time.deltaTime;
                yield return null;
            }

        }
    ///1.터치 시 핸들의 위치를 바꿔주는 동작(함수)

    //2.터치 시 스위치의 배경 색상을 바꿔주는 동장(함수)
    
    IEnumerator changeBackgroundColor(Color fromColor, Color toColor, float duration)
    {
        //TODO: 
        //1.정해진 시간동안 색상을 변경시켜야 한다.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            Color newColor = Color.Lerp(fromColor, toColor, t);

            backgroundImage.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }

    }
    }
