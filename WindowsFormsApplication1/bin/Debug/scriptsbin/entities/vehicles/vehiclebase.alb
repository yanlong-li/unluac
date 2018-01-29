Script.ReloadScript("Scripts/Utils/Math.lua")
Script.ReloadScript("Scripts/Entities/Vehicles/VehicleSeat.lua")
VehicleBase = {
  State = {
    pos = {},
    Carriage = {},
    aiDriver = nil
  },
  Seats = {},
  AI = {
    commandSet = {}
  },
  Client = {},
  Server = {},
  Hit = {}
}
function IsAnyPassenger(seats)
  for i, seat in pairs(seats) do
    if seat:GetPassengerId() then
      return true
    end
  end
  return false
end
function VehicleBase:HasDriver()
  for i, seat in pairs(self.Seats) do
    if seat.isDriver and seat.passengerId then
      return true
    end
  end
  return false
end
function VehicleBase:GetDriverId()
  for i, seat in pairs(self.Seats) do
    if seat.isDriver then
      return seat:GetPassengerId()
    end
  end
  return nil
end
function GetNextAvailableSeat(seats)
  for i, seat in pairs(seats) do
    if not seat:GetPassengerId() then
      return i
    end
  end
  return -1
end
function VehicleBase:ApplyMaterial(strMat, bIncludeCharacters)
  Log("VehicleBase:ApplyMaterial is deprecated!")
end
function VehicleBase:InitSeats()
  if self.Seats then
    for i, seat in pairs(self.Seats) do
      mergef(seat, VehicleSeat, 1)
      seat:Init(self, i)
    end
  end
end
function VehicleBase:InitVehicleBase()
  self:OnPropertyChange()
end
function VehicleBase:OnPropertyChange()
  if self.OnPropertyChangeExtra then
    self:OnPropertyChangeExtra()
  end
end
function VehicleBase:MountEntity(a_className, transformTable, propertiesTable)
  local spawnParams = {}
  spawnParams.class = a_className
  spawnParams.name = self:GetName() .. "_" .. a_className .. "_mount"
  spawnParams.scale = 1
  spawnParams.flags = 0
  if transformTable.helper then
    if self.vehicle:HasHelper(transformTable.helper) then
      spawnParams.position = self.vehicle:GetHelperPos(transformTable.helper, true)
      spawnParams.orientation = self.vehicle:GetHelperDir(transformTable.helper, true)
    else
      spawnParams.position = {
        0,
        0,
        0
      }
      spawnParams.orientation = {
        0,
        1,
        0
      }
    end
  else
    spawnParams.position = self:GetPos()
  end
  if transformTable.position then
    FastSumVectors(spawnParams.position, spawnParams.position, transformTable.position)
  end
  if transformTable.orientation then
    spawnParams.orientation = transformTable.orientation
  end
  if propertiesTable then
    spawnParams.properties = new(propertiesTable)
  else
    spawnParams.properties = nil
  end
  local spawnedEntity = System.SpawnEntity(spawnParams)
  if spawnedEntity ~= nil then
    if transformTable.scale then
      spawnedEntity:SetScale(transformTable.scale)
    end
    spawnedEntity:EnablePhysics(0)
    spawnedEntity:SetFlags(ENTITY_FLAG_RECVSHADOW, 0)
    spawnedEntity:SetFlags(ENTITY_FLAG_CASTSHADOW, 0)
    if spawnedEntity.AIDriver then
      spawnedEntity:AIDriver(0)
    end
    local physProps = {
      Physics = {
        mass = 100,
        bResting = 1,
        bVisible = 1,
        bRigidBodyActive = 0,
        damping = 0.1
      },
      Simulation = {
        max_time_step = 0.04,
        min_energy = 0.0016,
        max_logged_collisions = 1
      },
      Flags = {flags_collider_mask = 8}
    }
    self:AttachChild(spawnedEntity.id, 0)
    self.State.Carriage[count(self.State.Carriage) + 1] = {
      id = spawnedEntity.id,
      object = propertiesTable.object_Model,
      position = spawnParams.position,
      orientation = spawnParams.orientation,
      useText = transformTable.useText
    }
  else
    Log("Couldn't spawn the child entity attachment")
  end
  return spawnedEntity
