using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TJSwitch : MonoBehaviour
{
    public bool isOn;                                     // TODO: 스위치 온/오프 상태
    [Range(0, 3)]
    public float moveDuration = 3f;                       // TODO: 스위치 이동 애니메이션 시간

    const float totalHandleMoveLength = 72f;
    const float halfMoveLenght = totalHandleMoveLength / 2;

    Image handleImage;                                   // TODO: 스위치 핸들 이미지
    Image backgroundImage;                               // TODO: 스위치 배경 이미지
    RectTransform handleRectTransform;                   // TODO: 스위치 핸들RectTransform
    void Start()
    {
        GameObject handleObject = transform.Find("Handle").gameObject;

        handleRectTransform = handleObject.GetComponent<RectTransform>();

        isOn = true;

        if (isOn)
        
            handleRectTransform.anchoredPosition = new Vector2(halfMoveLenght, 0);
            else
                handleRectTransform.anchoredPosition = new Vector2(-halfMoveLenght, 0);
        
        void OnClickSwitch()
        {
            isOn = !isOn;

            Vector2 fromPosition = handleRectTransform.anchoredPosition;
            Vector2 toPosition = (isOn) ? new Vector2(halfMoveLenght, 0) : new Vector2(-halfMoveLenght, 0);
            Vector2 distance = toPosition - fromPosition;

            float ratio = Mathf.Abs(distance.x) / totalHandleMoveLength;
            float duration = moveDuration * ratio;
           
            StartCoroutine(moveHandle(fromPosition,toPosition, duration));
            
        }
       IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
        {
            float currentTime = 0f;
          while(currentTime < duration)
            {
                Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, duration);
                handleRectTransform.anchoredPosition = newPosition;

                currentTime += Time.deltaTime;
                yield return null;
            }

        }
        ///1.터치 시 핸들의 위치를 바꿔주는 동작(함수)

        //2.터치 시 스위치의 배경 색상을 바꿔주는 동장(함수)
    }
}