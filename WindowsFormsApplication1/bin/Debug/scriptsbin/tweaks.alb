Tweaks = {
  RELOAD = true,
  SAVECHANGES = false,
  {
    MENU = "ItemTweaks",
    {
      NAME = "Item offset (right direction)",
      CVAR = "i_offset_right",
      DELTA = "0.01"
    },
    {
      NAME = "Item offset (up direction)",
      CVAR = "i_offset_up",
      DELTA = "0.01"
    },
    {
      NAME = "Item offset (front direction)",
      CVAR = "i_offset_front",
      DELTA = "0.01"
    }
  },
  {
    MENU = "Debug",
    {
      NAME = "time_scale - slow the game down or speed it up",
      CVAR = "time_scale",
      DELTA = "0.125"
    },
    {
      NAME = "set p_draw_helpers from the console to show physics helpers",
      CVAR = "p_draw_helpers"
    },
    {
      MENU = "AI Debug",
      {
        NAME = "Enable AI debug drawing",
        CVAR = "ai_DebugDraw",
        DELTA = "1"
      },
      {
        NAME = "set ai_drawpath to an entity name to draw its path",
        CVAR = "ai_DrawPath"
      },
      {
        NAME = "set ai_stats_target to the name of the entity to track, or all",
        CVAR = "ai_StatsTarget"
      }
    }
  },
  {
    NAME = " * Reload Tweak Menu *",
    LUA = "MTJ.Test",
    INCREMENTER = function()
      Script.ReloadScript("Scripts/Tweaks.lua")
    end
  },
  {
    NAME = " * Save LUA Tweak changes *",
    LUA = "Tweaks.SAVECHANGES"
  }
}
Script.ReloadScript("Scripts/TweaksSave.lua")
Script.ReloadScript("Scripts/SaveUtils.lua")
