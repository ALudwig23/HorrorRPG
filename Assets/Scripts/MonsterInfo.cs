using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public class Monster1
    {
        private float MaxHealth;
        private float CurrentHealth;
        private string[] moveset = { "Basic",  };

        public Sprite Monster1Sprite;
        

    }

    public class Monster2
    {
        private float MaxHealth;
        private float CurrentHealth;
        private string[] moveset = { };
    }

    public class Monster3 
    { 
        private float MaxHealth;
        private float CurrentHealth;
        private string[] moveset = { };

    }
}
