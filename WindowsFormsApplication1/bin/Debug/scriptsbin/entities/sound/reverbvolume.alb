ReverbVolume = {
  type = "ReverbVolume",
  Editor = {
    Model = "Editor/Objects/Sound.cgf",
    Icon = "ReverbVolume.bmp"
  },
  Properties = {
    reverbpresetReverbPreset = "",
    OuterRadius = 2,
    Environment = 1,
    bFullEffectWhenInside = 1,
    bEnabled = 1
  },
  bstarted = 0
}
function ReverbVolume:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function ReverbVolume:OnLoad(table)
  self.bstarted = table.bstarted
  self.bEnabled = table.bEnabled
end
function ReverbVolume:OnSave(table)
  table.bstarted = self.bstarted
  table.bEnabled = self.bEnabled
end
function ReverbVolume:OnPropertyChange()
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
  self.bEnabled = self.Properties.bEnabled
  self.Environment = self.Properties.Environment
end
function ReverbVolume:CliSrv_OnInit()
  self.bstarted = 0
  self.sReverbName = ""
  self:NetPresent(0)
  self.inside = 0
  self.fFadeValue = 0
  self.Environment = 0
  self:SetFlags(ENTITY_FLAG_VOLUME_SOUND, 0)
  if self.Initialized then
    return
  end
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
  self.Initialized = 1
end
function ReverbVolume:OnShutDown()
end
function ReverbVolume:RegisterReverb(player)
  if self.bEnabled == 0 then
    return
  end
  if self.bstarted == 0 then
    Sound.RegisterWeightedEaxEnvironment(self.Properties.reverbpresetReverbPreset, self.id, self.Properties.bFullEffectWhenInside, 0)
    self.bstarted = 1
  end
end
function ReverbVolume:UnregisterReverb(player)
  if self.bstarted ~= 0 then
    Sound.UnregisterWeightedEaxEnvironment(self.Properties.reverbpresetReverbPreset, self.id)
    self.bstarted = 0
    self.fFadeValue = 0
  end
end
function ReverbVolume:UpdateReverb(player, fFade, fDistSq)
  if self.bEnabled == 0 or fFade == 0 and fDistSq == 0 then
    Sound.UnregisterWeightedEaxEnvironment(self.Properties.reverbpresetReverbPreset, self.id)
    self.bstarted = 0
    return
  end
  if self.Properties.OuterRadius ~= 0 then
    if self.inside == 1 then
      if self.fFadeValue ~= fFade then
        self.fFadeValue = math.abs(fFade)
        Sound.UpdateWeightedEaxEnvironment(self.Properties.reverbpresetReverbPreset, self.id, self.fFadeValue)
      end
    else
      local fLocalFade = 1 - math.sqrt(fDistSq) / self.Properties.OuterRadius
      if fLocalFade > 0 then
        Sound.UpdateWeightedEaxEnvironment(self.Properties.reverbpresetReverbPreset, self.id, fLocalFade)
        self.fFadeValue = fLocalFade
      end
    end
  end
end
function ReverbVolume:OnMove()
end
ReverbVolume.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end
}
ReverbVolume.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
    self:OnMove()
  end,
  OnShutDown = function(self)
  end,
  OnBeginState = function(self)
  end,
  OnBeginState = function(self)
  end,
  OnEnterNearArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    ReverbVolume.RegisterReverb(self, player)
  end,
  OnMoveNearArea = function(self, player, areaId, fFade, fDistsq)
    if g_localActorId ~= player.id then
      return
    end
    ReverbVolume.UpdateReverb(self, player, fFade, fDistsq)
  end,
  OnEnterArea = function(self, player, areaId, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 1
    self.fFadeValue = 0
    ReverbVolume.RegisterReverb(self, player)
    ReverbVolume.UpdateReverb(self, player, fFade, 0)
  end,
  OnSoundEnterArea = function(self)
    if g_localActorId ~= player.id then
      return
    end
    if self.bEnabled ~= 0 then
    end
  end,
  OnProceedFadeArea = function(self, player, areaId, fExternalFade)
    if g_localActorId ~= player.id then
      return
    end
    if fExternalFade > 0 then
      self.inside = 1
      ReverbVolume.RegisterReverb(self, player)
      ReverbVolume.UpdateReverb(self, player, fExternalFade, 0)
    else
      ReverbVolume.UpdateReverb(self, player, 0, 0)
    end
  end,
  OnLeaveArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 0
  end,
  OnLeaveNearArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    ReverbVolume.UnregisterReverb(self, player)
    self.inside = 0
  end,
  OnUnBindThis = function(self)
    ReverbVolume.UnregisterReverb(self, player)
    self.inside = 0
  end,
  OnEndState = function(self)
  end
}
function ReverbVolume:Event_Deactivate(sender)
  self.bEnabled = 0
end
function ReverbVolume:Event_Activate(sender)
  self.bEnabled = 1
end
ReverbVolume.FlowEvents = {
  Inputs = {
    Deactivate = {
      ReverbVolume.Event_Deactivate,
      "bool"
    },
    Activate = {
      ReverbVolume.Event_Activate,
      "bool"
    }
  },
  Outputs = {}
}
