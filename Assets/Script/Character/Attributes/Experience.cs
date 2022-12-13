using System;
using System.Collections;
using System.Collections.Generic;
using Script.Character;
using UnityEngine;
[Serializable]
public class Experience
{
 [HideInInspector] public int points, pointsGoal = 50;
  public int level, reward;
  int freePoint;
  private UI _ui;
  private Character _character;
  
  public int ExpBar() => points / pointsGoal;
  void RaiseGoal() => pointsGoal += pointsGoal / 4;
  public void SpendFreePoint()=>freePoint--;
  public void Initialize(UI ui, Character character)
  {
    _ui = ui;
    _character = character;
  }
  
  void LevelUp()
  {
    if (points >= pointsGoal)
    {
      freePoint = 1;
      level += 1;
      points -= pointsGoal;
      RaiseGoal();
      _ui.UIExperience.ActivateButtons(true);
    } 
  }
  
  
  public void Add(int i)
  {
    if (i > 0)
    {
      _ui.ShowPopUp(AttackResult.Exp,_character.Position,i);
      points += i;
      LevelUp();
    }
  }
 }
