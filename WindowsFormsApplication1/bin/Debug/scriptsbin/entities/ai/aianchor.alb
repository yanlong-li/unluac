AIAnchor = {
  type = "AIAnchor",
  Properties = {
    aianchor_AnchorType = "_wrong_type_",
    aicmdset_CmdSetType = 0
  }
}
function AIAnchor:OnPropertyChange()
  self:Register()
end
function AIAnchor:OnInit()
  self:Register()
end
function AIAnchor:OnReset()
end
function AIAnchor:Register()
  self.registered = nil
  if AIAnchorTable[self.Properties.aianchor_AnchorType] == nil then
    System.Log("AIAnchor[" .. self:GetName() .. "]:  undefined type [" .. self.Properties.aianchor_AnchorType .. "] Cant register with [AISYSTEM]")
  else
    AI.RegisterWithAI(self.id, AIAnchorTable[self.Properties.aianchor_AnchorType], self.Properties)
    self.registered = 1
  end
end
function AIAnchor:OnSave(save)
  save.aianchor_AnchorType = self.Properties.aianchor_AnchorType
end
function AIAnchor:OnLoad(save)
  self.Properties.aianchor_AnchorType = save.aianchor_AnchorType
  if not self.registered then
    self:Register()
  end
end
