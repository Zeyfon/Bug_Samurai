using System.Collections;
using UnityEngine;
using System;

public class EnemyFX : EntityFX
{
    #region Inspector
    [Header("Attacks")]
    [Header("AttackSignal")]
    [SerializeField] GameObject _attackSignalVFX;
    [SerializeField] Transform _attackSignalVFXOrigin;
    [SerializeField] AudioClip _attackSignalSFX;
    [Range(0, 1)]
    [SerializeField] float _attackSignalSFXVolume = 0.5f;
    [Header("Attack")]
    [SerializeField] GameObject _attackVFX;
    [SerializeField] Transform _attackVFXOrigin;
    [SerializeField] AudioClip _attackSFX;
    [Range(0, 1)]
    [SerializeField] float _attackSFXVolume = 0.5f;
    [Header("Combo Attack")]
    [SerializeField] GameObject _comboAttack1VFX;
    [SerializeField] Transform _comboAttack1VFXOrigin;
    [SerializeField] AudioClip _comboAttack1SFX;
    [Range(0,1)]
    [SerializeField] float _comboAttack1SFXVolume = 0.5f;
    [Space]
    [SerializeField] GameObject _comboAttack2VFX;
    [SerializeField] Transform _comboAttack2VFXOrigin;
    [SerializeField] AudioClip _comboAttack2SFX;
    [Range(0, 1)]
    [SerializeField] float _comboAttack2SFXVolume = 0.5f;
    [Space]
    [SerializeField] GameObject _comboAttack3VFX;
    [SerializeField] Transform _comboAttack3VFXOrigin;
    [SerializeField] AudioClip _comboAttack3SFX;
    [Range(0, 1)]
    [SerializeField] float _comboAttack3SFXVolume = 0.5f;
    [Header("Running Attack")]
    [SerializeField] GameObject _runningAttackVFX;
    [SerializeField] Transform _runningAttackVFXOrigin;
    [SerializeField] AudioClip _runningAttackSFX;
    [Range(0, 1)]
    [SerializeField] float _runningAttackSFXVolume = 0.5f;
    [Header("AOEAttack")]
    [SerializeField] GameObject _aoeAttackVFX;
    [SerializeField] Transform _aoeAttackVFXOrigin;
    [SerializeField] AudioClip _aoeAttackSFX;
    [Range(0, 1)]
    [SerializeField] float _aoeAttackSFXVolume = 0.5f;

    [Header("Defense")]
    [SerializeField] GameObject _defenseVFX;
    [SerializeField] Transform _defenseVFXOrigin;
    [SerializeField] AudioClip _defenseSFX;
    [Range(0, 1)]
    [SerializeField] float _defenseSFXVolume = 0.5f;

    [Header("Movement SFX")]
    [SerializeField] AudioClip _walkingSFX;
    [Range(0, 1)]
    [SerializeField] float _walkingSFXVolume = 0.5f;
    [SerializeField] AudioClip _runningSFX;
    [Range(0, 1)]
    [SerializeField] float _runningSFXVolume = 0.5f;

    [Header("Health")]
    [SerializeField] AudioClip _damagedSFX;
    [Range(0, 1)]
    [SerializeField] float _damagedSFXVolume = 0.5f;
    [SerializeField] AudioClip _stunnedSFX;
    [Range(0, 1)]
    [SerializeField] float _stunnedSFXVolume = 0.5f;
    [SerializeField] AudioClip _diesSFX;
    [Range(0, 1)]
    [SerializeField] float _diesSFXVolume = 0.5f;
    [Space]
    [SerializeField] SpriteRenderer _bodyRenderer;
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _damageMaterial;
    [Range(0, 1)]
    [SerializeField] float _damageVisualTime = 0.5f;
    #endregion

    public void PlayAttackSignalVFX()
    {
        base.CreateVFXGameObject(_attackSignalVFX, _attackSignalVFXOrigin);
    }
    public void PlayAttackSignalSFX()
    {
        _audioSource.PlayOneShot(_attackSignalSFX, _attackSignalSFXVolume);
    }
    public void PlayAttackVFX()
    {
        base.CreateVFXGameObject(_attackVFX, _attackVFXOrigin);
    }
    public void PlayAttackSFX()
    {
        _audioSource.PlayOneShot(_attackSFX, _attackSFXVolume);
    }
    public void PlayComboAttack1SFX()
    {
        _audioSource.PlayOneShot(_comboAttack1SFX, _comboAttack1SFXVolume);
    }
    public void PlayComboAttack1VFX()
    {
        base.CreateVFXGameObject(_comboAttack1VFX, _comboAttack1VFXOrigin);
    }
    public void PlayComboAttack2SFX()
    {
        _audioSource.PlayOneShot(_comboAttack2SFX, _comboAttack2SFXVolume);
    }
    public void PlayComboAttack2VFX()
    {
        base.CreateVFXGameObject(_comboAttack2VFX, _comboAttack2VFXOrigin);
    }
    public void PlayComboAttack3SFX()
    {
        _audioSource.PlayOneShot(_comboAttack3SFX, _comboAttack3SFXVolume);
    }
    public void PlayComboAttack3VFX()
    {
        base.CreateVFXGameObject(_comboAttack3VFX, _comboAttack3VFXOrigin);
    }
    public void PlayRunningAttackSFX()
    {
        _audioSource.PlayOneShot(_runningAttackSFX, _runningAttackSFXVolume);
    }
    public void PlayRunningAttackVFX()
    {
        base.CreateVFXGameObject(_runningAttackVFX, _runningAttackVFXOrigin);
    }
    public void PlayAOEAttackSFX()
    {
        _audioSource.PlayOneShot(_aoeAttackSFX, _aoeAttackSFXVolume);
    }
    public void PlayAOEAttackVFX()
    {
        base.CreateVFXGameObject(_aoeAttackVFX, _aoeAttackVFXOrigin);
    }
    public void PlayDefenseSFX()
    {
        _audioSource.PlayOneShot(_defenseSFX, _defenseSFXVolume);
    }
    public void PlayDefenseVFX()
    {
        base.CreateVFXGameObject(_defenseVFX, _defenseVFXOrigin);
    }
    public void PlayWalkingSFX()
    {
        _audioSource.PlayOneShot(_walkingSFX, _walkingSFXVolume);
    }
    public void PlayRunningSFX()
    {
        _audioSource.PlayOneShot(_runningSFX, _runningSFXVolume);
    }
    public void PlayDamageSFX()
    {
        _audioSource.PlayOneShot(_damagedSFX, _damagedSFXVolume);
    }
    public void PlayStunnedSFX()
    {
        _audioSource.PlayOneShot(_damagedSFX, _damagedSFXVolume);
    }
    public void PlayDeadSFX()
    {
        _audioSource.PlayOneShot(_diesSFX, _diesSFXVolume);
    }

    public void VisualDamage()
    {
        StartCoroutine(VisualDamageCoroutine());
    }

    IEnumerator VisualDamageCoroutine()
    {
        SetMaterial(_damageMaterial);
        yield return new WaitForSeconds(_damageVisualTime);
        SetMaterial(_normalMaterial);

    }

    void SetMaterial(Material material)
    {
        _bodyRenderer.material = material;
    }
}