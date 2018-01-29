NOCOVER = {
  Available_pipes = {
    "comeout_standfire",
    "comeout_crouchfire"
  },
  count = 2,
  position = 1
}
function NOCOVER:GetPipe()
  if NOCOVER.position > NOCOVER.count then
    NOCOVER.position = 1
  end
  NOCOVER.position = NOCOVER.position + 1
  return NOCOVER.Available_pipes[NOCOVER.position - 1]
end
function NOCOVER:SelectAttack(entity)
  local shootspot = AI.GetAnchor(entity.id, AIAnchorTable.COMBAT_SHOOTSPOTSTAND, 10)
  if shootspot then
    entity:SelectPipe(0, "shoot_from_spot", shootspot)
    entity:InsertSubpipe(0, "setup_combat")
    return true
  end
  shootspot = AI.GetAnchor(entity.id, AIAnchorTable.COMBAT_SHOOTSPOTCROUCH, 10)
  if shootspot then
    entity:SelectPipe(0, "shoot_from_spot", shootspot)
    entity:InsertSubpipe(0, "setup_crouch")
    return true
  end
  return false
end
