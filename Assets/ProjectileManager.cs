using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [System.Serializable]
    public class Projectile
    {
        public string tag;
        [HideInInspector]
        public int power;
        public int minPower;
        public int maxPower;
        [HideInInspector]
        public int fireRate;
        public int minFireRate;
        public int maxFireRate;
    }

    public static ProjectileManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("More than one Projectile Manager in the Scene.");
            return;
        }
        instance = this;
    }

    //[Tooltip("steps between min and max power or fire rate")]
    //[Range(1,10)]
    //public int steps = 5;
    public List<Projectile> projectiles;

    int curStep = 1;

    public void IncreaseFireRate(Projectile p)
    {
        // 5 hard coded for now for UI simplicity
        int step = (p.maxFireRate - p.minFireRate) / 5;
        p.fireRate += curStep * step + 1;
        if (p.fireRate > p.maxFireRate)
            p.fireRate = p.minFireRate;
    }

    public void IncreasePower(Projectile p)
    {
        // 5 hard coded for now for UI simplicity
        int step = (p.maxPower - p.minPower) / 5;
        p.power += step + 1;
        if (p.power > p.maxPower)
            p.power = p.minPower;
    }
}
