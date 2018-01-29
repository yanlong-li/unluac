VehicleBaseAI = {
  PropertiesInstance = {
    groupid = 173,
    aibehavior_behaviour = "TankIdle",
    bCircularPath = 0,
    PathName = "",
    FormationType = "",
    bAutoDisable = 1
  },
  Properties = {
    soclasses_SmartObjectClass = "",
    bAutoGenAIHidePts = 0,
    aicharacter_character = "Tank",
    leaderName = "",
    followDistance = 5,
    bSpeciesHostility = 1,
    attackrange = 100,
    commrange = 100,
    accuracy = 1,
    Perception = {
      camoScale = 1,
      velBase = 1,
      velScale = 0.5,
      FOVPrimary = -1,
      FOVSecondary = -1,
      sightrange = 100,
      sightrangeVehicle = -1,
      stanceScale = 1
    }
  },
  AI = {
    BehaviourSignals = {},
    goalType = 0,
    commandSet = {}
  },
  AIMovementAbility = {
    walkSpeed = 5,
    runSpeed = 8,
    sprintSpeed = 10,
    maneuverSpeed = 4,
    b3DMove = 0,
    minTurnRadius = 1,
    maxTurnRadius = 30,
    maxAccel = 1000000,
    maxDecel = 1000000,
    usePathfinder = 1,
    pathType = AIPATH_DEFAULT,
    passRadius = 3,
    pathLookAhead = 5,
    pathRadius = 2,
    maneuverTrh = 2,
    velDecay = 30
  },
  AIFireProperties = {},
  AISoundRadius = 120,
  hidesUser = 1,
  forgetTimeTarget = 16,
  forgetTimeSeek = 20,
  forgetTimeMemory = 20
}
function VehicleBaseAI:AIDriver(enable)
  if not AI then
    return
  end
  if enable == 0 then
    AI.LogEvent(" >>>> VehicleBaseAI:AIDriver disabling " .. self:GetName())
    self:ChangeSpecies()
    self:TriggerEvent(AIEVENT_DRIVER_OUT)
    self.State.aiDriver = nil
    self:EnableMountedWeapons(true)
  else
    if self.health and 0 >= self.health then
      return
    end
    AI.LogEvent(" >>>> VehicleBaseAI:AIDriver enabling " .. self:GetName())
    self:TriggerEvent(AIEVENT_DRIVER_IN)
    if self.Behaviour and self.Behaviour.Constructor then
      self.Behaviour:Constructor(self)
    end
    self:EnableMountedWeapons(false)
    return 1
  end
end
function VehicleBaseAI:InitAI()
  if not AI then
    return
  end
  mergef(self.AI.commandSet, X2AI.commandSet, 1)
  self.AI = {
    BehaviourSignals = {},
    goalType = 0,
    commandSet = {}
  }
  AI.RegisterWithAI(self.id, self.AIType, self.Properties, self.PropertiesInstance, self.AIMovementAbility)
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_TARGET, self.forgetTimeTarget)
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_SEEK, self.forgetTimeSeek)
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_MEMORY, self.forgetTimeMemory)
  if self.AICombatClass then
    AI.ChangeParameter(self.id, AIPARAM_COMBATCLASS, self.AICombatClass)
  end
  if self.Properties.bAutoGenAIHidePts and self.Properties.bAutoGenAIHidePts == 1 then
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 0)
  else
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 2)
  end
