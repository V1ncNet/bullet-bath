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

        public int curFRStep = 1;
        public int curPoStep = 1;
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
        currentProjectile.curFRStep++;
        if (currentProjectile.curFRStep > buttonLevels.Length)
        {
            currentProjectile.curFRStep = 1;
        }
        currentProjectile.fireRate = currentProjectile.maxFireRate * currentProjectile.curFRStep / buttonLevels.Length;
        FRButtonImage.sprite = buttonLevels[currentProjectile.curFRStep - 1];
    }

    public void ChangePower()
    {
        Debug.Log("power button");
        currentProjectile.curPoStep++;
        if (currentProjectile.curPoStep > buttonLevels.Length)
        {
            currentProjectile.curPoStep = 1;
        }
        currentProjectile.power = currentProjectile.maxPower * currentProjectile.curPoStep / buttonLevels.Length;
        PoButtonImage.sprite = buttonLevels[currentProjectile.curPoStep - 1];
    }

    void UpdateUI()
    {
        FRButtonImage.sprite = buttonLevels[currentProjectile.curFRStep - 1];
        PoButtonImage.sprite = buttonLevels[currentProjectile.curPoStep - 1];
    }

    public void SetProjectile(int index)
    {
        currentProjectile = projectiles[index];
        UpdateUI();
    }
}
