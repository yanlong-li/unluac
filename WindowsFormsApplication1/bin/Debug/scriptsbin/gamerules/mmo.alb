Script.LoadScript("scripts/gamerules/singleplayer.lua", 1, 1)
Mmo = new(SinglePlayer)
Mmo.States = {"InGame"}
Mmo.MIN_PLAYER_LIMIT_WARN_TIMER = 15
Mmo.START_TIMER = 30
Mmo.INVULNERABILITY_TIME = 5
Mmo.WEAPON_ABANDONED_TIME = 45
Mmo.TICK_TIMERID = 1010
Mmo.TICK_TIME = 1000
Mmo.SCORE_KILLS_KEY = 100
Mmo.SCORE_DEATHS_KEY = 101
Mmo.SCORE_HEADSHOTS_KEY = 102
Mmo.SCORE_PING_KEY = 103
Mmo.DamagePlayerToPlayer = {
  helmet = 1.25,
  kevlar = 1.15,
  head = 2.25,
  torso = 1.15,
  arm_left = 0.85,
  arm_right = 0.85,
  leg_left = 0.85,
  leg_right = 0.85,
  foot_left = 0.7,
  foot_right = 0.7,
  hand_left = 0.7,
  hand_right = 0.7,
  assist_min = 0.8
}
function Mmo:IsMultiplayer()
  return true
end
function Mmo.Server:OnInit()
  Log("Mmo Server:OnInit")
  SinglePlayer.Server.OnInit(self)
  self.isServer = CryAction.IsServer()
  self.isClient = CryAction.IsServer()
  self.killHit = {}
  self.channelSpectatorMode = {}
  self:GotoState("InGame")
  self.works = {}
end
function Mmo.Server:OnStartGame()
  Log("Mmo Server:OnStartGame")
  self:StartTicking()
end
function Mmo.Client:OnActorAction(player, action, activation, value)
  Log("NEVER CALL! Mmo Client:OnActorAction")
  if action == "scores" and not self.force_scores then
    if activation == "press" then
      self:ShowScores(true)
    else
      self:ShowScores(false)
    end
  elseif action == "attack1" and activation == "press" then
    if player:IsDead() and player.actor:GetSpectatorMode() == 0 or player.actor:GetSpectatorMode() == 3 then
      self.server:RequestRevive(player.id)
      return false
    end
  elseif action == "next_spectator_target" and activation == "press" then
    if player:IsDead() or player.actor:GetSpectatorMode() == 3 then
      self.server:RequestSpectatorTarget(player.id, 1)
    end
  elseif action == "prev_spectator_target" and activation == "press" then
    if player:IsDead() or player.actor:GetSpectatorMode() == 3 then
      self.server:RequestSpectatorTarget(player.id, -1)
    end
  elseif action == "cycle_spectator_mode" and activation == "press" and self.game:CanChangeSpectatorMode(player.id) then
    if self.game:GetTeam(player.id) ~= 0 and player.actor:GetSpectatorMode() == 3 then
      self.server:RequestSpectatorTarget(player.id, 1)
    else
      local mode = player.actor:GetSpectatorMode()
      local target = 0
      if mode ~= 0 then
        mode = mode + 1
        if mode > 3 then
          mode = 1
        end
        if mode == 3 then
          self.server:RequestSpectatorTarget(player.id, 1)
        else
          self.game:ChangeSpectatorMode(player.id, mode, NULL_ENTITY)
        end
      end
    end
  end
  return true
end
function Mmo.Client:OnDisconnect(cause, desc)
  Log("NEVER CALL! Mmo Client:OnDisconnect")
