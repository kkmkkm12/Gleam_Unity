using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// Haribo: 객체 재활용을 위한 오브젝트 풀링 클래스. MonoBehaviour를 상속받는 클래스만을 처리시키게 한다.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> pool;

    /// <summary>
    ///  객체를 생성하고 풀에 담아주는 생성자(C# 생성자: 클래스의 인스턴스를 생성할 때 호출됨.CarList.cs의 Start()에서 호출해줌. 반환 타입을 명시할 필요가 없음)
    /// </summary>
    /// <param name="prefab"> 오브젝트 풀링 쓸 프리펩 </param>
    /// <param name="poolSize"> 처음 Pool 사이즈 몇개 얼마나 할지 설정</param>
    public ObjectPool(T prefab, int poolSize, Transform content)
    {
        this.prefab = prefab;
        pool = new List<T>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            T t = GameObject.Instantiate(prefab, content);
            t.gameObject.SetActive(false); //T 속성 가지고 있는 객체 비활성화 해준 다음에, 
            pool.Add(t);//풀에 담아주기
        }
    }

    /// <summary>
    /// 하리보: 해당 원하는 속성(T) 가지고 있는 비활성화인 객체를 가져다가 쓰는 함수
    /// </summary>
    /// <returns></returns>
    public T GetObj()
    {
        foreach (T t in pool)
        {
            if (!t.gameObject.activeInHierarchy)
            {
                t.gameObject.SetActive(true);
                return t; //여기서 리턴하고, 아래 함수 로직은 실행되지않음. 리턴해주고 끝
            }
        }
        //새로운 오브젝트 생성: 위에 foreach문 다 돌았는데, 리턴 안되었으면 풀 안에있는 객체들 다 사용중이란뜻(모두 활성화중).
        T newObj = GameObject.Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    /// <summary>
    /// 하리보:  다쓴 객체 비활성화 해주는 함수(pool에서 객체를 빼는 로직은 안씀. 데이터가 추가되는게 더 많을거같아 일단 보류)
    /// </summary>
    /// <param name="obj"></param>
    public void OffActive(T obj)
    {
        obj.gameObject.SetActive(false);
    }
    // public void RemovePool(int maxPoolSize)
    // {
    //     if (pool.Count > maxPoolSize)
    //     {
    //         for (int i = pool.Count - 1; i >= 0; i--)
    //         {
    //             if (!pool[i].gameObject.activeInHierarchy && pool.Count > maxPoolSize)
    //             {
    //                 GameObject.Destroy(pool[i].gameObject);
    //                 pool.RemoveAt(i);
    //             }
    //         }
    //     }
    // }

    /// <summary>
    /// 하리보: 풀에 객체 몇개 있는지 세어주는 함수
    /// </summary>
    /// <returns></returns>
    public int GetCountInPool()
    {
        return pool.Count;
    }


    /// <summary>
    /// 곽경민: 들어온 객체 풀에서 찾아서 풀에서 제거
    /// </summary>
    /// <param name="obj"></param>
    public void RemovePool(T obj)           
    {
        for(int i = 0; i < pool.Count; i++)
        {
            T t = pool[i];
            if(obj == t)
            {
                pool.Remove(obj);
            }
        }
    }
}
