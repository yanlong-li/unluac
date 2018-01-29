SinglePlayer = {
  DamagePlayerToAI = {
    helmet = 4,
    kevlar = 0.75,
    head = 50,
    torso = 1.2,
    arm_left = 0.65,
    arm_right = 0.65,
    hand_left = 0.3,
    hand_right = 0.3,
    leg_left = 0.65,
    leg_right = 0.65,
    foot_left = 0.3,
    foot_right = 0.3,
    assist_min = 0.8
  },
  DamagePlayerToPlayer = {
    helmet = 4,
    kevlar = 0.45,
    head = 20,
    torso = 1.4,
    arm_left = 0.65,
    arm_right = 0.65,
    hand_left = 0.3,
    hand_right = 0.3,
    leg_left = 0.65,
    leg_right = 0.65,
    foot_left = 0.3,
    foot_right = 0.3,
    assist_min = 0.8
  },
  DamageAIToPlayer = {
    helmet = 1,
    kevlar = 0.45,
    head = 1,
    torso = 1,
    arm_left = 0.65,
    arm_right = 0.65,
    hand_left = 0.3,
    hand_right = 0.3,
    leg_left = 0.65,
    leg_right = 0.65,
    foot_left = 0.3,
    foot_right = 0.3,
    assist_min = 0.8
  },
  DamageAIToAI = {
    helmet = 4,
    kevlar = 0.75,
    head = 20,
    torso = 1,
    arm_left = 0.65,
    arm_right = 0.65,
    hand_left = 0.3,
    hand_right = 0.3,
    leg_left = 0.65,
    leg_right = 0.65,
    foot_left = 0.3,
    foot_right = 0.3,
    assist_min = 0.8
  },
  tempVec = {
    x = 0,
    y = 0,
    z = 0
  },
  playerDeathLocations = {},
  lastSaveName = "",
  lastSaveDeathCount = 0,
  hudWhite = {
    x = 1,
    y = 1,
    z = 1
  },
  Client = {},
  Server = {},
  spawns = {}
}
if not g_dmgMult then
  g_dmgMult = 1
end
if not g_barbWireMaterial then
  g_barbWireMaterial = System.GetSurfaceTypeIdByName("mat_metal_barbwire")
end
function SinglePlayer:IsMultiplayer()
  return false
end
function SinglePlayer:OnReset(toGame)
  AIReset()
end
function SinglePlayer:InitHitMaterials()
  local mats = {
    "mat_helmet",
    "mat_kevlar",
    "mat_head",
    "mat_torso",
    "mat_arm_left",
    "mat_arm_right",
    "mat_hand_left",
    "mat_hand_right",
    "mat_leg_left",
    "mat_leg_right",
    "mat_foot_left",
    "mat_foot_right",
    "mat_alien_vulnerable",
    "mat_alien_hunter_leg",
    "mat_alien_hunter_torso",
    "mat_alien_hunter_head",
    "mat_alien_hunter_vulnerable",
    "mat_alien_hunter_topFace",
    "mat_alien_hunter_bottomFace",
    "mat_alien_hunter_leftFace",
    "mat_alien_hunter_rightFace"
  }
end
function SinglePlayer:InitHitTypes()
  local types = {
    "normal",
    "repair",
    "lockpick",
    "bullet",
    "gaussbullet",
    "frost",
    "fire",
    "radiation",
    "melee",
    "tac",
    "frag",
    "fall",
    "collision",
    "event",
    "punish",
    "avmine",
    "moacbullet",
    "trooper_melee",
    "scout_moac",
    "aacannon",
    "emp"
  }
end
function SinglePlayer:IsHeadShot(hit)
  return hit.material_type and (hit.material_type == "head" or hit.material_type == "helmet") or false
end
function SinglePlayer:CalcDamage(material_type, damage, tbl, assist)
  if damage and damage ~= 0 then
    if material_type and tbl then
      local mult = tbl[material_type] or 1
      local asm = assist * tbl.assist_min
      if mult < asm then
        mult = asm
      end
      return damage * mult
    end
    return damage
  end
  return 0
end
function SinglePlayer:CalcExplosionDamage(entity, explosion, obstruction)
  if explosion.impact and explosion.impact_targetId and explosion.impact_targetId == entity.id then
    return explosion.damage
  end
  local effect = 1
  if not entity.vehicle then
    local distance = vecLen(vecSub(entity:GetWorldPos(), explosion.pos))
    if distance <= explosion.min_radius or explosion.min_radius == explosion.radius then
      effect = 1
    else
      distance = math.max(0, math.min(distance, explosion.radius))
      local r = explosion.radius - explosion.min_radius
      local d = distance - explosion.min_radius
      effect = (r - d) / r
      effect = math.max(math.min(1, effect * effect), 0)
    end
    effect = effect * (1 - obstruction)
    if entity.actor and entity.actor:GetPhysicalizationProfile() == "sleep" then
      return explosion.damage * 2 * effect
    end
  else
    effect = 1 - obstruction
  end
  if explosion.type == "emp" then
    self.game:ProcessEMPEffect(entity.id, effect)
  end
  return explosion.damage * effect
end
function SinglePlayer:OnShoot(shooter)
  if shooter and shooter.OnShoot and not shooter:OnShoot() then
    return false
  end
  return true
end
function SinglePlayer:IsUsable(srcId, objId)
  if not objId then
    return 0
  end
  local obj = System.GetEntity(objId)
  if obj.IsUsable then
    if obj:IsHidden() then
      return 0
    end
    local src = System.GetEntity(srcId)
    if src and src.actor and (src:IsDead() or src.actor:GetSpectatorMode() ~= 0 or src.actorStats.isFrozen) then
      return 0
    end
    return obj:IsUsable(src)
  end
  return 0
