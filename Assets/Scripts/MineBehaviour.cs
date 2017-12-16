﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class MineBehaviour : MonoBehaviour
    {
        public GameObject GE;

        private GameEnvironment gameEnvironment;
        private int ore;
        private int LastSecond;
        private List<GameObject> dwarvesInside;

        void Start()
        {
            gameEnvironment = GE.GetComponent<GameEnvironment>();
            ore = Random.Range(0, gameEnvironment.Variables.dwarfOreMiningRate * 60); // random value between 0 and the amount of gold ore one dwarf can mine in 1 min.
            LastSecond = 0;
            dwarvesInside = new List<GameObject>();
        }

        void Update()
        {
            if (Time.time - LastSecond >= 1)
            {
                LastSecond = (int)Mathf.Floor(Time.time);
                ore += gameEnvironment.Variables.oreSpawnRate - dwarvesInside.Count * gameEnvironment.Variables.dwarfOreMiningRate;
            }
        }

        public void AddDwarfInside(GameObject dwarf)
        {
            dwarvesInside.Add(dwarf);
        }
    }
}