end
function VehicleBaseAI:ResetAI()
  if not AI then
    return
  end
  mergef(self.AI.commandSet, X2AI.commandSet, 1)
  self.AI = {
    goalType = 0,
    BehaviourSignals = {},
    commandSet = {}
  }
  self:AIDriver(0)
  AI.ResetParameters(self.id, self.Properties, self.PropertiesInstance, nil)
  if self.AICombatClass then
    AI.ChangeParameter(self.id, AIPARAM_COMBATCLASS, self.AICombatClass)
  end
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_TARGET, self.forgetTimeTarget)
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_SEEK, self.forgetTimeSeek)
  AI.ChangeParameter(self.id, AIPARAM_FORGETTIME_MEMORY, self.forgetTimeMemory)
  self.AI.BehaviourSignals[AIGOALTYPE_UNDEFINED] = ""
  self.AI.BehaviourSignals[AIGOALTYPE_GOTO] = "GO_TO"
  self.AI.BehaviourSignals[AIGOALTYPE_PATH] = "GO_PATH"
  self.AI.BehaviourSignals[AIGOALTYPE_FOLLOW] = "FOLLOW"
  self.AI.BehaviourSignals[AIGOALTYPE_ATTACK] = "GO_TO"
  self.AI.BehaviourSignals[AIGOALTYPE_REINFORCEMENT] = "GO_TO"
  self:Event_Init()
  if self.Properties.bAutoGenAIHidePts and self.Properties.bAutoGenAIHidePts == 1 then
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 0)
  else
    self:SetFlags(ENTITY_FLAG_AI_HIDEABLE, 2)
  end
end
function VehicleBaseAI:OnLoadAI(saved)
  self.AI = {}
  if saved.AI then
    self.AI = saved.AI
  end
  if self.Properties and self.Properties.aicharacter_character then
    local characterTable = AICharacter[self.Properties.aicharacter_character]
    if characterTable and characterTable.OnLoad then
      characterTable.OnLoad(self, saved)
    end
  end
end
function VehicleBaseAI:OnSaveAI(save)
  if self.AI then
    save.AI = self.AI
  end
  if self.Properties and self.Properties.aicharacter_character then
    local characterTable = AICharacter[self.Properties.aicharacter_character]
    if characterTable and characterTable.OnSave then
      characterTable.OnSave(self, save)
    end
  end
end
function VehicleBaseAI:DestroyAI()
  if not AI then
    return
  end
  if self.AI.convoyPrev then
    self:Event_ConvoyRemove()
  end
end
function VehicleBaseAI:InitConvoy()
  if not AI then
    return
  end
  if self.AI.isConvoyLeader then
    local vehicle = self
    while vehicle do
      local nextvehicle = vehicle.AI.convoyNext
      vehicle.AI.convoyNext = nil
      vehicle.AI.convoyPrev = nil
      vehicle.AI.convoyLeader = nil
      vehicle.AI.convoyPosition = nil
      vehicle = nextvehicle
    end
  end
  self.AI.convoyUnits = 0
  self.AI.isConvoyLeader = false
end
function VehicleBaseAI:ConvoySwapPositions(other)
  if not AI then
    return
  end
  local temp
  if self.AI.convoyPrev == other then
    self.AI.convoyPrev = other.AI.convoyPrev
    other.AI.convoyPrev = self
    other.AI.convoyNext = self.AI.convoyNext
    self.AI.convoyNext = other
  elseif other.AI.convoyPrev == self then
    other.AI.convoyPrev = self.AI.convoyPrev
    self.AI.convoyPrev = other
    self.AI.convoyNext = other.AI.convoyNext
    other.AI.convoyNext = self
  else
    temp = other.AI.convoyPrev
    other.AI.convoyPrev = self.AI.convoyPrev
    self.AI.convoyPrev = temp
    temp = other.AI.convoyNext
    other.AI.convoyNext = self.AI.convoyNext
    self.AI.convoyNext = temp
  end
  temp = other.AI.isConvoyLeader
  other.AI.isConvoyLeader = self.AI.isConvoyLeader
  self.AI.isConvoyLeader = temp
  local leader
  if self.AI.isConvoyLeader then
    leader = self
  elseif other.AI.isConvoyLeader then
    leader = other
  end
  if leader then
    leader:SetConvoyLeader()
  end
