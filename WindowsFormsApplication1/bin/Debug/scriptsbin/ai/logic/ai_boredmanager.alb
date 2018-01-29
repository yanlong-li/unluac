AI_BoredManager = {
  ASTable = {}
}
function AI_BoredManager:FindSomethingToDO(entity, range)
  local NumEntries
  NumEntries = count(self.ASTable)
  if NumEntries == 0 then
    return
  end
  self.SelectedSignal = nil
  local maxpriority = -1
  for name, table in pairs(self.ASTable) do
    local process = 1
    if table.SPECIAL_AI_ONLY and entity.Properties.special and entity.Properties.special == 0 then
      process = 0
    end
    if process == 1 then
      local foundObject = AI:FindObjectOfType(entity.id, range, table.anchorType)
      if foundObject and maxpriority < table.priority then
        maxpriority = table.priority
        self.SelectedSignal = table
        return self.SelectedSignal
      end
    end
  end
  return nil
end
