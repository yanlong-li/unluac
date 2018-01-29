Wind = {
  type = "WindController",
  Properties = {
    vVelocity = {
      x = 1,
      y = 0,
      z = 0
    },
    fFadeTime = 1
  },
  Editor = {
    Icon = "Tornado.bmp"
  }
}
function Wind:OnInit()
  self.oldVelocity = {
    x = 0,
    y = 0,
    z = 0
  }
  self.occupied = 0
  self.lasttime = 0
  self:OnReset()
end
function Wind:OnPropertyChange()
  self:OnReset()
end
function Wind:OnReset()
end
function Wind:OnSave(tbl)
  tbl.bTriggered = self.bTriggered
  tbl.oldVelocity = self.oldVelocity
  tbl.fadeamt = self.fadeamt
  tbl.Who = self.Who
end
function Wind:OnLoad(tbl)
  self.bTriggered = tbl.bTriggered
  self.oldVelocity = tbl.oldVelocity
  self.fadamt = tbl.fadeamt
  self.Who = tbl.Who
end
function Wind:OnProceedFadeArea(player, areaId, fadeCoeff)
  self:Fade(fadeCoeff)
end
function Wind:ResetValues()
  System.SetWind(self.oldVelocity)
end
function Wind:OnEnterArea(player, areaId)
  if player and not player.actor:IsPlayer() or self.occupied == 1 then
    return
  end
  self.oldVelocity = System.GetWind()
  self.occupied = 1
end
function Wind:OnLeaveArea(player, areaId)
  if player and not player.actor:IsPlayer() then
    return
  end
  self:ResetValues()
  self.occupied = 0
end
function Wind:OnShutDown()
end
function Wind:Event_Enable(sender)
  if self.occupied == 0 then
    if self.fadeamt and self.fadeamt < 1 then
      self:ResetValues()
    end
    self:OnEnterArea()
    self.fadeamt = 0
    self.lasttime = _time
    self.exitfrom = nil
  end
  self:SetTimer(0, 1)
  BroadcastEvent(self, "Enable")
end
function Wind:Event_Disable(sender)
  if self.occupied == 1 then
    self.occupied = 0
    self.fadeamt = 0
    self.lasttime = _time
    self.exitfrom = 1
  end
  self:SetTimer(0, 1)
  BroadcastEvent(self, "Disable")
end
function Wind:OnTimer()
  self:SetTimer(0, 1)
  if self.fadeamt then
    local delta = _time - self.lasttime
    self.lasttime = _time
    self.fadeamt = self.fadeamt + delta / self.Properties.fFadeTime
    if 1 <= self.fadeamt then
      self.fadeamt = 1
      self:KillTimer(0)
    end
    local fadeCoeff = self.fadeamt
    if self.exitfrom then
      fadeCoeff = 1 - fadeCoeff
    end
    fadeCoeff = math.sqrt(fadeCoeff)
    fadeCoeff = math.sqrt(fadeCoeff)
    self:Fade(fadeCoeff)
  else
    self:KillTimer(0)
  end
end
function Wind:Fade(fadeCoeff)
  System.SetWind(LerpColors(self.oldVelocity, self.Properties.vVelocity, fadeCoeff))
end
Wind.FlowEvents = {
  Inputs = {
    Disable = {
      Wind.Event_Disable,
      "bool"
    },
    Enable = {
      Wind.Event_Enable,
      "bool"
    }
  },
  Outputs = {Disable = "bool", Enable = "bool"}
}