end
function VehicleBaseAI:SetConvoyLeader()
  if not AI then
    return
  end
  local vehicle = self
  self.AI.convoyUnits = 0
  while vehicle do
    self.AI.convoyUnits = self.AI.convoyUnits + 1
    vehicle.AI.convoyLeader = self
    vehicle.PropertiesInstance.FormationType = self.PropertiesInstance.FormationType
    vehicle = vehicle.AI.convoyNext
  end
  self.AI.isConvoyLeader = true
end
function VehicleBaseAI:Event_ConvoyRemove()
  if not AI then
    return
  end
  if self.AI.convoyNext then
    self.AI.convoyNext.convoyPrev = self.AI.convoyPrev
  end
  if self.AI.convoyPrev then
    self.AI.convoyPrev.convoyNext = self.AI.convoyNext
  end
  if self.AI.convoyLeader and self.AI.convoyLeader.convoyUnits and self.AI.convoyLeader.convoyUnits > 0 then
    self.AI.convoyLeader.convoyUnits = self.AI.convoyLeader.convoyUnits - 1
  end
  if self.AI.isConvoyLeader and self.AI.convoyNext then
    self.AI.convoyNext:SetConvoyLeader()
  elseif self.AI.convoyLeader and self.AI.convoyLeader.convoyUnits and self.AI.convoyLeader.convoyUnits > 0 then
    self.convoyLeader.AI.convoyUnits = self.AI.convoyLeader.convoyUnits - 1
  end
  self.AI.convoyPrev = nil
  self.AI.convoyNext = nil
  self.AI.convoyLeader = nil
  self.AI.isConvoyLeader = false
end
function VehicleBaseAI:StartConvoy()
  if not AI then
    return
  end
  if self.AI.convoyPrev then
    AI.LogEvent("Vehicle " .. self:GetName() .. " joining " .. self.AI.convoyPrev:GetName() .. " in convoy")
    g_SignalData.id = self.id
    g_SignalData.iValue = AIGOALTYPE_FOLLOW
    self.AI.bInConvoy = true
    self:FindPassengers()
    if self.AI.convoyNext then
      self.AI.convoyNext:StartConvoy()
    end
  else
    AI.Warning("Vehicle " .. self:GetName() .. " couldn't join a convoy")
  end
end
function VehicleBaseAI:FindPassengers()
  if not AI then
    return
  end
  local numSeats = count(self.Seats)
  local numHumans = AI.GetGroupCount(self.id, GROUP_ENABLED, AIOBJECT_PUPPET)
  local numCrew
  if numSeats < numHumans then
    numCrew = numSeats
  else
    numCrew = numHumans
  end
  for j = 1, numHumans do
    local puppy = AI.GetGroupMember(self.id, j, GROUP_ENABLED, AIOBJECT_PUPPET)
    if puppy.AI.theVehicle == self then
      puppy.AI.theVehicle = nil
    end
  end
  for i = 1, numCrew do
    local pos = self:GetExitPos(i)
    local mindist = 1000000
    local selected
    for j = 1, numHumans do
      local puppy = AI.GetGroupMember(self.id, j, GROUP_ENABLED, AIOBJECT_PUPPET)
      if puppy.AI.theVehicle then
        System.Log(puppy:GetName() .. " is already in vehicle " .. puppy.AI.theVehicle:GetName())
      end
      if AI.GetTypeOf(puppy.id) == AIOBJECT_PUPPET and puppy.AI.theVehicle == nil then
        FastDifferenceVectors(g_Vectors.temp, puppy:GetWorldPos(), pos)
        local dist = LengthSqVector(g_Vectors.temp)
        if mindist > dist then
          selected = puppy
          mindist = dist
        end
      end
    end
    if selected then
      g_SignalData.fValue = i
      selected.AI.theVehicle = self
      AI.Signal(SIGNALFILTER_SENDER, 1, "ORDER_ENTER_VEHICLE", selected.id, g_SignalData)
    end
  end
end
function VehicleBaseAI:SignalDriver(signalText, data)
  if not AI then
    return
  end
  local driverId = self:GetDriverId()
  if driverId then
    AI.Signal(SIGNALFILTER_SENDER, -1, signalText, driverId, data)
  end
