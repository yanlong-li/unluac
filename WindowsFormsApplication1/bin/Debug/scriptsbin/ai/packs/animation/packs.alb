ANIMATIONPACK = {
  AVAILABLE = {
    Basic = "Scripts/AI/Packs/Animation/Basic.lua",
    Trooper = "Scripts/AI/Packs/Animation/Trooper.lua",
    TeamLeader = "Scripts/AI/Packs/Animation/TeamLeader.lua",
    Alien = "Scripts/AI/Packs/Animation/Alien.lua",
    Leader = "Scripts/AI/Packs/Animation/Leader.lua"
  }
}
function ANIMATIONPACK:LoadAll()
  for name, filename in pairs(self.AVAILABLE) do
    Script.ReloadScript(filename)
  end
end