end
function Mmo.Server:OnClientConnect(channelId, reset, name)
  Log("NEVER CALL! Mmo Server:OnClientConnect")
  local player
  local player = self.game:SpawnMmoPlayer(channelId, name or "Player")
  if not reset then
    self.game:ChangeSpectatorMode(player.id, 2, NULL_ENTITY)
  end
  if not reset then
    if not CryAction.IsChannelOnHold(channelId) then
      self:ResetScore(player.id)
      self.otherClients:ClClientConnect(channelId, player:GetName(), false)
    else
      self.otherClients:ClClientConnect(channelId, player:GetName(), true)
    end
  else
    local specMode = self.channelSpectatorMode[channelId] or 0
    local teamId = self.game:GetChannelTeam(channelId) or 0
    if specMode == 0 or teamId ~= 0 then
      self.Server.RequestSpawnGroup(self, player.id, self.game:GetTeamDefaultSpawnGroup(teamId) or NULL_ENTITY, true)
      self:RevivePlayer(player.actor:GetChannel(), player)
    else
      self.Server.OnChangeSpectatorMode(self, player.id, specMode, nil, true)
    end
  end
  return player
end
function Mmo.Server:OnClientDisconnect(channelId)
  Log("NEVER CALL! Mmo Server:OnClientDisconnect")
  local player = self.game:GetPlayerByChannelId(channelId)
  self.channelSpectatorMode[player.actor:GetChannel()] = nil
  self.works[player.id] = nil
  self.otherClients:ClClientDisconnect(channelId, player:GetName())
end
function Mmo.Server:OnClientEnteredGame(channelId, player, reset)
  Log("NEVER CALL! Mmo Server:OnClientEnteredGame")
  local onHold = CryAction.IsChannelOnHold(channelId)
  if not onHold and not reset then
    self.game:ChangeSpectatorMode(player.id, 2, NULL_ENTITY)
  elseif not reset then
    if player.actor:GetHealth() > 0 then
      player.actor:SetPhysicalizationProfile("alive")
    else
      player.actor:SetPhysicalizationProfile("ragdoll")
    end
  end
  if not reset then
    self.otherClients:ClClientEnteredGame(channelId, player:GetName())
  end
  self:SetupPlayer(player)
  if not g_localActorId or player.id ~= g_localActorId then
    self.onClient:ClSetupPlayer(player.actor:GetChannel(), player.id)
  end
end
function Mmo.Server:OnChangeSpectatorMode(playerId, mode, targetId, resetAll, norevive)
  Log("NEVER CALL! Mmo Server:OnChangeSpectatorMode mode=%d", mode)
  local player = System.GetEntity(playerId)
  if not player then
    return
  end
  if mode > 0 then
    if resetAll then
      self:ResetScore(playerId)
      player.death_time = nil
      player.inventory:Destroy()
      if mode == 1 or mode == 2 then
        self.game:SetTeam(0, playerId)
      end
    end
    if mode == 1 or mode == 2 then
      local pos = g_Vectors.temp_v1
      local angles = g_Vectors.temp_v2
      player.actor:SetSpectatorMode(mode, NULL_ENTITY)
      local locationId = self.game:GetInterestingSpectatorLocation()
      if locationId then
        local location = System.GetEntity(locationId)
        if location then
          pos = location:GetWorldPos(pos)
          angles = location:GetWorldAngles(angles)
          self.game:MovePlayer(playerId, pos, angles)
        end
      end
    elseif mode == 3 then
      if targetId and targetId ~= 0 then
        local player = System.GetEntity(playerId)
        player.actor:SetSpectatorMode(3, targetId)
      else
        local newTargetId = self.game:GetNextSpectatorTarget(playerId, 1)
        if newTargetId and newTargetId ~= 0 then
          local player = System.GetEntity(playerId)
          player.actor:SetSpectatorMode(3, newTargetId)
        end
      end
    end
  elseif not norevive and self:CanRevive(playerId) then
    player.actor:SetSpectatorMode(0, NULL_ENTITY)
    self:RevivePlayer(player.actor:GetChannel(), player)
  end
  self.channelSpectatorMode[player.actor:GetChannel()] = mode
end
function Mmo.Server:OnUpdate(frameTime)
  self:UpdatePings(frameTime)