end
function VehicleBaseAI:SignalCrew(signalText, data)
  if not AI then
    return
  end
  for i, seat in pairs(self.Seats) do
    if seat.passengerId then
      AI.Signal(SIGNALFILTER_SENDER, 0, signalText, seat.passengerId, data)
    end
  end
end
function VehicleBaseAI:Event_Follow(params)
  if not AI then
    return
  end
  self:AIDriver(1)
  CopyVector(g_SignalData.point, self:GetWorldPos())
  g_SignalData.id = self.id
  g_SignalData.iValue = AIGOALTYPE_FOLLOW
  self:FindPassengers()
  AI.Signal(0, 1, "FOLLOW", self.id)
  if self.AI.convoyNext and self.AI.isConvoyLeader then
    self.AI.convoyReadyUnits = 0
    self.AI.bInConvoy = true
    self.AI.convoyNext:StartConvoy()
  end
end
function VehicleBaseAI:Event_Break(params)
  if not AI then
    return
  end
  AI.Signal(0, 1, "STOP_VEHICLE", self.id)
end
function VehicleBaseAI:Event_Attack(params)
  if not AI then
    return
  end
  self:AIDriver(1)
  AI.Signal(0, 1, "OnPlayerSeen", self.id)
end
function VehicleBaseAI:Event_Goto(sender)
  if not AI then
    return
  end
  if sender then
    AI.LogEvent("Vehicle " .. self:GetName() .. " going to " .. sender:GetName())
    CopyVector(g_SignalData.point, sender:GetWorldPos())
    g_SignalData.id = self.id
    g_SignalData.iValue = AIGOALTYPE_GOTO
    self.AI.bInConvoy = false
    self:FindPassengers()
    if self.AI.convoyNext and self.AI.isConvoyLeader then
      self.AI.convoyReadyUnits = 0
      self.AI.bInConvoy = true
      self.AI.convoyNext:StartConvoy()
    end
  else
    AI.Warning("Vehicle " .. self:GetName() .. " missing destination in event Goto")
  end
end
function VehicleBaseAI:Event_GoPath(sender)
  if not AI then
    return
  end
  local firstpoint = System.GetEntityByName(self.PropertiesInstance.AI.PathName .. "0")
  if firstpoint then
    if self.PropertiesInstance.bCircularPath == 1 then
      AI.LogEvent("Vehicle " .. self:GetName() .. " following circular path " .. self.PropertiesInstance.AI.PathName)
    else
      AI.LogEvent("Vehicle " .. self:GetName() .. " following linear path " .. self.PropertiesInstance.AI.PathName)
    end
    CopyVector(g_SignalData.point, firstpoint:GetWorldPos())
    g_SignalData.id = self.id
    g_SignalData.iValue = AIGOALTYPE_PATH
    self:FindPassengers()
    if self.AI.convoyNext and self.AI.isConvoyLeader then
      self.AI.convoyReadyUnits = 0
      self.AI.bInConvoy = true
      self.AI.convoyNext:StartConvoy()
    end
  else
    AI.Warning("Vehicle " .. self:GetName() .. " has wrong path name or first path point is missing")
  end
end
function VehicleBaseAI:Event_Test(sender)
  if not AI then
    return
  end
  self:AIDriver(1)
  local point = System.GetEntityByName("place")
  if point then
    g_SignalData.point = point:GetWorldPos()
    AI.Signal(SIGNALFILTER_SENDER, 0, "ORDER_MOVE", self.id, g_SignalData)
  end
end
function CreateVehicleAI(child)
  if child.AIProperties then
    mergef(child, child.AIProperties, 1)
  end
  child.AIProperties = nil
  mergef(child, VehicleBaseAI, 1)
end
function VehicleBaseAI:Event_Init(sender)
  self:InitConvoy()
  BroadcastEvent(self, "Init")
