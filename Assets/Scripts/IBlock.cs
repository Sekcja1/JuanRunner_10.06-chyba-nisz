using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBlock : MonoBehaviour
{
    protected IBlock()
    {
        Destroyed = false;
    }

    #region Basic

    /// <summary>
    /// Czy można przjść przez blok
    /// </summary>
    public bool Walkable { get; set; }

    /// <summary>
    /// Opór ruch na bloku
    /// </summary>
    public float MoveResistans { get; set; }

    /// <summary>
    /// Zabicie przy kolizji
    /// </summary>
    public bool KillOnCollision { get; set; }

    /// <summary>
    /// Czy blok zajmuje połowe kratki
    /// </summary>
    public bool HalfBlock { get; set; }

    /// <summary>
    /// Czy blok zajmuje ćwierć kratki
    /// </summary>
    public bool QuarterBlockVertical { get; set; }

    /// <summary>
    /// Czy blok wchodzi w interakcje z graczem
    /// </summary>
    public bool Solid { get; set; }

    /// <summary>
    /// Czy obiekt został zniszczony
    /// </summary>
    public bool Destroyed { get; set; }
    #endregion

    #region DestroyableBlocks
    /// <summary>
    /// Wytrzymałość bloku
    /// </summary>
    public float Durability { get; set; }

    /// <summary>
    /// Czy blok jest zniszczalny
    /// </summary>
    public bool Destroyable { get; set; }

    /// <summary>
    /// Lista tekstur (sprite) dla różnych poziomów wytrzymałośći bloków (wartość procentowa)
    /// </summary>
    List<KeyValuePair<Sprite, int>> DestructionLevels { get; set; }

    #endregion

    #region ExplosiveBlocks
    /// <summary>
    /// Czy blok ma wybuchnąć po zniszczeniu
    /// </summary>
    public bool ExplodeOnDestroy { get; set; }

    /// <summary>
    /// Obrażenia jakie zada blok po wybuchu
    /// </summary>
    public float ExplodeDamage { get; set; }

    /// <summary>
    /// Zasięg wybuchu podany w kratkach
    /// </summary>
    int ExplodeRange { get; set; }
    #endregion

    #region EnvironmentBlock
    /// <summary>
    /// Czy gracz może podnieść blok
    /// </summary>
    public bool CollectableBlock { get; set; }

    /// <summary>
    /// Czy blok jest drabiną (umożliwia przemieszczanie w pionie (no shit sherlock))
    /// </summary>
    public bool Ladder { get; set; }
    #endregion

    #region SensorBlock
    /// <summary>
    /// Czy gracz znajduje się w poblirzu bloku
    /// </summary>
    public bool DetectPlayer { get; set; }

    /// <summary>
    /// Czy w pobliżu znajduje się przeciwnik
    /// </summary>
    public bool DetectEnemy { get; set; }

    /// <summary>
    /// Zasięg wykrywania 
    /// </summary>
    int DetectRange { get; set; }
    #endregion

    #region LootBlock
    /// <summary>
    /// Czy blok wyrzuca przedmioty
    /// </summary>
    public bool DropItems { get; set; }
    #endregion

    #region Postaci
    /// <summary>
    /// Czy jest graczem
    /// </summary>
    public bool IsPlayer { get; set; }

    /// <summary>
    /// Numer gracza
    /// </summary>
    public int PlayerNumber { get; set; }

    /// <summary>
    /// Czy jest wrogiem
    /// </summary>
    public bool IsEnemy { get; set; }
    #endregion

    public ParticleSystem Fire { get; set; }

    public bool AddDamage(float damage)
    {
        try
        {
           Fire.Play();

            
        }
        catch (Exception e)
        {
        }
        if (Destroyable)
        {
            Durability -= damage;
        }

        Destroyed = Durability <= 0;

        return Destroyed;
    }
}
