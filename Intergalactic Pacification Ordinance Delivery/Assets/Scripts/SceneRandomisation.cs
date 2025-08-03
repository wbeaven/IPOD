using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRandomisation : MonoBehaviour
{
    [SerializeField] GameObject[] skyboxLights;
    [SerializeField] Material[] skyboxes;

    [SerializeField] string[] planets;
    [SerializeField] string[] enemies;
    
    void Start()
    {
        RandomSkybox();
        RandomPlanet();
    }

    void Update()
    {
        
    }

    private void RandomSkybox()
    {
        int getSkybox = Random.Range(0, skyboxes.Length);
        RenderSettings.skybox = skyboxes[getSkybox];
        skyboxLights[getSkybox].SetActive(true);
    }

    private void RandomPlanet()
    {
        SceneManager.LoadScene(planets[Random.Range(0, planets.Length)], LoadSceneMode.Additive);
    }

    private void RandomEnemies()
    {
        SceneManager.LoadScene(enemies[Random.Range(0, enemies.Length)], LoadSceneMode.Additive);
    }
}
