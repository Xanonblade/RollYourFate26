using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenario
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
        //{ Scenario.FreeFall, new Tuple < Era, int >(Era.Future, 0) },
        //{ Scenario.Airlock, new Tuple < Era, int >(Era.Future, 1) },
    };

    private static Dictionary<Era, int> eraSceneIDs = new Dictionary<Era, int>() {
        {Era.WildWest, 2 },
        {Era.Medieval, 1 },
        {Era.Future, 3 }
    };

    private static Dictionary<Scenario, Tuple<float, float>> playerMinMaxPitch = new Dictionary<Scenario, Tuple<float, float>>() {
        { Scenario.Train, new Tuple<float, float>(-65f, 60f) },
        {Scenario.Duel, new Tuple<float, float>(-180f, 180f) }
    };

    private static Dictionary<Scenario, Tuple<float, float>> playerMinMaxYaw = new Dictionary<Scenario, Tuple<float, float>>() {
        { Scenario.Train, new Tuple<float, float>(-80f, 80f) }
    };

    private static List<Scenario> uncompletedScenarios;

    private static Transform spawnTransform;

    private static Scenario currentLevel;

    /// <summary>
    /// The location to spawn the player at when the scene loads
    /// </summary>
    public static Vector3 SpawnLocation => spawnTransform.position;

    /// <summary>
    /// The rotation to give the player when the scene loads
    /// </summary>
    public static Quaternion SpawnRotation => spawnTransform.rotation;

    /// <summary>
    /// The minimum pitch for the players head movement this scenario
    /// </summary>
    public static float GetMinPitch { get { return playerMinMaxPitch[CurrentScenario].Item1; } }

    /// <summary>
    /// The maximum pitch for the players head movement this scenario
    /// </summary>
    public static float GetMaxPitch { get { return playerMinMaxPitch[CurrentScenario].Item2; } }

    /// <summary>
    /// The minimum yaw for the players head movement this scenario
    /// </summary>
    public static float GetMinYaw { get { return playerMinMaxYaw[CurrentScenario].Item1; } }

    /// <summary>
    /// The maximum yaw for the players head movement this scenario
    /// </summary>
    public static float GetMaxYaw { get { return playerMinMaxYaw[CurrentScenario].Item2; } }

    /// <summary>
    /// The current scenario loaded
    /// </summary>
    public static Scenario CurrentScenario { get { return currentLevel; } }

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
        spawnTransform = GameObject.FindWithTag("SpawnPoints")
            .GetComponent<SpawnPoints>()
            .SpawnPointsList[scenarioData.Item2];
    }
}
