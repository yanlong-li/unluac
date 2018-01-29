AIReinforcementSpot = {
  type = "AIReinforcementSpot",
  Properties = {
    bEnabled = 1,
    groupid = 173,
    radius = 30,
    AvoidWhenTargetInRadius = 15,
    bWhenAllAlerted = 1,
    bWhenInCombat = 1,
    iGroupBodyCount = 2,
    eiReinforcementType = 0
  }
}
function AIReinforcementSpot:OnInit()
  self:Register()
end
function AIReinforcementSpot:OnPropertyChange()
  self:Register()
end
function AIReinforcementSpot:OnReset()
  self.bNowEnabled = self.Properties.bEnabled
  if self.Properties.bEnabled == 0 then
    self:TriggerEvent(AIEVENT_DISABLE)
  else
    self:TriggerEvent(AIEVENT_ENABLE)
  end
  self:ActivateOutput("GroupID", self.Properties.groupid)
end
function AIReinforcementSpot:Register()
  if self.Properties.aianchor_AnchorType ~= "" then
    AI.RegisterWithAI(self.id, AIAnchorTable.REINFORCEMENT_SPOT, self.Properties)
    AI.ChangeParameter(self.id, AIPARAM_GROUPID, self.Properties.groupid)
  end
  self:OnReset()
end
function AIReinforcementSpot:Event_Enable(params)
  self:TriggerEvent(AIEVENT_ENABLE)
  self.bNowEnabled = 1
  self:ActivateOutput("GroupID", self.Properties.groupid)
end
function AIReinforcementSpot:Event_Disable(params)
  self:TriggerEvent(AIEVENT_DISABLE)
  self.bNowEnabled = 0
end
function AIReinforcementSpot:Alarm()
  self:TriggerEvent(AIEVENT_DISABLE)
  self.bNowEnabled = 0
  self:ActivateOutput("GroupID", self.Properties.groupid)
  self:ActivateOutput("Called", true)
end
AIReinforcementSpot.FlowEvents = {
  Inputs = {
    Enable = {
      AIReinforcementSpot.Event_Enable,
      "bool"
    },
    Disable = {
      AIReinforcementSpot.Event_Disable,
      "bool"
    }
  },
  Outputs = {Called = "bool", GroupID = "int"}
}
