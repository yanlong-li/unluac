Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Turtles = {
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
MakeDerivedEntity(Turtles, Chickens)
function Turtles:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Turtles:GetFlockType()
  return Boids.FLOCK_TURTLES
end
