AI_IdleTable = {
  FixWheel_Smoke = {
    {
      AnchorType = AIAnchorTable.IDLE_FIX_WHEEL,
      Run = 0,
      AnimStart = "fixwheel_start",
      AnimLoop = {
        "fixwheel_loop",
        80,
        "fixwheel_idle_01",
        20
      },
      AnimEnd = "fixwheel_end",
      SeqLoopStart = 1,
      Loop = 8
    },
    {
      AnchorType = AIAnchorTable.IDLE_SMOKE,
      AnimStart = "smoking_start",
      AnimLoop = {
        "smoking_idle",
        60,
        "smoking_01",
        13,
        "smoking_02",
        13,
        "smoking_03",
        14
      },
      AnimEnd = {
        "smoking_end_01",
        50,
        "smoking_end_02",
        50
      },
      Loop = 8,
      SeqLoop = -1
    }
  },
  Idle_Smoke = {
    {
      AnchorType = AIAnchorTable.IDLE_SMOKE,
      AnimStart = "smoking_start",
      AnimLoop = {
        "smoking_idle",
        60,
        "smoking_01",
        13,
        "smoking_02",
        13,
        "smoking_03",
        14
      },
      AnimEnd = {
        "smoking_end_01",
        50,
        "smoking_end_02",
        50
      },
      Loop = 1,
      SeqLoop = -1
    }
  },
  IDLE_SMOKE = {
    {
      AnchorType = AIAnchorTable.IDLE_SMOKE,
      AnimStart = "smoking_start",
      AnimLoop = {
        "smoking_idle",
        60,
        "smoking_01",
        13,
        "smoking_02",
        13,
        "smoking_03",
        14
      },
      AnimEnd = {
        "smoking_end_01",
        50,
        "smoking_end_02",
        50
      },
      Loop = -1,
      SeqLoop = -1
    }
  },
  ACTION_INVESTIGATE_AREA = {
    WithWeapon = 1,
    {
      AnchorType = AIAnchorTable.ACTION_INVESTIGATE_AREA,
      SeqLoop = 1,
      Run = 1,
      EndLoopSignal = "SEARCH_AROUND"
    }
  },
  USE_RADIO_ALARM = {
    Ignorant = 1,
    {
      AnchorType = AIAnchorTable.USE_RADIO_ALARM,
      AnimLoop = "radio_reinforcements_01",
      Loop = 1,
      SeqLoop = 1,
      Run = 1,
      EndLoopSignal = "RETURN_TO_FIRST"
    }
  },
  USE_BROKEN_RADIO_ALARM = {
    Ignorant = 1,
    {
      AnchorType = AIAnchorTable.USE_BROKEN_RADIO_ALARM,
      AnimLoop = "smoking_03",
      Loop = 1,
      SeqLoop = 1,
      Run = 1,
      EndLoopSignal = "RETURN_TO_FIRST"
    }
  },
  IDLE_FIX_GENERATOR = {
    {
      AnchorType = AIAnchorTable.IDLE_FIX_GENERATOR,
      AnimStart = "repair_start",
      AnimLoop = "repair_loop",
      AnimEnd = "repair_end",
      Loop = 7,
      Run = 0
    },
    {
      AnchorType = AIAnchorTable.IDLE_MISSION_TALK_INPLACE,
      AnimLoop = "stand_idle",
      Loop = 1,
      SeqLoop = 1,
      Run = 0,
      EndLoopSignal = "BackToPrevious"
    }
  }
}
