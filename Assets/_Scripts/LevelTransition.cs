using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private bool inProgress;
    public void GoToLevel()
    {
        if (inProgress)
        {
            return;
        }
        TransitionEffect.Instance.StartFading();
        Dummy dummy = new GameObject("Dummy", typeof(Dummy)).GetComponent<Dummy>();
        dummy.StartCoroutine(ChangeScene());
        inProgress = true;
    }

    private System.Collections.IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
}


class Dummy : MonoBehaviour { }