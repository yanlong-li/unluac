EntityCommon = {
  TempPhysParams = {mass = 0, density = 0},
  TempPhysicsFlags = {flags_mask = 0, flags = 0}
}
function MakeDerivedEntity(_DerivedClass, _Parent)
  local derivedProperties = _DerivedClass.Properties
  _DerivedClass.Properties = {}
  mergef(_DerivedClass, _Parent, 1)
  mergef(_DerivedClass.Properties, derivedProperties, 1)
  _DerivedClass.__super = BasicEntity
  return _DerivedClass
end
function BroadcastEvent(sender, Event)
  sender:ProcessBroadcastEvent(Event)
  if sender.Events then
    local eventTargets = sender.Events[Event]
    if eventTargets then
      for i, target in pairs(eventTargets) do
        local TargetId = target[1]
        local TargetEvent = target[2]
        if TargetId == 0 then
          if Mission then
            local func = Mission["Event_" .. TargetEvent]
            if func ~= nil then
              func(sender)
            else
              System.Log("Mission does not support event " .. TargetEvent)
            end
          end
        else
          local entity = System.GetEntity(TargetId)
          if entity ~= nil then
            local TargetName = entity:GetName()
            local func = entity["Event_" .. TargetEvent]
            if func ~= nil then
              func(entity, sender)
            end
          end
        end
      end
    end
  end
end
function DumpEntities()
  local ents = System.GetEntities()
  System.Log("Entities dump")
  for idx, e in pairs(ents) do
    local pos = e:GetPos()
    local ang = e:GetAngles()
    System.Log("[" .. tostring(e.id) .. "]..name=" .. e:GetName() .. " clsid=" .. e.class .. format(" pos=%.03f,%.03f,%.03f", pos.x, pos.y, pos.z) .. format(" ang=%.03f,%.03f,%.03f", ang.x, ang.y, ang.z))
  end
end
function MakeUsable(entity)
  if not entity.Properties then
    entity.Properties = {}
  end
  entity.Properties.UseText = ""
  entity.Properties.bUsable = 0
  function entity:IsUsable()
    if not self.__usable then
      self.__usable = self.Properties.bUsable
    end
    return self.__usable
  end
  function entity:ResetOnUsed()
    self.__usable = nil
  end
  function entity:GetUsableMessage()
    return self.Properties.UseText
  end
  function entity:OnUsed(user, idx)
    BroadcastEvent(self, "Used")
  end
  function entity:Event_Used()
    BroadcastEvent(self, "Used")
  end
  function entity:Event_EnableUsable()
    self.__usable = 1
    BroadcastEvent(self, "EnableUsable")
  end
  function entity:Event_DisableUsable()
    self.__usable = 0
    BroadcastEvent(self, "DisableUsable")
  end
end
function MakePickable(entity)
  if not entity.Properties then
    entity.Properties = {}
  end
  entity.Properties.bPickable = 0
end
function MakeCutsceneEventsAcceptable(entity)
  function entity:Event_ActiveMelee()
  end
  function entity:Event_ActiveRanged()
  end
  function entity:Event_InactiveWeapon()
  end
  function entity:Event_VisibleMelee()
  end
  function entity:Event_VisibleRanged()
  end
  function entity:Event_HideMelee()
  end
  function entity:Event_HideRanged()
  end
  function entity:Event_PickBranch()
  end
  function entity:Event_ActiveMusical()
  end
  function entity:Event_VisibleMusical()
  end
  function entity:Event_HideMusical()
  end
  function entity:Event_HideAllWeapon()
  end
  function entity:Event_VisibleBackpack()
  end
  function entity:Event_HideBackpack()
  end