end
function Mmo:StartTicking()
  self:SetTimer(self.TICK_TIMERID, self.TICK_TIME)
end
function Mmo.Server:OnTimer(timerId, msec)
  if timerId == self.TICK_TIMERID and self.OnTick then
    pcall(self.OnTick, self)
    self:SetTimer(self.TICK_TIMERID, self.TICK_TIME)
  end
end
function Mmo.Client:OnUpdate(frameTime)
  SinglePlayer.Client.OnUpdate(self, frameTime)
end
function Mmo:UpdatePings(frameTime)
end
function Mmo:SetupPlayer(player)
  Log("Mmo:SetupPlayer")
  player.ammoCapacity = {
    bullet = 120,
    explosivegrenade = 3,
    scargrenade = 10,
    tacbullet = 20,
    tagbullet = 10,
    incendiarybullet = 120
  }
  if player.inventory and player.ammoCapacity then
    for ammo, capacity in pairs(player.ammoCapacity) do
      player.inventory:SetAmmoCapacity(ammo, capacity)
    end
  end
end
function Mmo:RequestSpawnGroup(groupId)
  self.server:RequestSpawnGroup(g_localActorId, groupId)
end
function Mmo:SetPlayerSpawnGroup(playerId, spawnGroupId)
  Log("NEVER CALL! Mmo:SetPlayerSpawnGroup")
  local player = System.GetEntity(playerId)
  if player then
    player.spawnGroupId = spawnGroupId
  end
end
function Mmo:GetPlayerSpawnGroup(player)
  Log("NEVER CALL! Mmo:GetPlayerSpawnGroup")
end
function Mmo:CanRevive(playerId)
  Log("NEVER CALL! Mmo:CanRevive")
  local player = System.GetEntity(playerId)
  if not player then
    return false
  end
  local groupId = player.spawnGroupId
  if not self.USE_SPAWN_GROUPS or groupId and groupId ~= NULL_ENTITY then
    return true
  end
  return false
end
function Mmo:RevivePlayer(channelId, player, keepEquip)
  Log("NEVER CALL! Mmo:RevivePlayer")
  local result = false
  local groupId = player.spawnGroupId
  local teamId = self.game:GetTeam(player.id)
  if player:IsDead() then
    keepEquip = false
  end
  if self.USE_SPAWN_GROUPS and groupId and groupId ~= NULL_ENTITY then
    local spawnGroup = System.GetEntity(groupId)
    if spawnGroup and spawnGroup.vehicle then
      result = false
      for i, seat in pairs(spawnGroup.Seats) do
        if not seat.seat:IsDriver() and not seat.seat:IsGunner() and seat.seat:IsFree() then
          self.game:RevivePlayerInVehicle(player.id, spawnGroup.id, i, teamId, not keepEquip)
          result = true
          break
        end
      end
      if not result then
        self.game:RevivePlayerInVehicle(player.id, spawnGroup.id, -1, teamId, not keepEquip)
        result = true
      end
    end
  elseif self.USE_SPAWN_GROUPS then
    Log("Failed to spawn %s! teamId: %d  groupId: %s  groupTeamId: %d", player:GetName(), self.game:GetTeam(player.id), tostring(groupId), self.game:GetTeam(groupId or NULL_ENTITY))
    return false
  end
  if not result then
    self.game:ReviveMmoPlayer(player.id, teamId, not keepEquip)
    result = true
  end
  player:UpdateAreas()
  if result then
    if player.actor:GetSpectatorMode() ~= 0 then
      player.actor:SetSpectatorMode(0, NULL_ENTITY)
    end
    player.death_time = nil
    player.frostShooterId = nil
    if self.INVULNERABILITY_TIME and 0 < self.INVULNERABILITY_TIME then
      self.game:SetInvulnerability(player.id, true, self.INVULNERABILITY_TIME)
    end
  end
  if not result then
    Log("Failed to spawn %s! teamId: %d  groupId: %s  groupTeamId: %d", player:GetName(), self.game:GetTeam(player.id), tostring(groupId), self.game:GetTeam(groupId or NULL_ENTITY))
  end
  return result