end
function VehicleBase:DestroyCarriage()
  if self.State.Carriage then
    for i, cargo in pairs(self.State.Carriage) do
      if cargo.id then
        Entity.DetachThis(cargo.id, 0)
        System.RemoveEntity(cargo.id)
      end
    end
  end
end
function VehicleBase:DestroyVehicleBase()
  self:DestroyCarriage()
  if self.DestroyAI then
    self:DestroyAI()
  end
end
function VehicleBase:GetExitPos(seatId)
  if self.Seats[seatId] == nil then
    Log("VehicleBase:GetExitPos(seatId) - Invalid seat id: " .. tostring(seatId))
    return
  end
  local exitPos
  local seat = self.Seats[seatId]
  if seat.exitHelper then
    exitPos = self.vehicle:MultiplyWithWorldTM(self:GetVehicleHelperPos(self.Seats[seatId].exitHelper))
  else
    exitPos = self.vehicle:MultiplyWithWorldTM(self:GetVehicleHelperPos(self.Seats[seatId].enterHelper))
  end
  return exitPos
end
function VehicleBase:GetSeatPos(seatId)
  if seatId == -1 then
    Log("Error: VehicleBase:GetSeatPos(seatId) - seatId -1 is invalid")
    return {
      x = 0,
      y = 0,
      z = 0
    }
  else
    local helper = self.Seats[seatId].enterHelper
    local pos
    if self.vehicle:HasHelper(helper) then
      pos = self.vehicle:GetHelperWorldPos(helper)
    else
      pos = self:GetHelperPos(helper, HELPER_WORLD)
    end
    return pos
  end
end
function VehicleBase:OnCargoUsed(playerId, idx)
  local cargo = self.State.Carriage[idx]
  if cargo then
    local ent = System.GetEntity(cargo.id)
    if ent then
      if ent.OnCargoUsed then
        ent:OnCargoUsed(playerId)
      end
      Entity.DetachThis(cargo.id, 0)
      System.RemoveEntity(cargo.id)
    end
  end
end
function VehicleBase:OnUsed(user, idx)
  if not CryAction.IsClient() then
    return
  end
  if idx <= 100 then
  elseif idx <= 300 then
  elseif idx <= 400 then
    self:OnCargoUsed(user.id, idx - 300)
  end
  self.vehicle:OnUsed(user.id, idx)
end
function VehicleBase:IsUsable(user)
  if user:IsOnVehicle() then
    return 0
  end
  self.onUsablePos = user:GetWorldPos(self.onUsablePos)
  local pos = self.onUsablePos
  local radiusSq = 4
  if self.State.Carriage then
    for i, cargo in pairs(self.State.Carriage) do
      local ent = System.GetEntity(cargo.id)
      if ent then
        local distSq = DistanceSqVectors(pos, ent:GetWorldPos())
        if distSq < 2.5 then
          return 300 + i
        end
      end
    end
  end
  local ret = self.vehicle:IsUsable(user.id)
  if ret ~= 0 then
    return ret
  end
  return 0
end
function VehicleBase:CanEnter(userId)
  if g_gameRules and g_gameRules.CanEnterVehicle then
    return g_gameRules:CanEnterVehicle(self, userId)
  end
end
function VehicleBase:GetSeat(userId)
  for i, seat in pairs(self.Seats) do
    if seat:GetPassengerId() == userId then
      return seat
    end
  end
  return nil
end
function VehicleBase:GetSeatByIndex(i)
  return self.Seats[i]
