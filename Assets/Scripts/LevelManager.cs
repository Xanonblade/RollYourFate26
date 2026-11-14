using System;
using System.Collections.Generic;
using UnityEngine;

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
    private static List<Scenario> uncompletedScenarios;

    public static void NextLevel()
    {
        //populate scenarios array
        if (uncompletedScenarios == null)
        {
            uncompletedScenarios = new List<Scenario>();

            for (int i = 0; i < Scenario.GetValues(typeof(Scenario)).Length; i++)
            {
                //TODO: avoid two of same era in a row
                uncompletedScenarios.Add((Scenario)i);

                Debug.Log(((Scenario)i).ToString());
            }

        }
    }
}
