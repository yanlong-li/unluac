AIBehaviour.flytrap_alert = {
  Name = "flytrap_alert",
  alertness = AIALERTNESS_ALERT,
  Constructor = function(self, entity)
    local target = AI.GetAttentionTargetEntity(entity.id)
    if target == nil then
      AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
      return
    end
    if entity.Properties.bSpeciesHostility ~= 0 then
      entity.unit:NpcAddAggro(target.unit, 1)
      return
    end
    AI.BeginGoalPipe("flytrapAlert_default")
    AI.PushGoal("strafe", 1, 2, 50, 1)
    AI.PushGoal("bodypos", 1, BODYPOS_STAND)
    AI.PushGoal("run", 1, 0)
    AI.PushGoal("timeout", 1, 1)
    AI.PushGoal("signal", 1, AISIGNAL_INCLUDE_DISABLED, "UpdateAlert", SIGNALFILTER_SENDER)
    AI.EndGoalPipe()
    entity:SelectPipe(AIGOALPIPE_DONT_RESET_AG, "flytrapAlert_default")
    local skillCount = table.getn(entity.AI.skills.onAlert)
    if skillCount > 0 then
      X2AI:UseSkill(entity, 0, "onAlert")
    end
    if entity.AI.param.alwaysTeleportOnReturn then
      entity.unit:NpcSetCheckPosContext(AICPC_FLYTRAP)
    end
  end,
  Destructor = function(self, entity)
    entity.unit:NpcSetCheckPosContext(AICPC_NONE)
  end,
  UpdateAlert = function(self, entity, sender, data)
    local target = AI.GetAttentionTargetEntity(entity.id)
    if target ~= nil and (AI.IsInSight(entity.id, target.id) == false or target.unit:IsValidNpcTarget(entity.unit) == false) then
      entity:SelectPipe(AIGOALPIPE_DONT_RESET_AG, "common_RunCommandSetAfterHalfSec")
      return
    end
    X2AI:UseSkill(entity, 0, "alert")
  end,
  OnRequestSkillInfo_onAlert = function(self, entity)
    entity.unit:NpcSetSkillList(entity.AI.skills.onAlert)
  end,
  OnRequestSkillInfo_alert = function(self, entity)
    entity.unit:NpcSetSkillList(entity.AI.skills.alert)
  end,
  OnAggroTargetChanged = function(self, entity, sender, data)
    AI.SetRefPointPosition(entity.id, entity:GetPos())
    AI.SetAttentionTargetOf(entity.id, data.id, AITARGETREASON_ATTACK)
  end
}