end
function MakeSpawnable(entity)
  entity.spawnedEntity = nil
  if not entity.Properties then
    entity.Properties = {}
  end
  local p = entity.Properties
  p.bSpawner = false
  p.SpawnedEntityName = ""
  local _OnDestroy = entity.OnDestroy
  function entity:OnDestroy()
    if self.whoSpawnedMe then
      self.whoSpawnedMe:NotifyRemoval(self.id)
    end
    if _OnDestroy then
      _OnDestroy(self)
    end
  end
  function entity:NotifyRemoval(spawnedEntityId)
    if self.spawnedEntity and self.spawnedEntity == spawnedEntityId then
      self.spawnedEntity = nil
    end
  end
  local _OnReset = entity.OnReset
  function entity:OnReset()
    if self.spawnedEntity then
      System.RemoveEntity(self.spawnedEntity)
      self.spawnedEntity = nil
    end
    if self.whoSpawnedMe then
      System.RemoveEntity(self.id)
      return
    end
    _OnReset(self)
  end
  function entity:GetFlowgraphForwardingEntity()
    return self.spawnedEntity
  end
  function entity:Event_Spawned()
    BroadcastEvent(self, "Spawned")
  end
  if not entity.FlowEvents then
    entity.FlowEvents = {}
  end
  local fe = entity.FlowEvents
  fe.Inputs = fe.Inputs or {}
  fe.Outputs = fe.Outputs or {}
  local allEvents = {}
  local name, data
  for name, data in pairs(fe.Outputs) do
    allEvents[name] = data
  end
  for name, data in pairs(fe.Inputs) do
    allEvents[name] = data
  end
  for name, data in pairs(allEvents) do
    do
      local isInput = fe.Inputs[name]
      local isOutput = fe.Outputs[name]
      local isDeath = name == "Dead"
      local _event = data
      if type(_event) == "table" then
        _event = _event[1]
      else
        _event = nil
      end
      entity["Event_" .. name] = function(self, sender, param)
        if isOutput and (sender and sender.id == self.spawnedEntity or sender == self) then
          BroadcastEvent(self, name)
        end
        if isInput and self.spawnedEntity and (not sender or self.spawnedEntity ~= sender.id) then
          local ent = System.GetEntity(self.spawnedEntity)
          if _event and ent and ent ~= sender then
            _event(ent, sender, param)
          end
        elseif _event and not self.spawnedEntity then
          _event(self, sender, param)
        end
        if isDeath and sender and sender.id == self.spawnedEntity then
          self.spawnedEntity = nil
        end
      end
    end
  end
  function entity:Event_Spawn()
    if self.whoSpawnedMe then
      self.whoSpawnedMe:Event_Spawn()
    else
      if self.spawnedEntity then
        return
      end
      local params = {
        class = self.class,
        position = self:GetPos(),
        orientation = self:GetDirectionVector(1),
        scale = self:GetScale(),
        archetype = self:GetArchetype(),
        properties = self.Properties,
        propertiesInstance = self.PropertiesInstance
      }
      if self.Properties.SpawnedEntityName ~= "" then
        params.name = self.Properties.SpawnedEntityName
      else
        params.name = self:GetName() .. "_s"
      end
      local ent = System.SpawnEntity(params, self.id)
      if ent then
        self.spawnedEntity = ent.id
        if not ent.Events then
          ent.Events = {}
        end
        local evts = ent.Events
        for name, data in pairs(self.FlowEvents.Outputs) do
          if not evts[name] then
            evts[name] = {}
          end
          table.insert(evts[name], {
            self.id,
            name
          })
        end
        ent.whoSpawnedMe = self
        self:ActivateOutput("Spawned", ent.id)
      end
    end
  end
  function entity:Event_SpawnKeep()
    local params = {
      class = self.class,
      position = self:GetPos(),
      orientation = self:GetDirectionVector(1),
      scale = self:GetScale(),
      archetype = self:GetArchetype(),
      properties = self.Properties,
      propertiesInstance = self.PropertiesInstance
    }
    local rndOffset = 1
    params.position.x = params.position.x + random(0, rndOffset * 2) - rndOffset
    params.position.y = params.position.y + random(0, rndOffset * 2) - rndOffset
    params.name = self:GetName()
    local ent = System.SpawnEntity(params, self.id)
    if ent then
      self.spawnedEntity = ent.id
      if not ent.Events then
        ent.Events = {}
      end
      local evts = ent.Events
      for name, data in pairs(self.FlowEvents.Outputs) do
        if not evts[name] then
          evts[name] = {}
        end
        table.insert(evts[name], {
          self.id,
          name
        })
      end
      self:ActivateOutput("Spawned", ent.id)
    end
  end
  MakeCutsceneEventsAcceptable(entity)
  fe.Inputs.Spawn = {
    entity.Event_Spawn,
    "bool"
  }
  fe.Outputs.Spawned = "entity"
end
function EntityCommon.PhysicalizeRigid(entity, nSlot, Properties, bActive)
  local Mass = Properties.Mass
  local Density = Properties.Density
  local physType
  if Properties.bArticulated == 1 then
    physType = PE_ARTICULATED
  elseif Properties.bRigidBody == 1 then
    physType = PE_RIGID
  else
    physType = PE_STATIC
  end
  local TempPhysParams = EntityCommon.TempPhysParams
  TempPhysParams.density = Density
  TempPhysParams.mass = Mass
  TempPhysParams.flags = 0
  entity:Physicalize(nSlot, physType, TempPhysParams)
  local Simulation = Properties.Simulation
  if Simulation then
    entity:SetPhysicParams(PHYSICPARAM_SIMULATION, Simulation)
  end
  local Buoyancy = Properties.Buoyancy
  if Buoyancy then
    entity:SetPhysicParams(PHYSICPARAM_BUOYANCY, Buoyancy)
  end
  local PhysFlags = EntityCommon.TempPhysicsFlags
  PhysFlags.flags = 0
  if Properties.bPushableByPlayers == 1 then
    PhysFlags.flags = pef_pushable_by_players
  end
  if Simulation and Simulation.bFixedDamping and Simulation.bFixedDamping == 1 then
    PhysFlags.flags = PhysFlags.flags + pef_fixed_damping
  end
  if Simulation and Simulation.bUseSimpleSolver and Simulation.bUseSimpleSolver == 1 then
    PhysFlags.flags = PhysFlags.flags + ref_use_simple_solver
  end
  if Properties.bCanBreakOthers == nil or Properties.bCanBreakOthers == 0 then
    PhysFlags.flags = PhysFlags.flags + pef_never_break
  end
  PhysFlags.flags_mask = pef_fixed_damping + ref_use_simple_solver + pef_pushable_by_players + pef_never_break
  entity:SetPhysicParams(PHYSICPARAM_FLAGS, PhysFlags)
  if Properties.bResting == 0 then
    entity:AwakePhysics(1)
  else
    entity:AwakePhysics(0)
  end
end
function CompareEntitiesByName(ent1, ent2)
  return ent1:GetName() < ent2:GetName()
end
function MakeCompareEntitiesByDistanceFromPoint(point)
  function CompareEntitiesByDistanceFromPoint(ent1, ent2)
    distance1 = DistanceSqVectors(ent1:GetWorldPos(), point)
    distance2 = DistanceSqVectors(ent2:GetWorldPos(), point)
    return distance1 > distance2
  end
  return CompareEntitiesByDistanceFromPoint
end
