using System.Collections;
using UnityEngine;
using System;

public class EnemyFX : EntityFX
{
    #region Inspector

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
    [SerializeField] GameObject _runningAttack3VFX;
    [SerializeField] Transform _runningAttack3VFXOrigin;
    [SerializeField] AudioClip _runningAttack3SFX;
    [Range(0, 1)]
    [SerializeField] float _runningAttack3SFXVolume = 0.5f;
    [Header("AOEAttack")]
    [SerializeField] GameObject _aoeAttack3VFX;
    [SerializeField] Transform _aoeAttack3VFXOrigin;
    [SerializeField] AudioClip _aoeAttack3SFX;
    [Range(0, 1)]
    [SerializeField] float _aoeAttack3SFXVolume = 0.5f;

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
    #endregion



}