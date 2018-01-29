Script.ReloadScript("SCRIPTS/Entities/AI/Shared/BasicAI.lua")
Script.ReloadScript("SCRIPTS/Entities/actor/BasicActor.lua")
Civilian_x = {
  AnimationGraph = "monster_full_body.xml",
  UpperBodyGraph = "",
  Properties = {
    attackrange = 0,
    species = 10,
    bSpeciesHostility = 0,
    fGroupHostility = 0,
    nVoiceID = 0,
    aicharacter_character = "Hostage",
    fileModel = "Objects/Characters/monster/flathead_orc_warrior/flathead_orc_warrior.chr",
    voiceType = "enemy",
    eiCharacterClass = 1,
    CharacterLevel = 1
  },
  gameParams = {
    inertia = 0,
    inertiaAccel = 0,
    backwardMultiplier = 0.5
  },
  AIMovementAbility = {
    pathFindPrediction = 0.5,
    usePredictiveFollowing = 1,
    walkSpeed = 2,
    runSpeed = 4,
    sprintSpeed = 6.4,
    b3DMove = 0,
    pathLookAhead = 1,
    pathRadius = 0.4,
    pathSpeedLookAheadPerSpeed = -1.5,
    cornerSlowDown = 0.75,
    maxAccel = 6,
    maneuverSpeed = 1.5,
    velDecay = 0.5,
    minTurnRadius = 0,
    maxTurnRadius = 3,
    maneuverTrh = 2,
    resolveStickingInTrace = 1,
    pathRegenIntervalDuringTrace = 4
  }
}
function Civilian_x:OnResetCustom()
  self.isFree = true
  self:HolsterItem(true)
  self:ResetOnUsed()
end
function Civilian_x:SetFree(rescuer)
  AI.Signal(SIGNALFILTER_SENDER, 0, "GET_UNTIED", self.id)
end
function Civilian_x:OnUsed(user)
  BroadcastEvent(self, "Used")
  AI.Signal(SIGNALFILTER_SENDER, 1, "USED", self.id)
end
function Civilian_x:Cower()
  if not self.AI.waiting then
    if not self.AI.Cower then
      self:InsertSubpipe(AIGOALPIPE_NOTDUPLICATE, "hostage_cower")
    else
      self:KillTimer(HOSTAGE_COWER_TIMER)
    end
    self.AI.Cower = true
    AI.ModifySmartObjectStates(self.id, "Cower")
    self:SetTimer(HOSTAGE_COWER_TIMER, 3000)
  end
end