end
function Mmo:GetDamageAbsorption(player, hit)
  Log("NEVER CALL! Mmo:GetDamageAbsorption")
  if hit.damage == 0 or hit.type == "punish" then
    return 0
  end
end
function Mmo:ProcessActorDamage(hit)
  Log("NEVER CALL! Mmo:ProcessActorDamage")
  local target = hit.target
  local health = target.actor:GetHealth()
  if type ~= "heal" then
    health = math.floor(health - hit.damage * (1 - self:GetDamageAbsorption(target, hit)))
  else
    health = math.min(health - hit.damage, target.actor:GetMaxHealth())
  end
  target.actor:SetHealth(health)
  return health <= 0
end
function Mmo:CalcExplosionDamage(entity, explosion, obstruction)
  local newDamage = SinglePlayer.CalcExplosionDamage(self, entity, explosion, obstruction)
  if explosion.effectClass == "claymoreexplosive" then
    local explosionPos = explosion.pos
    local entityPos = entity:GetWorldPos()
    local edir = vecNormalize(vecSub(entityPos, explosionPos))
    local dot = 1
    if edir then
      dot = vecDot(edir, explosion.dir)
    end
    if dot > 0 then
      newDamage = 0.1 * newDamage + 0.9 * newDamage * dot
    else
      newDamage = 0.1 * newDamage
    end
    local debugHits = self.game:DebugHits()
    if debugHits > 0 then
      local angle = math.abs(math.acos(dot))
      Log("%s hit by claymore: dot %f, angle %f, damage %f", entity:GetName(), dot, angle, newDamage)
    end
  end
  return newDamage
end
function Mmo.Server:OnPlayerKilled(hit)
  Log("NEVER CALL! Mmo Server:OnPlayerKilled")
  hit.target.death_time = _time
  self.game:KillPlayer(hit.targetId, false, true, hit.shooterId, hit.weaponId, hit.damage, hit.materialId, hit.typeId, hit.dir)
  self:ProcessScores(hit)
end
function Mmo.Client:OnKill(playerId, shooterId, weaponClassName, damage, material, hit_type)
  Log("NEVER CALL! Mmo Client:OnKill")
  local matName = self.game:GetHitMaterialName(material) or ""
  local type = self.game:GetHitType(hit_type) or ""
  local headshot = string.find(matName, "head")
  local melee = string.find(type, "melee")
  if playerId == g_localActorId then
    graveyardWindow:Show(true)
  end
end
function Mmo:ShatterEntity(entityId, hit)
  Log("NEVER CALL! Mmo:ShatterEntity")
  local entity = System.GetEntity(entityId)
  if entity then
    if hit.shooterId == entityId and entity.frostShooterId then
      hit.shooterId = entity.frostShooterId
    end
    if entity.actor and entity.actor:IsPlayer() and not entity:IsDead() then
      entity.death_time = _time
      self.game:KillPlayer(entityId, false, false, hit.shooterId, hit.weaponId, hit.damage, hit.materialId, hit.typeId, hit.dir)
      self:ProcessScores(hit)
    end
  end
  local damage = math.min(100, hit.damage or 0)
  damage = math.max(20, damage)
  self.game:ShatterEntity(entityId, hit.pos, vecScale(hit.dir, damage))
  if entity.Server and entity.Server.OnShattered then
    entity.Server.OnShattered(entity, hit)
  end
end
function Mmo:ProcessDeath(hit)
  Log("NEVER CALL! Mmo:ProcessDeath")
