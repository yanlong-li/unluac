GravityGlobals = {
  gravityNodes = {}
}
function GravityGlobals:RemoveGravityArea(node)
  if not node then
    return
  end
  local asize = table.getn(self.gravityNodes)
  for i = 1, asize do
    if self.gravityNodes[i] and self.gravityNodes[i].id == node.id then
      if node.GetName then
        System.Log(node:GetName() .. "removed from the gravity array!")
      end
      self.gravityNodes[i] = nil
      return
    end
  end
end
function GravityGlobals:AddGravityArea(node)
  if not node then
    return
  end
  local asize = table.getn(self.gravityNodes)
  for i = 1, asize do
    if self.gravityNodes[i] and self.gravityNodes[i].id == node.id then
      return
    end
  end
  for i = 1, asize do
    if self.gravityNodes[i] == nil then
      self.gravityNodes[i] = node
      return
    end
  end
  self.gravityNodes[asize + 1] = node
end
function GravityGlobals:GetPosGravity(pos)
  local asize = table.getn(self.gravityNodes)
  local node, candidateGravity
  for i = 1, asize do
    node = self.gravityNodes[i]
    if node and node.GetPos and node.GetPosGravity then
      local gravity = node:GetPosGravity(pos)
      if gravity and (not candidateGravity or candidateGravity < gravity) then
        candidateGravity = gravity
      end
    elseif node then
      self.gravityNodes[i] = nil
    end
  end
  return candidateGravity
end
function EntityUpdateGravity(ent)
  if GravityGlobals then
    local UpImpulse = 0
    local pos = g_Vectors.temp_v1
    CopyVector(pos, ent:GetPos())
    local newGravity
    local tempg = GravityGlobals:GetPosGravity(pos)
    if tempg then
      if ent.SaveLastGravity == nil then
        local physicalStats = ent:GetPhysicalStats()
        ent:AwakePhysics(1)
        ent.SaveLastGravity = physicalStats.gravity
        if math.abs(tempg) < 9.81 then
          UpImpulse = physicalStats.mass
          if UpImpulse then
            UpImpulse = UpImpulse * 1
          else
            UpImpulse = 0
          end
        end
      end
      if ent.TempPhysicsParams == nil then
        ent.TempPhysicsParams = {
          gravityz = -9.81,
          freefall_gravityz = -9.81,
          lying_gravityz = -9.81,
          zeroG = 0,
          air_control = 0
        }
      end
      newGravity = tempg
    elseif ent.SaveLastGravity then
      newGravity = ent.SaveLastGravity
      ent.SaveLastGravity = nil
    end
    if newGravity then
      if ent.SaveLastGravity then
        newGravity = newGravity * (ent.SaveLastGravity / -9.81)
      end
      local pparams = ent.TempPhysicsParams
      pparams.gravityz = newGravity
      pparams.freefall_gravityz = newGravity
      pparams.lying_gravityz = newGravity
      if ent.type == "Player" and ent.isDedbody ~= 1 then
        pparams.gravity = -newGravity
        ent:SetPhysicParams(PHYSICPARAM_PLAYERDYN, pparams)
      elseif ent.IsProjectile then
        if not pparams.gravity then
          pparams.gravity = {
            x = 0,
            y = 0,
            z = newGravity
          }
        else
          pparams.gravity.z = newGravity
        end
        ent:SetPhysicParams(PHYSICPARAM_PARTICLE, pparams)
      else
        ent:SetPhysicParams(PHYSICPARAM_SIMULATION, pparams)
      end
    end
    if UpImpulse > 0 then
      ent:AddImpulse(-1, pos, g_Vectors.v001, UpImpulse)
    end
  end
end
