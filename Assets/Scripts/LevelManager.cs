using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenario
{
    Train,
    Duel,
    WitchTrial,
    Jousting,
    FreeFall,
    Airlock
}

enum Era
{
    WildWest,
    Medieval,
    Future
}

public static class LevelManager
{
    private static Dictionary<Scenario, Tuple<Era, int>> scenarioIDs = new Dictionary<Scenario, Tuple<Era, int>>() {
        { Scenario.Train, new Tuple<Era, int>(Era.WildWest, 0) },
        { Scenario.Duel, new Tuple < Era, int >(Era.WildWest, 1) },
        { Scenario.WitchTrial, new Tuple < Era, int >(Era.Medieval, 0) },
        { Scenario.Jousting, new Tuple < Era, int >(Era.Medieval, 1) },
        { Scenario.FreeFall, new Tuple < Era, int >(Era.Future, 0) },
        { Scenario.Airlock, new Tuple < Era, int >(Era.Future, 1) },
    };

    private static Dictionary<Era, int> eraSceneIDs = new Dictionary<Era, int>() {
        {Era.WildWest, 2 },
        {Era.Medieval, 1 },
        {Era.Future, 3 }
    };

    private static List<Scenario> uncompletedScenarios;

    private static Vector3 spawnLocation;

    private static Scenario currentLevel;

    /// <summary>
    /// Which location to spawn the player at when the scene loads
    /// </summary>
    public static Vector3 SpawnLocation => spawnLocation;

    /// <summary>
    /// Load an unloaded level scene
    /// </summary>
    public static void NextLevel()
    {
        //populate scenarios array the first time
        if (uncompletedScenarios == null)
        {
            uncompletedScenarios = new List<Scenario>();
            List<Scenario> scenarioFullList = Enum.GetValues(typeof(Scenario)).Cast<Scenario>().ToList();

            uncompletedScenarios.Add(Scenario.Train);
            scenarioFullList.Remove(Scenario.Train);

            while (scenarioFullList.Count > 0)
            {
                int index = (int)Math.Floor(UnityEngine.Random.value * scenarioFullList.Count);

                uncompletedScenarios.Add(scenarioFullList[index]);
                scenarioFullList.RemoveAt(index);
            }

            //subscribe to events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        currentLevel = uncompletedScenarios.First();
        uncompletedScenarios.RemoveAt(0);

        Tuple<Era, int> scenarioData = scenarioIDs[currentLevel];

        //load corresponding era scene
        SceneManager.LoadScene(eraSceneIDs[scenarioData.Item1]);
    }

    public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Tuple<Era, int> scenarioData = scenarioIDs[currentLevel];

        SpawnPoints temp = GameObject.FindWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        Debug.Log(temp);

        //find correct spawn position
        spawnLocation = GameObject.FindWithTag("SpawnPoints")
            .GetComponent<SpawnPoints>()
            .SpawnPointsList[scenarioData.Item2]
            .position;
    }
}