end
function Mmo:ResetScore(playerId)
  Log("NEVER CALL! Mmo:ResetScore")
  self.game:SetSynchedEntityValue(playerId, self.SCORE_KILLS_KEY, 0)
  self.game:SetSynchedEntityValue(playerId, self.SCORE_DEATHS_KEY, 0)
  self.game:SetSynchedEntityValue(playerId, self.SCORE_HEADSHOTS_KEY, 0)
  CryAction.SendGameplayEvent(playerId, eGE_ScoreReset, "", 0)
end
function Mmo:GetPlayerDeaths(playerId)
  Log("NEVER CALL! Mmo:GetPlayerDeaths")
end
function Mmo:Award(player, deaths, kills, headshots)
  Log("NEVER CALL! Mmo:Award")
  if player then
    local ckills = kills + (self.game:GetSynchedEntityValue(player.id, self.SCORE_KILLS_KEY) or 0)
    local cdeaths = deaths + (self.game:GetSynchedEntityValue(player.id, self.SCORE_DEATHS_KEY) or 0)
    local cheadshots = headshots + (self.game:GetSynchedEntityValue(player.id, self.SCORE_HEADSHOTS_KEY) or 0)
    self.game:SetSynchedEntityValue(player.id, self.SCORE_KILLS_KEY, ckills)
    self.game:SetSynchedEntityValue(player.id, self.SCORE_DEATHS_KEY, cdeaths)
    self.game:SetSynchedEntityValue(player.id, self.SCORE_HEADSHOTS_KEY, cheadshots)
    if kills and kills ~= 0 then
      CryAction.SendGameplayEvent(player.id, eGE_Scored, "kills", ckills)
    end
    if deaths and deaths ~= 0 then
      CryAction.SendGameplayEvent(player.id, eGE_Scored, "deaths", cdeaths)
    end
    if headshots and headshots ~= 0 then
      CryAction.SendGameplayEvent(player.id, eGE_Scored, "headshots", cheadshots)
    end
  end
end
function Mmo:ProcessScores(hit)
  local target = hit.target
  local shooter = hit.shooter
  local headshot = self:IsHeadShot(hit)
  local h = 0
  if headshot then
    h = 1
  end
  if target.actor and target.actor:IsPlayer() then
    self:Award(target, 1, 0, 0)
  end
  if shooter and shooter.actor and shooter.actor:IsPlayer() then
    if target ~= shooter then
      self:Award(shooter, 0, 1, h)
    else
      self:Award(shooter, 0, -1, 0)
    end
  end
end
function Mmo.Server:RequestRevive(entityId)
  Log("NEVER CALL! Mmo Server:RequestRevive")
  local player = System.GetEntity(entityId)
  if player and player.actor and player.death_time and _time - player.death_time > 2.5 and player:IsDead() then
    self:RevivePlayer(player.actor:GetChannel(), player)
  end
end
function Mmo.Server:RequestSpawnGroup(playerId, groupId, force)
  Log("NEVER CALL! Mmo Server:RequestSpawnGroup")
  local player = System.GetEntity(playerId)
  if player then
    local teamId = self.game:GetTeam(playerId)
    if not force and teamId ~= self.game:GetTeam(groupId) then
      return
    end
    local group = System.GetEntity(groupId)
    if group and group.vehicle and (group.vehicle:IsDestroyed() or group.vehicle:IsSubmerged()) then
      return
    end
    self:SetPlayerSpawnGroup(playerId, groupId)
    if not g_localActorId or g_localActorId ~= playerId then
      local channelId = player.actor:GetChannel()
      if channelId and channelId > 0 then
        self.onClient:ClSetSpawnGroup(channelId, groupId)
      end
    end
    self:UpdateSpawnGroupSelection(player.id)
  end
end
function Mmo:UpdateSpawnGroupSelection(playerId)
  local teamId = self.game:GetTeam(playerId)
  local players
  if teamId == 0 then
    players = self.game:GetPlayers(true)
  else
    players = self.game:GetTeamPlayers(teamId, true)
  end
  if players then
    local groupId = self:GetPlayerSpawnGroup(System.GetEntity(playerId)) or NULL_ENTITY
    for i, player in pairs(players) do
      if player.id ~= playerId then
        local channelId = player.actor:GetChannel()
        if channelId and channelId > 0 then
          self.onClient:ClSetPlayerSpawnGroup(channelId, playerId, groupId)
        end
      end
    end
  end