end
function VehicleBase:GetSeatId(userId)
  for i, seat in pairs(self.Seats) do
    if seat:GetPassengerId() == userId then
      return i
    end
  end
  return nil
end
function VehicleBase:ResetVehicleBase()
  self.State.pos = self:GetWorldPos(self.State.pos)
  if self.AIDriver then
    self:AIDriver(0)
  end
  self.State.aiDriver = nil
  self:OnPropertyChange()
  if self.Seats then
    for i, seat in pairs(self.Seats) do
      seat:OnReset()
    end
  end
  if self.ResetAI then
    self:ResetAI()
  end
end
function VehicleBase:OnShutDown()
end
function VehicleBase:OnDestroy()
  self:DestroyVehicleBase()
end
function VehicleBase:UseCustomFiring(weaponId)
  return false
end
function VehicleBase:GetFiringPos(weaponId)
  return g_Vectors.v000
end
function VehicleBase:GetFiringVelocity()
  return g_Vectors.v000
end
function VehicleBase:OnAIShoot()
  local weaponId = self.Seats[1]:GetWeaponId(1)
  if weaponId then
    local weapon = System.GetEntity(weaponId)
    weapon:StartFire(self.id)
    weapon:StopFire()
  end
end
function VehicleBase:IsGunner(userId)
  local seat = self:GetSeat(userId)
  if seat and seat.Weapons then
    return true
  end
  return false
end
function VehicleBase:IsDriver(userId)
  local seat = self:GetSeat(userId)
  if seat and seat.isDriver then
    return true
  end
  return false
end
function VehicleBase:GetVehicleHelperPos(helperName)
  helperName = helperName or ""
  local pos
  if self.vehicle:HasHelper(helperName) then
    pos = self.vehicle:GetHelperPos(helperName, true)
  else
    pos = self:GetHelperPos(helperName, HELPER_LOCAL)
  end
  return pos
end
function VehicleBase:RequestSeatByPosition(userId)
  local pos = System.GetEntity(userId):GetWorldPos()
  local radiusSq = 10
  for i, seat in pairs(self.Seats) do
    if seat.enterHelper and not seat.passengerId then
      if seat.useBoundsForEntering == nil or seat.useBoundsForEntering == true then
        if self.vehicle:IsInsideRadius(pos, 1) then
          return i
        end
      else
        local enterPos
        if self.vehicle:HasHelper(seat.enterHelper) then
          enterPos = self.vehicle:GetHelperWorldPos(seat.enterHelper)
        else
          enterPos = self:GetHelperPos(seat.enterHelper, HELPER_WORLD)
        end
        local distanceSq = DistanceSqVectors(pos, enterPos)
        if radiusSq >= distanceSq then
          return i
        end
      end
    end
  end
  return nil
end
function VehicleBase:RequestClosestSeat(userId)
  local pos = System.GetEntity(userId):GetWorldPos()
  local minDistanceSq = 100000
  local selectedSeat
  for i, seat in pairs(self.Seats) do
    if seat.enterHelper and seat:IsFree() then
      local enterPos
      if self.vehicle:HasHelper(seat.enterHelper) then
        enterPos = self.vehicle:GetHelperWorldPos(seat.enterHelper)
      else
        enterPos = self:GetHelperPos(seat.enterHelper, HELPER_WORLD)
      end
      local distanceSq = DistanceSqVectors(pos, enterPos)
      if minDistanceSq >= distanceSq then
        minDistanceSq = distanceSq
        selectedSeat = i
      end
    end
  end
  if selectedSeat then
    if AI then
      AI.LogEvent(System.GetEntity(userId):GetName() .. " found seat " .. selectedSeat)
    end
  elseif AI then
    AI.LogEvent(System.GetEntity(userId):GetName() .. " found no seat")
  end
  return selectedSeat
