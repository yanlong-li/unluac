Bugs = {
  type = "Bugs",
  MapVisMask = 0,
  ENTITY_DETAIL_ID = 1,
  Properties = {
    Movement = {
      HeightMin = 1,
      HeightMax = 5,
      SpeedMin = 5,
      SpeedMax = 15,
      FactorOrigin = 1,
      MaxAnimSpeed = 1,
      RandomMovement = 1
    },
    Boid = {
      nCount = 10,
      object_Model1 = "Objects\\Characters\\Animals\\Frog\\Frog.chr",
      object_Model2 = "",
      object_Model3 = "",
      object_Model4 = "",
      object_Model5 = "",
      Animation = "",
      Size = 1,
      SizeRandom = 0,
      nBehaviour = 0
    },
    Options = {
      bFollowPlayer = 0,
      bNoLanding = 0,
      VisibilityDist = 100,
      bActivate = 1,
      Radius = 20
    }
  },
  Properties1 = {
    object_Model1 = "Objects\\Characters\\Animals\\GlowBug\\GlowBug.cgf",
    object_Model2 = "",
    object_Model3 = "",
    object_Model4 = "",
    object_Model5 = "",
    object_Character = "",
    nNumBugs = 10,
    nBehaviour = 0,
    Scale = 1,
    HeightMin = 1,
    HeightMax = 5,
    SpeedMin = 5,
    SpeedMax = 15,
    FactorOrigin = 1,
    RandomMovement = 1,
    bFollowPlayer = 0,
    Radius = 10,
    bActivateOnStart = 1,
    bNoLanding = 0,
    AnimationSpeed = 1,
    Animation = "",
    VisibilityDist = 100
  },
  Animations = {
    "walk_loop",
    "idle01",
    "idle01",
    "idle01",
    "idle01",
    "idle01"
  },
  Editor = {Icon = "Bug.bmp"},
  params = {}
}
function Bugs:OnInit()
  self:NetPresent(0)
  self.flock = 0
  self:CreateFlock()
  if self.Properties.Options.bActivate ~= 1 and self.flock ~= 0 then
    Boids.EnableFlock(self, 0)
  end
end
function Bugs:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Bugs:OnShutDown()
end
function Bugs:CreateFlock()
  local Movement = self.Properties.Movement
  local Boid = self.Properties.Boid
  local Options = self.Properties.Options
  local params = self.params
  params.count = Boid.nCount
  params.behavior = Boid.nBehaviour
  params.model = Boid.object_Model
  params.model = Boid.object_Model1
  params.model1 = Boid.object_Model2
  params.model2 = Boid.object_Model3
  params.model3 = Boid.object_Model4
  params.model4 = Boid.object_Model5
  params.boid_size = Boid.Size
  params.boid_size_random = Boid.SizeRandom
  params.min_height = Movement.HeightMin
  params.max_height = Movement.HeightMax
  params.min_speed = Movement.SpeedMin
  params.max_speed = Movement.SpeedMax
  params.factor_origin = Movement.FactorOrigin
  params.factor_keep_height = Movement.FactorHeight
  params.factor_avoid_land = Movement.FactorAvoidLand
  params.spawn_radius = Options.Radius
  params.gravity_at_death = Boid.gravity_at_death
  params.boid_mass = Boid.Mass
  params.max_anim_speed = Movement.MaxAnimSpeed
  params.follow_player = Options.bFollowPlayer
  params.no_landing = Options.bNoLanding
  params.avoid_obstacles = Options.bObstacleAvoidance
  params.max_view_distance = Options.VisibilityDist
  if self.flock == 0 then
    self.flock = 1
    Boids.CreateFlock(self, Boids.FLOCK_BUGS, params)
  end
  if self.flock ~= 0 then
    Boids.SetFlockParams(self, params)
  end
end
function Bugs:OnPropertyChange()
  if self.Properties.Options.bActivate == 1 then
    self:CreateFlock()
  end
end
function Bugs:Event_Activate(sender)
  if self.Properties.Options.bActivate == 0 and self.flock == 0 then
    self:CreateFlock()
  end
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 1)
  end
end
function Bugs:Event_Deactivate(sender)
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 0)
  end
end
function Bugs:OnProceedFadeArea(player, areaId, fadeCoeff)
  if self.flock ~= 0 then
    Boids.SetFlockPercentEnabled(self, fadeCoeff * 100)
  end
end
Bugs.FlowEvents = {
  Inputs = {
    Activate = {
      Bugs.Event_Activate,
      "bool"
    },
    Deactivate = {
      Bugs.Event_Deactivate,
      "bool"
    }
  },
  Outputs = {Activate = "bool", Deactivate = "bool"}
}
