function IStr(arg)
  return string.format("%i", arg)
end
function FStr(arg)
  return string.format("%.1f", arg)
end
function CommaStr(arg)
  if type(arg) == "number" then
    return string.format("|,%i;", arg)
  elseif type(arg) == "string" then
    return string.format("|,%s;", arg)
  end
end
local IsDigit = function(byte)
  if byte < 48 or byte > 57 then
    return false
  end
  return true
end
function RemoveNonDigit(num)
  local limit = string.len(num)
  local byte, char
  local strNum = ""
  for i = 1, limit do
    byte = string.byte(num, i)
    if IsDigit(byte) then
      strNum = strNum .. string.char(byte)
    end
  end
  if limit == 0 then
    strNum = "0"
  end
  return strNum
end
