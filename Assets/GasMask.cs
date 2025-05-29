using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMask : MonoBehaviour
{
    public GameObject NormalEffects;
    public GameObject MaskEffects;

    public bool equipped = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            equipped = !equipped;
            anim.SetTrigger("equip");

        }


    }

    void LateUpdate()
    {
        anim.ResetTrigger("equip");

    }

    public void Equip()
    {
        if (equipped)
        {
            NormalEffects.SetActive(false);
            MaskEffects.SetActive(true);
        }
        if (!equipped)
        {
            NormalEffects.SetActive(true);
            MaskEffects.SetActive(false);
        }

    }
}
