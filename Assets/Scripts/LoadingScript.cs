using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (text != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            text.text = sceneName;
        }
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        var objects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj.activeInHierarchy && obj.GetComponent<Camera>() == null && !obj.CompareTag("Loading"))
            {
                obj.SetActive(false);
            }
        }
        yield return new WaitForSeconds(3f);

        foreach (GameObject obj in objects)
        {
            if (obj.CompareTag("Pause"))
            {
                obj.SetActive(false);
            }
            obj.SetActive(true);
        }
        panel.SetActive(false);
    }
}
