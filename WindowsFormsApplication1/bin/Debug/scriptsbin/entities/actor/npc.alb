Script.ReloadScript("SCRIPTS/Entities/actor/BasicActor.lua")
Script.ReloadScript("SCRIPTS/Entities/AI/Shared/BasicAI.lua")
npc = {
  Server = {},
  Client = {},
  PropertiesInstance = {},
  Properties = {},
  AnimationGraph = "monster_full_body.xml",
  UpperBodyGraph = "monster_upper_body.xml",
  ActorTemplate = {
    modelType = "actor",
    colliderEnergyScale = 10,
    colliderRagdollScale = 150,
    Properties = {aicharacter_character = "unknown", preferredCombatDistance = 20},
    PropertiesInstance = {aibehavior_behaviour = "unknown"},
    gameParams = {
      inertia = 30,
      inertiaAccel = 0,
      stance = {
        {stanceId = STANCE_STAND, maxSpeed = 12},
        {stanceId = STANCE_RELAXED, maxSpeed = 12}
      }
    },
    AIMovementAbility = {
      pathFindPrediction = 0.5,
      allowEntityClampingByAnimation = 1,
      usePredictiveFollowing = 1,
      pathSpeedLookAheadPerSpeed = -1.5,
      cornerSlowDown = 0.75,
      velDecay = 0.5,
      faceTargetInstantlyOnStrafing = false,
      lookIdleTurnSpeed = -1,
      lookCombatTurnSpeed = -1,
      aimTurnSpeed = -1,
      fireTurnSpeed = -1,
      directionalScaleRefSpeedMin = 1,
      directionalScaleRefSpeedMax = 8,
      AIMovementSpeeds = {
        Relaxed = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        },
        Combat = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        },
        Crouch = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        },
        Stealth = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        },
        Prone = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        },
        Swim = {
          Slow = {
            1.8,
            1.8,
            1.8
          },
          Walk = {
            1.8,
            1.8,
            1.8
          },
          Run = {
            5.4,
            5.4,
            5.4
          },
          Sprint = {
            5.4,
            5.4,
            5.4
          }
        }
      }
    }
  }
}
function npc.ActorTemplate:Kill(ragdoll, shooterId, weaponId, freeze)
  BasicActor.Kill(self, false, shooterId, weaponId, freeze)
  return true
end
function npc.ActorTemplate:HealthChanged()
  BasicActor.HealthChanged(self)
  if self.actor:GetHealth() <= 0 then
    return
  end
  self.actor:SetAnimationInput("Signal", "wound", true)
end
CreateActor(npc.ActorTemplate)
CreateAILite(npc.ActorTemplate)
function npc:Create(name, srcActor)
  local instance = {
    AIMovementAbility = {},
    gameParams = {}
  }
  if srcActor.AIMovementAbility then
    mergef(instance.AIMovementAbility, srcActor.AIMovementAbility, 1)
  end
  if srcActor.gameParams then
    mergef(instance.gameParams, srcActor.gameParams, 1)
  end
  mergef(instance, npc.ActorTemplate, 1)
  instance.Properties.fileModel = srcActor.Properties.fileModel
  instance.Properties.clientFileModel = srcActor.Properties.clientFileModel
  if srcActor.AnimationGraph then
    instance.AnimationGraph = srcActor.AnimationGraph
  end
  if srcActor.UpperBodyGraph then
    instance.UpperBodyGraph = srcActor.UpperBodyGraph
  end
  _G[name] = instance
end