end
function SinglePlayer:OnNewUsable(srcId, objId, usableId)
  if not srcId then
    return
  end
  if objId and not System.GetEntity(objId) then
    objId = nil
  end
  local src = System.GetEntity(srcId)
  if src and src.SetOnUseData then
    src:SetOnUseData(objId or NULL_ENTITY, usableId)
  end
  if srcId ~= g_localActorId then
    return
  end
  if self.UsableMessage then
    HUD.SetInstructionObsolete(self.UsableMessage)
    self.UsableMessage = nil
  end
end
function SinglePlayer:OnUsableMessage(srcId, objId, objEntityId, usableId)
  if srcId ~= g_localActorId then
    return
  end
  local msg = ""
  if objId then
    obj = System.GetEntity(objId)
    if obj then
      if obj.GetUsableMessage then
        msg = obj:GetUsableMessage(usableId)
      else
        local state = obj:GetState()
        if state ~= "" then
          state = obj[state]
          if state.GetUsableMessage then
            msg = state.GetUsableMessage(obj, usableId)
          end
        end
      end
    end
  end
  if HUD then
    HUD.SetUsability(objEntityId, msg)
  end
end
function SinglePlayer:OnLongHover(srcId, objId)
end
function SinglePlayer:EndLevel(params)
  if not System.IsEditor() and not params.nextlevel then
    Game.PauseGame(true)
  end
end
function SinglePlayer:CreateExplosion(shooterId, weaponId, damage, pos, dir, radius, angle, pressure, holesize, effect, effectScale, minRadius, minPhysRadius, physRadius)
  Log("NEVER CALL! SinglePlayer CreateExplosion")
  dir = dir or g_Vectors.up
  radius = radius or 5.5
  minRadius = minRadius or radius / 2
  physRadius = physRadius or radius
  minPhysRadius = minPhysRadius or physRadius / 2
  angle = angle or 0
  pressure = pressure or 200
  if holesize == nil then
    holesize = math.min(radius, 5)
  end
  if radius == 0 then
    return
  end
  self.game:ServerExplosion(shooterId or NULL_ENTITY, weaponId or NULL_ENTITY, damage, pos, dir, radius, angle, pressure, holesize, effect, effectScale, nil, minRadius, minPhysRadius, physRadius)
end
function SinglePlayer:CreateHit(targetId, shooterId, weaponId, dmg, radius, material, partId, type, pos, dir, normal)
  Log("NEVER CALL! SinglePlayer CreateHit")
  radius = radius or 0
  local materialId = 0
  if material then
    materialId = self.game:GetHitMaterialId(material)
  end
  partId = partId or -1
  local typeId = 0
  if type then
    typeId = self.game:GetHitTypeId(type)
  else
    typeId = self.game:GetHitTypeId("normal")
  end
  self.game:ServerHit(targetId, shooterId, weaponId, dmg, radius, materialId, partId, typeId, pos, dir, normal)
end
function SinglePlayer:ClientViewShake(pos, radius, amount, duration, frequency, source)
  if g_localActor and g_localActor.actor then
    self:ViewShake(g_localActor, pos, radius, amount, duration, frequency, source)
  end
end
function SinglePlayer:ViewShake(player, pos, radius, amount, duration, frequency, source)
  local delta = self.tempVec
  CopyVector(delta, pos)
  FastDifferenceVectors(delta, delta, player:GetWorldPos())
  local distance = LengthVector(delta)
  local deltaDist = radius - distance
  if deltaDist > 0 then
    local amt = amount * (deltaDist / radius)
    local halfDur = duration * 0.5
    player.actor:SetViewShake({
      x = 2 * g_Deg2Rad * amt,
      y = 2 * g_Deg2Rad * amt,
      z = 2 * g_Deg2Rad * amt
    }, {
      x = 0.02 * amt,
      y = 0.02 * amt,
      z = 0.02 * amt
    }, halfDur + halfDur * (deltaDist / radius), 0.05, 4)
    player.viewBlur = duration
    player.viewBlurAmt = 0.5
  end
end
function SinglePlayer:GetCollisionMinVelocity(entity, collider, hit)
  local minVel = 10
  if entity.actor and not entity.actor:IsPlayer() or entity.advancedDoor then
    minVel = 1
  end
  if entity.actor and collider and collider.vehicle then
    minVel = 6
  end
  if hit.target_velocity and vecLenSq(hit.target_velocity) == 0 then
    minVel = minVel * 2
  end
  return minVel
end
function SinglePlayer:GetCollisionDamageMult(entity, collider, hit)
  local mult = 1
  local debugColl = self.game:DebugCollisionDamage()
  if collider and collider.GetForeignCollisionMult then
    local foreignMult = collider.GetForeignCollisionMult(collider, entity, hit)
    mult = mult * foreignMult
    if debugColl > 0 and foreignMult ~= 1 then
      Log("<%s>: collider <%s> has ForeignCollisionMult %.2f", entity:GetName(), collider:GetName(), foreignMult)
    end
  end
  if entity.GetSelfCollisionMult then
    local selfMult = entity.GetSelfCollisionMult(entity, collider, hit)
    mult = mult * selfMult
    if debugColl > 0 and selfMult ~= 1 then
      Log("<%s>: returned SelfCollisionMult %.2f", entity:GetName(), selfMult)
    end
  end
  return mult