end
function Mmo:UpdateSpawnGroupSelectionForPlayer(playerId, teamId)
  local oPlayer = System.GetEntity(playerId)
  local players = self.game:GetPlayers(true)
  if players then
    local channelId = oPlayer.actor:GetChannel()
    if channelId and channelId > 0 then
      for i, player in pairs(players) do
        if player.id ~= playerId then
          local groupId = NULL_ENTITY
          if teamId == self.game:GetTeam(player.id) then
            groupId = self:GetPlayerSpawnGroup(System.GetEntity(player.id)) or NULL_ENTITY
          end
          self.onClient:ClSetPlayerSpawnGroup(channelId, player.id, groupId)
        end
      end
    end
  end
end
function Mmo.Server:RequestSpectatorTarget(playerId, change)
  Log("NEVER CALL! Mmo Server:RequestSpectatorTarget")
  local targetId = self.game:GetNextSpectatorTarget(playerId, change)
  if targetId then
    if targetId ~= 0 then
      local player = System.GetEntity(playerId)
      self.game:ChangeSpectatorMode(playerId, 3, targetId)
    elseif self.game:GetTeam(playerId) == 0 then
      self.game:ChangeSpectatorMode(playerId, 1, NULL_ENTITY)
    end
  end
end
function Mmo.Server:OnSpawnGroupInvalid(playerId, spawnGroupId)
  local teamId = self.game:GetTeam(playerId) or 0
  self.Server.RequestSpawnGroup(self, playerId, NULL_ENTITY, true)
  local player = System.GetEntity(playerId)
  if player then
    self.onClient:ClSpawnGroupInvalid(player.actor:GetChannel(), spawnGroupId)
  end
end
function Mmo.Client:ClSetupPlayer(playerId)
  self:SetupPlayer(System.GetEntity(playerId))
end
function Mmo.Client:ClSpawnGroupInvalid(spawnGroupId)
  if HUD and g_localActor and g_localActor:IsDead() then
    HUD.OpenPDA(true, false)
  end
end
function Mmo.Client:ClSetSpawnGroup(groupId)
  if g_localActor then
    g_localActor.spawnGroupId = groupId
  end
end
function Mmo.Client:ClSetPlayerSpawnGroup(playerId, groupId)
  local player = System.GetEntity(playerId)
  if player then
    player.spawnGroupId = groupId
  end
end
function Mmo.Server:OnItemPickedUp(itemId, actorId)
end
function Mmo.Server:OnItemDropped(itemId, actorId)
end
function Mmo:GetServerStateTable()
  local s = self:GetState()
  return self.Server[s]
end
function Mmo:GetClientStateTable()
  return self.Server[self:GetState()]
end
function Mmo:OnTick()
  local onTick = self:GetServerStateTable().OnTick
  if onTick then
    onTick(self)
  end
end
function Mmo:DefaultState(cs, state)
  local default = self[cs]
  self[cs][state] = {
    OnClientConnect = default.OnClientConnect,
    OnClientDisconnect = default.OnClientDisconnect,
    OnClientEnteredGame = default.OnClientEnteredGame,
    OnDisconnect = default.OnDisconnect,
    OnActorAction = default.OnActorAction,
    OnStartLevel = default.OnStartLevel,
    OnStartGame = default.OnStartGame,
    OnKill = default.OnKill,
    OnHit = default.OnHit,
    OnExplosion = default.OnExplosion,
    OnChangeTeam = default.OnChangeTeam,
    OnChangeSpectatorMode = default.OnChangeSpectatorMode,
    RequestSpectatorTarget = default.RequestSpectatorTarget,
    OnSetTeam = default.OnSetTeam,
    OnItemPickedUp = default.OnItemPickedUp,
    OnItemDropped = default.OnItemDropped,
    OnTimer = default.OnTimer,
    OnUpdate = default.OnUpdate
  }
