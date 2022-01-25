using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour, I_Item, I_SpawnObject
{
    [SerializeField]
    private StatusItemData m_cStatusItemData;
    [SerializeField]
    private string m_targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_targetTag)
            use(collision.GetComponent<UnitBase>());
    }


    public void use(UnitBase unit)
    {
        unit.nHP += m_cStatusItemData.nAddHp;
        unit.fStamina += m_cStatusItemData.fAddStatmina;
        unit.fSpeed += m_cStatusItemData.fAddSpeed;

        returnToObjectTool();
    }

    public void returnToObjectTool()
    {
        gameObject.SetActive(false);
        GameManager.Instance.cStageManager.cStageObjectPool.returnObject(gameObject);
    }
}