end
function SinglePlayer:OnCollision(entity, hit)
  Log("NEVER CALL! SinglePlayer OnCollision")
  local collider = hit.target
  local colliderMass = hit.target_mass
  local contactVelocitySq, contactMass
  if self.game:IsFrozen(entity.id) and (not entity.CanShatter or tonumber(entity:CanShatter()) ~= 0) then
    local energy = self:GetCollisionEnergy(entity, hit)
    local minEnergy = 1000
    if energy >= minEnergy then
      collider = collider or entity
      local colHit = self.collisionHit
      colHit.pos = hit.pos
      colHit.dir = hit.dir or hit.normal
      colHit.radius = 0
      colHit.partId = -1
      colHit.target = entity
      colHit.targetId = entity.id
      colHit.weapon = collider
      colHit.weaponId = collider.id
      colHit.shooter = collider
      colHit.shooterId = collider.id
      colHit.materialId = 0
      colHit.damage = 0
      colHit.typeId = g_collisionHitTypeId
      colHit.type = "collision"
      if collider.vehicle and collider.GetDriverId then
        local driverId = collider:GetDriverId()
        if driverId then
          colHit.shooterId = driverId
          colHit.shooter = System.GetEntity(colHit.shooterId)
        end
      end
      self:ShatterEntity(entity.id, colHit)
    end
    return
  end
  if not entity.Server or not entity.Server.OnHit then
    return
  end
  if entity.IsDead and entity:IsDead() then
    return
  end
  local minVelocity
  if collider or colliderMass > 0 then
    FastDifferenceVectors(self.tempVec, hit.velocity, hit.target_velocity)
    contactVelocitySq = vecLenSq(self.tempVec)
    contactMass = colliderMass
    minVelocity = self:GetCollisionMinVelocity(entity, collider, hit)
  else
    contactVelocitySq = vecLenSq(hit.velocity)
    contactMass = entity:GetMass()
    minVelocity = 7.5
  end
  if contactVelocitySq < 0.01 then
    contactVelocitySq = 0.01
  end
  local damage = 0
  if contactMass > 0.01 then
    local minVelocitySq = minVelocity * minVelocity
    local bigObject = false
    if contactMass > 200 and contactMass < 10000 and contactVelocitySq > 2.25 and hit.target_velocity and vecLenSq(hit.target_velocity) > contactVelocitySq * 0.3 then
      bigObject = true
      if collider and (collider.vehicle or collider.advancedDoor) then
        bigObject = false
      end
    end
    local collideBarbWire = false
    if hit.materialId == g_barbWireMaterial and entity and entity.actor then
      collideBarbWire = true
    end
    if contactVelocitySq >= minVelocitySq or bigObject or collideBarbWire then
      if AI and entity and entity.AI and not entity.AI.Colliding then
        g_SignalData.id = hit.target_id
        g_SignalData.fValue = contactVelocitySq
        AI.Signal(SIGNALFILTER_SENDER, 1, "OnCollision", entity.id, g_SignalData)
        entity.AI.Colliding = true
        entity:SetTimer(COLLISION_TIMER, 4000)
      end
      local contactVelocity = math.sqrt(contactVelocitySq) - minVelocity
      if contactVelocity < 0 then
        contactVelocitySq = minVelocitySq
        contactVelocity = 0
      end
      if entity.vehicle then
        if not self:IsMultiplayer() then
          damage = 5.0E-4 * self:GetCollisionEnergy(entity, hit)
        else
          damage = 2.0E-4 * self:GetCollisionEnergy(entity, hit)
        end
      else
        damage = 0.0025 * self:GetCollisionEnergy(entity, hit)
      end
      damage = damage * self:GetCollisionDamageMult(entity, collider, hit)
      if collideBarbWire and entity.id == g_localActorId then
        damage = damage * (contactMass * 0.15) * (30 / contactVelocitySq)
      end
      if bigObject then
        if damage > 0.5 then
          damage = damage * (contactMass / 10) * (10 / contactVelocitySq)
          if entity.id ~= g_localActorId then
            damage = damage * 3
          end
        else
          return
        end
      end
      if entity.GetCollisionDamageThreshold then
        local old = damage
        damage = __max(0, damage - entity:GetCollisionDamageThreshold())
      end
      if entity.actor then
        if entity.actor:IsPlayer() and hit.target_velocity and vecLen(hit.target_velocity) == 0 then
          damage = damage * 0.2
        end
        if collider and collider.class == "AdvancedDoor" and collider:GetState() == "Opened" then
          entity:KnockedOutByDoor(hit, contactMass, contactVelocity)
        end
        if collider and not collider.actor then
          local contactVelocityCollider = __max(0, vecLen(hit.target_velocity) - minVelocity)
          local killVelocity = entity.collisionKillVelocity or 20
          if contactVelocity > killVelocity and contactVelocityCollider > killVelocity and colliderMass > 50 and not entity.actor:IsPlayer() then
            local bNoDeath = entity.Properties.Damage.bNoDeath
            local bFall = bNoDeath and bNoDeath ~= 0
            if not AI.Hostile(entity.id, g_localActorId, false) then
              return
            end
            if bFall then
              entity.actor:Fall(hit.pos)
            else
              entity:Kill(true, NULL_ENTITY, NULL_ENTITY)
            end
          elseif g_localActorId and AI.Hostile(entity.id, g_localActorId, false) then
            if not entity.isAlien and contactVelocity > 5 and contactMass > 10 and not entity.actor:IsPlayer() then
              if damage < 50 then
                damage = 50
                entity.actor:Fall(hit.pos)
              end
            elseif not entity.isAlien and contactMass > 2 and contactVelocity > 15 and not entity.actor:IsPlayer() and damage < 50 then
              damage = 50
              entity.actor:Fall(hit.pos)
            end
          end
        end
      end
      if damage >= 0.5 then
        collider = collider or entity
        if entity.actor and not self:IsMultiplayer() and not AI.Hostile(entity.id, g_localActorId, false) and entity.id ~= g_localActorId and damage >= entity.actor:GetHealth() then
          entity.actor:Fall(hit.pos)
          return
        end
        local curtime = System.GetCurrTime()
        if entity.lastCollDamagerId and entity.lastCollDamagerId == collider.id and curtime < entity.lastCollDamageTime + 0.3 and damage < entity.lastCollDamage * 2 then
          return
        end
        entity.lastCollDamagerId = collider.id
        entity.lastCollDamageTime = curtime
        entity.lastCollDamage = damage
        local colHit = self.collisionHit
        colHit.pos = hit.pos
        colHit.dir = hit.dir or hit.normal
        colHit.radius = 0
        colHit.partId = -1
        colHit.target = entity
        colHit.targetId = entity.id
        colHit.weapon = collider
        colHit.weaponId = collider.id
        colHit.shooter = collider
        colHit.shooterId = collider.id
        colHit.materialId = 0
        colHit.damage = damage
        colHit.typeId = g_collisionHitTypeId
        colHit.type = "collision"
        colHit.impulse = hit.impulse
        if collider.vehicle and collider.GetDriverId then
          local driverId = collider:GetDriverId()
          if driverId then
            colHit.shooterId = driverId
            colHit.shooter = System.GetEntity(colHit.shooterId)
          end
        end
        local deadly = false
        if entity.Server.OnHit(entity, colHit) then
          if entity.actor and self.ProcessDeath then
            self:ProcessDeath(colHit)
          elseif entity.vehicle and self.ProcessVehicleDeath then
            self:ProcessVehicleDeath(colHit)
          end
          deadly = true
        end
        local debugHits = self.game:DebugHits()
        if debugHits > 0 then
          self:LogHit(colHit, debugHits > 1, deadly)
        end
      end
    end
  end
