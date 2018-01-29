X2AI.commandSet = {
  runType = AICSRT_NONE,
  commanderUnit = nil,
  commands = {},
  canInteract = true,
  curCmdIdx = 0,
  curCmdIdxState = AICIS_DONE,
  commandStepStartTime = 0,
  commandRunningTime = 0,
  prevs = {}
}
local commandSet = X2AI.commandSet
local GoToRunIfNotInCombat = function(entityId)
  if AI.GetAlertness(entityId) >= AIALERTNESS_COMBAT then
    return
  end
  AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entityId)
end
function commandSet:RunCommandSet(entity, cmdTable)
  local MAX_COMMAND_SETS = 5
  if table.getn(self.prevs) >= MAX_COMMAND_SETS - 1 then
    return
  end
  if self:CanRunCommandSet() then
    if self.canInteract == false and self.commanderUnit ~= nil and cmdTable.commanderUnit ~= nil and cmdTable.commanderUnit ~= self.commanderUnit then
      return
    end
    prevCmdSet = {}
    merge(prevCmdSet, self, true)
    prevCmdSet.prevs = nil
    table.insert(self.prevs, prevCmdSet)
  end
  self.runType = cmdTable.runType
  self.commanderUnit = cmdTable.commanderUnit
  self.commands = cmdTable.commands
  self.canInteract = cmdTable.canInteract
  self.curCmdIdx = 0
  self.curCmdIdxState = AICIS_DONE
  self.commandStepStartTime = 0
  self.commandRunningTime = 0
  entity.unit:NpcSetInteractable(self.canInteract)
  AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
end
function commandSet:Clear(entity)
  self.runType = nil
  self.commanderUnit = nil
  self.commands = nil
  self.canInteract = nil
  self.curCmdIdx = nil
  self.curCmdIdxState = nil
  self.commandStepStartTime = nil
  self.commandRunningTime = nil
  self.prevs = nil
  mergeOverwrite(self, X2AI.commandSet, true)
  entity.unit:NpcSetInteractable(false)
  GoToRunIfNotInCombat(entity.id)
end
function commandSet:CopyAndRun(entity, srcCmdSet)
  if self.canInteract == false and self.commanderUnit ~= nil and srcCmdSet.commanderUnit ~= nil and srcCmdSet.commanderUnit ~= self.commanderUnit then
    return
  end
  mergeOverwrite(self, srcCmdSet, true)
  entity.unit:NpcSetInteractable(self.canInteract)
  GoToRunIfNotInCombat(entity.id)
end
function commandSet:CanRunCommandSet()
  if self.runType == AICSRT_NONE then
    return false
  end
  local commandCount = table.getn(self.commands)
  if self.runType == AICSRT_RUN_ONCE and (commandCount > self.curCmdIdx or self.curCmdIdx == commandCount and self.curCmdIdxState == AICIS_PROCESSING) then
    return true
  else
  end
  return false
end
function commandSet:EndCommandSet(entity)
  local prevsCount = table.getn(self.prevs)
  if prevsCount < 1 then
    if self.runType ~= AICSRT_NONE then
      entity.unit:NpcSetInteractable(true)
    end
    self.runType = AICSRT_NONE
    AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_IDLE", entity.id)
    return
  end
  mergeOverwriteF(self, self.prevs[prevsCount], true)
  table.remove(self.prevs, prevsCount)
  entity.unit:NpcSetInteractable(self.canInteract)
  AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
end
function commandSet:RunCommandSetStep(entity)
  if self.curCmdIdxState == AICIS_DONE then
    self.curCmdIdx = self.curCmdIdx + 1
    self.commandRunningTime = 0
  end
  local cmd = self.commands[self.curCmdIdx]
  if cmd == nil and self.runType == AICSRT_RUN_ONCE then
    self:EndCommandSet(entity)
    return
  else
  end
  self.commandStepStartTime = System.GetCurrTime()
  local runWell = true
  if cmd[1] == AICC_FOLLOW_UNIT then
    if self.commanderUnit then
      runWell = entity.unit:NpcFollowUnit(self.commanderUnit)
    else
      runWell = false
    end
  elseif cmd[1] == AICC_FOLLOW_PATH then
    runWell = entity.unit:NpcFollowPath(cmd[3], cmd[2], AISPEED_AUTO_DETECT)
  elseif cmd[1] == AICC_USE_SKILL then
    X2AI:UseSkillPipe(entity, 0, "ByCommandSet")
    runWell = true
  elseif cmd[1] == AICC_TIMEOUT then
    local waitTime = cmd[2] - self.commandRunningTime
    X2AI:WaitFor(entity, waitTime)
  else
    runWell = false
  end
  if runWell then
    self.curCmdIdxState = AICIS_PROCESSING
  else
    self.curCmdIdxState = AICIS_DONE
    AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
  end
end
function commandSet:GetCurrentCommand()
  if self:CanRunCommandSet() then
    return self.commands[self.curCmdIdx][1]
  end
  return 0
end
function commandSet:GetCurrentCommandWithParam()
  if self:CanRunCommandSet() then
    return self.commands[self.curCmdIdx]
  end
  return 0
end
function commandSet:GetCurrentProcessingCommand()
  if self:CanRunCommandSet() and self.curCmdIdxState == AICIS_PROCESSING then
    return self.commands[self.curCmdIdx][1]
  end
  return 0
end
function commandSet:OnCommandCompleted(entity, cmdType)
  if cmdType <= 0 or self:GetCurrentProcessingCommand() ~= cmdType then
    return
  end
  self.curCmdIdxState = AICIS_DONE
  AI.Signal(SIGNALFILTER_SENDER, AISIGNAL_INCLUDE_DISABLED, "GO_TO_RUN_COMMAND_SET", entity.id)
end
