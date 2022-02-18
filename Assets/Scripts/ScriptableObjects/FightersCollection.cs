using System.Collections;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Fighters Collection", fileName = "Main Collection")]
    public class FightersCollection : ScriptableObject
    {
        public string Name = "Main";

        public FighterScriptable[] Fighters;
    }    
}