end
function SinglePlayer:OnSpawn()
  self:InitHitMaterials()
  self:InitHitTypes()
end
function SinglePlayer.Server:OnInit()
  self.fallHit = {}
  self.explosionHit = {}
  self.collisionHit = {}
end
function SinglePlayer.Client:OnInit()
  self.fadeFrames = 0
  self.curFadeTime = 0
  self.fadeTime = 2
  self.fading = true
  self.fadingToBlack = false
  if not System.IsEditor() then
    Sound.SetMasterVolumeScale(0)
  else
    Sound.SetMasterVolumeScale(1)
  end
end
function SinglePlayer.Server:OnClientConnect(channelId)
  player = self.game:SpawnMmoPlayer(channelId, "Dude")
  if not player then
    Log("OnClientConnect: Failed to spawn the player!")
    return
  end
end
function SinglePlayer.Server:OnClientEnteredGame(channelId, player, loadingSaveGame)
end
function SinglePlayer:X2ProcessFallDamage(actor, fallspeed, freefall)
  Log("NEVER CALL! SinglePlayer X2ProcessFallDamage")
  local dead = actor.IsDead and actor:IsDead()
  if dead then
    return
  end
  local fataldamage = 105
  local safeZVel = 5
  if fallspeed < 8 then
    return
  end
  local fatalZVel = 17
  local deltaZVel = fatalZVel - safeZVel
  local excursionZVel = fallspeed - safeZVel
  local damage = (1 - (deltaZVel - excursionZVel) / deltaZVel) * fataldamage
  if actor.actorStats.inFreeFall == 1 then
    damage = 1000
  end
  self.fallHit.partId = -1
  self.fallHit.pos = actor:GetWorldPos()
  self.fallHit.dir = g_Vectors.v001
  self.fallHit.radius = 0
  self.fallHit.target = actor
  self.fallHit.targetId = actor.id
  self.fallHit.weapon = actor
  self.fallHit.weaponId = actor.id
  self.fallHit.shooter = actor
  self.fallHit.shooterId = actor.id
  self.fallHit.materialId = 0
  self.fallHit.damage = damage
  self.fallHit.damageType = 0
  self.fallHit.missedType = 4
  self.fallHit.typeId = self.game:GetHitTypeId("fall")
  self.fallHit.type = "fall"
  local deadly = false
  if not dead and actor.Server.OnX2Hit(actor, self.fallHit) then
    self:ProcessDeath(self.fallHit)
    deadly = true
  end
  local debugHits = self.game:DebugHits()
  if debugHits > 0 then
    self:LogHit(self.fallHit, debugHits > 1, deadly)
  end
end
function SinglePlayer:ProcessFallDamage(actor, fallspeed, freefall)
  Log("NEVER CALL! SinglePlayer ProcessFallDamage")
  do return end
  local dead = actor.IsDead and actor:IsDead()
  if dead then
    return
  end
  local fataldamage = 105
  local safeZVel = 5
  if fallspeed < 8 then
    return
  end
  local fatalZVel = 17
  local deltaZVel = fatalZVel - safeZVel
  local excursionZVel = fallspeed - safeZVel
  local damage = (1 - (deltaZVel - excursionZVel) / deltaZVel) * fataldamage
  if actor.actorStats.inFreeFall == 1 then
    damage = 1000
  end
  self.fallHit.partId = -1
  self.fallHit.pos = actor:GetWorldPos()
  self.fallHit.dir = g_Vectors.v001
  self.fallHit.radius = 0
  self.fallHit.target = actor
  self.fallHit.targetId = actor.id
  self.fallHit.weapon = actor
  self.fallHit.weaponId = actor.id
  self.fallHit.shooter = actor
  self.fallHit.shooterId = actor.id
  self.fallHit.materialId = 0
  self.fallHit.damage = damage
  self.fallHit.typeId = self.game:GetHitTypeId("fall")
  self.fallHit.type = "fall"
  local deadly = false
  if not dead and actor.Server.OnHit(actor, self.fallHit) then
    self:ProcessDeath(self.fallHit)
    deadly = true
  end
  local debugHits = self.game:DebugHits()
  if debugHits > 0 then
    self:LogHit(self.fallHit, debugHits > 1, deadly)
  end
end
function SinglePlayer:GetEnergyAbsorption(player)
  local suitMaxEnergy = 200
  return tonumber(System.GetCVar("g_suitArmorHealthValue")) / suitMaxEnergy
