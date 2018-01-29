function SetViewOfRebuyWindow(id, parent)
  local sideMargin, titleMargin, bottomMargin = GetWindowMargin()
  local widget = CreateWindow(id, parent)
  widget:Show(false)
  widget:SetExtent(POPUP_WINDOW_WIDTH, 180)
  widget:AddAnchor("TOPLEFT", parent, "TOPRIGHT", 0, -5)
  widget:SetTitle(locale.store.reBuyList)
  local rebuyList = SetViewOfStoreItemSlot(widget, 6, 2, sideMargin, titleMargin, 53)
  widget.rebuyList = rebuyList
  return widget
end
