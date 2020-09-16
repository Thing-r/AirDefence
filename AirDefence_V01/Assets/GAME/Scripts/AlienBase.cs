using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AlienBase : MonoBehaviour
{
    public float healthPoints = .25f;
    public Slider healthSlide;
    public Text scoreUI;
    public Text finalScoreUI;
    public GameObject finalPanelUI;
    public GameObject spawner;
   // public List<Material> skyboxes;
    public float currentDamage;

    private bool gameover;
    private int score;

    private void Start()
    {
        currentDamage = healthPoints;
        healthSlide.maxValue = healthPoints;
        healthSlide.value = healthPoints;
       // RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Count)];

        StartCoroutine(ScoreCounter());
    }

    public void ApplyDamage(float damage)
    {
        /*        */
        healthPoints -= damage;
        healthSlide.value = healthPoints;

        if (healthPoints <= 0)
        {
            Destroy(spawner);
            GetComponent<BoxCollider>().enabled = false;

            gameObject.AddComponent<Rigidbody>();
            foreach (Transform ch in transform)
            {
                var rb = ch.gameObject.AddComponent<Rigidbody>();
                rb.AddTorque(new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)));
            }

            finalScoreUI.text = score.ToString();
            gameover = true;
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator ScoreCounter()
    {
        while (!gameover)
        {
            score++;
            scoreUI.text = "Score: " + score;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(10);
        Time.timeScale = .2f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finalPanelUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("r4Game");
    }


}
