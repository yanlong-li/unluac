Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Crabs = {
  Properties = {
    Sounds = {
      soundS0_cluck = "",
      soundS1_scared = "",
      soundS2_die = "",
      soundS3_pickup = "",
      soundS4_throw = ""
    }
  },
  Animations = {
    "walk_loop",
    "idle01",
    "idle01",
    "idle01",
    "idle01",
    "idle01"
  },
  Editor = {Icon = "Bug.bmp"}
}
MakeDerivedEntity(Crabs, Chickens)
function Crabs:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
