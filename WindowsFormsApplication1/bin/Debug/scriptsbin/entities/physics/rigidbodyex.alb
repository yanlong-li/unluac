Script.ReloadScript("Scripts/Entities/Physics/BasicEntity.lua")
RigidBodyEx = {
  Properties = {
    Physics = {
      bRigidBodyActive = 1,
      bActivateOnDamage = 0,
      bResting = 1,
      bCanBreakOthers = 0,
      Simulation = {
        max_time_step = 0.02,
        sleep_speed = 0.04,
        damping = 0,
        bFixedDamping = 0,
        bUseSimpleSolver = 0
      },
      Buoyancy = {
        water_density = 1000,
        water_damping = 0,
        water_resistance = 1000
      }
    }
  }
}
local Physics_DX9MP_Simple = {
  bRigidBodyActive = 0,
  bActivateOnDamage = 0,
  bResting = 1,
  Simulation = {
    max_time_step = 0.02,
    sleep_speed = 0.04,
    damping = 0,
    bFixedDamping = 0,
    bUseSimpleSolver = 0
  },
  Buoyancy = {
    water_density = 1000,
    water_damping = 0,
    water_resistance = 1000
  }
}
MakeDerivedEntity(RigidBodyEx, BasicEntity)
function RigidBodyEx:OnLoad(table)
  self.bRigidBodyActive = table.bRigidBodyActive
end
function RigidBodyEx:OnSave(table)
  table.bRigidBodyActive = self.bRigidBodyActive
end
function RigidBodyEx:OnSpawn()
  Log("RigidBodyEx:OnSpawn()")
  if self.Properties.Physics.bRigidBodyActive == 1 then
    self.bRigidBodyActive = 1
  end
  self:SetFromProperties()
end
function RigidBodyEx.Client:OnStartGame()
  if self.Properties.Physics.bPhysicalize == 1 then
    self:PhysicalizeThis()
  end
end
function RigidBodyEx.Server:OnStartGame()
  if self.Properties.Physics.bPhysicalize == 1 then
    self:PhysicalizeThis()
  end
end
function RigidBodyEx:SetFromProperties()
  local Properties = self.Properties
  if Properties.object_Model == "" then
    return
  end
  self:LoadObject(0, Properties.object_Model)
  self:CharacterUpdateOnRender(0, 1)
  if Properties.object_ModelFrozen ~= "" then
    self.frozenModelSlot = self:LoadObject(-1, Properties.object_ModelFrozen)
    self:DrawSlot(self.frozenModelSlot, 0)
  else
    self.frozenModelSlot = nil
  end
  self:DrawSlot(0, 1)
  if self.Properties.bAutoGenAIHidePts == 1 then
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 0)
  else
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 2)
  end
  Log("bgimmick(" .. tostring(Properties.bGimmick) .. ")")
  if Properties.bGimmick == 1 then
    self:SetFlags(ENTITY_FLAG_STATIC_GIMMICK, 0)
  end
end
function RigidBodyEx:PhysicalizeThis()
  local physics = self.Properties.Physics
  EntityCommon.PhysicalizeRigid(self, 0, physics, self.bRigidBodyActive)
end
function RigidBodyEx:OnPropertyChange()
  self:SetFromProperties()
end
function RigidBodyEx:OnReset()
  self:ResetOnUsed()
  local PhysProps = self.Properties.Physics
  if PhysProps.bPhysicalize == 1 then
    if self.bRigidBodyActive ~= PhysProps.bRigidBodyActive then
      self.bRigidBodyActive = PhysProps.bRigidBodyActive
      self:PhysicalizeThis()
    end
    if PhysProps.bRigidBody == 1 then
      self:AwakePhysics(1 - PhysProps.bResting)
      self.bRigidBodyActive = PhysProps.bRigidBodyActive
    end
  end
end
function RigidBodyEx:GetFrozenSlot()
  if self.frozenModelSlot then
    return self.frozenModelSlot
  end
  return -1
end
function RigidBodyEx:Event_Remove()
  self:DrawSlot(0, 0)
  self:DestroyPhysics()
  self:ActivateOutput("Remove", true)
end
function RigidBodyEx:Event_Hide()
  self:Hide(1)
  self:ActivateOutput("Hide", true)
