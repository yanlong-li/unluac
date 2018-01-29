HQ = {
  Client = {},
  Server = {},
  Editor = {
    Model = "Editor/Objects/HQ.cgf"
  },
  Properties = {
    objModel = "objects/default.cgf",
    objDestroyedModel = "",
    teamName = "",
    nHitPoints = 500,
    perimeterAreaId = 0,
    hqType = "bunker",
    Explosion = {
      Effect = "",
      EffectScale = 1,
      EffectDirection = {
        x = 0,
        y = 0,
        z = 1
      }
    }
  }
}
function HQ:OnPreFreeze(freeze, vapor)
  if freeze then
    return false
  end
end
function HQ:CanShatter()
  return 0
end
function HQ:Destroy()
  self.destroyed = true
  self:EnableSlot(0, false)
  self:EnableSlot(1, true)
  local explosion = self.Properties.Explosion
  if explosion.Effect ~= "" then
    Particle.SpawnEffect(explosion.Effect, self:GetWorldPos(), explosion.EffectDirection, explosion.EffectScale)
  end
end
function HQ:LoadGeometry(slot, model)
  if string.len(model) > 0 then
    local ext = string.lower(string.sub(model, -4))
    if ext == ".chr" or ext == ".cdf" or ext == ".cga" then
      self:LoadCharacter(slot, model)
    else
      self:LoadObject(slot, model)
    end
  end
end
function HQ:OnSpawn()
  CryAction.CreateGameObjectForEntity(self.id)
  CryAction.BindGameObjectToNetwork(self.id)
  CryAction.ForceGameObjectUpdate(self.id, true)
  self:LoadGeometry(0, self.Properties.objModel)
  self:LoadGeometry(1, self.Properties.objDestroyedModel)
end
function HQ:EnableSlot(slot, enable)
  if enable then
    self:Physicalize(slot, PE_STATIC, {})
    self:DrawSlot(slot, 1)
  else
    self:DestroyPhysics()
    self:DrawSlot(slot, 0)
  end
end
function HQ:OnReset()
  self:EnableSlot(1, false)
  self:EnableSlot(0, true)
  self.isServer = CryAction.IsServer()
  self.isClient = CryAction.IsClient()
  if self.isServer then
    if self.Properties.teamName ~= "" then
      self:SetTeamId(g_gameRules.game:GetTeamId(self.Properties.teamName) or 0)
    else
      self:SetTeamId(0)
    end
  end
  self.destroyed = false
  self:SetHealth(self.Properties.nHitPoints)
end
function HQ:OnPropertyChange()
  self:OnReset()
end
function HQ:OnDestroy()
end
function HQ:OnSave(save)
end
function HQ:OnLoad(saved)
end
function HQ:GetTeamId()
  return g_gameRules.game:GetTeam(self.id)
end
function HQ:SetTeamId(teamId)
  g_gameRules.game:SetTeam(teamId, self.id)
end
function HQ:IsDead()
  return self.destroyed
end
function HQ:SetHealth(health)
  self.synched.health = health
end
function HQ:GetHealth()
  return self.synched.health
end
function HQ.Server:OnInit()
  self:OnReset()
end
function HQ.Server:OnShutDown()
  if g_gameRules then
  end
end
function HQ.Server:OnInitClient(channelId)
  if self.destroyed then
    self.onClient:ClDestroy(channelId)
  end
end
function HQ.Client:OnInit()
  self:OnReset()
end
function HQ.Server:OnHit(hit)
  if self.destroyed then
    return
  end
  local destroyed = false
  local teamId = g_gameRules.game:GetTeam(hit.shooterId)
  if (teamId == 0 or teamId ~= self:GetTeamId()) and hit.explosion and hit.type == "tac" then
    self:SetHealth(self:GetHealth() - hit.damage)
    if 0 >= self:GetHealth() then
      destroyed = true
    end
    if 0 < hit.damage and hit.type ~= "repair" and g_gameRules.Server.OnHQHit then
      g_gameRules.Server.OnHQHit(g_gameRules, self, hit)
    end
  end
  if destroyed then
    if not self.isClient then
      self:Destroy()
    end
    self.allClients:ClDestroy()
    if g_gameRules and g_gameRules.OnHQDestroyed then
      g_gameRules:OnHQDestroyed(self, hit.shooterId, teamId)
    end
  end
  return destroyed
end
function HQ.Client:ClDestroy()
  self:Destroy()
end
function HQ.Server:OnEnterArea(entity, areaId)
  if entity.actor and areaId == self.Properties.perimeterAreaId and g_gameRules.Server.OnPerimeterBreached then
    g_gameRules.Server.OnPerimeterBreached(g_gameRules, self, entity)
  end
end
