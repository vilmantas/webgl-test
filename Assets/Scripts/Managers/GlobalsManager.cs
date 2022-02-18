using System;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    // Have this class manage globaly preloaded settings i.e. player selections, enemies, effects. 
    public class GlobalsManager : MonoBehaviour
    {
        [HideInInspector]
        public FightersCollection PlayerSelection;
        
        [NonSerialized] 
        public static GlobalsManager Instance;

        public void DoSetup()
        {
            Instance = this;

            PlayerSelection = Instantiate(Resources.Load("PlayerFighterOptions")) as FightersCollection;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Debug.Log("HEre");
        }

        private void Start()
        {
            Debug.Log("HWWW");
            PlayerSelection = Instantiate(Resources.Load("PlayerFighterOptions")) as FightersCollection;
            
        }
    }
}