end
function VehicleBase:RequestMostPrioritarySeat(userId)
  local pos = System.GetEntity(userId):GetWorldPos()
  local selectedSeat
  local seat = self.Seats[1]
  if seat:IsFree() then
    return 1
  end
  for i, seat in pairs(self.Seats) do
    if seat.enterHelper and seat.Weapons and seat:IsFree() then
      if AI then
        AI.LogEvent(System.GetEntity(userId):GetName() .. " found seat " .. i)
      end
      return i
    end
  end
  for i, seat in pairs(self.Seats) do
    if seat.enterHelper and seat:IsFree() then
      if AI then
        AI.LogEvent(System.GetEntity(userId):GetName() .. " found seat " .. i)
      end
      return i
    end
  end
  return
end
function VehicleBase:RequestSeat(userId)
  local pos = System.GetEntity(userId):GetWorldPos()
  local radiusSq = 6
  for i, seat in pairs(self.Seats) do
    if seat:IsFree() then
      return i
    end
  end
  return nil
end
function VehicleBase:EnterVehicle(passengerId, seatId, isAnimationEnabled)
  return self.vehicle:EnterVehicle(passengerId, seatId, isAnimationEnabled)
end
function VehicleBase:LeaveVehicle(passengerId, fastLeave)
  if AI then
    AI.Signal(SIGNALFILTER_SENDER, 0, "exited_vehicle", passengerId)
  end
  return self.vehicle:ExitVehicle(passengerId)
end
function VehicleBase:MotionTrackable()
  return 1
end
function VehicleBase:UpdateRadar(radarContact)
  if not self.vehicle:IsDestroyed() then
    radarContact.img = "textures/gui/hud/radar/vehicle_grey.dds"
    radarContact.radius = 4
    radarContact.color[1] = 26
    radarContact.color[2] = 255
    radarContact.color[3] = 26
    if type(self:GetDriverId()) == "userdata" then
      local driver = System.GetEntity(self:GetDriverId())
      if driver and driver.actor then
        if driver.actor:IsPlayer() then
          return false
        end
        local alertness = driver.Behaviour.alertness
        if g_localActor then
          local targetName
          if AI then
            targetName = AI.GetAttentionTargetOf(self.id)
          end
          if targetName and targetName == g_localActor:GetName() then
            alertness = 2
            radarContact.blinking = 1
            radarContact.blinkColor[1] = 255
            radarContact.blinkColor[2] = 255
            radarContact.blinkColor[3] = 255
          end
        end
        if not alertness or alertness == 0 then
          radarContact.color[1] = 26
          radarContact.color[2] = 255
          radarContact.color[3] = 26
        elseif alertness == 1 then
          radarContact.color[1] = 255
          radarContact.color[2] = 128
          radarContact.color[3] = 26
        else
          radarContact.color[1] = 255
          radarContact.color[2] = 26
          radarContact.color[3] = 26
        end
      end
    end
    return true
  else
    return false
  end
end
function VehicleBase:MotionTrackable()
  return not self.vehicle:IsDestroyed()
end
function VehicleBase:ReserveSeat(userId, seatidx)
  self.Seats[seatidx].passengerId = userId
end
function VehicleBase:IsDead()
  return self.vehicle:IsDestroyed()
end
function VehicleBase:GetWeaponVelocity(weaponId)
  return self:GetFiringVelocity()
end
function VehicleBase:OnShoot(weapon, remote)
  if weapon.userId then
    local seat = self:GetSeat(weapon.userId)
    if seat and seat.Animations and seat.Animations.weaponRecoil then
      local user = System.GetEntity(weapon.userId)
      if user:IsDead() then
        return
      end
      user:StartAnimation(0, seat.Animations.weaponRecoil, 0, 1.0E-9, 1, false)
    end
  end
  return true