end
function VehicleBaseAI:Event_ConvoyAdd(sender)
  if not AI then
    return
  end
  if sender and sender.id and sender ~= self and AI.GetTypeOf(sender.id) == AIOBJECT_VEHICLE then
    AI.LogEvent("Vehicle " .. self:GetName() .. " receiving Convoy event from " .. sender:GetName())
    self.AI.convoyPrev = sender
    sender.AI.convoyNext = self
    if sender.AI.convoyPrev == nil then
      sender.AI.isConvoyLeader = true
      sender.AI.convoyLeader = sender
      sender.AI.convoyUnits = 2
      sender.AI.convoyPosition = 1
      self.AI.convoyLeader = sender
    else
      self.AI.convoyLeader = sender.convoyLeader
      self.AI.convoyLeader.convoyUnits = self.AI.convoyLeader.convoyUnits + 1
    end
    if sender.AI.convoyPosition then
      self.AI.convoyPosition = sender.AI.convoyPosition + 1
    end
  else
    AI.LogEvent("Vehicle " .. self:GetName() .. " receiving Convoy event ")
  end
  BroadcastEvent(self, "ConvoyAdd")
end
function VehicleBaseAI:Event_ConvoyRemove()
  if not AI then
    return
  end
  if self.AI.convoyNext then
    self.AI.convoyNext.convoyPrev = self.AI.convoyPrev
  end
  if self.AI.convoyPrev then
    self.AI.convoyPrev.convoyNext = self.AI.convoyNext
  end
  if self.AI.convoyLeader and self.AI.convoyLeader.convoyUnits and self.AI.convoyLeader.convoyUnits > 0 then
    self.AI.convoyLeader.convoyUnits = self.AI.convoyLeader.convoyUnits - 1
  end
  if self.AI.isConvoyLeader and self.AI.convoyNext then
    self.AI.convoyNext:SetConvoyLeader()
  elseif self.AI.convoyLeader and self.AI.convoyLeader.convoyUnits and self.AI.convoyLeader.convoyUnits > 0 then
    self.AI.convoyLeader.convoyUnits = self.AI.convoyLeader.convoyUnits - 1
  end
  self.AI.convoyPrev = nil
  self.AI.convoyNext = nil
  self.AI.convoyLeader = nil
  self.AI.isConvoyLeader = false
  BroadcastEvent(self, "ConvoyRemove")
end
function VehicleBaseAI:Event_LogConvoy(sender)
  if not AI then
    return
  end
  local vehicle = self
  while vehicle do
    if vehicle.convoyPosition then
      System.Log("--- Vehicle " .. vehicle:GetName() .. " at position " .. vehicle.convoyPosition)
    end
    if vehicle.convoyPrev then
      System.Log(" prev = " .. vehicle.convoyPrev:GetName())
    else
      System.Log(" prev = NIL")
    end
    if vehicle.convoyNext then
      System.Log(" next = " .. vehicle.convoyNext:GetName())
    else
      System.Log(" next = NIL")
    end
    if vehicle.isConvoyLeader then
      System.Log(" LEADER")
    end
    if vehicle.convoyLeader then
      System.Log(" my leader= " .. vehicle.convoyLeader:GetName())
    else
      System.Log(" my leader= NULL")
    end
    vehicle = vehicle.convoyNext
  end
end
function VehicleBaseAI:SetFireSpot(targetpos, distance)
  if not AI then
    return
  end
  AI.LogEvent("SETTING FIRE SPOT")
  local moveDir = g_Vectors.temp_v1
  local spotpos = g_Vectors.temp_v2
  local spot_Z = g_Vectors.temp_v3
  FastDifferenceVectors(moveDir, self:GetWorldPos(), targetpos)
  moveDir.z = 0
  local module = distance / LengthVector(moveDir)
  FastScaleVector(moveDir, moveDir, module)
  moveDir.z = distance / 2
  FastSumVectors(spotpos, targetpos, moveDir)
  spot_Z.x = 0
  spot_Z.y = 0
  spot_Z.z = -distance / 1.8
  hits = Physics.RayWorldIntersection(spotpos, spot_Z, 2, ent_terrain + ent_static + ent_rigid + ent_sleeping_rigid, self.id, nil, g_HitTable)
  if 0 < hits then
    local firstHit = g_HitTable[1]
    if firstHit.entity then
      return false
    end
    AI.LogEvent("SETTING FIRE SPOT2")
    AI.SetRefPointPosition(self.id, firstHit.pos)
    return true
  end
  return false
