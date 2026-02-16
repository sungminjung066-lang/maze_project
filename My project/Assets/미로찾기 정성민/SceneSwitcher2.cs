using UnityEngine;

using UnityEngine.SceneManagement;



public class SceneSwitcher2 : MonoBehaviour

{

    private void OnTriggerEnter(Collider other)

    {

        // 플레이어가 Goal 큐브에 도착했을 때 씬 전환

        if (other.CompareTag("Player"))

        {

            SceneManager.LoadScene("maze1");

        }

    }

}
