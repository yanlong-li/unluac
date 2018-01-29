SoundMoodVolume = {
  type = "SoundMoodVolume",
  Editor = {
    Model = "Editor/Objects/Sound.cgf",
    Icon = "SoundMoodVolume.bmp"
  },
  Properties = {
    sSoundMoodName = "",
    OuterRadius = 2,
    bEnabled = 1
  },
  bstarted = 0
}
function SoundMoodVolume:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function SoundMoodVolume:OnLoad(table)
  self.bstarted = table.bstarted
  self.bEnabled = table.bEnabled
end
function SoundMoodVolume:OnSave(table)
  table.bstarted = self.bstarted
  table.bEnabled = self.bEnabled
end
function SoundMoodVolume:OnPropertyChange()
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
  self.bEnabled = self.Properties.bEnabled
  self.sSoundMoodName = self.Properties.sSoundMoodName
end
function SoundMoodVolume:CliSrv_OnInit()
  self.bstarted = 0
  self.sSoundMoodName = ""
  self:NetPresent(0)
  self.inside = 0
  self.fFadeValue = 0
  self:SetFlags(ENTITY_FLAG_VOLUME_SOUND, 0)
  if self.Initialized then
    return
  end
  self:SetSoundEffectRadius(self.Properties.OuterRadius)
  self.Initialized = 1
end
function SoundMoodVolume:OnShutDown()
end
function SoundMoodVolume:RegisterSoundMood(player)
  if self.bstarted == 0 then
    Sound.RegisterSoundMood(self.Properties.sSoundMoodName)
    self.bstarted = 1
  end
end
function SoundMoodVolume:UnregisterSoundMood(player)
  if self.bstarted ~= 0 then
    Sound.UnregisterSoundMood(self.Properties.sSoundMoodName)
    self.bstarted = 0
  end
end
function SoundMoodVolume:UpdateSoundMood(player, fDistSq, fExternalFade)
  if self.Properties.OuterRadius ~= 0 then
    if fExternalFade == 0 then
      if fDistSq == 0 then
        SoundMoodVolume.UnregisterSoundMood(self, player)
      elseif self.inside == 1 and self.fFadeValue ~= 1 then
        Sound.UpdateSoundMood(self.Properties.sSoundMoodName, 1, 0)
        self.fFadeValue = 1
      else
        local fFade = 1 - math.sqrt(fDistSq) / self.Properties.OuterRadius
        if fFade > 0 then
          Sound.UpdateSoundMood(self.Properties.sSoundMoodName, fFade, 0)
          self.fFadeValue = fFade
        end
      end
    else
      Sound.UpdateSoundMood(self.Properties.sSoundMoodName, fExternalFade, 0)
      self.fFadeValue = fExternalFade
    end
  end
end
function SoundMoodVolume:OnMove()
end
SoundMoodVolume.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end
}
SoundMoodVolume.Client = {
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
    SoundMoodVolume.RegisterSoundMood(self, player)
  end,
  OnMoveNearArea = function(self, player, nAreaID, fFade, fDistsq)
    if g_localActorId ~= player.id then
      return
    end
    SoundMoodVolume.UpdateSoundMood(self, player, fDistsq, 0)
  end,
  OnEnterArea = function(self, player, nAreaID, fFade)
    if g_localActorId ~= player.id then
      return
    end
    SoundMoodVolume.RegisterSoundMood(self, player)
    self.inside = 1
    self.fFadeValue = 0
    SoundMoodVolume.UpdateSoundMood(self, player, 1, 0)
  end,
  OnSoundEnterArea = function(self, player, areaId, fFade)
    if g_localActorId ~= player.id then
      return
    end
  end,
  OnProceedFadeArea = function(self, player, nAreaID, fExternalFade)
    if g_localActorId ~= player.id then
      return
    end
    if fExternalFade > 0 then
      self.inside = 1
      SoundMoodVolume.RegisterSoundMood(self, player)
      SoundMoodVolume.UpdateSoundMood(self, player, 0, fExternalFade)
    else
      SoundMoodVolume.UpdateSoundMood(self, player, 0, 0)
    end
    self.fFadeValue = fExternalFade
  end,
  OnLeaveArea = function(self)
    if g_localActorId ~= player.id then
      return
    end
    self.inside = 0
  end,
  OnLeaveNearArea = function(self, player, areaId, fFade)
    if g_localActorId ~= player.id then
      return
    end
    SoundMoodVolume.UnregisterSoundMood(self, player)
    self.inside = 0
  end,
  OnEndState = function(self)
  end
}
function SoundMoodVolume:Event_Deactivate(sender)
  self.bEnabled = 0
  SoundMoodVolume.UnregisterSoundMood(self)
end
function SoundMoodVolume:Event_Activate(sender)
  self.bEnabled = 1
  SoundMoodVolume.RegisterSoundMood(self)
end
function SoundMoodVolume:Event_Fade(sender, fFade)
  self.fFadeValue = fFade
  Sound.UpdateSoundMood(self.Properties.sSoundMoodName, self.fFadeValue, 0)
end
SoundMoodVolume.FlowEvents = {
  Inputs = {
    Deactivate = {
      SoundMoodVolume.Event_Deactivate,
      "bool"
    },
    Activate = {
      SoundMoodVolume.Event_Activate,
      "bool"
    },
    Fade = {
      SoundMoodVolume.Event_Fade,
      "float"
    }
  },
  Outputs = {}
}