end
function VehicleBaseAI:UserEntered(user)
  if not AI then
    return
  end
  if self:IsDriver(user.id) then
    self:ChangeSpecies(user, 1)
  end
  do return end
  user:SelectPipe(0, "do_nothing")
  user:InsertSubpipe(0, "ignore_none")
end
function VehicleBaseAI:SetRefPointAtDistanceFromTarget(mindistance, maxdistance, minZ)
  if not AI then
    return
  end
  local targetPos = g_Vectors.temp
  local targetDir = g_Vectors.temp_v1
  AI.GetAttentionTargetDirection(self.id, targetDir)
  AI.LogEvent("TARGET DIR: " .. Vec2Str(targetDir))
  if LengthSqVector(targetDir) < 0.05 then
    return false
  end
  AI.GetAttentionTargetPosition(self.id, targetPos)
  ScaleVectorInPlace(targetDir, -maxdistance)
  local hits = Physics.RayWorldIntersection(targetPos, targetDir, 2, ent_terrain + ent_static + ent_rigid + ent_sleeping_rigid, self.id, nil, g_HitTable)
  local pos
  if hits > 0 then
    local firstHit = g_HitTable[1]
    if mindistance <= firstHit.dist then
      pos = firstHit.pos
    end
  else
    FastSumVectors(targetPos, targetPos, targetDir)
    pos = targetPos
  end
  if pos and minZ then
    targetDir.x = 0
    targetDir.y = 0
    targetDir.z = -3
    hits = Physics.RayWorldIntersection(targetPos, targetDir, 2, ent_terrain + ent_static + ent_rigid + ent_sleeping_rigid, self.id, nil, g_HitTable)
    if hits > 0 then
      local firstHit = g_HitTable[1]
      if minZ > firstHit.normal.z then
        pos = nil
      end
    end
  end
  if pos then
    AI.SetRefPointPosition(self.id, pos)
    return true
  end
  return false
end
function VehicleBaseAI:GetFireProperties()
  if not AI then
    return
  end
  local seat = self.Seats[1]
  if seat then
    local nw = seat.seat:GetWeaponCount()
    for j = 1, nw do
      if self.AIFireProperties[j] == nil then
        self.AIFireProperties[j] = {}
      end
      local weapon = System.GetEntity(seat.seat:GetWeaponId(j))
      local mass, initial_speed
      if weapon then
        mass = 1
        initial_speed = 400
        self.AIFireProperties[j].damage_radius = 10
        self.AIFireProperties[j].damage = 500
        self.AIFireProperties[j].mass = mass
        self.AIFireProperties[j].initial_speed = initial_speed
        local spread
        if spread == nil or spread <= 0 then
          spread = 0
        elseif spread > 10 then
          spread = 10
        end
        self.AIFireProperties[j].accuracy_multiplier = (101 - spread * spread) / 10
      else
      end
    end
  end
  if self.GetSpecificFireProperties then
    self:GetSpecificFireProperties()
  end
