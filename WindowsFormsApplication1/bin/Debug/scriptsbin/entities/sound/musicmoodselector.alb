MusicMoodSelector = {
  type = "MusicMoodSelector",
  Editor = {Icon = "Music.bmp"},
  Properties = {sMood = "", bCrossfade = 1},
  InsideArea = 0,
  InsideAreaRefCount = 0
}
function MusicMoodSelector:OnSave(props)
  props.bCrossfade = self.Properties.bCrossfade
end
function MusicMoodSelector:OnLoad(props)
  self.Properties.bCrossfade = props.bCrossfade
end
function MusicMoodSelector:OnPropertyChange()
end
function MusicMoodSelector:CliSrv_OnInit()
end
function MusicMoodSelector:OnShutDown()
end
function MusicMoodSelector:Client_OnEnterArea(player, areaId)
  self.InsideArea = 1
  MusicMoodSelector.InsideAreaRefCount = MusicMoodSelector.InsideAreaRefCount + 1
  Sound.SetDefaultMusicMood(self.Properties.sMood)
  local bPlayFromStart = self.Properties.bCrossfade == 0
  Sound.SetMusicMood(self.Properties.sMood, bPlayFromStart)
end
function MusicMoodSelector:Client_OnLeaveArea(player, areaId)
  if g_localActorId ~= player.id then
    return
  end
  self.InsideArea = 0
  MusicMoodSelector.InsideAreaRefCount = MusicMoodSelector.InsideAreaRefCount - 1
  if 0 >= MusicMoodSelector.InsideAreaRefCount then
    MusicMoodSelector.InsideAreaRefCount = 0
    Sound.SetDefaultMusicMood("")
  end
end
function MusicMoodSelector:Event_SetMood()
  local bPlayFromStart = self.Properties.bCrossfade == 0
  Sound.SetMusicMood(self.Properties.sMood, bPlayFromStart)
end
function MusicMoodSelector:Event_SetDefaultMood()
  Sound.SetDefaultMusicMood(self.Properties.sMood)
end
function MusicMoodSelector:Event_ResetDefaultMood()
  Sound.SetDefaultMusicMood("")
end
function MusicMoodSelector:Client_OnProceedFadeArea(player, areaId, fadeCoeff)
  if g_localActorId ~= player.id then
    return
  end
end
MusicMoodSelector.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  Inactive = {},
  Active = {}
}
MusicMoodSelector.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = MusicMoodSelector.Client_OnEnterArea,
  OnLeaveArea = MusicMoodSelector.Client_OnLeaveArea,
  OnProceedFadeArea = MusicMoodSelector.Client_OnProceedFadeArea
}
MusicMoodSelector.FlowEvents = {
  Inputs = {
    ResetDefaultMood = {
      MusicMoodSelector.Event_ResetDefaultMood,
      "bool"
    },
    SetDefaultMood = {
      MusicMoodSelector.Event_SetDefaultMood,
      "bool"
    },
    SetMood = {
      MusicMoodSelector.Event_SetMood,
      "bool"
    }
  },
  Outputs = {
    ResetDefaultMood = "bool",
    SetDefaultMood = "bool",
    SetMood = "bool"
  }
}
