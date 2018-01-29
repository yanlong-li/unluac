AIBehaviour.spawning = {
  Name = "spawning",
  alertness = AIALERTNESS_IDLE,
  Constructor = function(self, entity)
    entity.AI.spawnState = AISS_SPAWNING
    if entity.AI.spawningEffectTime == nil or entity.AI.spawningEffectTime < 0.1 then
      entity.AI.spawningEffectTime = 0.1
    end
    if entity.unit then
      entity.unit:NpcActivateAggroUpdate(false)
      entity.unit:NpcActivateAITimeEvent(true, entity.AI.spawningEffectTime)
    end
    entity.unit:SetSoState("Busy")
    AI.BeginGoalPipe("spawning_wait")
    AI.PushGoal("bodypos", 1, BODYPOS_RELAX)
    AI.PushGoal("run", 1, 0)
    AI.EndGoalPipe()
    entity:SelectPipe(AIGOALPIPE_DONT_RESET_AG, "spawning_wait")
  end,
  Destructor = function(self, entity)
    if entity.unit then
      entity.unit:NpcActivateAITimeEvent(false, 0)
      entity.unit:NpcActivateAggroUpdate(true)
      entity.unit:RemoveBuff(NPC_SPAWN_INVINCIBLE_BUFF_TYPE)
    end
    entity.AI.spawnState = AISS_NORMAL
    entity.unit:SetDefaultSoState(false)
  end,
  OnAITimeEvent = function(self, entity, sender, data)
    entity.unit:NpcActivateAITimeEvent(false, 0)
    AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
  end,
  OnGroupLeaderDied = function(self, entity)
  end,
  OnGroupMemberDied = function(self, entity)
  end
}
