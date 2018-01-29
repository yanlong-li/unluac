MissionHint = {
  type = "Sound",
  Properties = {
    Hints = {
      sndHint1 = "",
      sndHint2 = "",
      sndHint3 = "",
      sndHint4 = "",
      sndHint5 = "",
      sndHint6 = "",
      sndHint7 = "",
      sndHint8 = "",
      sndHint9 = "",
      sndHint10 = ""
    },
    sndSkipAcknowledge = "",
    iAllowedToSkip = 3,
    fVolume = 1,
    bLoop = 0,
    bOnce = 0,
    bEnabled = 1,
    bScaleDownVolumes = 1
  },
  skipped = 0,
  HintCount = 1,
  SkipCount = 0,
  Editor = {
    Model = "Editor/Objects/Sound.cgf"
  }
}
function MissionHint:OnSave(props)
  props.HintCount = self.HintCount
end
function MissionHint:OnLoad(props)
  self:OnReset()
  self.HintCount = props.HintCount
end
function MissionHint:OnPropertyChange()
  if self.soundName ~= self.Properties.sndSource or self.soundid == nil or self.Properties.bLoop ~= self.loop then
    self.loop = self.Properties.bLoop
  end
  self:OnReset()
  if self.soundid ~= nil then
    if self.Properties.bLoop ~= 0 then
      Sound.SetSoundLoop(self.soundid, 1)
    else
      Sound.SetSoundLoop(self.soundid, 0)
    end
    Sound.SetSoundVolume(self.soundid, self.Properties.iVolume)
  end
end
function MissionHint:OnReset()
  self.SkipCount = 0
  self.HintCount = 1
  self.skipped = 0
  self:StopSound()
  self.soundid = nil
  self:ActivateOutput("Done", false)
  Sound.SetGroupScale(SOUNDSCALE_MISSIONHINT, 1)
end
MissionHint.Server = {
  OnInit = function(self)
    self:Activate(0)
    self.started = 0
  end,
  OnShutDown = function(self)
  end
}
MissionHint.Client = {
  OnInit = function(self)
    self:Activate(0)
    self.started = 0
    self.loop = self.Properties.bLoop
    self.soundName = ""
    self:ActivateOutput("Done", false)
    if self.Properties.bPlay == 1 then
      self:Play()
    end
  end,
  OnTimer = function(self)
    if self.soundid then
      if not Sound.IsPlaying(self.soundid) or g_localActor:IsDead() then
        Sound.StopSound(self.soundid)
        self.soundid = nil
        Sound.SetGroupScale(SOUNDSCALE_MISSIONHINT, 1)
      else
        self:SetTimer(0, 1000)
      end
    end
  end,
  OnShutDown = function(self)
    self:StopSound()
  end,
  OnSoundDone = function(self)
    self:ActivateOutput("Done", true)
  end
}
function MissionHint:Play()
  System.Log("Now playing with " .. self.SkipCount .. " skip and " .. self.HintCount .. " hint")
  if self.Properties.bEnabled == 0 or self.skipped == 1 then
    return
  end
  if self.soundid ~= nil and Sound.IsPlaying(self.soundid) then
    Sound.StopSound(self.soundid)
    Sound.SetGroupScale(SOUNDSCALE_MISSIONHINT, 1)
    self.SkipCount = self.SkipCount + 1
  end
  self.soundid = nil
  if self.SkipCount > self.Properties.iAllowedToSkip then
    if sndSkipAcknowledge ~= "" then
      self.skipped = 1
      self.soundName = self.Properties.sndSkipAcknowledge
    end
  elseif self.soundid == nil then
    self:LoadSnd()
  end
  local sndFlags = SOUND_2D
  if self.Properties.bLoop ~= 0 then
    sndFlags = bor(sndFlags, SOUND_LOOP)
  end
  self:SetTimer(0, 1000)
  self.soundid = self:PlaySoundEvent(self.soundName, g_Vectors.v000, g_Vectors.v010, sndFlags, SOUND_SEMANTIC_DIALOG)
  if self.Properties.bScaleDownVolumes == 1 then
    Sound.SetGroupScale(SOUNDSCALE_MISSIONHINT, SOUND_VOLUMESCALEMISSIONHINT)
  end
end
function MissionHint:StopSound()
  if self.Properties.bEnabled == 0 then
    return
  end
  if self.soundid ~= nil then
    Sound.StopSound(self.soundid)
    self.soundid = nil
  end
  self.started = 0
end
function MissionHint:LoadSnd()
  if self.Properties.Hints["sndHint" .. self.HintCount] ~= "" then
    self.soundName = self.Properties.Hints["sndHint" .. self.HintCount]
    self.HintCount = self.HintCount + 1
  end
end
function MissionHint:Event_Stop(sender)
  self:StopSound()
end
function MissionHint:Event_Play(sender)
  self:Play()
end
function MissionHint:Event_Enable(sender)
  self.Properties.bEnabled = true
  self:OnPropertyChange()
end
function MissionHint:Event_Disable(sender)
  self.Properties.bEnabled = false
  self:OnPropertyChange()
end
MissionHint.FlowEvents = {
  Inputs = {
    Play = {
      MissionHint.Event_Play,
      "bool"
    },
    Stop = {
      MissionHint.Event_Stop,
      "bool"
    },
    Enable = {
      MissionHint.Event_Enable,
      "bool"
    },
    Disable = {
      MissionHint.Event_Disable,
      "bool"
    }
  },
  Outputs = {Done = "bool"}
}
