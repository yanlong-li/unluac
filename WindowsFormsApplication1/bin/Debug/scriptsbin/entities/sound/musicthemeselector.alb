MusicThemeSelector = {
  type = "MusicThemeSelector",
  Editor = {Icon = "Music.bmp"},
  Properties = {
    bIndoorOnly = 0,
    bOutdoorOnly = 0,
    sTheme = "",
    sDefaultMood = "",
    sMood = "",
    bEndOnExit = 1,
    bForceChange = 1,
    bKeepMood = 0,
    nDelayInSec = 0
  },
  InsideArea = 0
}
function MusicThemeSelector:OnSave(save)
  save.InsideArea = self.InsideArea
  save.bIndoorOnly = self.Properties.bIndoorOnly
  save.bOutdoorOnly = self.Properties.bIndoorOnly
  save.bEndOnExit = self.Properties.bEndOnExit
  save.bForceChange = self.Properties.bForceChange
  save.bKeepMood = self.Properties.bKeepMood
  save.nDelayInSec = self.Properties.nDelayInSec
end
function MusicThemeSelector:OnLoad(load)
  self.InsideArea = load.InsideArea
  self.Properties.bIndoorOnly = load.bIndoorOnly
  self.Properties.bOutdoorOnly = load.bIndoorOnly
  self.Properties.bEndOnExit = load.bEndOnExit
  self.Properties.bForceChange = load.bForceChange
  self.Properties.bKeepMood = load.bKeepMood
  self.Properties.nDelayInSec = load.nDelayInSec
end
function MusicThemeSelector:OnPropertyChange()
  if self.InsideArea == 1 then
    Sound.SetMusicTheme(self.Properties.sTheme)
  end
end
function MusicThemeSelector:CliSrv_OnInit()
end
function MusicThemeSelector:OnShutDown()
end
function MusicThemeSelector:SetTheme()
  local bActivate = 1
  local Indoor = System.IsPointIndoors(System.GetViewCameraPos())
  if self.Properties.bIndoorOnly == 1 and Indoor == nil then
    bActivate = 0
  elseif self.Properties.bOutdoorOnly == 1 and Indoor ~= nil then
    bActivate = 0
  end
  if bActivate == 1 then
    self.InsideArea = 1
    Sound.SetMusicTheme(self.Properties.sTheme, self.Properties.bForceChange, self.Properties.bKeepMood, self.Properties.nDelayInSec)
    if self.Properties.sDefaultMood ~= "" then
      Sound.SetDefaultMusicMood(self.Properties.sDefaultMood)
    end
    local bNotKeepMood = self.Properties.bKeepMood == 0
    if self.Properties.sMood ~= "" and bNotKeepMood then
      Sound.SetMusicMood(self.Properties.sMood, 1, self.Properties.bForceChange)
    end
  elseif bActivate == 0 and self.InsideArea == 1 then
    self.InsideArea = 0
    Sound.EndMusicTheme(2, 10)
  end
end
function MusicThemeSelector:Client_OnEnterArea(player, areaId)
  self:SetTheme()
end
function MusicThemeSelector:Event_SetTheme()
  self:SetTheme()
end
function MusicThemeSelector:Event_EndTheme()
  Sound.EndMusicTheme(2, 10)
end
function MusicThemeSelector:Event_Reset()
  Sound.SetMusicTheme("")
  if self.Properties.sDefaultMood ~= "" then
    Sound.SetDefaultMusicMood("")
  end
end
function MusicThemeSelector:Client_OnLeaveArea(player, areaId)
  if g_localActorId ~= player.id then
    return
  end
  if self.InsideArea == 1 then
    self.InsideArea = 0
    if self.Properties.bEndOnExit == 1 then
      Sound.EndMusicTheme(2, 10)
    end
  end
end
function MusicThemeSelector:Client_OnProceedFadeArea(player, areaId, fFade)
end
MusicThemeSelector.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  Inactive = {},
  Active = {}
}
MusicThemeSelector.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = MusicThemeSelector.Client_OnEnterArea,
  OnLeaveArea = MusicThemeSelector.Client_OnLeaveArea,
  OnProceedFadeArea = MusicThemeSelector.Client_OnProceedFadeArea
}
MusicThemeSelector.FlowEvents = {
  Inputs = {
    Reset = {
      MusicThemeSelector.Event_Reset,
      "bool"
    },
    SetTheme = {
      MusicThemeSelector.Event_SetTheme,
      "bool"
    },
    EndTheme = {
      MusicThemeSelector.Event_EndTheme,
      "bool"
    }
  },
  Outputs = {Reset = "bool", SetTheme = "bool"}
}
