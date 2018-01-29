Script.ReloadScript("Scripts/Entities/Vehicles/VehicleBase.lua")
Script.ReloadScript("Scripts/Entities/Vehicles/VehicleBaseAI.lua")
Log("Executing Vehiclepool..")
VEHICLE_SCRIPT_TIMER = 100
AISOUND_TIMER = VEHICLE_SCRIPT_TIMER + 2
AISOUND_TIMEOUT = 250
for i, vehicle in pairs(VehicleSystem.VehicleImpls) do
  local gVehicle = {
    Properties = {
      bDisableEngine = 0,
      Paint = "",
      bFrozen = 0,
      FrozenModel = "",
      Modification = "",
      soclasses_SmartObjectClass = "",
      bAutoGenAIHidePts = 0,
      teamName = ""
    },
    Editor = {
      Icon = "Vehicle.bmp",
      IconOnTop = 1
    },
    Client = {},
    Server = {}
  }
  local scriptName = Vehicle.GetOptionalScript(vehicle)
  if scriptName then
    Script.ReloadScript(scriptName)
    if _G[vehicle] then
      mergef(gVehicle, _G[vehicle], 1)
    end
  end
  function gVehicle:OnSpawn()
    mergef(self, VehicleBase, 1)
    self:SpawnVehicleBase()
  end
  function gVehicle.Server:OnShutDown()
    if g_gameRules then
    end
  end
  function gVehicle:OnReset()
    self:ResetVehicleBase()
    if CryAction.IsServer() and g_gameRules then
      local teamId = g_gameRules.game:GetTeamId(self.Properties.teamName)
      if teamId and teamId ~= 0 then
        g_gameRules.game:SetTeam(teamId, self.id)
      else
        g_gameRules.game:SetTeam(0, self.id)
      end
    end
  end
  function gVehicle:OnFrost(shooterId, weaponId, frost)
    local f = self.vehicle:GetFrozenAmount() + frost
    self.vehicle:SetFrozenAmount(f)
  end
  function gVehicle:OnUnlocked(playerId)
    if g_gameRules and g_gameRules.OnVehicleUnlocked then
      g_gameRules.OnVehicleUnlocked(g_gameRules, self.id, playerId)
    end
  end
  function gVehicle.Client:OnTimer(timerId, mSec)
    if timerId == AISOUND_TIMER and self.AISoundRadius and self:HasDriver() then
      self:SetTimer(AISOUND_TIMER, mSec)
      AI.SoundEvent(self:GetWorldPos(self.State.pos), self.AISoundRadius, AISE_MOVEMENT_LOUD, self.id)
    end
  end
  function gVehicle.Server:OnEnterArea(entity, areaId)
    if self.OnEnterArea then
      self.OnEnterArea(self, entity, areaId)
    end
  end
  function gVehicle.Server:OnLeaveArea(entity, areaId)
    if self.OnLeaveArea then
      self.OnLeaveArea(self, entity, areaId)
    end
  end
  function gVehicle:Event_Enable()
    self:Hide(0)
    BroadcastEvent(self, "Enable")
  end
  function gVehicle:Event_Disable()
    self:Hide(1)
    BroadcastEvent(self, "Disable")
  end
  function gVehicle:Event_EnableEngine()
    self.vehicle:DisableEngine(0)
    BroadcastEvent(self, "EnableEngine")
  end
  function gVehicle:Event_DisableEngine()
    self.vehicle:DisableEngine(1)
    BroadcastEvent(self, "DisableEngine")
  end
  function gVehicle:Event_EnableMovement()
    self.vehicle:EnableMovement(1)
    BroadcastEvent(self, "EnableMovement")
  end
  function gVehicle:Event_DisableMovement()
    self.vehicle:EnableMovement(0)
    BroadcastEvent(self, "DisableMovement")
  end
  MakeRespawnable(gVehicle)
  gVehicle.Properties.Respawn.bAbandon = 1
  gVehicle.Properties.Respawn.nAbandonTimer = 90
  local FlowEvents = {
    Inputs = {
      Enable = {
        gVehicle.Event_Enable,
        "bool"
      },
      Disable = {
        gVehicle.Event_Disable,
        "bool"
      },
      EnableEngine = {
        gVehicle.Event_EnableEngine,
        "bool"
      },
      DisableEngine = {
        gVehicle.Event_DisableEngine,
        "bool"
      },
      EnableMovement = {
        gVehicle.Event_EnableMovement,
        "bool"
      },
      DisableMovement = {
        gVehicle.Event_DisableMovement,
        "bool"
      }
    },
    Outputs = {
      Enable = "bool",
      Disable = "bool",
      EnableEngine = "bool",
      DisableEngine = "bool",
      EnableMovement = "bool",
      DisableMovement = "bool"
    }
  }
  if not gVehicle.FlowEvents then
    gVehicle.FlowEvents = FlowEvents
  else
    mergef(gVehicle.FlowEvents, FlowEvents, 1)
  end
  _G[vehicle] = gVehicle
  if _G[vehicle].AIProperties then
    CreateVehicleAI(_G[vehicle])
  end
end