end
Mmo:DefaultState("Server", "InGame")
Mmo:DefaultState("Client", "InGame")
Mmo.Server.InGame.OnTick = nil
function Mmo.Server.InGame:OnBeginState()
  Log("Mmo Server.InGame:OnBeginState")
  self:StartTicking()
  CryAction.SendGameplayEvent(NULL_ENTITY, eGE_GameStarted)
end
function Mmo.Server.InGame:OnUpdate(frameTime)
  Mmo.Server.OnUpdate(self, frameTime)
end
function Mmo:CanWork(entityId, playerId, work_type)
  if self.isServer then
    local work = self.works[playerId]
    if work and work.active and work.entityId ~= entityId then
      return false
    end
  end
  local entity = System.GetEntity(entityId)
  if entity then
    if work_type == "repair" then
      if entity.vehicle then
        local dmgratio = entity.vehicle:GetDamageRatio()
        if dmgratio > 0 and not entity.vehicle:IsDestroyed() then
          return true
        end
      elseif entity.item then
        if (entity.class == "AutoTurret" or entity.class == "AutoTurretAA") and not entity.item:IsDestroyed() then
          local health = entity.item:GetHealth()
          local maxhealth = entity.item:GetMaxHealth()
          if health < maxhealth and not entity.item:IsDestroyed() then
            return true
          end
        end
      elseif entity.CanDisarm and entity:CanDisarm(playerId) then
        return true
      end
    elseif work_type == "lockpick" and entity.vehicle and entity.vehicle:IsLocked() and not entity.vehicle:IsDestroyed() then
      return true
    end
  end
  return false
end
function Mmo:StartWork(entityId, playerId, work_type)
  Log("NEVER CALL! Mmo StartWork")
  local work = self.works[playerId]
  if not work then
    work = {}
    self.works[playerId] = work
  end
  work.active = true
  work.entityId = entityId
  work.playerId = playerId
  work.type = work_type
  work.amount = 0
  work.complete = nil
  local entity = System.GetEntity(entityId)
  if entity and entity.CanDisarm and entity:CanDisarm(playerId) then
    work_type = "disarm"
    work.type = work_type
  end
  self.onClient:ClStartWorking(self.game:GetChannelId(playerId), entityId, work_type)
end
function Mmo:StopWork(playerId)
  Log("NEVER CALL! Mmo StopWork")
  local work = self.works[playerId]
  if work and work.active then
    if work.complete then
    else
    end
    work.active = false
    self.onClient:ClStopWorking(self.game:GetChannelId(playerId), work.entityId, work.complete or false)
    if work.complete then
      self.allClients:ClWorkComplete(work.entityId, work.type)
    end
  end
