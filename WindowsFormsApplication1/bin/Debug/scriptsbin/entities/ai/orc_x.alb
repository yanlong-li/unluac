Script.ReloadScript("SCRIPTS/Entities/actor/BasicActor.lua")
Script.ReloadScript("SCRIPTS/Entities/AI/Shared/BasicAI.lua")
Orc_x = {
  colliderEnergyScale = 10,
  colliderRagdollScale = 150,
  AnimationGraph = "monster_full_body.xml",
  UpperBodyGraph = "",
  Properties = {
    voiceType = "enemy",
    fileModel = "Objects/Characters/monster/flathead_orc_warrior/flathead_orc_warrior.cdf",
    aicharacter_character = "Orc",
    Damage = {
      bNoGrab = 0,
      bNoDeath = 0,
      FallPercentage = 0,
      FallSleepTime = 2
    },
    distanceToHideFrom = 3,
    preferredCombatDistance = 20,
    eiCharacterClass = 1,
    CharacterLevel = 1
  },
  PropertiesInstance = {aibehavior_behaviour = "OrcIdle"},
  gameParams = {
    inertia = 0,
    inertiaAccel = 0,
    backwardMultiplier = 0.5
  },
  AIMovementAbility = {
    pathFindPrediction = 0.5,
    allowEntityClampingByAnimation = 1,
    usePredictiveFollowing = 1,
    walkSpeed = 2,
    runSpeed = 4,
    sprintSpeed = 6.4,
    b3DMove = 0,
    pathLookAhead = 1,
    pathRadius = 0.4,
    pathSpeedLookAheadPerSpeed = -1.5,
    cornerSlowDown = 0.75,
    maxAccel = 3,
    maxDecel = 8,
    maneuverSpeed = 1.5,
    velDecay = 0.5,
    minTurnRadius = 0,
    maxTurnRadius = 3,
    maneuverTrh = 2,
    resolveStickingInTrace = 1,
    pathRegenIntervalDuringTrace = 4,
    lightAffectsSpeed = 1,
    lookIdleTurnSpeed = 30,
    lookCombatTurnSpeed = 50,
    aimTurnSpeed = -1,
    fireTurnSpeed = -1,
    directionalScaleRefSpeedMin = 1,
    directionalScaleRefSpeedMax = 8,
    AIMovementSpeeds = {
      Relaxed = {
        Slow = {
          1,
          1,
          1.9
        },
        Walk = {
          1.3,
          1,
          1.9
        },
        Run = {
          4.5,
          2,
          7.2
        }
      },
      Combat = {
        Slow = {
          0.8,
          0.8,
          1.3
        },
        Walk = {
          1.3,
          0.8,
          1.3
        },
        Run = {
          4.5,
          2.3,
          6
        },
        Sprint = {
          6.5,
          2.3,
          6.5
        }
      },
      Crouch = {
        Slow = {
          0.5,
          0.3,
          1.3
        },
        Walk = {
          0.9,
          0.3,
          1.3
        },
        Run = {
          3.5,
          2.7,
          5.5
        }
      },
      Stealth = {
        Slow = {
          0.8,
          0.7,
          1
        },
        Walk = {
          0.9,
          0.7,
          1
        },
        Run = {
          3.5,
          2.7,
          5.5
        }
      },
      Prone = {
        Slow = {
          0.4,
          0.4,
          0.5
        },
        Walk = {
          0.5,
          0.4,
          0.5
        },
        Run = {
          0.5,
          0.4,
          0.5
        }
      },
      Swim = {
        Slow = {
          0.5,
          0.6,
          0.7
        },
        Walk = {
          0.6,
          0.6,
          0.7
        },
        Run = {
          3,
          2.9,
          4.3
        }
      }
    }
  },
  AI_changeCoverLastTime = 0,
  AI_changeCoverInterval = 7
}
function Orc_x:Kill(ragdoll, shooterId, weaponId, freeze)
  BasicActor.Kill(self, false, shooterId, weaponId, freeze)
  return true
end
function Orc_x:HealthChanged()
  BasicActor.HealthChanged(self)
  if self.actor:GetHealth() <= 0 then
    return
  end
  self.actor:SetAnimationInput("Signal", "wound", true)
end
