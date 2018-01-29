IdleManager = {
  ModelTable = {},
  TagTable = {},
  CountTable = {},
  AnimationTable = {
    {
      Name = "_kickground01",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_kickground02",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_idle_leanright",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_idle_leanleft",
      duration = 5,
      Tag = 0
    },
    {
      Name = "_itchbutt",
      duration = 2,
      Tag = 0
    },
    {
      Name = "_checkwatch1",
      duration = 2,
      Tag = 0
    },
    {
      Name = "_checkwatch2",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_chinrub",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_foottap1",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_foottap2",
      duration = 5,
      Tag = 0
    },
    {
      Name = "_headscratch1",
      duration = 2,
      Tag = 0
    },
    {
      Name = "_headscratch2",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_humming",
      duration = 5,
      Tag = 0
    },
    {
      Name = "_insectswat1",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_insectswat2",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_kneecheck",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_laces",
      duration = 6,
      Tag = 0
    },
    {
      Name = "_legbob",
      duration = 2,
      Tag = 0
    },
    {
      Name = "_massageneck",
      duration = 3,
      Tag = 0
    },
    {
      Name = "_shouldershrug",
      duration = 4,
      Tag = 0
    },
    {
      Name = "_yawn",
      duration = 4,
      Tag = 0
    }
  },
  Animation = ""
}
function IdleManager:GetIdleAnimation(entity)
  local model = entity.Properties.fileModel
  if model then
    if self.ModelTable[model] == nil then
      local idleCounter = 0
      self.ModelTable[model] = {}
      local formatted_name = "combat_idle_rifle_01"
      self.ModelTable[model][1] = {Name = formatted_name, Tag = 0}
      self.TagTable[model] = 1
      self.CountTable[model] = 0
    end
    return self:GetIdle(model)
  end
end
function IdleManager:GetIdle(model_name)
  local tableA = self.ModelTable[model_name]
  local count = table.getn(tableA)
  if count < 1 then
    AI.Warning("[AI] No idle ainmation for " .. model_name .. " add animation or change the job ")
    return nil
  end
  local XRandom = random(1, count)
  local XVal = 0
  self.CountTable[model_name] = self.CountTable[model_name] + 1
  if count < self.CountTable[model_name] then
    self.TagTable[model_name] = self.TagTable[model_name] + 1
    self.CountTable[model_name] = 1
  end
  local TableTag = self.TagTable[model_name]
  if random(1, 2) == 1 then
    repeat
      while tableA[XRandom].Tag == TableTag do
        XRandom = XRandom + 1
        XRandom = 1
      end
    until count < XRandom
  else
    while tableA[XRandom].Tag == TableTag do
      XRandom = XRandom - 1
      if XRandom < 1 then
        XRandom = count
      end
    end
  end
  tableA[XRandom].Tag = TableTag
  return tableA[XRandom]
end
