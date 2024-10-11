using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private List<GameObject> _prefabList;           // 캐싱 리소스로서 가지고있는 UI
    private List<GameObject> _loadedList;           // Hierarchy에 생성되어 로드된 UI

    protected override void Init()
    {
        _prefabList = new List<GameObject>();
        _loadedList = new List<GameObject>();
    }

    protected override void Release()
    {
        _prefabList.Clear();
        _prefabList = null;

        CloseAll();  // 모든 로드된 UI를 닫고 해제
        _loadedList = null;
    }

    public T Open<T>() where T : UIBase
    {
        bool isPanel = typeof(T).Name.StartsWith("Panel");

        // 이미 로드된 UI가 _loadedList에 있는지 확인.        
        GameObject existObj = _loadedList.Find(e => e.GetComponent<T>() != null);
        if (existObj != null)
        {
            return existObj.GetComponent<T>();
        }

        // 캐싱된 UI가 있는지 확인
        GameObject cachingObj = _prefabList.Find(e => e.GetComponent<T>() != null);
        if (cachingObj != null)
        {
            GameObject obj = GameObject.Instantiate(cachingObj);
            if (obj != null)
            {
                // 로드 list에 추가
                _loadedList.Add(obj);

                obj.transform.SetParent(GameManager.Instance.UICanvasRoot, false);

                T objT = obj.GetComponent<T>();
                objT.Open();                    // 로드한 UI를 연다 ( = Initialize )
                return objT;
            }
        }

        // 새로 로드
        string path = string.Format("UI/{0}/{1}", isPanel ? "Panel" : "Popup", typeof(T).Name);
        GameObject prefab = ResourceManager.Instance.LoadUIPrefab(path);

        // prefab이 null인 경우 Resource 폴더에 있는 UI이다.
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>(path);
        }

        if (prefab != null)
        {
            // 캐싱 list에 추가
            _prefabList.Add(prefab);

            GameObject obj = GameObject.Instantiate(prefab);
            if (obj != null)
            {
                // 로드 list에 추가
                _loadedList.Add(obj);

                obj.transform.SetParent(GameManager.Instance.UICanvasRoot, false);

                T objT = obj.GetComponent<T>();
                objT.Open();                    // 로드한 UI를 연다 ( = Initialize )
                return objT;
            }
        }

        return null;
    }

    public void Close(UIBase ui)
    {
        if (_loadedList.Contains(ui.gameObject) == true)
        {
            _loadedList.Remove(ui.gameObject);
            ui.Close();
        }
    }

    public void CloseAll()
    {
        for (int i = 0; i < _loadedList.Count; i++)
        {
            GameObject obj = _loadedList[i];
            if (obj != null)
            {
                UIBase ui = obj.GetComponent<UIBase>();
                ui?.Close();
            }
        }

        _loadedList.Clear();
    }
}
