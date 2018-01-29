SoundSpot = {
  type = "Sound",
  Properties = {
    sndSource = "",
    fVolume = 1,
    InnerRadius = 2,
    OuterRadius = 10,
    bLoop = 1,
    bPlay = 0,
    bOnce = 0,
    bEnabled = 1
  },
  bBlockNow = 0,
  Editor = {
    Model = "Editor/Objects/Sound.cgf",
    Icon = "Sound.bmp"
  },
  bEnabled = 1,
  Client = {},
  Server = {}
}
function SoundSpot:OnSpawn()
  if System.IsEditor() then
    Sound.Precache(self.Properties.sndSource, 0)
  end
end
function SoundSpot:OnSoundDone()
  self:ActivateOutput("Done", true)
end
function SoundSpot:OnSave(props)
  props.bBlockNow = self.bBlockNow
  props.bEnabled = self.bEnabled
end
function SoundSpot:OnLoad(props)
  self.bBlockNow = props.bBlockNow
  self.bEnabled = props.bEnabled
  if self.bPlay == 1 and self.Properties.bOnce ~= 1 then
    self:Play()
  end
end
function SoundSpot:OnPropertyChange()
  if self.soundName ~= self.Properties.sndSource or self.Properties.bLoop ~= self.loop or self.Properties.bPlay ~= self.bPlay or self.Properties.bEnable ~= self.bEnable then
    self:OnReset()
  end
  if self.volume ~= self.Properties.fVolume then
    Sound.SetSoundVolume(self.soundid, self.Properties.fVolume)
    self.volume = self.Properties.fVolume
  end
  if self.InnerRadius ~= self.Properties.InnerRadius or self.OuterRadius ~= self.Properties.OuterRadius then
    Sound.SetMinMaxDistance(self.soundid, self.Properties.InnerRadius, self.Properties.OuterRadius)
    self.InnerRadius = self.Properties.InnerRadius
    self.OuterRadius = self.Properties.OuterRadius
  end
end
function SoundSpot:OnReset()
  self.bBlockNow = 0
  self.bEnabled = self.Properties.bEnabled
  self:Stop()
  if self.Properties.bPlay ~= 0 then
    self:Play()
  end
end
SoundSpot.Server = {
  OnInit = function(self)
    self.bBlockNow = 0
    self:NetPresent(0)
  end,
  OnShutDown = function(self)
  end
}
SoundSpot.Client = {
  OnInit = function(self)
    self.bBlockNow = 0
    self.loop = self.Properties.bLoop
    self.soundName = ""
    self.soundid = nil
    self:NetPresent(0)
    if self.Properties.bPlay == 1 then
      self:Play()
    end
  end,
  OnShutDown = function(self)
    self:Stop()
  end,
  OnSoundDone = function(self)
  end
}
function SoundSpot:Play()
  if self.bEnabled == 0 then
    return
  end
  if self.soundid ~= nil then
    self:Stop()
  end
  if self.bBlockNow == 1 then
    return
  end
  local sndFlags = bor(SOUND_DEFAULT_3D, SOUND_RADIUS)
  sndFlags = bor(sndFlags, SOUND_START_PAUSED)
  if self.Properties.bLoop ~= 0 then
    sndFlags = bor(sndFlags, SOUND_LOOP)
  end
  self.loop = self.Properties.bLoop
  self.soundid = self:PlaySoundEventEx(self.Properties.sndSource, sndFlags, self.Properties.fVolume, g_Vectors.v000, self.Properties.InnerRadius, self.Properties.OuterRadius, SOUND_SEMANTIC_SOUNDSPOT)
  self.soundName = self.Properties.sndSource
  self.volume = self.Properties.fVolume
  self.InnerRadius = self.Properties.InnerRadius
  self.OuterRadius = self.Properties.OuterRadius
  if self.soundid ~= nil then
    local bIsEventOrVoice = Sound.IsEvent(self.soundid) or Sound.IsVoice(self.soundid)
    if bIsEventOrVoice then
      System.LogToConsole("<Sound> SoundSpot: (" .. self:GetName() .. ") trys to play " .. self.soundName .. ". Cannot play Event or Voice on a SoundSpot!")
      self:Stop()
    else
      Sound.SetSoundPaused(self.soundid, 0)
    end
  end
  if self.Properties.bOnce == 1 then
    self.bBlockNow = 1
  end
end
function SoundSpot:Stop()
  if self.soundid ~= nil then
    self:StopSound(self.soundid)
    self.soundid = nil
  end
end
function SoundSpot:Event_Play(sender)
  if self.soundid ~= nil then
    self:Stop()
  end
  self:Play()
  if CryAction.IsServer() then
    self.allClients:ClEvent_Play()
  end
end
function SoundSpot:Event_Stop(sender)
  self:Stop()
  if CryAction.IsServer() then
    self.allClients:ClEvent_Stop()
  end
end
function SoundSpot:Event_Enable(sender)
  self.bEnabled = 1
  if CryAction.IsServer() then
    self.allClients:ClEvent_Enable(true)
  end
end
function SoundSpot:Event_Disable(sender)
  self.bEnabled = false
  if CryAction.IsServer() then
    self.allClients:ClEvent_Enable(false)
  end
end
SoundSpot.FlowEvents = {
  Inputs = {
    Play = {
      SoundSpot.Event_Play,
      "bool"
    },
    Stop = {
      SoundSpot.Event_Stop,
      "bool"
    },
    Enable = {
      SoundSpot.Event_Enable,
      "bool"
    },
    Disable = {
      SoundSpot.Event_Disable,
      "bool"
    }
  },
  Outputs = {Done = "bool"}
}
function SoundSpot.Client:ClEvent_Stop()
  if not CryAction.IsServer() then
    self:Event_Stop(nil)
  end
end
function SoundSpot.Client:ClEvent_Enable(bEnable)
  if not CryAction.IsServer() then
    if bEnable then
      self:Event_Enable(nil)
    else
      self:Event_Disable(nil)
    end
  end
end
function SoundSpot.Client:ClEvent_Play()
  if not CryAction.IsServer() then
    self:Event_Play(nil)
  end
end