end
function SinglePlayer:GetDamageAbsorption(actor, hit)
  if hit.damage == 0 or hit.type == "punish" then
    return 0
  end
  return 0
end
function SinglePlayer:ProcessActorDamage(hit)
  local target = hit.target
  local shooter = hit.shooter
  local weapon = hit.weapon
  local health = target.actor:GetHealth()
  if target.Properties.bInvulnerable and target.Properties.bInvulnerable == 1 then
    return health <= 0
  end
  if hit.target.actor:IsPlayer() then
    if hit.type == "fire" then
      HUD.HitIndicator()
    end
    if hit.explosion and hit.target.actor.id == g_localActorId then
      HUD.DamageIndicator(hit.weaponId or NULL_ENTITY, hit.shooterId or NULL_ENTITY, hit.dir, false)
    end
  end
  local dmgMult = 1
  if target and target.actor and target.actor:IsPlayer() then
    dmgMult = g_dmgMult
  end
  local totalDamage = 0
  local splayer = source and shooter.actor and shooter.actor:IsPlayer()
  local sai = not splayer and shooter and shooter.actor
  local tplayer = target and target.actor and target.actor:IsPlayer()
  local tai = not tplayer and target and target.actor
  if not self:IsMultiplayer() then
    if sai and not tai then
      totalDamage = AI.ProcessBalancedDamage(shooter.id, target.id, dmgMult * hit.damage, hit.type)
      totalDamage = totalDamage * (1 - self:GetDamageAbsorption(target, hit))
    elseif sai and tai then
      totalDamage = AI.ProcessBalancedDamage(shooter.id, target.id, dmgMult * hit.damage, hit.type)
      totalDamage = totalDamage * (1 - self:GetDamageAbsorption(target, hit))
    else
      totalDamage = dmgMult * hit.damage * (1 - self:GetDamageAbsorption(target, hit))
    end
  else
    totalDamage = dmgMult * hit.damage * (1 - self:GetDamageAbsorption(target, hit))
  end
  health = math.floor(health - totalDamage)
  if 0 < self.game:DebugCollisionDamage() then
    Log("<%s> hit damage: %d // absorbed: %d // health: %d", target:GetName(), hit.damage, hit.damage * self:GetDamageAbsorption(target, hit), health)
  end
  if health <= 0 then
    if target.Properties.Damage.bNoDeath and target.Properties.Damage.bNoDeath == 1 then
      target.actor:Fall(hit.pos)
      return false
    elseif target.id == g_localActorId then
      if System.GetCVar("g_PlayerFallAndPlay") == 1 then
        HUD.StartPlayerFallAndPlay()
        return false
      end
    elseif hit.shooterId == g_localActorId and not AI.Hostile(hit.target.id, g_localActorId, false) then
      target.actor:Fall(hit.pos)
      return false
    end
  end
  local isGod = target.actorStats.godMode
  if isGod and isGod > 0 and health <= 0 then
    target.actor:SetHealth(0)
    health = target.actor:GetMaxHealth()
  end
  target.actor:SetHealth(health)
  if health > 0 and target.Properties.Damage.FallPercentage and not target.isFallen then
    local healthPercentage = target:GetHealthPercentage()
    if healthPercentage < target.Properties.Damage.FallPercentage and totalDamage > tonumber(System.GetCVar("g_fallAndPlayThreshold")) then
      target.actor:Fall(hit.pos)
      return false
    end
  end
  if health > 0 and not target:IsOnVehicle() and target.AI and target.AI.curSuitMode ~= BasicAI.SuitMode.SUIT_ARMOR then
    if hit.type == "gaussbullet" then
      target:AddImpulse(hit.partId or -1, hit.pos, hit.dir, math.min(1000, hit.damage * 2.5), 1)
    else
      target:AddImpulse(hit.partId or -1, hit.pos, hit.dir, math.min(200, hit.damage * 0.75), 1)
    end
  end
  local shooterId = shooter and shooter.id or NULL_ENTITY
  local weaponId = weapon and weapon.id or NULL_ENTITY
  target.actor:DamageInfo(shooterId, target.id, weaponId, totalDamage, hit.type)
  if hit.material_type then
    AI.DebugReportHitDamage(target.id, shooterId, totalDamage, hit.material_type)
  else
    AI.DebugReportHitDamage(target.id, shooterId, totalDamage, "")
  end
  return health <= 0
end
function SinglePlayer:ReleaseCorpseItem(actor)
  if actor.isAlien then
    return
  end
  local item = actor.inventory:GetCurrentItem()
  if item then
    if item.item:IsMounted() then
      item.item:StopUse(actor.id)
    else
      local boneName = actor:GetAttachmentBone(0, "equip_hand_right")
      local time = 200 + math.random() * 550
      local strenght = (1250 - time) / 1250
      local proc
      if boneName then
        function proc()
          actor.actor:DropItem(item.id)
          self.drop_p = item:GetWorldPos(self.drop_p)
          self.drop_a = item:GetWorldAngles(self.drop_a)
          item:SetWorldPos(self.drop_p)
          item:SetWorldAngles(self.drop_a)
        end
      else
        function proc()
          actor.actor:DropItem()
        end
      end
      Script.SetTimer(time, proc)
    end
  end
