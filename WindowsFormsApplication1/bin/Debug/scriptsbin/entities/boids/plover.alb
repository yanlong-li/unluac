Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Plover = {
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
  Editor = {Icon = "Bird.bmp"}
}
MakeDerivedEntity(Plover, Chickens)
function Plover:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
