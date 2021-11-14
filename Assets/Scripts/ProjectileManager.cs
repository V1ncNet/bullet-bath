using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{
    [System.Serializable]
    public class Projectile
    {
        [HideInInspector]
        public int power;
        public int maxPower;
        [HideInInspector]
        public int fireRate;
        public int maxFireRate;
    }

    public static ProjectileManager proManager;

    void Awake()
    {
        if (proManager)
        {
            Debug.LogError("More than one Projectile Manager in the Scene.");
            return;
        }
        proManager = this;
    }

    //pools and projectiles might be merged in a future version
    [Tooltip("Projectiles order should match the respected pools order")]
    public List<Projectile> projectiles;
    public GameObject FireRateButton;
    public GameObject PowerButton;
    public Sprite[] buttonLevels;

    Projectile currentProjectile;
    Image FRButtonImage;
    Image PoButtonImage;
    int curFRStep = 1;
    int curPoStep = 1;

    void Start()
    {
        foreach (Projectile projectile in projectiles)
        {
            projectile.fireRate = projectile.maxFireRate / buttonLevels.Length;
            projectile.power = projectile.maxPower / buttonLevels.Length;
        }
        currentProjectile = projectiles[0];
        FRButtonImage = FireRateButton.GetComponent<Image>();
        PoButtonImage = PowerButton.GetComponent<Image>();
    }

    // the following might scale very bad but it is kept simple for the sake of the project's deadline

    public void ChangeFireRate()
    {
        Debug.Log("firerate button");
        curFRStep++;
        if (curFRStep > buttonLevels.Length)
        {
            curFRStep = 1;
        }
        currentProjectile.fireRate = currentProjectile.maxFireRate * curFRStep / buttonLevels.Length;
        FRButtonImage.sprite = buttonLevels[curFRStep - 1];
    }

    public void ChangePower()
    {
        Debug.Log("power button");
        curPoStep++;
        if (curPoStep > buttonLevels.Length)
        {
            curPoStep = 1;
        }
        currentProjectile.power = currentProjectile.maxPower * curPoStep / buttonLevels.Length;
        PoButtonImage.sprite = buttonLevels[curPoStep - 1];
    }

    public void SetProjectile(int index)
    {
        currentProjectile = projectiles[index];
    }
}
