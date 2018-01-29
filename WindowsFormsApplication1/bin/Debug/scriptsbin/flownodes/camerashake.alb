CameraShake = {
  Category = "legacy",
  Inputs = {
    {"t_Activate", "bool"},
    {"Position", "vec3"},
    {"Radius", "float"},
    {"Strength", "float"},
    {"Duration", "float"},
    {"Frequency", "float"}
  },
  Outputs = {
    {"Done", "bool"}
  },
  Implementation = function(bActivate, vPosition, fRadius, fStrength, fDuration, fFrequency)
    g_gameRules:ClientViewShake(vPosition, fRadius, fStrength, fDuration, fFrequency)
    Script.SetTimer(fDuration * 1000, function()
      return 1
    end)
  end
}