end
function SinglePlayer:ProcessDeath(hit)
  Log("NEVER CALL! SinglePlayer:ProcessDeath")
  hit.target:Kill(true, hit.shooterId, hit.weaponId)
  local isHeadShot = self:IsHeadShot(hit)
  if hit.target.id == g_localActorId then
    if isHeadShot then
      HUD.ShowDeathFX(2)
    elseif hit.type == "melee" then
      HUD.ShowDeathFX(3)
    elseif hit.type == "freeze" then
      HUD.ShowDeathFX(4)
    else
      HUD.ShowDeathFX(1)
    end
    local g_difficultyHintSystem = System.GetCVar("g_difficultyHintSystem")
    local g_difficultyLevel = System.GetCVar("g_difficultyLevel")
    if not self:IsMultiplayer() and g_difficultyHintSystem > 0 and g_difficultyLevel ~= 1 and g_difficultyLevel ~= 4 and hit.shooter and hit.shooter.actor and not hit.shooter.actor:IsPlayer() then
      local displayHint = false
      if g_difficultyHintSystem == 1 then
        local g_difficultyRadius = System.GetCVar("g_difficultyRadius")
        local g_difficultyRadiusThreshold = System.GetCVar("g_difficultyRadiusThreshold")
        local newLoc = {
          x = 0,
          y = 0,
          z = 0
        }
        g_localActor:GetWorldPos(newLoc)
        local inRadiusCount = 0
        local radiusSq = g_difficultyRadius * g_difficultyRadius
        for i, loc in pairs(self.playerDeathLocations) do
          local distance = vecLenSq(vecSub(loc, newLoc))
          if radiusSq > distance then
            inRadiusCount = inRadiusCount + 1
          end
        end
        if inRadiusCount >= g_difficultyRadiusThreshold - 1 then
          displayHint = true
        else
          table.insert(self.playerDeathLocations, newLoc)
        end
      elseif g_difficultyHintSystem == 2 then
        local g_difficultySaveThreshold = System.GetCVar("g_difficultySaveThreshold")
        local curSaveName = ""
        if curSaveName == self.lastSaveName then
          self.lastSaveDeathCount = self.lastSaveDeathCount + 1
        else
          self.lastSaveName = curSaveName
          self.lastSaveDeathCount = 1
        end
        if g_difficultySaveThreshold <= self.lastSaveDeathCount then
          displayHint = true
          self.lastSaveDeathCount = 0
        end
      end
      if displayHint == true then
        HUD.DisplayBigOverlayFlashMessage("@HintDifficulty", 5, 400, 375, self.hudWhite)
      end
    end
  elseif hit.shooterId == g_localActorId then
    if not AI.Hostile(hit.target.id, g_localActorId, false) then
      local g_punishFriendlyDeaths = tonumber(System.GetCVar("g_punishFriendlyDeaths"))
      if g_punishFriendlyDeaths ~= 0 then
        self:CreateHit(g_localActorId, g_localActorId, g_localActorId, 1000, nil, nil, nil, "punish")
        HUD.ShowWarningMessage(5, "@killed_friend")
      end
    end
    self.game:SPNotifyPlayerKill(hit.target.id, hit.weaponId, isHeadShot)
  end
  self:ReleaseCorpseItem(hit.target)
end
function SinglePlayer:GetDamageTable(source, target)
  local splayer = source and source.actor and source.actor:IsPlayer()
  local sai = not splayer and source and source.actor
  local tplayer = target and target.actor and target.actor:IsPlayer()
  local tai = not tplayer and target and target.actor
  if splayer then
    if tplayer then
      return self.DamagePlayerToPlayer
    elseif tai then
      return self.DamagePlayerToAI
    end
  elseif sai then
    if tplayer then
      return self.DamageAIToPlayer
    elseif tai then
      return self.DamageAIToAI
    end
  end
  return
end
function SinglePlayer:LogHit(hit, extended, dead)
  if dead then
    Log("'%s' hit '%s' for %d with '%s'... *DEADLY*", EntityName(hit.shooter), EntityName(hit.target), hit.damage or 0, hit.weapon and hit.weapon:GetName() or "")
  else
    Log("'%s' hit '%s' for %d with '%s'...", EntityName(hit.shooter), EntityName(hit.target), hit.damage or 0, hit.weapon and hit.weapon:GetName() or "")
  end
  if extended then
    Log("  shooterId..: %s", tostring(hit.shooterId))
    Log("  targetId...: %s", tostring(hit.targetId))
    Log("  weaponId...: %s", tostring(hit.weaponId))
    Log("  type.......: %s [%d]", hit.type, hit.typeId or 0)
    Log("  material...: %s [%d]", tostring(hit.material), hit.materialId or 0)
    Log("  damage.....: %d", hit.damage or 0)
    Log("  partId.....: %d", hit.partId or -1)
    Log("  pos........: %s", Vec2Str(hit.pos))
    Log("  dir........: %s", Vec2Str(hit.dir))
    Log("  radius.....: %.3f", hit.radius or 0)
    Log("  explosion..: %s", tostring(hit.explosion or false))
    Log("  remote.....: %s", tostring(hit.remote or false))
  end
end
function SinglePlayer.Server:OnX2Hit(hit)
  local target = hit.target
  if not target then
    return
  end
  local dead = target.IsDead and target:IsDead()
  if dead and target.Server and target.Server.OnDeadHit and g_gameRules.game:PerformDeadHit() then
    target.Server.OnDeadHit(target, hit)
  end
  if target then
    SetCombatResult(hit)
  end
  if not dead and target.Server and target.Server.OnX2Hit then
    local deadly = false
    if hit.type == "event" and target.actor then
      target.actor:SetHealth(0)
      target:HealthChanged()
      self:ProcessDeath(hit)
    elseif target.Server.OnX2Hit(target, hit) then
      if target.actor and self.ProcessDeath then
        self:ProcessDeath(hit)
      end
      deadly = true
    end
    local debugHits = self.game:DebugHits()
    if debugHits > 0 then
      self:LogHit(hit, debugHits > 1, deadly)
    end
  end
