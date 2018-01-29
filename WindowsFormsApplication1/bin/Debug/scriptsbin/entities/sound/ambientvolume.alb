AmbientVolume = {
  type = "Sound",
  Properties = {
    soundName = "",
    OuterRadius = 10,
    bEnabled = 1,
    bIgnoreCulling = 0,
    bIgnoreObstruction = 0,
    bSensitiveToBattle = 0,
    bLogBattleValue = 0
  },
  started = 0,
  Editor = {
    Model = "Editor/Objects/Sound.cgf",
    Icon = "AmbientVolume.bmp"
  },
  bEnabled = 1,
  fFade = 1
}
function AmbientVolume:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
  if System.IsEditor() then
    Sound.Precache(self.Properties.soundName, SOUND_PRECACHE_LOAD_SOUND)
  end
end
function AmbientVolume:OnSave(save)
  save.started = self.started
  save.bEnabled = self.Properties.bEnabled
  save.bIgnoreCulling = self.Properties.bIgnoreCulling
  save.bIgnoreObstruction = self.Properties.bIgnoreObstruction
  save.bSensitiveToBattle = self.Properties.bSensitiveToBattle
  save.bLogBattleValue = self.Properties.bLogBattleValue
  if self.soundid ~= nil then
    save.fLastVolume = Sound.GetSoundVolume(self.soundid)
  end
end
function AmbientVolume:OnLoad(load)
  self:OnReset()
  self.started = load.started
  self.Properties.bEnabled = load.bEnabled
  self.Properties.bIgnoreCulling = load.bIgnoreCulling
  self.Properties.bIgnoreObstruction = load.bIgnoreObstruction
  self.Properties.bSensitiveToBattle = load.bSensitiveToBattle
  self.Properties.bLogBattleValue = load.bLogBattleValue
  if self.started == 1 and self.Properties.bOnce ~= 1 then
    self:Play()
  end
  if self.soundid ~= nil then
    Sound.SetSoundVolume(self.soundid, load.fLastVolume)
  end
end
function AmbientVolume:OnPropertyChange()
  if self.soundName ~= self.Properties.soundName or self.bEnabled ~= self.Properties.bEnabled then
    self:OnReset()
  end
  Sound.SetSoundVolume(self.soundid, 1 * self.fFade)
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
end
function AmbientVolume:OnReset()
  self:Stop()
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
  self.bEnabled = self.Properties.bEnabled
end
function AmbientVolume:UpdateBattleNoise()
  if self.Properties.bSensitiveToBattle == 1 and self.soundid then
    local fBattle = Game.QueryBattleStatus()
    Sound.SetParameterValue(self.soundid, "battle", fBattle)
    if self.Properties.bLogBattleValue == 1 then
      System.Log("AV: Current Battle Value :" .. tostring(fBattle))
    end
    self:SetTimer(1, 300)
  else
    self:KillTimer(1)
  end
end
AmbientVolume.Server = {
  OnInit = function(self)
    self.started = 0
    self:NetPresent(0)
    self:SetFlags(ENTITY_FLAG_VOLUME_SOUND, 0)
  end,
  OnShutDown = function(self)
  end
}
AmbientVolume.Client = {
  OnInit = function(self)
    self.started = 0
    self.inside = 0
    self.soundName = ""
    self.soundid = nil
    self:NetPresent(0)
    self:SetFlags(ENTITY_FLAG_VOLUME_SOUND, 0)
    self:SetSoundEffectRadius(self.Properties.OuterRadius)
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 1
    if self.soundid == nil then
      self:Play()
    end
    if fFade == -1 then
      fFade = 1
    end
    Sound.SetSoundVolume(self.soundid, fFade)
    if self.fFade ~= fFade then
      if fFade < 0 then
        self.fFade = 0
      else
        self.fFade = fFade
      end
    end
  end,
  OnProceedFadeArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    if self.soundid == nil then
      self:Play()
    end
    Sound.SetSoundVolume(self.soundid, fFade)
    if self.fFade ~= fFade then
      if fFade < 0 then
        self.fFade = 0
      else
        self.fFade = fFade
      end
    end
  end,
  OnEnterNearArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    if self.soundid == nil then
      self:Play()
    end
  end,
  OnMoveNearArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    if self.soundid == nil then
      self:Play()
    end
  end,
  OnLeaveNearArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    if self.inside ~= 1 then
      self:Stop()
    end
  end,
  OnLeaveArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 0
  end,
  OnTimer = function(self, timerid, msec)
    if timerid == 1 then
      self:UpdateBattleNoise()
    end
  end,
  OnUnBindThis = function(self)
    System.LogToConsole("OnUnBindThis-Client")
    self:Stop()
    self.inside = 0
  end
}
function AmbientVolume:Play()
  if self.Properties.bEnabled == 0 then
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
  self.soundid = self:PlaySoundEventEx(self.Properties.soundName, sndFlags, 0, g_Vectors.v000, 0, 0, SOUND_SEMANTIC_AMBIENCE)
  Sound.SetFadeTime(self.soundid, 1, 300)
  Sound.SetSoundVolume(self.soundid, 0)
  self:UpdateBattleNoise()
  Sound.SetSoundPaused(self.soundid, 0)
  self.soundName = self.Properties.soundName
  if self.soundid ~= nil then
    self.started = 1
  end
end
function AmbientVolume:Stop()
  if self.soundid ~= nil then
    self:StopSound(self.soundid)
    self.soundid = nil
  end
  self.started = 0
  self:KillTimer(1)
end
function AmbientVolume:Event_SoundName(sender, sSoundName)
  self.Properties.soundName = sSoundName
  self:OnReset()
end
function AmbientVolume:Event_Deactivate(sender)
  self.Properties.bEnabled = 0
  self:Stop()
end
function AmbientVolume:Event_Activate(sender)
  self.Properties.bEnabled = 1
  if self.inside == 1 then
    self:Play()
    Sound.SetSoundVolume(self.soundid, self.fFade)
  end
end
function AmbientVolume:Event_Radius(sender, fRadius)
end
AmbientVolume.FlowEvents = {
  Inputs = {
    sound_SoundName = {
      AmbientVolume.Event_SoundName,
      "string"
    },
    Deactivate = {
      AmbientVolume.Event_Deactivate,
      "bool"
    },
    Activate = {
      AmbientVolume.Event_Activate,
      "bool"
    },
    Radius = {
      AmbientVolume.Event_Radius,
      "float"
    }
  },
  Outputs = {}
}
