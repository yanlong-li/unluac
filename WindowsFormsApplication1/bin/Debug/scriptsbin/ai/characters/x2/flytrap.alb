AICharacter.flytrap = {
  Class = UNIT_CLASS_INFANTRY,
  TeamRole = GU_HUMAN_COVER,
  Constructor = function(self, entity)
    entity.AI.idlePos = entity:GetPos()
    entity.AI.idleDir = entity:GetDirectionVector()
    entity.AI.param = {
      msgs = {},
      alertDuration = 3,
      alertSafeTargetRememberTime = 5,
      attackEndDistance = entity.Properties.Perception.sightrange,
      meleeAttackRange = entity.Properties.attackStartRange,
      combatSkills = {
        melee = {},
        ranged = {}
      }
    }
    function entity.AI:SetParamX2(paramBlock)
      local paramF = loadstring("return {" .. paramBlock .. "}")
      if paramF ~= nil then
        local param = paramF()
        mergeOverwriteF(entity.AI.param, param, true)
      else
        local pos = entity:GetPos()
        System.Error("NPC AI param is incorrect!!! / unit id: " .. tostring(entity.unit:GetId()) .. ", unit name: " .. tostring(entity.unit:GetName()) .. ", pos: (" .. tostring(pos.x) .. ", " .. tostring(pos.y) .. ", " .. tostring(pos.z) .. ")")
      end
    end
    AI.SetSkillUsePattern(entity.id, SUP_FLYTRAP)
  end,
  AnyBehavior = {
    GO_TO_SPAWN = "spawning",
    GO_TO_IDLE = "hold_position",
    GO_TO_RUN_COMMAND_SET = "run_command_set",
    GO_TO_TALK = "talk",
    GO_TO_ALERT = "flytrap_alert",
    GO_TO_COMBAT = "flytrap_attack",
    GO_TO_DEAD = "dead",
    GO_TO_DESPAWN = "despawning"
  },
  spawning = {},
  hold_position = {
    OnAggroTargetChanged = "flytrap_attack",
    ReturnToIdlePos = "return_state",
    OnTalk = "talk"
  },
  run_command_set = {
    OnAggroTargetChanged = "flytrap_attack",
    OnTalk = "talk"
  },
  talk = {
    OnReturnToTalkPos = "return_state",
    OnAggroTargetChanged = "flytrap_attack"
  },
  flytrap_alert = {
    OnAggroTargetChanged = "flytrap_attack"
  },
  flytrap_attack = {},
  return_state = {},
  dead = {},
  despawning = {}
}