end
function SinglePlayer.Server:OnHit(hit)
  Log("NEVER CALL! SinglePlayer Server:OnHit")
  local target = hit.target
  if not target then
    return
  end
  if target.actor and target.actor:IsPlayer() and self.game:IsInvulnerable(target.id) then
    hit.damage = 0
  end
  local headshot = self:IsHeadShot(hit)
  if headshot and (AI.GetGroupOf(target.id) == 0 and target.AI and target.AI.theVehicle or target.AI and target.AI.curSuitMode and target.AI.curSuitMode == BasicAI.SuitMode.SUIT_ARMOR or target.Properties and target.Properties.bNanoSuit == 1) then
    headshot = false
    hit.material_type = "torso"
  end
  if self:IsMultiplayer() or not hit.target.actor or not hit.target.actor:IsPlayer() then
    local material_type = hit.material_type
    if headshot and hit.type == "melee" then
      material_type = "torso"
    end
    hit.damage = math.floor(0.5 + self:CalcDamage(material_type, hit.damage, self:GetDamageTable(hit.shooter, hit.target), hit.assistance))
  end
  if self.game:IsFrozen(target.id) and (not target.CanShatter or tonumber(target:CanShatter()) ~= 0) then
    if hit.damage > 0 and hit.type ~= "frost" then
      self:ShatterEntity(hit.target.id, hit)
    end
    return
  end
  local dead = target.IsDead and target:IsDead()
  if dead and target.Server and target.Server.OnDeadHit and g_gameRules.game:PerformDeadHit() then
    target.Server.OnDeadHit(target, hit)
  end
  if not dead and target.Server and target.Server.OnHit then
    if headshot and target.actor and target.actor:LooseHelmet(hit.dir, hit.pos) and hit.weapon.class ~= "DSG1" then
      local health = target.actor:GetHealth()
      if health > 2 then
        target.actor:SetHealth(health - 1)
      end
      target:HealthChanged()
      return
    end
    local deadly = false
    if hit.type == "event" and target.actor then
      target.actor:SetHealth(0)
      target:HealthChanged()
      self:ProcessDeath(hit)
    elseif target.Server.OnHit(target, hit) then
      if target.actor and self.ProcessDeath then
        self:ProcessDeath(hit)
      elseif target.vehicle and self.ProcessVehicleDeath then
        self:ProcessVehicleDeath(hit)
      end
      deadly = true
    end
    local debugHits = self.game:DebugHits()
    if debugHits > 0 then
      self:LogHit(hit, debugHits > 1, deadly)
    end
  end
end
function SinglePlayer.Client:OnUpdate(deltaTime)
  self.fading = nil
  self.fadeAlpha = 0
  if 0 < self.fadeFrames then
    self.fadeFrames = self.fadeFrames - 1
    self.curFadeTime = self.fadeTime + deltaTime
  end
  if 0 < self.curFadeTime then
    self.curFadeTime = self.curFadeTime - deltaTime
    if self.fadingToBlack then
      self.fadeAlpha = 255 * (1 - self.curFadeTime / self.fadeTime)
    else
      self.fadeAlpha = 255 * (self.curFadeTime / self.fadeTime)
    end
    local dt = 1 - self.fadeAlpha / 255
    if not self.fadingToBlack then
      Sound.SetMasterVolumeScale(dt)
    end
    self.fading = true
  end
end
function SinglePlayer.Server:OnStartLevel()
  self.playerDeathLocations = {}
  self.lastSaveName = ""
  self.lastSaveDeathCount = 0
  CryAction.SendGameplayEvent(NULL_ENTITY, eGE_GameStarted)
end
function SinglePlayer.Client:OnStartLevel()
  if not self.faded then
    self.fadeFrames = 8
    self.faded = true
  end
end
function SinglePlayer.Client:OnHit(hit)
  Log("NEVER CALL! SinglePlayer Client:OnHit")
  if not hit.target or not self.game:IsFrozen(hit.target.id) then
    local trg = hit.target
    if trg and not hit.backface and trg.Client and trg.Client.OnHit then
      trg.Client.OnHit(trg, hit)
    end
  end
end
function SinglePlayer.Client:OnExplosion(explosion)
end
function SinglePlayer.Server:OnExplosion(explosion)
  Log("NEVER CALL! SinglePlayer Server:OnExplosion")
  local entities = explosion.AffectedEntities
  local entitiesObstruction = explosion.AffectedEntitiesObstruction
  if entities then
    for i, entity in ipairs(entities) do
      local incone = true
      if explosion.angle > 0 and explosion.angle < 2 * math.pi then
        self.explosion_entity_pos = entity:GetWorldPos(self.explosion_entity_pos)
        local entitypos = self.explosion_entity_pos
        local ha = explosion.angle * 0.5
        local edir = vecNormalize(vecSub(entitypos, explosion.pos))
        local dot = 1
        if edir then
          dot = vecDot(edir, explosion.dir)
        end
        local angle = math.abs(math.acos(dot))
        if ha < angle then
          incone = false
        end
      end
      local frozen = self.game:IsFrozen(entity.id)
      if incone and (frozen or entity.Server and entity.Server.OnHit) then
        local obstruction = entitiesObstruction[i]
        local damage = explosion.damage
        damage = math.floor(0.5 + self:CalcExplosionDamage(entity, explosion, obstruction))
        local dead = entity.IsDead and entity:IsDead()
        local explHit = self.explosionHit
        explHit.pos = explosion.pos
        explHit.dir = vecNormalize(vecSub(entity:GetWorldPos(), explosion.pos))
        explHit.radius = explosion.radius
        explHit.partId = -1
        explHit.target = entity
        explHit.targetId = entity.id
        explHit.weapon = explosion.weapon
        explHit.weaponId = explosion.weaponId
        explHit.shooter = explosion.shooter
        explHit.shooterId = explosion.shooterId
        explHit.materialId = 0
        explHit.damage = damage
        explHit.typeId = explosion.typeId or 0
        explHit.type = explosion.type or ""
        explHit.explosion = true
        explHit.impact = explosion.impact
        explHit.impact_targetId = explosion.impact_targetId
        local deadly = false
        local canShatter = not entity.CanShatter or tonumber(entity:CanShatter()) ~= 0
        if self.game:IsFrozen(entity.id) and canShatter then
          if damage > 15 then
            local hitpos = entity:GetWorldPos()
            local hitdir = vecNormalize(vecSub(hitpos, explosion.pos))
            self:ShatterEntity(entity.id, explHit)
          end
        else
          if entity.actor and entity.actor:IsPlayer() and self.game:IsInvulnerable(entity.id) then
            explHit.damage = 0
          end
          if not dead and entity.Server and entity.Server.OnHit and entity.Server.OnHit(entity, explHit) then
            if entity.actor and self.ProcessDeath then
              self:ProcessDeath(explHit)
            elseif entity.vehicle and self.ProcessVehicleDeath then
              self:ProcessVehicleDeath(explHit)
            end
            deadly = true
          end
        end
        local debugHits = self.game:DebugHits()
        if debugHits > 0 then
          self:LogHit(explHit, debugHits > 1, deadly)
        end
      end
    end
  end
