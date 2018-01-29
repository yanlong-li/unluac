Ladder = {
  Properties = {
    soclasses_SmartObjectClass = "Ladder",
    fileModel = "objects/Prototype/Measurement/ladder.cgf",
    fUseDistance = 2,
    sDirectionAxis = "x"
  },
  Editor = {Icon = "Ladder.bmp"},
  PhysParams = {mass = 0, density = 0},
  bottom_pos = {
    x = 0,
    y = 0,
    z = 0
  },
  top_pos = {
    x = 0,
    y = 0,
    z = 0
  },
  Server = {},
  Client = {}
}
function Ladder:OnLoad(table)
  self.bottom_pos = table.bottom_pos
  self.top_pos = table.top_pos
  self.direction = table.direction
end
function Ladder:OnSave(table)
  table.bottom_pos = self.bottom_pos
  table.top_pos = self.top_pos
  table.direction = self.direction
end
function Ladder:OnPropertyChange()
  self:Reset()
end
function Ladder:OnReset()
  self:Reset()
end
function Ladder:OnSpawn()
  self:Reset(1)
end
function Ladder:OnDestroy()
end
function Ladder.Server:OnUpdate(frameTime)
end
function Ladder:DoPhysicalize()
  if self.currModel ~= self.Properties.fileModel then
    self:LoadObject(0, self.Properties.fileModel)
    self:Physicalize(0, PE_RIGID, self.PhysParams)
    local bbmin, bbmax = self:GetLocalBBox()
    bbmax.z = bbmax.z + 1.5
    self:SetLocalBBox(bbmin, bbmax)
  end
  self.currModel = self.Properties.fileModel
end
function Ladder:Reset(onSpawn)
  self:Activate(1)
  self:DoPhysicalize()
  self:AwakePhysics(1)
  local axis = self.Properties.sDirectionAxis
  if axis == "X" or axis == "x" then
    self.direction = self:GetDirectionVector(0)
  elseif axis == "Y" or axis == "y" then
    self.direction = self:GetDirectionVector(1)
  else
    self.direction = self:GetDirectionVector(2)
  end
  local bottomTag = System.GetEntityByName(self:GetName() .. "_bottom")
  local topTag = System.GetEntityByName(self:GetName() .. "_top")
  if bottomTag and topTag then
    bottomTag:GetWorldPos(self.bottom_pos)
    topTag:GetWorldPos(self.top_pos)
  else
    local zAxis = self:GetDirectionVector(2)
    local bbmin, bbmax = self:GetLocalBBox()
    self:GetWorldPos(self.bottom_pos)
    self.bottom_pos.x = self.bottom_pos.x + zAxis.x * (bbmin.z + 1)
    self.bottom_pos.y = self.bottom_pos.y + zAxis.y * (bbmin.z + 1)
    self.bottom_pos.z = self.bottom_pos.z + zAxis.z * (bbmin.z + 1)
    self:GetWorldPos(self.top_pos)
    self.top_pos.x = self.top_pos.x + zAxis.x * (bbmax.z - 0.25)
    self.top_pos.y = self.top_pos.y + zAxis.y * (bbmax.z - 0.25)
    self.top_pos.z = self.top_pos.z + zAxis.z * (bbmax.z - 0.25)
    CopyVector(g_Vectors.temp_v1, self.direction)
    FastScaleVector(g_Vectors.temp_v1, g_Vectors.temp_v1, -0.5)
    FastSumVectors(self.top_pos, self.top_pos, g_Vectors.temp_v1)
    FastSumVectors(self.bottom_pos, self.bottom_pos, g_Vectors.temp_v1)
  end
end
function ProjectPointToLine(result, point, lineStart, lineEnd)
  local line = g_Vectors.temp_v4
  SubVectors(line, lineEnd, lineStart)
  NormalizeVector(line)
  result.x = point.x - lineStart.x
  result.y = point.y - lineStart.y
  result.z = point.z - lineStart.z
  local dot1 = dotproduct3d(result, line)
  local dot2 = dotproduct3d(line, line)
  local dot = dot1 / dot2
  result.x = lineStart.x + line.x * dot
  result.y = lineStart.y + line.y * dot
  result.z = lineStart.z + line.z * dot
end
function Ladder:IsUsable(user)
  if not user then
    return 0
  end
  local delta = g_Vectors.temp_v1
  local userPos = g_Vectors.temp_v2
  user:GetWorldPos(userPos)
  ProjectPointToLine(delta, userPos, self.bottom_pos, self.top_pos)
  SubVectors(delta, delta, userPos)
  local dist = LengthSqVector(delta)
  local useDistance = self.Properties.fUseDistance
  if dist < useDistance * useDistance then
    do return 1 end
    local distVec = g_Vectors.temp_v3
    SubVectors(distVec, self.bottom_pos, userPos)
    local distBottom = LengthSqVector(distVec)
    SubVectors(distVec, self.top_pos, userPos)
    local distTop = LengthSqVector(distVec)
    if distBottom > distTop then
      return 1
    else
      NormalizeVector(delta)
      local dot = dotproduct3d(delta, self.direction)
      if dot < 0.25 then
        return 0
      else
        return 1
      end
    end
  else
    return 0
  end
end
function Ladder:GetUsableMessage(idx)
  return "@use_ladder"
end
function Ladder:OnUsed(user)
end