end
function VehicleBaseAI:Act_Move(data)
  if not AI then
    return
  end
  AI.LogEvent(">>>> VehicleBaseAI :: ACT_MOVE " .. self:GetName())
  if data and data.point then
    AI.LogEvent(">>>> moving to point ")
    AI.SetRefPointPosition(self.id, data.point)
  end
  if self.State.aiDriver == nil then
    local numSeats = count(self.Seats)
    local numHumans = AI.GetGroupCount(self.id, GROUP_ENABLED, AIOBJECT_PUPPET)
    local bFound = false
    for j = 1, numHumans do
      local puppy = AI.GetGroupMember(self.id, j, GROUP_ENABLED, AIOBJECT_PUPPET)
      if puppy.AI.theVehicle == self then
        puppy.AI.theVehicle.AI.goalType = AIGOALTYPE_GOTO
        bFound = true
      end
    end
    if bFound == true then
      return
    end
    AI.LogEvent(">>>> NO AI DRIVER ")
    if self.AI.driver ~= nil then
      AI.LogEvent(">>>> AI DRIVER ENTERING  ")
      self.AI.goalType = AIGOALTYPE_GOTO
    end
    CopyVector(g_SignalData.point, data.point)
    g_SignalData.id = self.id
    g_SignalData.iValue = AIGOALTYPE_GOTO
    self:FindPassengers()
    if self.AI.convoyNext and self.AI.isConvoyLeader then
      self.AI.convoyReadyUnits = 0
      self.AI.bInConvoy = true
      self.AI.convoyNext:StartConvoy()
    end
  else
    AI.LogEvent(">>>> AI DRIVER IN ")
    self:InsertSubpipe(AIGOALPIPE_SAMEPRIORITY, "vehicle_goto", nil, data.iValue)
  end
end
function VehicleBaseAI:Act_MoveHeli(data)
  if not AI then
    return
  end
  AI.LogEvent(">>>> VehicleBaseAI :: ACT_MOVE heli " .. self:GetName())
  if data and data.point then
    AI.LogEvent(">>>> moving to point ")
    AI.SetRefPointPosition(self.id, data.point)
  end
  if self.State.aiDriver == nil then
    AI.LogEvent(">>>> NO AI DRIVER ")
    CopyVector(g_SignalData.point, data.point)
    g_SignalData.id = self.id
    g_SignalData.iValue = AIGOALTYPE_GOTO
    self:FindPassengers()
  else
    AI.LogEvent(">>>> AI DRIVER IN ")
    self:InsertSubpipe(AIGOALPIPE_SAMEPRIORITY, "h_move")
  end
end
function VehicleBaseAI:Act_Follow(leader)
  if not AI then
    return
  end
  self.AI.convoyLeader = leader
  self.AI.convoyPrev = leader
  leader.AI.convoyNext = self
  AI.LogEvent("Vehicle " .. self:GetName() .. " joining " .. self.AI.convoyPrev:GetName() .. " in convoy")
  g_SignalData.id = self.id
  g_SignalData.iValue = AIGOALTYPE_FOLLOW
  self.AI.bInConvoy = true
  if self.State.aiDriver == nil then
    if self.AI.driver ~= nil then
      AI.LogEvent(">>>> follow --- AI DRIVER ENTERING  ")
      AI.Signal(SIGNALFILTER_SENDER, 1, "READY_FOR_CONVOY_START", self.AI.convoyLeader.id, self.id)
    else
      self:FindPassengers()
    end
  else
    AI.LogEvent(">>>> follow --- AI DRIVER inside  ")
    AI.Signal(SIGNALFILTER_SENDER, 1, "FOLLOW", self.id)
  end
end
function VehicleBaseAI:Act_Unload(params)
  if not AI then
    return
  end
  AI.Signal(0, 1, "STOP_VEHICLE", self.id)
end
function VehicleBaseAI:OnVehicleDestroyed()
  if not AI then
    return
  end
  self:DisableAI()
  if self.AI.spawnerListenerId then
    local spawnerEnt = System.GetEntity(self.AI.spawnerListenerId)
    if spawnerEnt then
      spawnerEnt:UnitDown()
    end
  end
  BroadcastEvent(self, "Destroy")
  self:TriggerEvent(AIEVENT_AGENTDIED)
  if self.AI.MaybeNextLevel then
    self:MaybeNextLevel()
  end
end
function VehicleBaseAI:OnVehicleImmobilized()
  self:DisableAI()