end
function VehicleBase:SpawnVehicleBase()
  if self.OnPreSpawn then
    self:OnPreSpawn()
  end
  if _G[self.class .. "Properties"] then
    mergef(self, _G[self.class .. "Properties"], 1)
  end
  if self.OnPreInit then
    self:OnPreInit()
  end
  self:InitVehicleBase()
  self.ProcessMovement = nil
  if not EmptyString(self.Properties.FrozenModel) then
    self.frozenModelSlot = self:LoadObject(-1, self.Properties.FrozenModel)
    self:DrawSlot(self.frozenModelSlot, 0)
  end
  if self.OnPostSpawn then
    self:OnPostSpawn()
  end
  local aiSpeed = self.Properties.aiSpeedMult
  local AIProps = self.AIMovementAbility
  if AIProps and aiSpeed and aiSpeed ~= 1 then
    if AIProps.walkSpeed then
      AIProps.walkSpeed = AIProps.walkSpeed * aiSpeed
    end
    if AIProps.runSpeed then
      AIProps.runSpeed = AIProps.runSpeed * aiSpeed
    end
    if AIProps.sprintSpeed then
      AIProps.sprintSpeed = AIProps.sprintSpeed * aiSpeed
    end
    if AIProps.maneuverSpeed then
      AIProps.maneuverSpeed = AIProps.maneuverSpeed * aiSpeed
    end
  end
  if self.InitAI then
    self:InitAI()
  end
  self:InitSeats()
  self:OnReset()
end
function VehicleBase:LoadXML()
  local dataTable = VehicleSystem.LoadXML(self.class)
  if dataTable then
    if dataTable.Seats then
      self.Seats = new(dataTable.Seats)
    end
  else
    Log("[VehicleBase:LoadXML] dataTable nil!")
    return false
  end
  return true
end
function VehicleBase:GetFrozenSlot()
  return self.frozenModelSlot
end
function VehicleBase:GetFrozenAmount()
  return self.vehicle:GetFrozenAmount()
end
function VehicleBase.Server:OnHit(hit)
  Log("NEVER CALL! VehicleBase Server:OnHit")
  local explosion = hit.explosion or false
  local targetId = explosion and hit.impact and hit.impact_targetId or hit.targetId
  local hitType = explosion and hit.type == "" and "explosion" or hit.type
  local direction = hit.dir
  if hit.type ~= "fire" then
    g_gameRules.game:SendHitIndicator(hit.shooterId)
  end
  if hit.type == "collision" then
    direction.x = -direction.x
    direction.y = -direction.y
    direction.z = -direction.z
  end
  if g_localActorId and self:GetSeat(g_localActorId) then
    HUD.DamageIndicator(hit.weaponId, hit.shooterId, direction, true)
  end
  self.vehicle:OnHit(targetId, hit.shooterId, hit.damage, hit.pos, hit.radius, hitType, explosion)
  if AI and hit.type ~= "collision" then
    if hit.shooter then
      g_SignalData.id = hit.shooterId
    else
      g_SignalData.id = NULL_ENTITY
    end
    g_SignalData.fValue = hit.damage
    if hit.shooter and self.Properties.species ~= hit.shooter.Properties.species then
      CopyVector(g_SignalData.point, hit.shooter:GetWorldPos())
      AI.Signal(SIGNALFILTER_SENDER, 0, "OnEnemyDamage", self.id, g_SignalData)
    elseif self.Behaviour and self.Behaviour.OnFriendlyDamage ~= nil then
      AI.Signal(SIGNALFILTER_SENDER, 0, "OnFriendlyDamage", self.id, g_SignalData)
    else
      AI.Signal(SIGNALFILTER_SENDER, 0, "OnDamage", self.id, g_SignalData)
    end
  end
  return self.vehicle:IsDestroyed()
end
function VehicleBase.Client:OnHit(hit)
end
function VehicleBase:ProcessPassengerDamage(passengerId, actorHealth, damage, damageType, explosion)
  return self.vehicle:ProcessPassengerDamage(passengerId, actorHealth, damage, damageType, explosion)
