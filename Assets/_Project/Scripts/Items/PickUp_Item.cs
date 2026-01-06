using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Item : MonoBehaviour
{
    public enum itemType
    {
        addHealth,
        weapon
    }

    [SerializeField] public int _health = 10;
    [SerializeField] public itemType typeItem;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            switch (typeItem)
            {
                case itemType.addHealth:
                    collision.gameObject.GetComponent<Player>().GetItem("Health", _health);
                    break;

                case itemType.weapon:
                    break;

            }
            //Debug.Log($"Raccolto");
        }
    }
}
