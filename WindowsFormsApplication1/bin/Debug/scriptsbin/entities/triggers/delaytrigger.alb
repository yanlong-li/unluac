DelayTrigger = {
  type = "Trigger",
  Properties = {
    bEnabled = 1,
    bTriggerOnce = 0,
    Delay = 1,
    ScriptCommand = "",
    PlaySequence = "",
    ReferenceName = ""
  },
  Editor = {
    Model = "Editor/Objects/Clock.cgf",
    Icon = "Clock.bmp"
  }
}
function DelayTrigger:OnPropertyChange()
  self:OnReset()
end
function DelayTrigger:OnReset()
  if self.iTimer then
    Script.KillTimer(self.iTimer)
  end
  self.bEnabled = self.Properties.bEnabled
  self.bTriggered = 0
  self.bCounting = 0
end
function DelayTrigger:OnShutDown()
end
function DelayTrigger:OnSave(tbl)
  tbl.bEnabled = self.bEnabled
  tbl.bTriggered = self.bTriggered
  tbl.bCounting = self.bCounting
  if self.bCounting ~= 0 then
    assert(self.StartTime)
    tbl.timeElapsed = System.GetCurrTime() - self.StartTime
  end
end
function DelayTrigger:OnLoad(tbl)
  self:OnReset()
  self.bEnabled = tbl.bEnabled
  self.bTriggered = tbl.bTriggered
  self.bCounting = tbl.bCounting
  if self.bCounting ~= 0 then
    local setTimerTo = 1.0E-4
    if not tbl.timeElapsed then
      LogWarning("Invalid data in DelayTrigger restore")
    elseif tbl.timeElapsed < self.Properties.Delay then
      setTimerTo = self.Properties.Delay - tbl.timeElapsed
    end
    self.iTimer = Script.SetTimerForFunction(setTimerTo * 1000, "DelayTrigger.OnTimer", self)
  end
end
function DelayTrigger:OnInit()
  LogWarning("DelayTrigger is deprecated; use the flow node instead")
  self:Activate(0)
  self:OnReset()
end
function DelayTrigger:Event_InputTrigger(sender)
  BroadcastEvent(self, "InputTrigger")
  if self.Properties.bTriggerOnce ~= 0 and self.bTriggered ~= 0 then
    return
  end
  if self.bEnabled ~= 0 then
    self.bCounting = 1
    self.iTimer = Script.SetTimerForFunction(self.Properties.Delay * 1000, "DelayTrigger.OnTimer", self)
    self.StartTime = System.GetCurrTime()
  end
end
function DelayTrigger:Event_OutputTrigger(sender)
  if self.bEnabled ~= 0 then
    self.bCounting = 0
    self.bTriggered = 1
    BroadcastEvent(self, "OutputTrigger")
    if self.Properties.PlaySequence ~= "" then
      Movie.PlaySequence(self.Properties.PlaySequence)
    end
    if self.Properties.ScriptCommand and self.Properties.ScriptCommand ~= "" then
      dostring(self.Properties.ScriptCommand)
    end
  end
end
function DelayTrigger:Event_Enable(sender)
  self.bEnabled = 1
  BroadcastEvent(self, "Enable")
end
function DelayTrigger:Event_Disable(sender)
  self.bEnabled = 0
  BroadcastEvent(self, "Disable")
  if self.iTimer then
    Script.KillTimer(self.iTimer)
  end
  self.iTimer = nil
end
function DelayTrigger:OnTimer(timerid)
  self:Event_OutputTrigger(self)
  self.iTimer = nil
end
DelayTrigger.FlowEvents = {
  Inputs = {
    Disable = {
      DelayTrigger.Event_Disable,
      "bool"
    },
    Enable = {
      DelayTrigger.Event_Enable,
      "bool"
    },
    InputTrigger = {
      DelayTrigger.Event_InputTrigger,
      "bool"
    },
    OutputTrigger = {
      DelayTrigger.Event_OutputTrigger,
      "bool"
    }
  },
  Outputs = {
    Disable = "bool",
    Enable = "bool",
    InputTrigger = "bool",
    OutputTrigger = "bool"
  }
}
