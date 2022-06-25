using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalBoxs;
    public int finishedBoxs;
    public static GameManager Instance;
    List<GameObject> MoveObjs = new List<GameObject>();
    Stack<List<GameObject>> stack = new Stack<List<GameObject>>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else Instance = this;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetStage();
        if (Input.GetKeyDown(KeyCode.Z))
            UnDo();
    }

    public void CheckFinish()
    {
        if(finishedBoxs == totalBoxs)
        {
            print("YOU WIN!");
            StartCoroutine(LoadNextStage());
        }
    }

    void ResetStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #region UnDo
    public void Rigist(GameObject obj)
    {
        MoveObjs.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        MoveObjs.Remove(obj);
    }

    public void Save()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (var item in MoveObjs)
        {
            GameObject tp = Instantiate(item.gameObject, item.transform.position, Quaternion.identity);
            tp.SetActive(false);
            temp.Add(tp);
        }
        stack.Push(temp);

    }

    public void UnDo()
    {
        if (stack.Count == 0) return;
        foreach (var item in MoveObjs)
        {
            Destroy(item);        
        }
        List<GameObject> temp = stack.Pop();
        foreach (var item in temp) 
        {
            item.SetActive(true);        
        }
            


    
    }


    #endregion

}