end
function RigidBodyEx:Event_UnHide()
  self:Hide(0)
  self:ActivateOutput("UnHide", true)
end
function RigidBodyEx:Event_RagDollize()
  self:RagDollize(0)
  self:ActivateOutput("RagDollized", true)
end
function RigidBodyEx.Client:OnPhysicsBreak(vPos, nPartId, nOtherPartId)
  self:ActivateOutput("Break", nPartId + 1)
end
function RigidBodyEx.Client:OnCollision(hit)
end
function RigidBodyEx:OnDamage(hit)
  if self:IsARigidBody() == 1 and self.Properties.Physics.bActivateOnDamage == 1 and hit.explosion and self:GetState() ~= "Activated" then
    BroadcastEvent(self, "Activate")
    self:GotoState("AcTivated")
  end
  if hit.ipart and hit.ipart >= 0 then
    self:AddImpulse(hit.ipart, hit.pos, hit.dir, hit.impact_force_mul)
  end
end
function RigidBodyEx:Event_Activate(sender)
  self:GotoState("Activated")
end
function RigidBodyEx:CommonSwitchToMaterial(numStr)
  if not self.sOriginalMaterial then
    self.sOriginalMaterial = self:GetMaterial()
  end
  if self.sOriginalMaterial then
    self:SetMaterial(self.sOriginalMaterial .. numStr)
  end
end
function RigidBodyEx:Event_SwitchToMaterialOriginal(sender)
  self:CommonSwitchToMaterial("")
end
function RigidBodyEx:Event_SwitchToMaterial1(sender)
  self:CommonSwitchToMaterial("1")
end
function RigidBodyEx:Event_SwitchToMaterial2(sender)
  self:CommonSwitchToMaterial("2")
end
RigidBodyEx.Default = {
  OnBeginState = function(self)
    if self:IsARigidBody() == 1 then
      if self.bRigidBodyActive ~= self.Properties.Physics.bRigidBodyActive then
        self:SetPhysicsProperties(0, self.Properties.Physics.bRigidBodyActive)
      else
        self:AwakePhysics(1 - self.Properties.Physics.bResting)
      end
    end
  end,
  OnDamage = RigidBodyEx.OnDamage,
  OnCollision = RigidBodyEx.OnCollision,
  OnPhysicsBreak = RigidBodyEx.OnPhysicsBreak
}
RigidBodyEx.Activated = {
  OnBeginState = function(self)
    if self:IsARigidBody() == 1 and self.bRigidBodyActive == 0 then
      self:SetPhysicsProperties(0, 1)
      self:AwakePhysics(1)
    end
  end,
  OnDamage = RigidBodyEx.OnDamage,
  OnCollision = RigidBodyEx.OnCollision,
  OnPhysicsBreak = RigidBodyEx.OnPhysicsBreak
}
RigidBodyEx.FlowEvents = {
  Inputs = {
    Used = {
      RigidBodyEx.Event_Used,
      "bool"
    },
    EnableUsable = {
      RigidBodyEx.Event_EnableUsable,
      "bool"
    },
    DisableUsable = {
      RigidBodyEx.Event_DisableUsable,
      "bool"
    },
    Activate = {
      RigidBodyEx.Event_Activate,
      "bool"
    },
    Hide = {
      RigidBodyEx.Event_Hide,
      "bool"
    },
    UnHide = {
      RigidBodyEx.Event_UnHide,
      "bool"
    },
    Remove = {
      RigidBodyEx.Event_Remove,
      "bool"
    },
    RagDollize = {
      RigidBodyEx.Event_RagDollize,
      "bool"
    },
    SwitchToMaterial1 = {
      RigidBodyEx.Event_SwitchToMaterial1,
      "bool"
    },
    SwitchToMaterial2 = {
      RigidBodyEx.Event_SwitchToMaterial2,
      "bool"
    },
    SwitchToMaterialOriginal = {
      RigidBodyEx.Event_SwitchToMaterialOriginal,
      "bool"
    }
  },
  Outputs = {
    Used = "bool",
    EnableUsable = "bool",
    DisableUsable = "bool",
    Activate = "bool",
    Hide = "bool",
    UnHide = "bool",
    Remove = "bool",
    RagDollized = "bool",
    Break = "int"
  }
}
