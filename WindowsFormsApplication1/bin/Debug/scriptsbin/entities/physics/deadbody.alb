DeadBody = {
  type = "DeadBody",
  isPhysicalized = 0,
  temp_ModelName = "",
  DeadBodyParams = {
    max_time_step = 0.025,
    gravityz = -7.5,
    sleep_speed = 0.025,
    damping = 0.3,
    freefall_gravityz = -9.81,
    freefall_damping = 0.1,
    lying_mode_ncolls = 4,
    lying_gravityz = -5,
    lying_sleep_speed = 0.065,
    lying_damping = 1.5,
    sim_type = 1,
    lying_simtype = 1
  },
  PhysParams = {
    mass = 80,
    height = 1.8,
    eyeheight = 1.7,
    sphereheight = 1.2,
    radius = 0.45
  },
  BulletImpactParams = {stiffness_scale = 73, max_time_step = 0.01},
  Properties = {
    soclasses_SmartObjectClass = "",
    bResting = 1,
    object_Model = "objects/characters/human/us/grunt/us_grunt_a.cdf",
    lying_gravityz = -5,
    lying_damping = 1.5,
    bCollidesWithPlayers = 0,
    bPushableByPlayers = 0,
    Mass = 80,
    bNoFriendlyFire = 0
  },
  Editor = {
    Icon = "DeadBody.bmp",
    IconOnTop = 1
  }
}
function DeadBody:OnLoad(table)
  self.isPhysicalized = table.isPhysicalized
  self.temp_ModelName = table.temp_ModelName
  self.PhysParams = table.PhysParams
  self.DeadBodyParams = table.DeadBodyParams
end
function DeadBody:OnSave(table)
  table.isPhysicalized = self.isPhysicalized
  table.temp_ModelName = self.temp_ModelName
  table.PhysParams = self.PhysParams
  table.DeadBodyParams = self.DeadBodyParams
end
function DeadBody:OnReset()
  self:LoadCharacter(0, self.Properties.object_Model)
  self:PhysicalizeThis()
end
function DeadBody:Server_OnInit()
  if self.isPhysicalized == 0 then
    DeadBody.OnPropertyChange(self)
    self.isPhysicalized = 1
  end
end
function DeadBody:Client_OnInit()
  if self.isPhysicalized == 0 then
    DeadBody.OnPropertyChange(self)
    self.isPhysicalized = 1
  end
  self:SetUpdatePolicy(ENTITY_UPDATE_PHYSICS_VISIBLE)
end
function DeadBody:Server_OnDamageDead(hit)
  if hit.ipart then
    self:AddImpulse(hit.ipart, hit.pos, hit.dir, hit.impact_force_mul)
  else
    self:AddImpulse(-1, hit.pos, hit.dir, hit.impact_force_mul)
  end
end
function DeadBody:OnHit()
  BroadcastEvent(self, "Hit")
end
function DeadBody:OnPropertyChange()
  self.PhysParams.mass = self.Properties.Mass
  if self.Properties.object_Model ~= self.temp_ModelName then
    self.temp_ModelName = self.Properties.object_Model
    self:LoadCharacter(0, self.Properties.object_Model)
  end
  self:PhysicalizeThis()
end
function DeadBody:PhysicalizeThis()
  local Properties = self.Properties
  local status = 1
  local bPushableByPlayers = Properties.bPushableByPlayers
  local bCollidesWithPlayers = Properties.bCollidesWithPlayers
  if status == 1 then
    bPushableByPlayers = 0
    bCollidesWithPlayers = 0
  end
  self.PhysParams.mass = Properties.Mass
  self.PhysParams.Living = self.DeadBodyParams
  self:Physicalize(0, PE_LIVING, self.PhysParams)
  self:Physicalize(0, PE_ARTICULATED, self.PhysParams)
  self:SetPhysicParams(PHYSICPARAM_SIMULATION, self.Properties)
  if Properties.lying_damping then
    self.DeadBodyParams.lying_damping = Properties.lying_damping
  end
  if Properties.lying_gravityz then
    self.DeadBodyParams.lying_gravityz = Properties.lying_gravityz
  end
  self:SetPhysicParams(PHYSICPARAM_SIMULATION, self.DeadBodyParams)
  self:SetPhysicParams(PHYSICPARAM_ARTICULATED, self.DeadBodyParams)
  local flagstab = {
    flags_mask = geom_colltype_player,
    flags = geom_colltype_player * bCollidesWithPlayers
  }
  if status == 1 then
    flagstab.flags_mask = geom_colltype_explosion + geom_colltype_ray + geom_colltype_foliage_proxy + geom_colltype_player
  end
  self:SetPhysicParams(PHYSICPARAM_PART_FLAGS, flagstab)
  flagstab.flags_mask = pef_pushable_by_players
  flagstab.flags = pef_pushable_by_players * bPushableByPlayers
  self:SetPhysicParams(PHYSICPARAM_FLAGS, flagstab)
  if Properties.bResting == 1 then
    self:AwakePhysics(0)
  else
    self:AwakePhysics(1)
  end
  self:EnableProceduralFacialAnimation(false)
  self:PlayFacialAnimation("death_pose_0" .. random(1, 5), true)
end
function DeadBody:Event_Hide()
  self:Hide(1)
end
function DeadBody:Event_UnHide()
  self:Hide(0)
end
function DeadBody:Event_Awake()
  self:AwakePhysics(1)
end
function DeadBody:Event_Hit(sender)
  BroadcastEvent(self, "Hit")
end
DeadBody.Server = {
  OnInit = DeadBody.Server_OnInit,
  OnDamage = DeadBody.Server_OnDamageDead,
  OnHit = DeadBody.OnHit,
  OnUpdate = DeadBody.OnUpdate
}
DeadBody.Client = {
  OnInit = DeadBody.Client_OnInit,
  OnDamage = DeadBody.Client_OnDamage,
  OnUpdate = DeadBody.OnUpdate
}
DeadBody.FlowEvents = {
  Inputs = {
    Awake = {
      DeadBody.Event_Awake,
      "bool"
    },
    Hide = {
      DeadBody.Event_Hide,
      "bool"
    },
    UnHide = {
      DeadBody.Event_UnHide,
      "bool"
    }
  },
  Outputs = {Awake = "bool", Hit = "bool"}
}
