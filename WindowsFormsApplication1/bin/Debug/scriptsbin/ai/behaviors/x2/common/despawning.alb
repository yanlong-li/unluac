AIBehaviour.despawning = {
  Name = "despawning",
  alertness = AIALERTNESS_IDLE,
  Constructor = function(self, entity)
    entity.AI.spawnState = AISS_DESPAWNING
    AI.BeginGoalPipe("despawning_wait")
    AI.PushGoal("timeout", 1, 10)
    AI.EndGoalPipe()
    entity:SelectPipe(AIGOALPIPE_DONT_RESET_AG, "despawning_wait")
  end,
  Destructor = function(self, entity)
  end,
  OnEnemySeen = function(self, entity)
  end,
  OnFriendSeen = function(self, entity)
  end,
  OnAlertTargetChanged = function(self, entity, sender, data)
  end,
  OnGroupLeaderDied = function(self, entity)
  end,
  OnGroupMemberDied = function(self, entity)
  end
}
