using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;

    private void Awake()
    {
        //Xử lý tai thỏ
        RectTransform rect = GetComponent<RectTransform>();
        float ration = (float)Screen.width/(float)Screen.height;
        if (ration > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;

            leftBottom.y = 0f;
            rightTop.y = -100f;

            rect.offsetMin = leftBottom;
            rect.offsetMax = rightTop;
        }
    }
    //Hàm gọi trước khi canvas được acitve lên
    public virtual void Setup()
    {

    }

    //Gọi sau khi được active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //Đóng canvas trong vòng bao nhiêu giây
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    //Đóng canvas một cách trực tiếp
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
}