end
function VehicleBase:OnActorRequestToSit(seatId, passengerId)
end
function VehicleBase:OnActorRequestToSitCancelled(seatId, passengerId)
end
function VehicleBase:OnActorSitDown(seatId, passengerId)
  local passenger = System.GetEntity(passengerId)
  if not passenger then
    Log("Error: entity for player id <%s> could not be found. %s", tostring(passengerId))
    return
  end
  local seat = self.Seats[seatId]
  if not seat then
    Log("Error: entity for player id <%s> could not be found!", tostring(passengerId))
    return
  end
  if g_gameRules.OnEnterVehicleSeat then
    g_gameRules:OnEnterVehicleSeat(self, seat, passengerId)
  end
  if seat.isDriver then
    self:SetTimer(AISOUND_TIMER, AISOUND_TIMEOUT)
  end
  seat.passengerId = passengerId
  passenger.vehicleId = self.id
  passenger.AI.theVehicle = self
  if passenger.ai then
    if seat.isDriver then
      self.State.aiDriver = 1
      if passenger.actor and passenger.actor:GetHealth() > 0 then
        self:AIDriver(1)
      else
        self:AIDriver(0)
      end
    end
  else
    if self.hidesUser == 1 then
      local isOutsideGunner = false
      isOutsideGunner = seat.Sounds and 0 < seat.seat:GetWeaponCount() and seat.Sounds.inout == 1
      if AI and not isOutsideGunner then
        AI.ChangeParameter(passengerId, AIPARAM_INVISIBLE, 1)
      end
    end
    if AI and seat.isDriver then
      CopyVector(g_SignalData.point, g_Vectors.v000)
      CopyVector(g_SignalData.point2, g_Vectors.v000)
      g_SignalData.iValue = AIUSEOP_VEHICLE
      g_SignalData.iValue2 = 1
      g_SignalData.fValue = 1
      g_SignalData.id = seat.vehicleId
      AI.Signal(SIGNALFILTER_LEADER, 1, "ORD_USE", passengerId, g_SignalData)
    end
    self:EnableMountedWeapons(false)
  end
  if seat.isDriver and passenger.Properties and passenger.Properties.species and self.ChangeSpecies then
    self:ChangeSpecies(passenger, 1)
  else
  end
  local wc = seat.seat:GetWeaponCount()
  if AI then
    if 0 < seat.seat:GetWeaponCount() then
      if seat.isDriver then
        AI.Signal(SIGNALFILTER_SENDER, 1, "entered_vehicle", passengerId)
      else
        AI.Signal(SIGNALFILTER_SENDER, 1, "entered_vehicle_gunner", passengerId)
      end
    else
      AI.Signal(SIGNALFILTER_SENDER, 1, "entered_vehicle", passengerId)
    end
    AI.Signal(SIGNALFILTER_SENDER, 9, "ENTERING_END", passengerId)
  end
end
function VehicleBase:OnActorChangeSeat(passengerId, exiting)
  Log("ai changed a seat")
  local seat = self:GetSeat(passengerId)
  if not seat then
    Log("Error: VehicleBase:OnActorChangeSeat() could not find passenger id %s on the vehicle", tostring(passengerId))
    return
  end
  seat.passengerId = nil
  if g_gameRules and g_gameRules.OnLeaveVehicleSeat then
    g_gameRules:OnLeaveVehicleSeat(self, seat, passengerId, exiting)
  end
  if not passenger then
    return
  end
  passenger.vehicleId = nil
  passenger.vehicleSeatId = nil
  if passenger.ai and passenger:IsDead() then
    return
  end
  if seat.isDriver then
    self.State.aiDriver = nil
    if passenger.ai and exiting then
      self:AIDriver(0)
    end
  end
  BroadcastEvent(self, "PassengerExit")
