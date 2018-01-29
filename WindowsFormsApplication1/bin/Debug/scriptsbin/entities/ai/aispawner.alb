AISpawner = {
  Client = {},
  Server = {},
  Editor = {
    Model = "Editor/Objects/Particles.cgf",
    Icon = "SpawnPoint.bmp",
    DisplayArrow = 1
  },
  Properties = {
    NumUnits = 2,
    Limit = 4,
    bLimitStop = 1,
    bDoVisCheck = 0
  },
  unitsCounter = 0,
  totalUnitsCounter = 0,
  isEnabled = 0,
  visDummys = {},
  spawnedIds = {},
  spawnedIdsSize = 0
}
function AISpawner.Server:OnInit()
  AI.LogEvent("AISpawner.Server:OnInit >>>")
  self:OnReset()
  self:CreateDummy()
end
function AISpawner.Server:OnShutDown()
  self:RemoveDummy()
end
function AISpawner:OnPropertyChange()
  self:CreateDummy()
end
function AISpawner:OnReset()
  self.unitsCounter = 0
  self.totalUnitsCounter = 0
  self.isEnabled = 0
  self.spawnedIds = {}
  self.spawnedIdsSize = 0
end
function AISpawner:OnSave(save)
  save.unitsCounter = self.unitsCounter
  save.totalUnitsCounter = self.totalUnitsCounter
  save.isEnabled = self.isEnabled
  save.spawnedIds = self.spawnedIds
  save.spawnedIdsSize = self.spawnedIdsSize
end
function AISpawner:OnLoad(saved)
  self.unitsCounter = saved.unitsCounter
  self.totalUnitsCounter = saved.totalUnitsCounter
  self.isEnabled = saved.isEnabled
  self.spawnedIds = saved.spawnedIds
  self.spawnedIdsSize = saved.spawnedIdsSize
end
function AISpawner:UnitDown()
  self.unitsCounter = self.unitsCounter - 1
  if self.isEnabled == 0 then
    return
  end
  if self.unitsCounter < self.Properties.NumUnits then
    self:SpawnInitially()
  end
end
function AISpawner:SpawnUnit(id)
  local link = System.GetEntity(id)
  if link == nil then
    return
  end
  link.spawnedEntity = nil
  if link.Event_SpawnKeep then
    link:Event_SpawnKeep()
  elseif link.SpawnCopyAndLoad then
    link:SpawnCopyAndLoad()
  end
  if link.spawnedEntity then
    local newEntity = System.GetEntity(link.spawnedEntity)
    if newEntity then
      if link.PropertiesInstance.bAutoDisable ~= 1 then
        AI.AutoDisable(newEntity.id, 0)
      end
      if newEntity.class == "Scout" then
        AI.AutoDisable(newEntity.id, 0)
      end
      newEntity.AI.spawnerListenerId = self.id
      self.unitsCounter = self.unitsCounter + 1
      self.totalUnitsCounter = self.totalUnitsCounter + 1
      self:FindSpawnReinfPoint()
      newEntity.AI.reinfPoint = g_SignalData.ObjectName
      newEntity:SetName(newEntity:GetName() .. "_spawned")
      AI.Signal(SIGNALFILTER_SENDER, 0, "NEW_SPAWN", newEntity.id, g_SignalData)
      self.spawnedIds[self.spawnedIdsSize] = newEntity.id
      self.spawnedIdsSize = self.spawnedIdsSize + 1
    end
  end
end
function AISpawner:FindSpawnProtoUnitId()
  if self:CountLinks() < 1 then
    return 0
  end
  if self.Properties.bDoVisCheck == 0 then
    while true do
      local spawnIdx = random(1, self:CountLinks()) - 1
      local link = self:GetLink(spawnIdx)
      AI.LogEvent(" >>>FindSpawnProtoUnitIdx checking " .. spawnIdx)
      if AI.GetTypeOf(link.id) ~= AIOBJECT_WAYPOINT then
        return link.id
      end
    end
  else
    for protoId, dummy in pairs(self.visDummys) do
      if dummy:UnSeenFrames() > 100 then
        return protoId
      end
    end
  end
  return 0
