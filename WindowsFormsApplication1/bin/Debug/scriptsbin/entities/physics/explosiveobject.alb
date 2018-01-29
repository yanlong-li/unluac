ExplosiveObject = {
  Properties = {
    bAutoGenAIHidePts = 0,
    objModel = "objects/default.cgf",
    Explosion = {
      Effect = "explosions.grenade_air.explosion",
      EffectScale = 1,
      Radius = 10,
      Pressure = 1000,
      Damage = 1000,
      HoleSize = 10,
      Timer = 30,
      Direction = {
        x = 0,
        y = 0,
        z = 1
      }
    },
    bExplodeOnHit = 1,
    bPlayerOnly = 1,
    fDensity = 5000,
    fMass = 10,
    bResting = 1,
    bRigidBody = 0,
    UsageText = "Press USE to set the timer!",
    UsageRadius = 5,
    TimeLeftText = "%d...",
    PhysicsBuoyancy = {
      water_density = 1,
      water_damping = 1.5,
      water_resistance = 0
    },
    PhysicsSimulation = {
      max_time_step = 0.01,
      sleep_speed = 0.04,
      damping = 0
    }
  },
  Client = {},
  Server = {}
}
function ExplosiveObject:OnSpawn()
  local model = self.Properties.objModel
  if string.len(model) > 0 then
    local ext = string.lower(string.sub(model, -4))
    if ext == ".chr" or ext == ".cdf" or ext == ".cga" then
      self:LoadCharacter(0, model)
    else
      self:LoadObject(0, model)
    end
  end
end
function ExplosiveObject:OnInit()
  self:OnReset()
  self:PreLoadParticleEffect(self.Properties.Explosion.Effect)
end
function ExplosiveObject:OnReset()
  local params = {
    mass = self.Properties.fMass,
    density = self.Properties.fDensity
  }
  local bRigidBody = tonumber(self.Properties.bRigidBody) ~= 0
  if bRigidBody then
    self:Physicalize(0, PE_RIGID, params)
    if tonumber(self.Properties.bResting) ~= 0 then
      self:AwakePhysics(0)
    else
      self:AwakePhysics(1)
    end
    self:SetPhysicParams(PHYSICPARAM_BUOYANCY, self.Properties.PhysicsBuoyancy)
    self:SetPhysicParams(PHYSICPARAM_SIMULATION, self.Properties.PhysicsSimulation)
  else
    self:Physicalize(0, PE_STATIC, params)
  end
  self:Activate(0)
  self.active = false
  self.timer = nil
  if self.useMessageId then
    HUD:SetInstructionObsolete(self.useMessageId)
    self.useMessageId = nil
  end
  if self.Properties.bAutoGenAIHidePts == 1 then
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 0)
  else
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 2)
  end
end
function ExplosiveObject:OnPropertyChange()
  self:OnReset()
end
function ExplosiveObject:OnDestroy()
end
function ExplosiveObject.Server:OnHit(shooterId, weaponId, matName, damage)
  local playerOnly = NumberToBool(self.Properties.bPlayerOnly)
  local playerHit = shooterId == g_localActorId
  if not shooterId or not playerOnly or playerHit then
    self:Event_Hit()
  end
end
function ExplosiveObject:OnSave(save)
  save.timer = self.timer
  save.active = self.active
  save.userId = self.userId
end
function ExplosiveObject:OnLoad(saved)
  self.userId = saved.userId
  self.active = saved.active
  self.timer = saved.timer
end
function ExplosiveObject:OnUsed(user)
  if not self.active then
    self.userId = user.id
    self:Event_StartTimer()
  else
    self.userId = nil
    self:Event_StopTimer()
  end
  self:Event_Used()
end
function ExplosiveObject.Client:OnUpdate(frameTime)
  if self.active then
    self.timer = self.timer - frameTime
    if self.timer <= 0 then
      self:Event_Explode()
    elseif math.floor(self.timer + frameTime) > self.timer then
      HUD:AddInfoMessage(string.format(self.Properties.TimeLeftText, math.floor(self.timer)), 0.95)
    end
  end
end
function ExplosiveObject:Explode(shooterId)
  self:Activate(0)
  System.RemoveEntity(self.id)
  local props = self.Properties.Explosion
  g_gameRules:CreateExplosion(shooterId or NULL_ENTITY, weaponId or NULL_ENTITY, damage, self:GetWorldPos(), props.Direction, props.Radius, nil, props.Pressure, props.HoleSize, props.Effect, props.EffectScale)
  if self.useMessageId then
    HUD:SetInstructionObsolete(self.useMessageId)
    self.useMessageId = nil
  end
end
function ExplosiveObject:IsUsable(user)
  if self.active then
    return 0
  end
  local mp = System.IsMultiplayer()
  if mp and mp ~= 0 then
    return 0
  end
  self.userpos = user:GetWorldPos(self.userpos)
  self.pos = self:GetWorldPos(self.pos)
  local radius = self.Properties.UsageRadius
  if vecDistanceSq(self.userpos, self.pos) < radius * radius then
    return 1
  else
    return 0
  end
end
function ExplosiveObject:GetUsableMessage()
  return self.Properties.UsageText
end
function ExplosiveObject:Event_Explode()
  self:Explode(NULL_ENTITY)
  BroadcastEvent(self, "Explode")
end
function ExplosiveObject:Event_Hit(sender)
  BroadcastEvent(self, "Hit")
end
function ExplosiveObject:Event_StartTimer()
  self.active = true
  self.timer = math.floor(self.Properties.ExplosionTimer + 0.5) + 0.5
  self:Activate(1)
  if self.useMessageId then
    HUD:SetInstructionObsolete(self.useMessageId)
    self.useMessageId = nil
  end
end
function ExplosiveObject:Event_StopTimer()
  if tonumber(self.Properties.bTimerStoppable) ~= 0 then
    self.active = false
    self.timer = nil
    self:Activate(0)
  end
end
function ExplosiveObject:Event_Used()
  BroadcastEvent(self, "Used")
end
ExplosiveObject.FlowEvents = {
  Inputs = {
    StartTimer = {
      ExplosiveObject.Event_StartTimer,
      "bool"
    },
    StopTimer = {
      ExplosiveObject.Event_StopTimer,
      "bool"
    },
    Hit = {
      ExplosiveObject.Event_Hit,
      "bool"
    },
    Used = {
      ExplosiveObject.Event_Used,
      "bool"
    }
  },
  Outputs = {
    StartTimer = "bool",
    StopTimer = "bool",
    Hit = "bool",
    Used = "bool"
  }
}
