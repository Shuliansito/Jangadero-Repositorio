using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass
{
    private float speed;
    private float health;
    private float damage;
    private float _stamina = 100.0f;
    public PlayerClass(float speed, float health)
    {
        this.speed = speed;
        this.health = health;
    }
    /// <summary>
    /// Gets current speed value.
    /// </summary>
    /// <returns>PlayerClass.speed</returns>
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(int value)
    {
        this.speed=value;
    }
    /// <summary>
    /// Gets the current health value/
    /// </summary>
    /// <returns>PlayerClass.health</returns>
    public float GetHealth()
    {
        return health;
    }
    /// <summary>
    /// Gets the current damage value.
    /// </summary>
    /// <returns>PlayerClass.damage</returns>
    public float GetDamage()
    {
        return damage;
    }
    /// <summary>
    /// Increases the current player health by the specified amount if the entity is alive.
    /// </summary>
    /// <param name="heal">The amount of health to restore. Must be a positive value to increase health.</param>
    public void Healing(float heal)
    {
        if (heal < 0) { Debug.LogWarning("[Healing Method] Healing parameter is lower than 0, heal: "+heal); }
        if (health > 0) { health += heal; }
    }
    /// <summary>
    /// Reduces the current health by the specified damage amount, ensuring health remains within the valid range.
    /// </summary>
    /// <remarks>If the resulting health falls below 0 or exceeds 100, it is clamped to stay within the range
    /// of 0 to 100.</remarks>
    /// <param name="dm">The amount of damage to apply. Must be greater than 0 to reduce health.</param>
    public void Hurt(float dm)
    {
        if (dm > 0)
        {
            health -= dm;
        }
        else { Debug.LogWarning("[Hurting Method] Damage parameter is lower than 0, dm: " + dm); }
            health = Mathf.Clamp(health, 0.0f, 100.0f);
    }
    /// <summary>
    /// Ensures that the current health value remains within the valid range of 0 to 100.
    /// </summary>
    /// <remarks>Call this method after modifying the health value to prevent it from exceeding the allowed
    /// limits. This method is  used to maintain health within the expected bounds.</remarks>
    public void HealthLimit()
    {
        
        health = Mathf.Clamp(health, 0.0f, 100.0f);
    }
    /// <summary>
    /// Gets the current stamina value.
    /// </summary>
    /// <returns>The current stamina as a single-precision floating-point number.</returns>
    public float GetStamina()
    {
        return _stamina;
    }
    /// <summary>
    /// Sets the current stamina value.
    /// </summary>
    /// <param name="stam">The new stamina value to assign.</param>
    public void SetStamina(float stam)
    {
        _stamina=stam;
    }
    /// <summary>
    /// Adjusts the current stamina value by the specified amount, ensuring it remains within the valid range.
    /// </summary>
    /// <param name="num">The amount to add to the current stamina. Can be positive or negative. The resulting stamina is clamped between
    /// 0.0 and 100.0.</param>
    public void UpdateStamina(float num)
    {
        _stamina += num;
        _stamina = Mathf.Clamp(_stamina, 0.0f, 100.0f);
    }

    //TODO: Mejorar el sistema de muerte a medida avanza el proyecto
    public void DetectDeath()
    {
        
    }

    public void ResetStats()
    {
        health = 100f;
        _stamina = 100f;

    }
}
