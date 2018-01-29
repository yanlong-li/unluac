AICheckInBox = {
  type = "Trigger",
  Properties = {
    DimX = 5,
    DimY = 5,
    DimZ = 5,
    bTriggerOnce = 0,
    species = 1
  },
  Editor = {
    Model = "Editor/Objects/T.cgf",
    Icon = "Trigger.bmp",
    ShowBounds = 1
  },
  updateRate = 2000
}
function AICheckInBox:OnPropertyChange()
  local Min = {
    x = -self.Properties.DimX / 2,
    y = -self.Properties.DimY / 2,
    z = -self.Properties.DimZ / 2
  }
  local Max = {
    x = self.Properties.DimX / 2,
    y = self.Properties.DimY / 2,
    z = self.Properties.DimZ / 2
  }
  self:SetTriggerBBox(Min, Max)
end
function AICheckInBox:OnInit()
  self:OnPropertyChange()
  self:OnReset()
end
function AICheckInBox:OnShutDown()
end
function AICheckInBox:OnSave(tbl)
  tbl.bTriggered = self.bTriggered
end
function AICheckInBox:OnLoad(tbl)
  self.bTriggered = tbl.bTriggered
end
function AICheckInBox:OnReset()
  self.PopulationTable = {}
  self.bTriggered = nil
end
function AICheckInBox:Event_Occupied(sender)
  if self.Properties.bTriggerOnce == 1 and self.bTriggered == 1 then
    return
  end
  self.bTriggered = 1
  BroadcastEvent(self, "Occupied")
end
function AICheckInBox:Event_Empty(sender)
  if self.Properties.bTriggerOnce == 1 and self.bTriggered == 1 then
    return
  end
  self.bTriggered = 1
  BroadcastEvent(self, "Empty")
end
function AICheckInBox:OnTimer()
  self:CheckThePopulation()
end
function AICheckInBox:CheckThePopulation()
  if self:IsSomebodyInside() == nil then
    self:KillTimer(0)
    self:Event_Empty()
    return
  end
  self:SetTimer(0, self.updateRate)
end
function AICheckInBox:IsSomebodyInside()
  local numUnits = count(self.PopulationTable)
  for entityId, theId in pairs(self.PopulationTable) do
    local unit = System.GetEntity(theId)
    if unit == nil or unit:IsDead() then
      numUnits = numUnits - 1
    end
  end
  if numUnits < 1 then
    return nil
  end
  return 1
end
function AICheckInBox:OnEnterArea(entity, areaId)
  if not self:IsValidSource(entity) then
    return
  end
  if self:IsSomebodyInside() == nil then
    self:Event_Occupied()
    self:SetTimer(0, self.updateRate)
  end
  self.PopulationTable[entity.id] = entity.id
end
function AICheckInBox:OnLeaveArea(entity, areaId)
  if self.PopulationTable[entity.id] == nil then
    return
  end
  self.PopulationTable[entity.id] = nil
  self:CheckThePopulation()
end
function AICheckInBox:IsValidSource(entity)
  if entity.type == "Player" then
    return false
  end
  if entity.ai ~= nil and entity.Properties.species == self.Properties.species then
    return true
  end
  return false
end
AICheckInBox.FlowEvents = {
  Inputs = {},
  Outputs = {Empty = "bool", Occupied = "bool"}
}
