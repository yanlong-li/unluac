Script.ReloadScript("Scripts/Entities/Physics/BasicEntity.lua")
AnimObject = {
  Properties = {
    Animation = {
      Animation = "Default",
      Speed = 1,
      bLoop = 0,
      bPlaying = 0,
      bAlwaysUpdate = 0,
      playerAnimationState = "",
      bPhysicalizeAfterAnimation = 0
    },
    Physics = {bArticulated = 0},
    ActivatePhysicsThreshold = 0,
    ActivatePhysicsDist = 50,
    bNoFriendlyFire = 0
  },
  PHYSICALIZEAFTER_TIMER = 1,
  POSTQL_TIMER = 2
}
MakeDerivedEntity(AnimObject, BasicEntity)
function AnimObject:SetFromProperties()
  self.__super.SetFromProperties(self)
  self.animstarted = 0
  local Properties = self.Properties
  self.bAnimPlaying = Properties.Animation.bPlaying
  self.bAnimLoop = Properties.Animation.bLoop
  self.AnimName = Properties.Animation.Animation
  if Properties.Animation.bPlaying == 1 then
    self:DoStartAnimation()
  else
    self:ResetAnimation(0, -1)
  end
  if Properties.Animation.bAlwaysUpdate == 1 then
    self:Activate(1)
  end
  self:SetAnimationSpeed(0, 0, Properties.Animation.Speed)
  self.animationSpeed = Properties.Animation.Speed
  self.curAnimTime = 0
  if 0 < self.Properties.ActivatePhysicsThreshold then
    local apd = {
      threshold = self.Properties.ActivatePhysicsThreshold,
      detach_distance = self.Properties.ActivatePhysicsDist
    }
    self:SetPhysicParams(PHYSICPARAM_AUTO_DETACHMENT, apd)
  end
end
function AnimObject:OnReset()
  self.__super.OnReset(self)
  self.bAnimPlaying = 0
  self:SetFromProperties()
end
function AnimObject:Event_ResetAnimation()
  self:ResetAnimation(0, -1)
  self.animstarted = 0
end
function AnimObject:Event_StartAnimation(sender)
  self:StartEntityAnimation()
  self.animstarted = 1
end
function AnimObject:Event_StopAnimation(sender)
  if self.animstarted == 1 and self:IsAnimationRunning(0, 0) then
    self.curAnimTime = self:GetAnimationTime(0, 0)
  else
    self.curAnimTime = 0
  end
  self:StopAnimation(0, -1)
  self.animstarted = 0
end
function AnimObject:DoStartAnimation()
  self:StartAnimation(0, self.Properties.Animation.Animation, 0, 0, 1, self.Properties.Animation.bLoop, 1)
  self:ForceCharacterUpdate(0, true)
  self.animstarted = 1
  if 0 > self.Properties.Animation.Speed then
    self.curAnimTime = 0
  else
    self.curAnimTime = self:GetAnimationLength(0, self.Properties.Animation.Animation)
  end
end
function AnimObject:StartEntityAnimation()
  self:StopAnimation(0, -1)
  self:DoStartAnimation()
  self.bStopAnimAfterQL = false
  self:KillTimer(self.POSTQL_TIMER)
  local playerAnimationState = self.Properties.Animation.playerAnimationState
  if g_localActor and playerAnimationState ~= "" then
    g_localActor.actor:CreateCodeEvent({
      event = "animationControl",
      pos = self:GetWorldPos(),
      angle = self:GetWorldAngles()
    })
    g_localActor.actor:QueueAnimationState(playerAnimationState)
    if self.Properties.Animation.bPhysicalizeAfterAnimation == 1 then
      local animLen = self:GetAnimationLength(0, self.Properties.Animation.Animation) * 1000 / self.Properties.Animation.Speed
      self:SetTimer(self.PHYSICALIZEAFTER_TIMER, animLen)
    end
  end
end
function AnimObject.Client:OnTimer(timerId, mSec)
  if timerId == self.PHYSICALIZEAFTER_TIMER then
    local PhysProps = self.Properties.Physics
    PhysProps.bRigidBodyActive = 1
    PhysProps.bPhysicalize = 1
    PhysProps.bRigidBody = 1
    PhysProps.bResting = 0
    if self.bRigidBodyActive ~= PhysProps.bRigidBodyActive then
      self.bRigidBodyActive = PhysProps.bRigidBodyActive
      self:PhysicalizeThis()
    end
    if PhysProps.bRigidBody == 1 then
      self:AwakePhysics(1 - PhysProps.bResting)
      self.bRigidBodyActive = PhysProps.bRigidBodyActive
    end
  end
  if timerId == self.POSTQL_TIMER then
    self:StopAnimation(0, -1)
  end
end
function AnimObject.Client:OnUpdate(dt)
  if self.bStopAnimAfterQL then
    self.bStopAnimAfterQL = false
    self:SetTimer(self.POSTQL_TIMER, 0.2)
    if self.Properties.Animation.bAlwaysUpdate ~= 1 then
      self:Activate(0)
    end
  end
end
function AnimObject:OnLoad(table)
  self.bAnimPlaying = table.bAnimPlaying
  self.bAnimLoop = table.bAnimLoop
  self.AnimName = table.AnimName
  self.animstarted = table.animstarted
  if self.animstarted == 1 then
    self:StartEntityAnimation()
    self:SetAnimationTime(0, 0, table.animTime)
  elseif 0 < table.animTime then
    self:StartEntityAnimation()
    self:SetAnimationSpeed(0, 0, 0)
    self:SetAnimationTime(0, 0, table.animTime)
    self.bStopAnimAfterQL = true
    self:Activate(1)
  else
    self:ResetAnimation(0, -1)
  end
end
function AnimObject:OnSave(table)
  table.bAnimPlaying = self.bAnimPlaying
  table.bAnimLoop = self.bAnimLoop
  table.AnimName = self.AnimName
  if self.animstarted == 1 and self:IsAnimationRunning(0, 0) then
    table.animTime = self:GetAnimationTime(0, 0)
    table.animstarted = 1
  else
    table.animstarted = 0
    if self.curAnimTime then
      table.animTime = self.curAnimTime
    else
      table.animTime = 0
    end
  end
end
AnimObject.FlowEvents.Inputs.ResetAnimation = {
  AnimObject.Event_ResetAnimation,
  "bool"
}
AnimObject.FlowEvents.Inputs.StartAnimation = {
  AnimObject.Event_StartAnimation,
  "bool"
}
AnimObject.FlowEvents.Inputs.StopAnimation = {
  AnimObject.Event_StopAnimation,
  "bool"
}