end
function VehicleBase:OnActorStandUp(passengerId, exiting)
  local seat = self:GetSeat(passengerId)
  if not seat then
    Log("Error: VehicleBase:OnActorStandUp() could not find passenger id %s on the vehicle", tostring(passengerId))
    return
  end
  seat.passengerId = nil
  if g_gameRules and g_gameRules.OnLeaveVehicleSeat then
    g_gameRules:OnLeaveVehicleSeat(self, seat, passengerId, exiting)
  end
  local passenger = System.GetEntity(passengerId)
  if not passenger then
    return
  end
  passenger.vehicleId = nil
  passenger.vehicleSeatId = nil
  if passenger.ai and passenger:IsDead() then
    return
  end
  if seat.isDriver then
    self.State.aiDriver = nil
    if passenger.ai and exiting then
      self:AIDriver(0)
    end
  end
  if passenger.ai ~= 1 and exiting then
    if self.ChangeSpecies then
      self:ChangeSpecies()
    end
    if AI then
      AI.Signal(SIGNALFILTER_LEADERENTITY, 0, "ORD_LEAVE_VEHICLE", passengerId)
      AI.Signal(SIGNALFILTER_GROUPONLY, 0, "ORDER_EXIT_VEHICLE", passengerId)
      passenger.AI.theVehicle = nil
    end
  end
  if self.AI.unloadCount then
    self.AI.unloadCount = self.AI.unloadCount - 1
  end
  if self.AI.unloadCount == 0 then
    AI.Signal(SIGNALFILTER_SENDER, 9, "UNLOAD_DONE", self.id)
  end
  if passenger.AI.theVehicle ~= nil and AI then
    AI.Signal(SIGNALFILTER_SENDER, 0, "EXIT_VEHICLE_STAND", passenger.id)
  end
  if not passenger.ai then
    if AI then
      AI.ChangeParameter(passengerId, AIPARAM_INVISIBLE, 0)
    end
    self:EnableMountedWeapons(true)
  end
  if AI then
    AI.Signal(SIGNALFILTER_SENDER, 9, "EXITING_END", passengerId)
  end
  BroadcastEvent(self, "PassengerExit")
end
function VehicleBase:EnableMountedWeapons(enable)
  if not AI then
    return
  end
  for i, seat in pairs(self.Seats) do
    local wc = seat.seat:GetWeaponCount()
    for j = 1, wc do
      local weaponid = seat.seat:GetWeaponId(j)
      if weaponid then
        if enable then
          AI.SetSmartObjectState(weaponid, "Idle")
        else
          AI.SetSmartObjectState(weaponid, "Busy")
        end
      end
    end
  end
end
function VehicleBase:GetCollisionDamageThreshold()
  return self.vehicle:GetCollisionDamageThreshold()
end
function VehicleBase:GetSelfCollisionMult(collider, hit)
  local mult = 1
  mult = self.vehicle:GetSelfCollisionMult(hit.velocity, hit.normal, hit.partId or -1, hit.target_id or NULL_ENTITY)
  return mult
end
function VehicleBase:GetForeignCollisionMult(entity, hit)
  local mult = 1
  if entity.vehicle then
    local mass = self:GetMass()
    local entityMass = entity:GetMass()
    local speedSq = vecLenSq(hit.target_velocity)
    if mass > 1.5 * entityMass and speedSq > 0.01 then
      local ratio = 1 + 0.35 * __min(10, mass / entityMass) * __min(1, speedSq / sqr(10))
      mult = 1 / ratio
      local debug = g_gameRules and g_gameRules.game:DebugCollisionDamage() or 0
      if mult < 0.99 and debug > 0 or debug > 2 then
        Log("vehicle/vehicle (%s <- %s), coll, mult: %.2f", entity:GetName(), self:GetName(), mult)
      end
    end
  elseif entity.actor and not entity.actor:IsPlayer() then
    local driverId = self:GetDriverId()
    if driverId then
      local driver = System.GetEntity(driverId)
      if driver and driver.actor and not driver.actor:IsPlayer() then
        mult = 0
      end
    end
  end
  return mult
end
