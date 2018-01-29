SPRandomizer = {
  RandomPacks = {
    HumanPacks = {
      "voiceA",
      "voiceB",
      "voiceC",
      "voiceD"
    },
    CoreWorkers = {
      "coreworkerA",
      "coreworkerD"
    }
  },
  GroupMap = {}
}
function SPRandomizer:GetHumanPack(groupid, soundpack)
  local pack = SPRandomizer:CanRandomize(soundpack, SPRandomizer.RandomPacks)
  if pack == nil then
    return soundpack
  end
  local packcount = count(pack)
  local group_entry = "GROUP_" .. groupid
  if SPRandomizer.GroupMap[group_entry] then
    SPRandomizer.GroupMap[group_entry] = SPRandomizer.GroupMap[group_entry] + 1
    if packcount < SPRandomizer.GroupMap[group_entry] then
      SPRandomizer.GroupMap[group_entry] = 1
    end
    return pack[SPRandomizer.GroupMap[group_entry]]
  else
    local randn = random(1, packcount)
    SPRandomizer.GroupMap[group_entry] = randn
    return pack[randn]
  end
end
function SPRandomizer:CanRandomize(soundpack, packs)
  for i, pack in pairs(packs) do
    for j, cell in pairs(pack) do
      if string.lower(soundpack) == string.lower(cell) then
        return pack
      end
    end
  end
  return nil
end
