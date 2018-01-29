ElevatorSwitch = {
  Properties = {
    soclasses_SmartObjectClass = "ElevatorSwitch",
    objModel = "Objects/library/furnishings/doors/toiletstall_door_local.cgf",
    nFloor = 0,
    fDelay = 3,
    szUseMessage = "@pick_object",
    Sounds = {soundSoundOnPress = ""}
  },
  Server = {},
  Client = {}
}
function ElevatorSwitch:OnPreFreeze(freeze, vapor)
  if freeze then
    return false
  end
end
function ElevatorSwitch:OnPropertyChange()
  self:Reset()
end
function ElevatorSwitch:OnReset()
  self:Reset()
end
function ElevatorSwitch:OnSpawn()
  CryAction.CreateGameObjectForEntity(self.id)
  CryAction.BindGameObjectToNetwork(self.id)
  CryAction.ForceGameObjectUpdate(self.id, true)
  self.isServer = CryAction.IsServer()
  self.isClient = CryAction.IsClient()
  self:Reset(1)
end
function ElevatorSwitch:OnDestroy()
end
function ElevatorSwitch:DoPhysicalize()
  if self.currModel ~= self.Properties.objModel then
    self:LoadObject(0, self.Properties.objModel)
    self:Physicalize(0, PE_RIGID, {mass = 0})
  end
  self.currModel = self.Properties.objModel
end
function ElevatorSwitch:Reset(onSpawn)
  self:Activate(0)
  self:DoPhysicalize()
end
function ElevatorSwitch:IsUsable(user)
  if not user then
    return 0
  end
  return 1
end
function ElevatorSwitch:OnUsed(user)
  self.server:SvRequestUse(user.id)
end
function ElevatorSwitch:GetUsableMessage()
  return self.Properties.szUseMessage
end
function ElevatorSwitch.Server:SvRequestUse(userId)
  if self.Properties.fDelay > 0 then
    self:SetTimer(0, 1000 * self.Properties.fDelay)
  else
    self:Used()
  end
end
function ElevatorSwitch.Server:OnTimer(timerId, msec)
  self:Used()
end
function ElevatorSwitch:Used()
  local i = 0
  local link = self:GetLinkTarget("up", i)
  while link do
    link:Up(self.Properties.nFloor)
    i = i + 1
    link = self:GetLinkTarget("up", i)
  end
  i = 0
  link = self:GetLinkTarget("down", i)
  while link do
    link:Down(self.Properties.nFloor)
    i = i + 1
    link = self:GetLinkTarget("down", i)
  end
  self.allClients:ClUsed()
end
function ElevatorSwitch.Client:ClUsed()
  local sound = self.Properties.Sounds.soundSoundOnPress
  if sound and sound ~= "" then
    self:PlaySoundEvent(self.Properties.Sounds.soundSoundOnPress, g_Vectors.v000, g_Vectors.v010, SOUND_DEFAULT_3D, SOUND_SEMANTIC_MECHANIC_ENTITY)
  end
end
