﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class MineBehaviour : MonoBehaviour
    {
        public GameObject GE;

        private GameEnvironment _gameEnvironment;
        public int Ore;
        private int _lastSecond;
        public List<GameObject> DwarvesInside;

        void Start()
        {
            _gameEnvironment = GE.GetComponent<GameEnvironment>();
            Ore = Random.Range(0, _gameEnvironment.Variables.dwarfOreMiningRate * 60); // random value between 0 and the amount of gold ore one dwarf can mine in 1 min.
            _lastSecond = 0;
            DwarvesInside = new List<GameObject>();
        }

        void Update()
        {
            if (Time.time - _lastSecond >= 1)
            {
                _lastSecond = (int)Mathf.Floor(Time.time);
                Ore += _gameEnvironment.Variables.oreSpawnRate;

                foreach (var d in DwarvesInside)
                {
                    d.GetComponent<DwarfMemory>().TimeAsMiner++;
                }

                var miningRate = _gameEnvironment.Variables.dwarfOreMiningRate;
                foreach (var Dwarf in DwarvesInside)
                {
                    Debug.Log(Dwarf.name + "is in" + this.name);
                    if (Ore >= miningRate)
                    {
                        Dwarf.GetComponent<DwarfMemory>().GoldOreMined += miningRate;
                        _gameEnvironment.Variables.TotalGoldMined += miningRate;
                        Ore -= miningRate;
                    }
                    else
                    {
                        Dwarf.GetComponent<DwarfMemory>().GoldOreMined += Ore;
                        _gameEnvironment.Variables.TotalGoldMined += Ore;
                        Ore = 0;
                        EmptyMine();
                        break;
                    }
                }
            }
        }

        public void AddDwarfInside(GameObject dwarf)
        {
            DwarvesInside.Add(dwarf);
        }

        public void RemoveDwarfInside(GameObject dwarf)
        {
            DwarvesInside.Remove(dwarf);
        }

        private void EmptyMine()
        {
            foreach(var Dwarf in DwarvesInside)
            {
                Dwarf.GetComponent<DwarfMemory>().GetNewDestination();
            }
        }
    }
}
