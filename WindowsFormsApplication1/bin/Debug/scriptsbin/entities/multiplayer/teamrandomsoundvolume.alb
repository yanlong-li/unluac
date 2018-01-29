Script.ReloadScript("scripts/entities/sound/randomsoundvolume.lua")
TeamRandomSoundVolume = new(RandomSoundVolume)
TeamRandomSoundVolume.Properties.soundName = nil
TeamRandomSoundVolume.MAX_TEAM_COUNT = 4
for i = 1, TeamRandomSoundVolume.MAX_TEAM_COUNT do
  TeamRandomSoundVolume.Properties["TeamSound" .. i] = {teamName = "", soundName = ""}
end
function TeamRandomSoundVolume:OnSpawn()
end
function TeamRandomSoundVolume:OnSetTeam(teamId)
  local teamName = g_gameRules.game:GetTeamName(teamId)
  for i = 1, TeamRandomSoundVolume.MAX_TEAM_COUNT do
    local soundProps = self.Properties["TeamSound" .. i]
    if soundProps and soundProps.teamName == teamName then
      self:Stop()
      if soundProps.soundName ~= "" then
        self:Play(soundProps.soundName)
      end
      break
    end
  end
  if self.teamId then
    self.teamId = teamId
  end
end
function TeamRandomSoundVolume:Play(soundName)
  if soundName then
    self.Properties.soundName = soundName
    SoundEventSpot.Play(self)
    self.Properties.soundName = nil
  end
end