end
function SinglePlayer:ShatterEntity(entityId, hit)
  Log("NEVER CALL! SinglePlayer:ShatterEntity")
  local entity = System.GetEntity(entityId)
  local isPlayer = entity and entity.actor and entity.actor:IsPlayer()
  if isPlayer then
    local isGod = entity.actorStats.godMode
    if isGod and isGod > 0 then
      entity.actor:SetHealth(0)
      entity.actor:SetHealth(entity.actor:GetMaxHealth())
      return
    end
  end
  local damage = math.min(100, hit.damage or 0)
  damage = math.max(20, damage)
  local dir = hit.dir
  dir = dir or g_Vectors.up
  self.game:ShatterEntity(entityId, hit.pos, vecScale(dir, damage))
  if isPlayer then
    entity:Kill(false, hit.shooterId, hit.weaponId)
    self:ReleaseCorpseItem(entity)
    HUD.ShowDeathFX(4)
  end
end
function SinglePlayer.Client:OnPlayerKilled(player)
  if player.actor and player.actor:IsPlayer() then
    Script.SetTimer(4000, function()
      if not System.IsEditor() then
      end
    end)
    if g_gameRules then
      Script.SetTimer(3000, function()
        g_gameRules.curFadeTime = 1
        g_gameRules.fadeTime = 1
        g_gameRules.fading = true
        g_gameRules.fadingToBlack = true
      end)
    end
  end
end
function SinglePlayer:GetCollisionEnergy(entity, hit)
  local m0 = entity:GetMass()
  local m1 = hit.target_mass
  local bCollider = hit.target or m1 > 0.001
  local debugColl = self.game:DebugCollisionDamage()
  if debugColl > 0 then
    local targetName = hit.target and hit.target:GetName() or "[no entity]"
    Log("GetCollisionEnergy %s (%.1f) <-> %s (%.1f)", entity:GetName(), m0, targetName, m1)
  end
  local v0Sq = 0
  local v1Sq = 0
  if bCollider then
    m0 = __min(m0, m1)
    local v0normal = g_Vectors.temp_v1
    local v1normal = g_Vectors.temp_v2
    local vrel = g_Vectors.temp_v3
    local v0dotN = dotproduct3d(hit.velocity, hit.normal)
    FastScaleVector(v0normal, hit.normal, v0dotN)
    local v1dotN = dotproduct3d(hit.target_velocity, hit.normal)
    FastScaleVector(v1normal, hit.normal, v1dotN)
    FastDifferenceVectors(vrel, v0normal, v1normal)
    local vrelSq = vecLenSq(vrel)
    v0Sq = __min(sqr(v0dotN), vrelSq)
    v1Sq = __min(sqr(v1dotN), vrelSq)
    if debugColl > 0 then
      CryAction.PersistantSphere(hit.pos, 0.15, g_Vectors.v100, "CollDamage", 5)
      CryAction.PersistantArrow(hit.pos, 1.5, ScaleVector(hit.normal, sgn(v0dotN)), g_Vectors.v010, "CollDamage", 5)
      CryAction.PersistantArrow(hit.pos, 1.3, ScaleVector(hit.normal, sgn(v1dotN)), g_Vectors.v100, "CollDamage", 5)
      if v0Sq > 4 or v1Sq > 4 then
        Log("normal velocities: rel %.1f, <%s> %.1f / <%s> %.1f", math.sqrt(vrelSq), entity:GetName(), v0dotN, hit.target and hit.target:GetName() or "none", v1dotN)
        Log("target_type: %i, target_velocity: %s", hit.target_type, Vec2Str(hit.target_velocity))
      end
    end
  else
    v0Sq = sqr(dotproduct3d(hit.velocity, hit.normal))
    if debugColl > 0 and v0Sq > 25 then
      CryAction.PersistantArrow(hit.pos, 1.5, hit.normal, g_Vectors.v010, "CollDamage", 5)
      CryAction.Persistant2DText("z: " .. hit.velocity.z, 1.5, g_Vectors.v111, "CollDamage", 5)
    end
  end
  local colliderEnergyScale = 1
  if hit.target and entity.GetColliderEnergyScale then
    colliderEnergyScale = entity:GetColliderEnergyScale(hit.target)
    if debugColl ~= 0 then
      Log("colliderEnergyScale: %.1f", colliderEnergyScale)
    end
  end
  local energy0 = 0.5 * m0 * v0Sq
  local energy1 = 0.5 * m1 * v1Sq * colliderEnergyScale
  return energy0 + energy1
end
function SinglePlayer:PrecacheLevel()
end
