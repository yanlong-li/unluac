RelicEffect = {
  Properties = {},
  Editor = {
    Icon = "Tornado.bmp"
  }
}
function RelicEffect:OnInit()
  self.oldVelocity = {
    x = 0,
    y = 0,
    z = 0
  }
  self.occupied = 0
  self.lasttime = 0
  self:OnReset()
end
function RelicEffect:OnPropertyChange()
  self:OnReset()
end
function RelicEffect:OnReset()
end
function RelicEffect:OnProceedFadeArea(player, areaId, fadeCoeff)
  self:Fade(fadeCoeff)
end
function RelicEffect:ResetValues()
  System.SetWind(self.oldVelocity)
end
function RelicEffect:OnEnterArea(player, areaId)
  if player and not player.actor:IsPlayer() or self.occupied == 1 then
    return
  end
  self.oldVelocity = System.GetWind()
  self.occupied = 1
end
function RelicEffect:OnLeaveArea(player, areaId)
  if player and not player.actor:IsPlayer() then
    return
  end
  self:ResetValues()
  self.occupied = 0
end
function RelicEffect:OnShutDown()
end
function RelicEffect:Event_StartOccupying(sender)
  self:ActivateOutput("StartOccupying", true)
end
function RelicEffect:Event_StartFreeing(sender)
  self:ActivateOutput("StartFreeing", true)
end
function RelicEffect:Event_Occupied(sender)
  self:ActivateOutput("Occupied", true)
end
function RelicEffect:Event_Freed(sender)
  self:ActivateOutput("Freed", true)
end
RelicEffect.FlowEvents = {
  Inputs = {
    StartOccupying = {
      RelicEffect.Event_StartOccupying,
      "bool"
    },
    StartFreeing = {
      RelicEffect.Event_StartFreeing,
      "bool"
    },
    Occupied = {
      RelicEffect.Event_Occupied,
      "bool"
    },
    Freed = {
      RelicEffect.Event_Freed,
      "bool"
    }
  },
  Outputs = {
    StartOccupying = "bool",
    StartFreeing = "bool",
    Occupied = "bool",
    Freed = "bool"
  }
}