end
function AISpawner:FindSpawnReinfPoint()
  local targetPos = self:GetPos()
  if g_localActor then
    targetPos = g_localActor:GetPos()
  end
  local minDistSq = 100000000
  local bestIdx = 0
  local i = 0
  local link = self:GetLink(i)
  while link do
    if AI.GetTypeOf(link.id) == AIOBJECT_WAYPOINT then
      local distSq = DistanceSqVectors(targetPos, link:GetPos())
      if minDistSq > distSq then
        minDistSq = distSq
        bestIdx = i
      end
    end
    i = i + 1
    link = self:GetLink(i)
  end
  link = self:GetLink(bestIdx)
  g_SignalData.ObjectName = link:GetName()
end
function AISpawner:SpawnInitially()
  while self.unitsCounter < self.Properties.NumUnits do
    if self.totalUnitsCounter == self.Properties.Limit then
      self:Event_Limit()
      if self.isEnabled == 0 then
        return
      end
    end
    local spawnId = self:FindSpawnProtoUnitId()
    if spawnId == 0 then
      AI.LogEvent(" >>>Spawning initially : Can't find valid spawn proto/point")
      return
    end
    self:SpawnUnit(spawnId)
  end
end
function AISpawner:RemoveDummy()
  for protoId, dummy in pairs(self.visDummys) do
    System.RemoveEntity(dummy.id)
  end
  self.visDummys = {}
end
function AISpawner:CreateDummy()
  self:RemoveDummy()
  if self.Properties.bDoVisCheck == 0 then
    return
  end
  local i = 0
  local link = self:GetLink(i)
  while link do
    if AI.GetTypeOf(link.id) ~= AIOBJECT_WAYPOINT then
      local dummyEntity = self
      local params = {
        class = "Dummy",
        position = link:GetPos()
      }
      params.name = link:GetName() .. "_VisDummy"
      dummyEntity = System.SpawnEntity(params)
      dummyEntity:LoadObject(0, "objects/box_nodraw.cgf")
      local bbmin, bbmax = link:GetLocalBBox()
      dummyEntity:SetLocalBBox(bbmin, bbmax)
      self.visDummys[link.id] = dummyEntity
    end
    i = i + 1
    link = self:GetLink(i)
  end
end
function AISpawner:Event_Enable(params)
  self.isEnabled = 1
  self:SpawnInitially()
  BroadcastEvent(self, "Enable")
end
function AISpawner:Event_Disable(params)
  self.isEnabled = 0
  BroadcastEvent(self, "Disable")
end
function AISpawner:Event_Limit(params)
  if self.Properties.bLimitStop ~= 0 then
    self:Event_Disable()
  end
  BroadcastEvent(self, "Limit")
end
function AISpawner:Event_AutoDisableOn(params)
  for idx, spawnedId in pairs(self.spawnedIds) do
    local spawnedEntity = System.GetEntity(spawnedId)
    if spawnedEntity then
      AI.AutoDisable(spawnedEntity.id, 1)
      if spawnedEntity.AutoDisablePassangers then
        spawnedEntity:AutoDisablePassangers(1)
      end
    end
  end
  BroadcastEvent(self, "AutoDisableOn")
end
function AISpawner:Event_AutoDisableOff(params)
  for idx, spawnedId in pairs(self.spawnedIds) do
    local spawnedEntity = System.GetEntity(spawnedId)
    if spawnedEntity then
      AI.AutoDisable(spawnedEntity.id, 0)
      if spawnedEntity.AutoDisablePassangers then
        spawnedEntity:AutoDisablePassangers(0)
      end
    end
  end
  BroadcastEvent(self, "AutoDisableOff")
end
AISpawner.FlowEvents = {
  Inputs = {
    Enable = {
      AISpawner.Event_Enable,
      "bool"
    },
    Disable = {
      AISpawner.Event_Disable,
      "bool"
    },
    AutoDisableOn = {
      AISpawner.Event_AutoDisableOn,
      "bool"
    },
    AutoDisableOff = {
      AISpawner.Event_AutoDisableOff,
      "bool"
    }
  },
  Outputs = {Limit = "bool"}
}
