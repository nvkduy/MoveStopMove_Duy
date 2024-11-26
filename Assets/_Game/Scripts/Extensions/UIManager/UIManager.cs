using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, UICanvas> canvasActives = new Dictionary<Type, UICanvas>();
    private Dictionary<Type, UICanvas> canvasPrefabs = new Dictionary<Type, UICanvas>();
    [SerializeField] private Transform parent;

    private void Awake()
    {
        // Load UI prefabs từ Resources
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
        }
    }

    // Mở canvas
    public T OpenUI<T>() where T : UICanvas
    {
        // Check if UI Victory is open and UI Lose is being opened, or vice versa
        if (typeof(T) == typeof(CanvasVictory) && IsOpened<CanvasFail>())
        {
            CloseUIDirectly<CanvasFail>();
        }
        else if (typeof(T) == typeof(CanvasFail) && IsOpened<CanvasVictory>())
        {
            CloseUIDirectly<CanvasVictory>();
        }

        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();
        return canvas;
    }
   
    // Đóng canvas sau time s
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }

    // Đóng canvas trực tiếp
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
    }

    // Kiểm tra canvas đã được tạo hay chưa
    public bool IsLoaded<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null;
    }

    // Kiểm tra canvas đã được active chưa
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }

    // Lấy active canvas
    public T GetUI<T>() where T : UICanvas
    {
        // Nếu chưa load được sẽ được tạo mới nếu load được thì bỏ qua luôn và trả thẳng
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab, parent);
            canvasActives[typeof(T)] = canvas;
        }
        return canvasActives[typeof(T)] as T;
    }

    // Get prefabs
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;
    }

    // Đóng tất cả
    public void CloseAll()
    {
        foreach (var activeCanvas in canvasActives)
        {
            if (activeCanvas.Value != null && activeCanvas.Value.gameObject.activeSelf)
            {
                activeCanvas.Value.Close(0);
            }
        }
    }
}