end
function Mmo:Work(playerId, amount, frameTime)
  Log("NEVER CALL! Mmo Work")
  local work = self.works[playerId]
  if work and work.active then
    local entity = System.GetEntity(work.entityId)
    if entity then
      local workamount = amount * frameTime
      if work.type == "repair" then
        if not self.repairHit then
          self.repairHit = {
            typeId = self.game:GetHitTypeId("repair"),
            type = "repair",
            material = 0,
            materialId = 0,
            dir = g_Vectors.up,
            radius = 0,
            partId = -1
          }
        end
        local hit = self.repairHit
        hit.shooter = System.GetEntity(playerId)
        hit.shooterId = playerId
        hit.target = entity
        hit.targetId = work.entityId
        hit.pos = entity:GetWorldPos(hit.pos)
        hit.damage = workamount
        work.amount = work.amount + workamount
        if entity.vehicle then
          entity.Server.OnHit(entity, hit)
          work.complete = 0 >= entity.vehicle:GetDamageRatio()
          local progress = math.floor(0.5 + (1 - entity.vehicle:GetDamageRatio()) * 100)
          self.onClient:ClStepWorking(self.game:GetChannelId(playerId), progress)
          return not work.complete
        elseif entity.item and (entity.class == "AutoTurret" or entity.class == "AutoTurretAA") and not entity.item:IsDestroyed() then
          entity.Server.OnHit(entity, hit)
          work.complete = entity.item:GetHealth() >= entity.item:GetMaxHealth()
          local progress = math.floor(0.5 + entity.item:GetHealth() / entity.item:GetMaxHealth())
          self.onClient:ClStepWorking(self.game:GetChannelId(playerId), progress)
          return not work.complete
        end
      elseif work.type == "lockpick" then
        work.amount = work.amount + workamount
        if work.amount > 100 then
          entity.vehicle:Lock(false, playerId)
          work.complete = true
        end
        self.onClient:ClStepWorking(self.game:GetChannelId(playerId), math.floor(work.amount + 0.5))
        return not work.complete
      elseif work.type == "disarm" and entity.CanDisarm and entity:CanDisarm(playerId) then
        work.amount = work.amount + 25 * frameTime
        if work.amount > 100 then
          if self.OnDisarmed then
            self:OnDisarmed(work.entityId, playerId)
          end
          System.RemoveEntity(work.entityId)
          work.complete = true
        end
        self.onClient:ClStepWorking(self.game:GetChannelId(playerId), math.floor(work.amount + 0.5))
        return not work.complete
      end
    end
  end
  return false
end
function Mmo.Client:ClStartWorking(entityId, workName)
  self.work_type = workName
  self.work_name = "@ui_work_" .. workName
  HUD.SetProgressBar(true, 0, self.work_name)
end
function Mmo.Client:ClStepWorking(amount)
  HUD.SetProgressBar(true, amount, self.work_name or "")
end
function Mmo.Client:ClStopWorking(entityId, complete)
  HUD.SetProgressBar(false, -1, "")
end
function Mmo.Client:ClWorkComplete(entityId, workName)
  local sound
  if workName == "repair" then
    sound = "sounds/weapons:repairkit:repairkit_successful"
  elseif workName == "lockpick" then
    sound = "sounds/weapons:lockpick:lockpick_successful"
  end
  if sound then
    local entity = System.GetEntity(entityId)
    if entity then
      local sndFlags = SOUND_DEFAULT_3D
      sndFlags = band(sndFlags, bnot(SOUND_OBSTRUCTION))
      sndFlags = bor(sndFlags, SOUND_LOAD_SYNCHRONOUSLY)
      local pos = entity:GetWorldPos(g_Vectors.temp_v1)
      pos.z = pos.z + 1
      return Sound.Play(sound, pos, sndFlags, SOUND_SEMANTIC_MP_CHAT)
    end
  end
end
function Mmo:PrecacheLevel()
  local list = {
    "SOCOM",
    "OffHand",
    "Fists"
  }
  for i, v in ipairs(list) do
    CryAction.CacheItemGeometry(v)
    CryAction.CacheItemSound(v)
  end
end
function SetSpawnGroup(player, group)
  local p, gid
  if type(player) == "table" then
    p = player
  elseif type(player) == "userdata" then
    p = System.GetEntity(player)
  elseif type(player) == "string" then
    p = EntityNamed(player)
  end
  if type(group) == "table" then
    gid = group.id
  elseif type(player) == "userdata" then
    gid = group
  elseif type(player) == "string" then
    local ge = EntityNamed(group)
    if ge then
      gid = ge.id
    end
  end
  if p and gid then
    g_gameRules:SetPlayerSpawnGroup(p, gid)
  end
end
function Mmo.Client:ClClientConnect(name, reconnect)
end
function Mmo.Client:ClClientDisconnect(name)
end
function Mmo.Client:ClClientEnteredGame(name)
end
