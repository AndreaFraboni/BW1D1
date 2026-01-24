using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Item : MonoBehaviour
{
    public enum itemType
    {
        ADDHEALTH,
        WEAPON,
        COIN
    }

    [SerializeField] GameObject _weaponPrefab;
    [SerializeField] GameObject _healthPrefab;
    [SerializeField] GameObject _coinPrefab;

    [SerializeField] public int _health = 10;
    [SerializeField] public itemType typeItem;

    private Player _playerController;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_spriteRenderer)
        {
            Debug.Log("NON HO TROVATO IL COMPONENTE SPRITE RENDERER !!!");
        }
        _anim = GetComponent<Animator>();
        if (!_anim)
        {
            Debug.Log("NON HO TROVATO L'Animator Controller !!!");
        }
    }
    private void OnEnable()
    {
        Animator _prefabAnim;

        switch (typeItem)
        {
            case itemType.WEAPON:

                _spriteRenderer.sprite = _weaponPrefab.GetComponent<SpriteRenderer>().sprite;

                break;
            case itemType.COIN:

                _spriteRenderer.sprite = _coinPrefab.GetComponent<SpriteRenderer>().sprite;
                _prefabAnim = _coinPrefab.GetComponent<Animator>();
                _anim.runtimeAnimatorController = _prefabAnim.runtimeAnimatorController;

                break;
            case itemType.ADDHEALTH:

                _spriteRenderer.sprite = _healthPrefab.GetComponent<SpriteRenderer>().sprite;
                _prefabAnim = _healthPrefab.GetComponent<Animator>();
                _anim.runtimeAnimatorController = _prefabAnim.runtimeAnimatorController;

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (!collision.CompareTag(Tags.Player)) return;

        if (!collision.TryGetComponent<Player>(out _playerController))
        {
            Debug.LogError("NON trovo il componente PlayerController sul Player !!!");
            return;
        }

        switch (typeItem)
        {
            case itemType.WEAPON:

                _playerController.MountWeapon(_weaponPrefab);
                Destroy(gameObject);

                break;
            case itemType.ADDHEALTH:

                collision.gameObject.GetComponent<Player>().GetItem("Health", _health);
                Destroy(gameObject);

                break;
            case itemType.COIN:

                _playerController.GetCoins();
                Destroy(gameObject);

                break;
        }
    }
}
