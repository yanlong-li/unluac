Chickens = {
  type = "Boids",
  MapVisMask = 0,
  ENTITY_DETAIL_ID = 1,
  Properties = {
    Flocking = {
      bEnableFlocking = 0,
      FieldOfViewAngle = 250,
      FactorAlign = 0.01,
      FactorCohesion = 0.01,
      FactorSeparation = 1,
      AttractDistMin = 5,
      AttractDistMax = 20
    },
    Movement = {
      HeightMin = 0,
      HeightMax = 1,
      SpeedMin = 1,
      SpeedMax = 5,
      FactorOrigin = 0.1,
      FactorHeight = 1,
      FactorAvoidLand = 10,
      MaxAnimSpeed = 4
    },
    Boid = {
      nCount = 10,
      object_Model = "objects/characters/animals/Birds/Chicken/chicken.chr",
      Size = 1,
      SizeRandom = 0,
      gravity_at_death = -9.81,
      Mass = 10
    },
    Options = {
      bFollowPlayer = 0,
      bAvoidWater = 1,
      bNoLanding = 0,
      bObstacleAvoidance = 1,
      VisibilityDist = 30,
      bActivate = 1,
      Radius = 10
    },
    Sounds = {
      soundS0_cluck = "Sounds/x2ambience:world2_animal:chicken_cluck",
      soundS1_scared = "Sounds/x2ambience:world2_animal:chicken_run",
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
  Editor = {Icon = "Bird.bmp"},
  params = {
    x = 0,
    y = 0,
    z = 0
  }
}
function Chickens:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Chickens:OnInit()
  self.flock = 0
  self.currpos = {
    x = 0,
    y = 0,
    z = 0
  }
  if self.Properties.Options.bActivate == 1 then
    self:CreateFlock()
  end
end
function Chickens:OnShutDown()
end
function Chickens:GetFlockType()
  return Boids.FLOCK_CHICKENS
end
function Chickens:GetSounds()
  local Sounds = self.Properties.Sounds
  local r = {}
  if Sounds.soundS0_cluck ~= "" then
    table.insert(r, Sounds.soundS0_cluck)
  end
  if Sounds.soundS1_scared ~= "" then
    table.insert(r, Sounds.soundS1_scared)
  end
  if Sounds.soundS2_die ~= "" then
    table.insert(r, Sounds.soundS2_die)
  end
  if Sounds.soundS3_pickup ~= "" then
    table.insert(r, Sounds.soundS3_pickup)
  end
  if Sounds.soundS4_throw ~= "" then
    table.insert(r, Sounds.soundS4_throw)
  end
  return r
end
function Chickens:CreateFlock()
  local Flocking = self.Properties.Flocking
  local Movement = self.Properties.Movement
  local Boid = self.Properties.Boid
  local Options = self.Properties.Options
  local params = self.params
  params.count = Boid.nCount
  params.model = Boid.object_Model
  params.boid_size = Boid.Size
  params.boid_size_random = Boid.SizeRandom
  params.min_height = Movement.HeightMin
  params.max_height = Movement.HeightMax
  params.min_attract_distance = Flocking.AttractDistMin
  params.max_attract_distance = Flocking.AttractDistMax
  params.min_speed = Movement.SpeedMin
  params.max_speed = Movement.SpeedMax
  if Flocking.bEnableFlocking == 1 then
    params.factor_align = Flocking.FactorAlign
  else
    params.factor_align = 0
  end
  params.factor_cohesion = Flocking.FactorCohesion
  params.factor_separation = Flocking.FactorSeparation
  params.factor_origin = Movement.FactorOrigin
  params.factor_keep_height = Movement.FactorHeight
  params.factor_avoid_land = Movement.FactorAvoidLand
  params.spawn_radius = Options.Radius
  params.gravity_at_death = Boid.gravity_at_death
  params.boid_mass = Boid.Mass
  params.fov_angle = Flocking.FieldOfViewAngle
  params.max_anim_speed = Movement.MaxAnimSpeed
  params.follow_player = Options.bFollowPlayer
  params.avoid_water = Options.bAvoidWater
  params.no_landing = Options.bNoLanding
  params.avoid_obstacles = Options.bObstacleAvoidance
  params.max_view_distance = Options.VisibilityDist
  params.Sounds = self:GetSounds()
  params.Animations = self.Animations
  if self.flock == 0 then
    self.flock = 1
    Boids.CreateFlock(self, self:GetFlockType(), params)
  end
  if self.flock ~= 0 then
    Boids.SetFlockParams(self, params)
  end
end
function Chickens:OnPropertyChange()
  self:OnShutDown()
  if self.Properties.Options.bActivate == 1 then
    self:CreateFlock()
  end
end
function Chickens:Event_Activate()
  if self.Properties.Options.bActivate == 0 and self.flock == 0 then
    self:CreateFlock()
  end
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 1)
  end
end
function Chickens:Event_Deactivate()
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 0)
  end
end
function Chickens:OnProceedFadeArea(player, areaId, fadeCoeff)
  if self.flock ~= 0 then
    Boids.SetFlockPercentEnabled(self, fadeCoeff * 100)
  end
end
Chickens.FlowEvents = {
  Inputs = {
    Activate = {
      Chickens.Event_Activate,
      "bool"
    },
    Deactivate = {
      Chickens.Event_Deactivate,
      "bool"
    }
  },
  Outputs = {Activate = "bool", Deactivate = "bool"}
}
