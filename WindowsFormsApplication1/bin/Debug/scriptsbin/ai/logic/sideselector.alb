AISideSelector = {
  units = {}
}
function AISideSelector:Reset()
  self.units = {}
end
function AISideSelector:Register(entity)
  local NumEntries = 0
  NumEntries = count(self.units)
  self.units[NumEntries] = entity
  AI.LogEvent(">>>>sideSelector registered " .. entity:GetName() .. " total " .. NumEntries)
end
function AISideSelector:SelectSide(entity)
  AI.LogEvent(">>>>sideSelector selecting side " .. entity:GetName())
  local NumEntries
  NumEntries = count(self.units)
  if NumEntries == 0 then
    AI.LogEvent(">>>>sideSelector empty")
    return nil
  end
  local tmp = g_Vectors.temp_v1
  local targetName = AI.GetAttentionTargetOf(entity.id)
  local useBeacon = 0
  if targetName then
    local target = System.GetEntityByName(targetName)
    if target then
      CopyVector(tmp, target:GetWorldPos())
      AI.LogEvent(">>>>sideSelector targetNAme " .. targetName)
    else
      useBeacon = 1
    end
  else
    useBeacon = 1
  end
  if useBeacon == 1 then
    if AI.GetBeaconPosition(entity.id, tmp) == nil then
      AI.LogEvent(">>>>sideSelector no AttTarget/beacon")
      return nil
    end
    AI.LogEvent(">>>>sideSelector using beacon")
  end
  local leftCounter = 0
  local rightCounter = 0
  local direction = g_Vectors.temp_v2
  local selfPos = g_Vectors.temp_v3
  CopyVector(selfPos, entity:GetWorldPos())
  FastDifferenceVectors(tmp, tmp, selfPos)
  direction.x = -tmp.y
  direction.y = tmp.x
  AI.LogEvent(">>>>sideSelector units " .. NumEntries)
  for name, unit in pairs(self.units) do
    if unit.id ~= entity.id then
      local unitDir = g_Vectors.temp_v4
      FastDifferenceVectors(unitDir, unit:GetWorldPos(), selfPos)
      local cross = unitDir.x * direction.x + unitDir.y * direction.y
      if cross < 0 then
        rightCounter = rightCounter + 1
      else
        leftCounter = leftCounter + 1
      end
    end
  end
  if rightCounter < leftCounter then
    return 2
  else
    return 1
  end
end
