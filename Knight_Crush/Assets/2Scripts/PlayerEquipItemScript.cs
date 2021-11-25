using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;
using HeroEditor.Common;
using HeroEditor.Common.Enums;

public class PlayerEquipItemScript : MonoBehaviour
{
    public SpriteGroupEntry Weapon;
    public GameDataScript GameData;
    public Character Player;

    void Start()
    {
        GameData = FindObjectOfType<GameDataScript>();
        Weapon = GameData.getWeapon();
        Player = GameObject.FindWithTag("Player").GetComponent<Character>();
        Player.Equip(Weapon, EquipmentPart.MeleeWeapon1H);
    }

    void Update()
    {
        
    }
}
