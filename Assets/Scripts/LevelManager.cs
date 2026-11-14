using System;
using System.Collections.Generic;
using System.Linq;
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

            for (int i = 0; i < Enum.GetValues(typeof(Scenario)).Length; i++)
            {
                //TODO: avoid two of same era in a row
                uncompletedScenarios.Add((Scenario)i);
            }
        }

        Scenario nextLevel = uncompletedScenarios.Last();
        uncompletedScenarios.RemoveAt(uncompletedScenarios.Count - 1);

        Tuple<Era, int> scenarioData = scenarioIDs[nextLevel];

        //find correct spawn position
        spawnLocation = GameObject.FindWithTag("SpawnPoints")
            .GetComponent<SpawnPoints>()
            .SpawnPointsList[scenarioData.Item2]
            .position;

        //load corresponding era scene
        SceneManager.LoadScene(eraSceneIDs[scenarioData.Item1]);
    }
}