end
function VehicleBaseAI:DisableAI()
  self:SignalCrew("SHARED_LEAVE_ME_VEHICLE")
  if self.AIDriver then
    self:AIDriver(0)
  end
end
function VehicleBaseAI:GetSeatWithWeapon(weapon)
  for i, seat in pairs(self.Seats) do
    local wc = seat.seat:GetWeaponCount()
    for j = 1, wc do
      if seat.seat:GetWeaponId(j) == weapon.id then
        return i
      end
    end
  end
  return nil
end
function VehicleBaseAI:ChangeSpecies(driver, isEntered)
  if not AI then
    return
  end
  if isEntered == 0 or isEntered == nil then
    AI.ChangeParameter(self.id, AIPARAM_SPECIES, -1)
    self.AI.hostileSet = nil
    return
  end
  if driver.Properties.species == nil then
    return
  end
  if self.Properties.bHidesPlayer == 1 and isEntered == 1 and driver.ai == nil and driver.Properties.species ~= self.defaultSpecies then
    return
  end
  AI.ChangeParameter(self.id, AIPARAM_SPECIES, driver.Properties.species)
  self.AI.hostileSet = 1
end
function VehicleBaseAI:SpawnCopyAndLoad()
  local params = {
    class = self.class,
    position = self:GetPos(),
    orientation = self:GetDirectionVector(1),
    scale = self:GetScale(),
    properties = self.Properties,
    propertiesInstance = self.PropertiesInstance
  }
  local rndOffset = 1
  params.position.x = params.position.x + random(0, rndOffset * 2) - rndOffset
  params.position.y = params.position.y + random(0, rndOffset * 2) - rndOffset
  params.name = self:GetName()
  local ent = System.SpawnEntity(params)
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
    self:LoadLinked(ent)
  end
end
function VehicleBaseAI:LoadLinked(newVehicle)
  if not AI then
    return
  end
  local i = 0
  local link = self:GetLink(i)
  while link do
    if link and link.Event_SpawnKeep then
      link:Event_SpawnKeep()
      local newEntity = System.GetEntity(link.spawnedEntity)
      if link.PropertiesInstance.bAutoDisable ~= 1 then
        AI.AutoDisable(newEntity.id, 0)
      end
      newEntity:SetName(newEntity:GetName() .. "_vspawned")
      if newEntity then
        g_SignalData.fValue = i + 1
        g_SignalData.id = newVehicle.id
        g_SignalData.iValue2 = 1
        AI.Signal(SIGNALFILTER_SENDER, 0, "ACT_ENTERVEHICLE", newEntity.id, g_SignalData)
      end
    end
    i = i + 1
    link = self:GetLink(i)
  end
end
function VehicleBaseAI:AutoDisablePassangers(status)
  if not AI then
    return
  end
  for i, seat in pairs(self.Seats) do
    if seat:GetPassengerId() then
      AI.AutoDisable(seat:GetPassengerId(), status)
    end
  end
end
function VehicleBaseAI:OnPassengerDead(passenger)
  if self.class == "Asian_helicopter" then
    for i, seat in pairs(self.Seats) do
      if seat.passengerId then
        local member = System.GetEntity(seat.passengerId)
        if member ~= nil and seat.isDriver and passenger.id == member.id then
          Script.SetTimerForFunction(4000, "VehicleBaseAI.PassengerDeadExplode", self)
        end
      end
    end
  end
end
function VehicleBaseAI:PassengerDeadExplode()
  g_gameRules:CreateExplosion(self.id, self.id, 1000, self:GetPos(), nil, 10)
end
function VehicleBaseAI:IsEntityOnVehicle(entityId)
  if entityId then
    local numSeats = count(self.Seats)
    for i = 1, numSeats do
      local seat = self:GetSeatByIndex(i)
      if seat then
        local PassengerId = seat:GetPassengerId()
        if PassengerId == entityId then
          return true
        end
      end
    end
  end
end
VehicleBaseAI.FlowEvents = {}
