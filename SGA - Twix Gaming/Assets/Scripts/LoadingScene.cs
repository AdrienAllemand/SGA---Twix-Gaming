using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    private string sceneName;

	public void call()
    {
        Debug.Log("Clic!");
        SceneManager.LoadScene(sceneName);
    }

}
