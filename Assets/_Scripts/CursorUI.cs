using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [SerializeField] private RectTransform m_Cursor;
    [SerializeField] private Canvas m_Canvas;
    private void LateUpdate()
    {
        Cursor.visible = false;
        m_Cursor.anchoredPosition = Input.mousePosition / m_Canvas.scaleFactor;
    }   


}
