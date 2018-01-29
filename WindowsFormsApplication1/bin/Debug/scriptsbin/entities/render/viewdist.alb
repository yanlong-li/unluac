ViewDist = {
  type = "ViewDistController",
  Properties = {MaxViewDist = 25, fFadeTime = 1},
  Editor = {
    Model = "Editor/Objects/T.cgf"
  }
}
function ViewDist:OnSave(tbl)
  tbl.outsideViewDist = self.outsideViewDist
  tbl.occupied = self.occupied
  tbl.fadeamt = self.fadeamt
  tbl.lasttime = self.lasttime
  tbl.exitfrom = self.exitfrom
end
function ViewDist:OnLoad(tbl)
  self.outsideViewDist = tbl.outsideViewDist
  self.occupied = tbl.occupied
  self.fadeamt = tbl.fadeamt
  self.lasttime = tbl.lasttime
  self.exitfrom = tbl.exitfrom
end
function ViewDist:OnInit()
  self.outsideViewDist = 0
  self.occupied = 0
  self:OnReset()
  self.outsideViewDist = System.ViewDistanceGet()
end
function ViewDist:OnPropertyChange()
  self:OnReset()
end
function ViewDist:OnReset()
  if self.occupied == 1 then
    self:OnLeaveArea()
  end
  self.occupied = 0
  self:KillTimer(0)
end
function ViewDist:OnProceedFadeArea(player, areaId, fadeCoeff)
  self:FadeViewDist(fadeCoeff)
end
function ViewDist:ResetValues()
  System.ViewDistanceSet(self.outsideViewDist)
end
function ViewDist:OnEnterArea(player, areaId)
  if self.occupied == 1 then
    return
  end
  self.outsideViewDist = System.ViewDistanceGet()
  self.occupied = 1
end
function ViewDist:OnLeaveArea(player, areaId)
  self:ResetValues()
  self.occupied = 0
end
function ViewDist:OnShutDown()
end
function ViewDist:Event_Enable(sender)
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
function ViewDist:Event_Disable(sender)
  if self.occupied == 1 then
    self.occupied = 0
    self.fadeamt = 0
    self.lasttime = _time
    self.exitfrom = 1
  end
  self:SetTimer(0, 1)
  BroadcastEvent(self, "Disable")
end
function ViewDist:OnTimer()
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
    self:FadeViewDist(fadeCoeff)
  else
    self:KillTimer(0)
  end
end
function ViewDist:FadeViewDist(fadeCoeff)
  local cCoeff = math.sqrt(fadeCoeff)
  fadeCoeff = cCoeff
  System.ViewDistanceSet(Lerp(self.outsideViewDist, self.Properties.MaxViewDist, fadeCoeff))
end
ViewDist.FlowEvents = {
  Inputs = {
    Disable = {
      ViewDist.Event_Disable,
      "bool"
    },
    Enable = {
      ViewDist.Event_Enable,
      "bool"
    }
  },
  Outputs = {Disable = "bool", Enable = "bool"}
}
