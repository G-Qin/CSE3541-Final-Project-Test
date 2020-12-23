using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoCounter : MonoBehaviour
{
    public AK47 weapon;
    private int ammo;
    public Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammo = weapon.currentAmmo;
        ammoText.text = "Ammo-" + ammo.ToString();
    }
}
