CI_NEWTABLE = 1
CI_ENDTABLE = 2
CI_STRING = 3
CI_NUMBER = 4
function WriteIndex(stm, idx)
  if type(idx) == "number" then
    stm:WriteBool(1)
    stm:WriteInt(idx)
  elseif type(idx) == "string" then
    stm:WriteBool(nil)
    stm:WriteString(idx)
  else
    System.Log("Unrecognized idx type")
  end
end
function ReadIndex(stm)
  local bNumber = stm:ReadBool()
  if bNumber then
    return stm:ReadInt()
  else
    return stm:ReadString()
  end
end
function WriteToStream(stm, t, name)
  if name == nil then
    name = "__root__"
  end
  if type(t) == "table" then
    stm:WriteByte(CI_NEWTABLE)
    WriteIndex(stm, name)
    for i, val in pairs(t) do
      WriteToStream(stm, val, i)
    end
    stm:WriteByte(CI_ENDTABLE)
  else
    local tt = type(t)
    if tt == "string" then
      stm:WriteByte(CI_STRING)
      WriteIndex(stm, name)
      stm:WriteString(t)
    elseif tt == "number" then
      stm:WriteByte(CI_NUMBER)
      WriteIndex(stm, name)
      stm:WriteFloat(t)
    end
  end
end
function ReadFromStream(stm, parent)
  local chunkid = stm:ReadByte()
  local idx, val
  if chunkid ~= CI_ENDTABLE then
    idx = ReadIndex(stm)
    if chunkid == CI_NEWTABLE then
      val = {}
      while true do
        if ReadFromStream(stm, val) ~= CI_ENDTABLE then
        end
      end
      if parent then
        parent[idx] = val
      end
    elseif chunkid == CI_STRING then
      parent[idx] = stm:ReadString()
    elseif chunkid == CI_NUMBER then
      parent[idx] = stm:ReadFloat()
    end
  end
  if parent == nil then
    return val
  else
    return chunkid
  end
end
StringStream = {
  buffer = "",
  Write = function(self, str)
    self.buffer = self.buffer .. str
  end,
  WriteLine = function(self, str)
    self.buffer = self.buffer .. str .. "\n"
  end,
  WriteValue = function(self, v)
    local t = type(v)
    if t == "number" then
      self.buffer = self.buffer .. v
    elseif t == "string" then
      self.buffer = self.buffer .. "\"" .. v .. "\""
    elseif t == "boolean" then
      if t then
        self.buffer = self.buffer .. "true"
      elseif t then
        self.buffer = self.buffer .. "false"
      end
    else
      print("Unrecognised type " .. t)
    end
  end,
  WriteIndex = function(self, v)
    local t = type(v)
    if t == "number" then
      self.buffer = self.buffer .. "[" .. v .. "]"
    elseif t == "string" then
      self.buffer = self.buffer .. "[\"" .. v .. "\"]"
    else
      print("Unrecognised type " .. t)
    end
  end,
  Reset = function(self)
    self.buffer = ""
  end
}
function DumpTableAsLua(myTable, tableName, stream)
  stream:WriteLine("--Automatically dumped LUA table")
  stream:WriteLine(tableName .. " = {")
  for i, v in pairs(myTable) do
    stream:Write("  ")
    stream:WriteIndex(i)
    stream:Write(" = ")
    stream:WriteValue(v)
    stream:WriteLine(",")
  end
  stream:WriteLine("}")
end
function DumpTableAsLuaString(myTable, tableName)
  StringStream:Reset()
  DumpTableAsLua(myTable, tableName, StringStream)
  return StringStream.buffer
end
