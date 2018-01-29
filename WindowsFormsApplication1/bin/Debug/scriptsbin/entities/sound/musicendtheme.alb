MusicEndTheme = {
  type = "MusicEndTheme",
  Editor = {Icon = "Music.bmp"},
  Properties = {
    bIndoorOnly = 0,
    bOutdoorOnly = 0,
    bStopAtOnce = 0,
    bFadeOut = 1,
    bPlayEnd = 0,
    bPlayEndAtFadePoint = 0,
    nEndLimitInSec = 10,
    bEndEverything = 0
  },
  InsideArea = 0
}
function MusicEndTheme:OnSave(stm)
  stm.InsideArea = self.InsideArea
end
function MusicEndTheme:OnLoad(stm)
  self.InsideArea = stm.InsideArea
end
function MusicEndTheme:CliSrv_OnInit()
end
function MusicEndTheme:OnShutDown()
end
function MusicEndTheme:Client_OnEnterArea(player, areaId)
  if g_localActorId ~= player.id then
    return
  end
  local bActivate = 1
  local Indoor = System.IsPointIndoors(System.GetViewCameraPos())
  if self.Properties.bIndoorOnly == 1 and Indoor == nil then
    bActivate = 0
  elseif self.Properties.bOutdoorOnly == 1 and Indoor ~= nil then
    bActivate = 0
  end
  if bActivate == 1 then
    self.InsideArea = 1
    if self.Properties.bStopAtOnce == 1 then
      Sound.EndMusicTheme(0, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
    elseif self.Properties.bFadeOut == 1 then
      Sound.EndMusicTheme(1, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
    elseif self.Properties.bPlayEnd == 1 then
      Sound.EndMusicTheme(2, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
    elseif self.Properties.bPlayEndAtFadePoint == 1 then
      Sound.EndMusicTheme(3, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
    end
  elseif bActivate == 0 and self.InsideArea == 1 then
    self.InsideArea = 0
  end
end
function MusicEndTheme:Event_Stop()
  if self.Properties.bStopAtOnce == 1 then
    Sound.EndMusicTheme(0, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
  elseif self.Properties.bFadeOut == 1 then
    Sound.EndMusicTheme(1, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
  elseif self.Properties.bPlayEnd == 1 then
    Sound.EndMusicTheme(2, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
  elseif self.Properties.bPlayEndAtFadePoint == 1 then
    Sound.EndMusicTheme(3, self.Properties.nEndLimitInSec, self.Properties.bEndEverything)
  end
end
MusicEndTheme.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  Inactive = {},
  Active = {}
}
MusicEndTheme.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = MusicEndTheme.Client_OnEnterArea,
  OnLeaveArea = MusicEndTheme.Client_OnLeaveArea,
  OnProceedFadeArea = MusicEndTheme.Client_OnProceedFadeArea
}
MusicEndTheme.FlowEvents = {
  Inputs = {
    Stop = {
      MusicEndTheme.Event_Stop,
      "bool"
    }
  },
  Outputs = {}
}
