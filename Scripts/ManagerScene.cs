using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
   public void LoadScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
