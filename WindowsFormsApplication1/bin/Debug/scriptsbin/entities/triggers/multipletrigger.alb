MultipleTrigger = {
  type = "Trigger",
  Properties = {
    bEnabled = 1,
    iNumInputs = 1,
    ScriptCommand = "",
    PlaySequence = ""
  },
  Editor = {
    Model = "Editor/Objects/T.cgf",
    Icon = "MultiTrigger.bmp"
  }
}
function MultipleTrigger:OnPropertyChange()
  self:OnReset()
end
function MultipleTrigger:OnReset()
  self.numInputs = 0
end
function MultipleTrigger:OnShutDown()
end
function MultipleTrigger:OnLoad(props)
  self.numInputs = props.numInputs
end
function MultipleTrigger:OnSave(props)
  props.numInputs = self.numInputs or 0
end
function MultipleTrigger:OnInit()
  LogWarning("MultipleTrigger is deprecated")
  self:Activate(0)
  self:OnReset()
end
function MultipleTrigger:Event_InputTrigger(sender)
  if self.Properties.bEnabled ~= 0 then
    if self.numInputs >= self.Properties.iNumInputs then
      return
    end
    self.numInputs = self.numInputs + 1
    if self.numInputs >= self.Properties.iNumInputs then
      self:Event_OutputTrigger(sender)
    end
  end
  BroadcastEvent(self, "InputTrigger")
end
function MultipleTrigger:Event_OutputTrigger(sender)
  if self.Properties.bEnabled ~= 0 then
    if self.Properties.PlaySequence ~= "" then
      Movie.PlaySequence(self.Properties.PlaySequence)
    end
    if self.Properties.ScriptCommand and self.Properties.ScriptCommand ~= "" then
      local f = loadstring(self.Properties.ScriptCommand)
      if f ~= nil then
        f()
      end
    end
  end
  BroadcastEvent(self, "OutputTrigger")
end
function MultipleTrigger:Event_Enable(sender)
  self.Properties.bEnabled = 1
  BroadcastEvent(self, "Enable")
end
function MultipleTrigger:Event_Disable(sender)
  self.Properties.bEnabled = 0
  BroadcastEvent(self, "Disable")
end
MultipleTrigger.FlowEvents = {
  Inputs = {
    Disable = {
      MultipleTrigger.Event_Disable,
      "bool"
    },
    Enable = {
      MultipleTrigger.Event_Enable,
      "bool"
    },
    InputTrigger = {
      MultipleTrigger.Event_InputTrigger,
      "bool"
    },
    OutputTrigger = {
      MultipleTrigger.Event_OutputTrigger,
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
