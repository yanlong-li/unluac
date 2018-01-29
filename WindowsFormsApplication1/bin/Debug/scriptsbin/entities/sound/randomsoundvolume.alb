RandomSoundVolume = {
  type = "Sound",
  Properties = {
    soundName = "",
    bEnabled = 1,
    DiscRadius = 10,
    MinWaitTime = 2,
    MaxWaitTime = 5,
    bIgnoreCulling = 0,
    bIgnoreObstruction = 0,
    bRandomPosition = 1,
    bSensitiveToBattle = 0,
    bLogBattleValue = 0
  },
  started = 0,
  bEnabled = 1,
  Editor = {
    Model = "Editor/Objects/Sound.cgf",
    Icon = "RandomSoundVolume.bmp"
  },
  fFade = 1,
  bEnabled = 1
}
function RandomSoundVolume:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
  if System.IsEditor() then
    Sound.Precache(self.Properties.soundName, SOUND_PRECACHE_LOAD_SOUND)
  end
end
function RandomSoundVolume:OnSave(save)
  save.started = self.started
  save.bEnabled = self.Properties.bEnabled
  save.bIgnoreCulling = self.Properties.bIgnoreCulling
  save.bIgnoreObstruction = self.Properties.bIgnoreObstruction
  save.bRandomPosition = self.Properties.bRandomPosition
  save.bSensitiveToBattle = self.Properties.bSensitiveToBattle
  save.bLogBattleValue = self.Properties.bLogBattleValue
end
function RandomSoundVolume:OnLoad(load)
  self.started = load.started
  self.Properties.bEnabled = load.bEnabled
  self.Properties.bIgnoreCulling = load.bIgnoreCulling
  self.Properties.bIgnoreObstruction = load.bIgnoreObstruction
  self.Properties.bRandomPosition = load.bRandomPosition
  self.Properties.bSensitiveToBattle = load.bSensitiveToBattle
  self.Properties.bLogBattleValue = load.bLogBattleValue
  self.bEnabled = self.Properties.bEnabled
end
function RandomSoundVolume:OnPropertyChange()
  if self.soundName ~= self.Properties.soundName or self.bEnabled ~= self.Properties.bEnabled then
    self:OnReset()
  end
  if self.inside == 1 then
    self:SetTimer(0, self.Properties.MinWaitTime * 1000)
  end
  Sound.SetSoundVolume(self.soundid, 1 * self.fFade)
end
function RandomSoundVolume:OnReset()
  self:Stop()
  self.bEnabled = self.Properties.bEnabled
end
RandomSoundVolume.Server = {
  OnInit = function(self)
    self.started = 0
    self:NetPresent(0)
  end,
  OnShutDown = function(self)
  end
}
RandomSoundVolume.Client = {
  OnInit = function(self)
    self.started = 0
    self.inside = 0
    self.soundName = ""
    self.soundid = nil
    self.NextStart = self.Properties.MinWaitTime
    self:NetPresent(0)
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 1
    if self.bEnabled == 0 then
      return
    end
    if self.soundid == nil then
      self:SetTimer(0, self.Properties.MinWaitTime * 1000)
    end
  end,
  OnTimer = function(self, timerid, msec)
    if timerid == 0 then
      self:Play()
    end
    if timerid == 1 then
      self:UpdateBattleNoise()
    end
  end,
  OnProceedFadeArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    if self.fFade ~= fFade then
      if fFade < 0 then
        self.fFade = 0
      else
        self.fFade = fFade
      end
      self:OnPropertyChange()
    end
  end,
  OnLeaveArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 0
    self:Stop()
  end,
  OnUnBindThis = function(self)
    System.LogToConsole("OnUnBindThis-Client")
    self:Stop()
    self.inside = 0
  end
}
function RandomSoundVolume:UpdateBattleNoise()
  if self.Properties.bSensitiveToBattle == 1 and self.soundid then
    local fBattle = Game.QueryBattleStatus()
    Sound.SetParameterValue(self.soundid, "battle", fBattle)
    if self.Properties.bLogBattleValue == 1 then
      System.Log("RSV: Current Battle Value :" .. tostring(fBattle))
    end
  end
end
function RandomSoundVolume:Play()
  if self.bEnabled == 0 then
    return
  end
  if self.soundid ~= nil then
    self:Stop()
  end
  local sndFlags = SOUND_DEFAULT_3D
  if self.Properties.bIgnoreCulling == 1 then
    sndFlags = band(sndFlags, bnot(SOUND_CULLING))
  end
  if self.Properties.bIgnoreObstruction == 1 then
    sndFlags = band(sndFlags, bnot(SOUND_OBSTRUCTION))
  end
  sndFlags = bor(sndFlags, SOUND_START_PAUSED)
  local nTimeToWait
  if self.Properties.MinWaitTime > self.Properties.MaxWaitTime then
    Log("MinWaitTime > MaxWaitTime for Entity '" .. self:GetName() .. "'. Please correct values!")
    nTimeToWait = self.Properties.MinWaitTime
  else
    nTimeToWait = math.random(self.Properties.MinWaitTime, self.Properties.MaxWaitTime)
  end
  self:SetTimer(0, nTimeToWait * 1000)
  if self.Properties.bRandomPosition == 1 then
    if g_localActor then
      local pos = g_localActor:GetWorldPos(g_Vectors.temp_v1)
      local rx = math.sin(math.random(0, math.pi))
      local ry = math.cos(math.random(0, math.pi))
      pos.x = pos.x + rx * self.Properties.DiscRadius
      pos.y = pos.y + ry * self.Properties.DiscRadius
      self.soundid = Sound.PlayEx(self.Properties.soundName, pos, sndFlags, self.fFade, 0, 0, SOUND_SEMANTIC_AMBIENCE_ONESHOT)
    end
  else
    self.soundid = self:PlaySoundEventEx(self.Properties.soundName, sndFlags, self.fFade, g_Vectors.v000, 0, 0, SOUND_SEMANTIC_AMBIENCE_ONESHOT)
  end
  self:UpdateBattleNoise()
  Sound.SetSoundPaused(self.soundid, 0)
  self.soundName = self.Properties.soundName
  if self.soundid ~= nil then
    self.started = 1
  end
end
function RandomSoundVolume:Stop()
  if self.soundid ~= nil then
    Sound.StopSound(self.soundid)
    self.soundid = nil
  end
  self.started = 0
  self:KillTimer(0)
  self:KillTimer(1)
end
function RandomSoundVolume:Event_SoundName(sender, sSoundName)
  self.Properties.soundName = sSoundName
  self:OnReset()
end
function RandomSoundVolume:Event_Deactivate(sender)
  self.bEnabled = 0
  self:Stop()
end
function RandomSoundVolume:Event_Activate(sender)
  self.bEnabled = 1
  self:Play()
end
function RandomSoundVolume:Event_Radius(sender, fRadius)
  self.Properties.DiscRadius = fRadius
  self:OnPropertyChange()
end
RandomSoundVolume.FlowEvents = {
  Inputs = {
    sound_SoundName = {
      RandomSoundVolume.Event_SoundName,
      "string"
    },
    Deactivate = {
      RandomSoundVolume.Event_Deactivate,
      "bool"
    },
    Activate = {
      RandomSoundVolume.Event_Activate,
      "bool"
    },
    Radius = {
      RandomSoundVolume.Event_Radius,
      "float"
    }
  },
  Outputs = {}
}
